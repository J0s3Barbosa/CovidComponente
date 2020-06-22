using Services.Domain.Entities;
using Services.Domain.Interfaces;

namespace Services.Infra.Repository
{
    public class RepositoryArticle : RepositoryGenericAsync<Article>, IArticle
    {

    }
}
