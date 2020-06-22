using Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Application.Interfaces
{
    public interface IArticleLogic : IGenericLogicAsync<Article>
    {
        Task<Result<Article>> AddArticleAsync(Article todo);
        Task<int?> DeleteAsync(Guid id);
        Task<List<Article>> ListAsync(string description, string userEmail);
        Task<Result<Article>> CopyArticleAsync(Guid identifier);
        Task<Result<Article>> UpdateAsync(Guid identifier, Article todo);
        Task<Article> GetEntityAsync(Guid id);
    }
}
