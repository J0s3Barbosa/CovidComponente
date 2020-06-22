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
    public class ArticleLogic : IArticleLogic
    {
        readonly IArticle _IArticle;

        public ArticleLogic(IArticle iArticle)
        {
            this._IArticle = iArticle;
        }


        public  async Task<List<Article>> ListAsync()
        {
            List<Article> articles = await _IArticle.ListAsync();

            return articles;
        }
      
        public async Task<List<Article>> ListAsync(string description, string userEmail)
        {
            await Task.Run(() =>
             {
                 if (string.IsNullOrEmpty(userEmail))
                     throw new ArgumentException("UserEmail cannot be null.");

             });

            IEnumerable<Article> articles = await ListAsync();

            if (!string.IsNullOrEmpty(description))
                articles = await Task.Run(() => articles.Where(x => x.Description.Contains(description, StringComparison.OrdinalIgnoreCase)));
            if (!string.IsNullOrEmpty(userEmail))
                articles = await Task.Run(() => articles.Where(x => x.UserEmail.Equals(userEmail)));
            return articles.ToList();
        }

        /// <summary>
        /// get Article 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Article> GetEntityAsync(Guid id)
        {
            Article article = await _IArticle.GetEntityAsync(id);

            return article;
        }

        public async Task<int> AddAsync(Article article)
        {
            return await this._IArticle.AddAsync(article);
        }

        public async Task<Result<Article>> AddArticleAsync(Article addArticle)
        {
            var result = new Result<Article>();
            var newDescription = string.Empty;
            var list = await this.ListAsync();
            if (list.Any(x => x.Description.Equals(addArticle.Description, StringComparison.OrdinalIgnoreCase)
            ))
            {
                newDescription = TextServices.RandomString(3);
            }


            try
            {
               
                var article = new Article
                {
                    Id = Guid.NewGuid(),
                    Description = addArticle.Description +" " +  newDescription,
                    Text = addArticle.Text,
                    Access = addArticle.Access,
                    UserEmail = addArticle.UserEmail,

                };

                var save = await this.AddAsync(article);
                return (save > 0 ? result.ResultResponse(
                   this.ListAsync().GetAwaiter().GetResult().First(x => x.Id == article.Id))
                    : throw new Exception());

            }
            catch (Exception exc)
            {
                return result.ResultError(exc.Message);
            }


        }

        public async Task<int> DeleteAsync(Article article)
        {
            return await this._IArticle.DeleteAsync(article);
        }

        public async Task<int?> DeleteAsync(Guid id)
        {
            var article = await this.GetEntityAsync(id);
            if (article == null) return null;
            return await this.DeleteAsync(article);
        }

        public async Task<int> UpdateAsync(Article article)
        {
            return await this._IArticle.UpdateAsync(article);
        }
        /// <summary>
        /// update article
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        public async Task<Result<Article>> UpdateAsync(Guid identifier, Article article)
        {
            var result = new Result<Article>();

            var resource = await this.GetEntityAsync(identifier);
            if (resource == null) return result.ResultError("Resource not found!");

            //use reflection to check wich properties have changed and set proper value
            foreach (var entityProp in article.GetType().GetProperties())
            {
                var entityValue = entityProp.GetValue(article);

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
        /// copy article selected and change it into a new one with new ID, due date to next month
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task<Result<Article>> CopyArticleAsync(Guid identifier)
        {
            var result = new Result<Article>();

            var resource = await this.GetEntityAsync(identifier);
            if (resource == null) return result.ResultError("Resource not found!");


            resource.Id = Guid.NewGuid();
            resource.Description += " "+ TextServices.RandomString(3);

            var save = await this.AddAsync(resource);
            return (save > 0 ? result.ResultResponse(
                this.ListAsync().GetAwaiter().GetResult()
                .First(x => x.Id == resource.Id))
                : result.ResultError("Could not save this bill!"));
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

