using Services.Application.Interfaces;
using Services.Application.Services;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Application.Logic
{
    public class EstimatedtimeTrackerLogic : IEstimatedtimeTrackerLogic
    {
        readonly IEstimatedtimeTracker _IEstimatedtimeTracker;
        private static Random random = new Random();
        readonly string timeZone = "bras";
        const string dateFormat = "dd/MM/yyyy";
        const string dateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        const string timeFormat = "HH:mm:ss";

        public EstimatedtimeTrackerLogic(IEstimatedtimeTracker iEstimatedtimeTracker)
        {
            this._IEstimatedtimeTracker = iEstimatedtimeTracker;
        }


        public async Task<List<EstimatedtimeTracker>> ListAsync()
        {
            List<EstimatedtimeTracker> estimatedtimeTrackers = await _IEstimatedtimeTracker.ListAsync();

            return estimatedtimeTrackers;
        }

        public async Task<List<EstimatedtimeTracker>> ListAsync(string project, string owner)
        {
            await Task.Run(() =>
             {
                 if (string.IsNullOrEmpty(owner))
                     throw new ArgumentException("Owner cannot be null.");

             });

            IEnumerable<EstimatedtimeTracker> estimatedtimeTrackers = await ListAsync();

            if (!string.IsNullOrEmpty(project))
                estimatedtimeTrackers = await Task.Run(() => estimatedtimeTrackers
                .Where(x => x.Project.Contains(project, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.Date)
                );

            estimatedtimeTrackers = await Task.Run(() => 
            estimatedtimeTrackers
            .Where(x => x.Owner.Equals(owner))
            .OrderByDescending(x => x.Date)
            );


            return estimatedtimeTrackers.ToList();
        }

        /// <summary>
        /// get EstimatedtimeTracker 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EstimatedtimeTracker> GetEntityAsync(Guid id)
        {
            EstimatedtimeTracker EstimatedtimeTracker = await _IEstimatedtimeTracker.GetEntityAsync(id);

            return EstimatedtimeTracker;
        }

        public async Task<int> AddAsync(EstimatedtimeTracker EstimatedtimeTracker)
        {
            return await this._IEstimatedtimeTracker.AddAsync(EstimatedtimeTracker);
        }

        public async Task<Result<EstimatedtimeTracker>> AddEstimatedtimeTrackerAsync(EstimatedtimeTracker estimatedtimeTracker)
        {
            var result = new Result<EstimatedtimeTracker>();

            try
            {

                var EstimatedtimeTracker = new EstimatedtimeTracker
                {
                    Id = Guid.NewGuid(),
                    Project = estimatedtimeTracker.Project ,
                    Activity = estimatedtimeTracker.Activity,
                    TimeStarted = estimatedtimeTracker.TimeStarted,
                    TimeEnded = estimatedtimeTracker.TimeEnded,
                    TimeSpent = estimatedtimeTracker.TimeSpent,
                    Owner = estimatedtimeTracker.Owner,
                    Date = TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToString(dateTimeFormat),

                };

                var save = await this.AddAsync(EstimatedtimeTracker);
                return (save > 0 ? result.ResultResponse(
                   this.ListAsync().GetAwaiter().GetResult().First(x => x.Id == EstimatedtimeTracker.Id))
                    : throw new Exception());

            }
            catch (Exception exc)
            {
                return result.ResultError(exc.Message);
            }


        }

        public async Task<int> DeleteAsync(EstimatedtimeTracker EstimatedtimeTracker)
        {
            return await this._IEstimatedtimeTracker.DeleteAsync(EstimatedtimeTracker);
        }

        public async Task<int?> DeleteAsync(Guid id)
        {
            var EstimatedtimeTracker = await this.GetEntityAsync(id);
            if (EstimatedtimeTracker == null) return null;
            return await this.DeleteAsync(EstimatedtimeTracker);
        }

        public async Task<int> UpdateAsync(EstimatedtimeTracker EstimatedtimeTracker)
        {
            return await this._IEstimatedtimeTracker.UpdateAsync(EstimatedtimeTracker);
        }
        /// <summary>
        /// update EstimatedtimeTracker
        /// </summary>
        /// <param name="id"></param>
        /// <param name="EstimatedtimeTracker"></param>
        /// <returns></returns>
        public async Task<Result<EstimatedtimeTracker>> UpdateAsync(Guid id, EstimatedtimeTracker EstimatedtimeTracker)
        {
            var result = new Result<EstimatedtimeTracker>();

            var resource = await this.GetEntityAsync(id);
            if (resource == null) return result.ResultError("Resource not found!");

            //use reflection to check wich properties have changed and set proper value
            foreach (var entityProp in EstimatedtimeTracker.GetType().GetProperties())
            {
                var entityValue = entityProp.GetValue(EstimatedtimeTracker);

                if (entityProp.Name != "Id" && (!string.IsNullOrEmpty(entityValue as string) || entityValue != null))
                {
                    foreach (var resourceProp in resource.GetType().GetProperties().Where(x => x.Name == entityProp.Name))
                    {
                        var resourceValue = resourceProp.GetValue(resource);

                        if (string.IsNullOrEmpty(resourceValue as string) && entityValue != null || !resourceValue.ToString().ToLower().Equals(entityValue.ToString().ToLower()))
                        {
                            resourceProp.SetValue(resource, entityValue, null);
                        }
                    }
                }
            }


            if (await this.UpdateAsync(resource) <= 0) return result.ResultError("The resource was not updated!");
            else return result.ResultResponse(resource);
        }
        /// <summary>
        /// copy EstimatedtimeTracker selected and change it into a new one with new ID, and clear some fields
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task<Result<EstimatedtimeTracker>> CopyEstimatedtimeTrackerAsync(Guid id)
        {
            var result = new Result<EstimatedtimeTracker>();

            var resource = await this.GetEntityAsync(id);
            if (resource == null) return result.ResultError("Resource not found!");

            resource.Id = Guid.NewGuid();
            resource.Project += $" {TextServices.RandomString(3)}";
            resource.TimeStarted = string.Empty;
            resource.TimeEnded = string.Empty;
            resource.TimeSpent = string.Empty;
            resource.Date = TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToString(dateTimeFormat);

            var save = await this.AddAsync(resource);
            return (save > 0 ? result.ResultResponse(
                this.ListAsync().GetAwaiter().GetResult()
                .First(x => x.Id == resource.Id))
                : result.ResultError("Could not save this bill!"));
        }

        public async Task<Result<string>> CalculateTaskTimeAsync(string start, string finish)
        {
            var result = new Result<string>();
            string res = string.Empty;
            string err = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(finish) || string.IsNullOrWhiteSpace(finish))
                {
                    finish = TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToString(dateTimeFormat);
                }
                await Task.Run(() =>
                {
                    res = TimeServices.GetDiffTime(start, finish);

                });
            }
            catch (Exception exc)
            {
                err = exc.Message;
            }

            return (!string.IsNullOrEmpty(res) ? result.ResultResponse(
             res
               )
               : result.ResultError(err));
        }
     

        private static object GetPropertyValue(object src, string propName)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }

 

    }

}

