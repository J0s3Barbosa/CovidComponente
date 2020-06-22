using Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Application.Interfaces
{
    public interface IVideoPlayerLogic : IGenericLogicAsync<VideoPlayer>
    {
        Task<Result<VideoPlayer>> AddVideoPlayerAsync(VideoPlayer todo);
        Task<int?> DeleteAsync(Guid id);
        Task<Result<List<VideoPlayer>>> ListAsync(string description, string userEmail);
        Task<Result<VideoPlayer>> UpdateAsync(Guid identifier, VideoPlayer todo);
        Task<VideoPlayer> GetEntityAsync(Guid id);

    }
}
