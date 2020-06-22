using Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Application.Interfaces
{
    public interface IEstimatedtimeTrackerLogic : IGenericLogicAsync<EstimatedtimeTracker>
    {
        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="estimatedtimeTracker"></param>
        /// <returns></returns>
        Task<Result<EstimatedtimeTracker>> AddEstimatedtimeTrackerAsync(EstimatedtimeTracker estimatedtimeTracker);

        /// <summary>
        /// Calculate difference in date and time
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        Task<Result<string>> CalculateTaskTimeAsync(string start, string finish);
        Task<int?> DeleteAsync(Guid id);

        /// <summary>
        /// List async, could be filtered by project and owner
        /// </summary>
        /// <param name="project"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        Task<List<EstimatedtimeTracker>> ListAsync(string project, string owner);
        /// <summary>
        /// Duplicate the enity and clean some fields for the new entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<EstimatedtimeTracker>> CopyEstimatedtimeTrackerAsync(Guid id);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="estimatedtimeTracker"></param>
        /// <returns></returns>
        Task<Result<EstimatedtimeTracker>> UpdateAsync(Guid id, EstimatedtimeTracker estimatedtimeTracker);
        new Task<EstimatedtimeTracker> GetEntityAsync(Guid id);

    }
}
