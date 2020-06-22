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
    public class TodoLogic : ITodoLogic
    {
        readonly ITodo _ITodo;
        readonly string timeZone = "bras";
        const string dateFormat = "dd/MM/yyyy";
        const string dateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        const string timeFormat = "HH:mm:ss";
        public TodoLogic(ITodo iTodo)
        {
            this._ITodo = iTodo;
        }


        public async Task<List<Todo>> ListAsync()
        {
            List<Todo> todos = await _ITodo.ListAsync();

            return todos;
        }

        public async Task<List<Todo>> ListAsync(string description, string userEmail)
        {
            await Task.Run(() =>
             {
                 if (string.IsNullOrEmpty(userEmail))
                     throw new ArgumentException("UserEmail cannot be null.");

             });

            IEnumerable<Todo> todos = await ListAsync();

            if (!string.IsNullOrEmpty(description))
                todos = await Task.Run(() => todos
                .Where(x => x.Description.Contains(description, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.TimeStarted)
                );
            if (!string.IsNullOrEmpty(userEmail))

                todos = await Task.Run(() => todos
                .Where(x => x.UserEmail.Equals(userEmail))
                .OrderByDescending(x => x.TimeStarted)
                );
            return todos.ToList();
        }

        /// <summary>
        /// get Todo 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Todo> GetEntityAsync(Guid id)
        {
            Todo todo = await _ITodo.GetEntityAsync(id);

            return todo;
        }

        public async Task<int> AddAsync(Todo todo)
        {
            return await this._ITodo.AddAsync(todo);
        }

        public async Task<Result<Todo>> AddTodoAsync(Todo addTodo)
        {
            var result = new Result<Todo>();
            var list = await this.ListAsync();
            string newDescription = string.Empty;
            if (list.Any(x => x.Description.Equals(addTodo.Description, StringComparison.OrdinalIgnoreCase)
            ))
            {
                newDescription = TextServices.RandomString(3);
            }


            try
            {
                var start = addTodo.TimeStarted == "" ? TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToString(dateTimeFormat) : addTodo.TimeStarted;

                var todo = new Todo
                {
                    Id = Guid.NewGuid(),
                    Description = addTodo.Description + " " + newDescription,
                    Details = addTodo.Details,
                    TimeStarted = start,
                    TimeFinished = addTodo.TimeFinished,
                    TaskComplete = addTodo.TaskComplete,
                    UserEmail = addTodo.UserEmail,

                };

                var save = await this.AddAsync(todo);
                return (save > 0 ? result.ResultResponse(
                   this.ListAsync().GetAwaiter().GetResult().First(x => x.Id == todo.Id))
                    : throw new Exception());

            }
            catch (Exception exc)
            {
                return result.ResultError(exc.Message);
            }


        }

        public async Task<int> DeleteAsync(Todo todo)
        {
            return await this._ITodo.DeleteAsync(todo);
        }

        public async Task<int?> DeleteAsync(Guid id)
        {
            var todo = await this.GetEntityAsync(id);
            if (todo == null) return null;
            return await this.DeleteAsync(todo);
        }

        public async Task<int> UpdateAsync(Todo todo)
        {
            return await this._ITodo.UpdateAsync(todo);
        }
        /// <summary>
        /// update todo
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="todo"></param>
        /// <returns></returns>
        public async Task<Result<Todo>> UpdateAsync(Guid identifier, Todo todo)
        {
            var result = new Result<Todo>();

            var resource = await this.GetEntityAsync(identifier);
            if (resource == null) return result.ResultError("Resource not found!");

            //use reflection to check wich properties have changed and set proper value
            foreach (var entityProp in todo.GetType().GetProperties())
            {
                var entityValue = entityProp.GetValue(todo);

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
        /// copy todo selected and change it into a new one with new ID, due date to next month
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task<Result<Todo>> CopyTodoAsync(Guid identifier)
        {
            var result = new Result<Todo>();

            var resource = await this.GetEntityAsync(identifier);
            if (resource == null) return result.ResultError("Resource not found!");


            resource.Id = Guid.NewGuid();
            resource.Description = resource.Description + $" {TextServices.RandomString(4)}";
            resource.TimeStarted = TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToString(dateTimeFormat);
            resource.TimeFinished = string.Empty;

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
                    res = TimeServices.GetDiffDays(start, finish);

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

