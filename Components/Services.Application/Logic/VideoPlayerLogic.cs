using Services.Application.Interfaces;
using Services.Domain.DTO;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Application.Logic
{
    public class VideoPlayerLogic : IVideoPlayerLogic
    {
        readonly IVideoPlayer _IVideoPlayer;
        string dateFormat = "dd/MM/yyyy";

        public VideoPlayerLogic(IVideoPlayer iVideoPlayer)
        {
            this._IVideoPlayer = iVideoPlayer;
        }


        public async Task<List<VideoPlayer>> ListAsync()
        {
            List<VideoPlayer> VideoPlayers = await _IVideoPlayer.ListAsync();

            return VideoPlayers;
        }

        public async Task<Result<List<VideoPlayer>>> ListAsync(string description, string userEmail)
        {
            var result = new Result<List<VideoPlayer>>();

            IEnumerable<VideoPlayer> VideoPlayers = await ListAsync();

            if (string.IsNullOrEmpty(userEmail))
                return result.ResultError("UserEmail cannot be null!");

            if (!string.IsNullOrEmpty(description))
                VideoPlayers = await Task.Run(() => VideoPlayers.Where(x => x.Description.Contains(description, StringComparison.OrdinalIgnoreCase)));
            if (!string.IsNullOrEmpty(userEmail))
                VideoPlayers = await Task.Run(() => VideoPlayers.Where(x => x.UserEmail.Equals(userEmail)));
            return result.ResultResponse(VideoPlayers.ToList());
          
        }

        /// <summary>
        /// get VideoPlayer 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VideoPlayer> GetEntityAsync(Guid id)
        {
            VideoPlayer VideoPlayer = await _IVideoPlayer.GetEntityAsync(id);

            return VideoPlayer;
        }

        public async Task<int> AddAsync(VideoPlayer VideoPlayer)
        {
            return await this._IVideoPlayer.AddAsync(VideoPlayer);
        }

        public async Task<Result<VideoPlayer>> AddVideoPlayerAsync(VideoPlayer addVideoPlayer)
        {
            var result = new Result<VideoPlayer>();
            var list = await this.ListAsync();
            if (list.Any(x => x.Description.Equals(addVideoPlayer.Description, StringComparison.OrdinalIgnoreCase)
            ))
            {
                return result.ResultError("This VideoPlayer has already been registered!");
            }


            try
            {
                addVideoPlayer.Id = Guid.NewGuid();

                var save = await this.AddAsync(addVideoPlayer);
                return (save > 0 ? result.ResultResponse(
                   this.ListAsync().GetAwaiter().GetResult().First(x => x.Id == addVideoPlayer.Id))
                    : throw new Exception());

            }
            catch (Exception exc)
            {
                return result.ResultError(exc.Message);
            }


        }

        public async Task<int> DeleteAsync(VideoPlayer VideoPlayer)
        {
            return await this._IVideoPlayer.DeleteAsync(VideoPlayer);
        }

        public async Task<int?> DeleteAsync(Guid id)
        {
            var VideoPlayer = await this.GetEntityAsync(id);
            if (VideoPlayer == null) return null;
            return await this.DeleteAsync(VideoPlayer);
        }

        public async Task<int> UpdateAsync(VideoPlayer VideoPlayer)
        {
            return await this._IVideoPlayer.UpdateAsync(VideoPlayer);
        }
        /// <summary>
        /// update VideoPlayer
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="VideoPlayer"></param>
        /// <returns></returns>
        public async Task<Result<VideoPlayer>> UpdateAsync(Guid identifier, VideoPlayer VideoPlayer)
        {
            var result = new Result<VideoPlayer>();

            var resource = await this.GetEntityAsync(identifier);
            if (resource == null) return result.ResultError("Resource not found!");

            var dateFormat = "{0:dd/MM/yyyy}";

            //use reflection to check wich properties have changed and set proper value
            foreach (var entityProp in VideoPlayer.GetType().GetProperties())
            {
                var entityValue = entityProp.GetValue(VideoPlayer);

                if (entityProp.Name != "Id" && !string.IsNullOrEmpty(entityValue as string))
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

