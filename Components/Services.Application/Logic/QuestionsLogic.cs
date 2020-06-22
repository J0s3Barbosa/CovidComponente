using Services.Application.Interfaces;
using Services.Domain.DTO;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Application.Logic
{
    public class QuestionsLogic : IQuestionsLogic
    {
        readonly IQuestions _IQuestions;

        public QuestionsLogic(IQuestions iQuestions)
        {
            this._IQuestions = iQuestions;
        }

        public List<Questions> List()
        {
            List<Questions> questionsList = _IQuestions.List();

            return questionsList;
        }
        public List<Questions> List(string question)
        {
            IEnumerable<Questions> questionList = List();

            if (!string.IsNullOrEmpty(question))
                questionList = questionList.Where(x => x.Question.Contains(question, StringComparison.OrdinalIgnoreCase));


            return questionList.ToList();
        }

        public List<QuestionsDTO> Questionnaire()
        {

            List<QuestionsDTO> QuestionnaireList = new List<QuestionsDTO>();
            List<Questions> questionsList = _IQuestions.List();
            foreach (var question in questionsList)
            {
                var questionnaireObj = new QuestionsDTO
                {
                    Question = question.Question,
                    Option1 = question.Option1,
                    Option2 = question.Option2,
                    Option3 = question.Option3,
                    Option4 = question.Option4,
                };

                QuestionnaireList.Add(questionnaireObj);
            }

            return QuestionnaireList;
        }

        //b413cfc0-f53a-4765-9430-3912efcd79cb
        //option2 Option2
        public bool CheckAnswer(Guid id, string markedOption)
        {
            var resource = GetEntity(id);
            if (resource == null) return false;

            if (resource.CorrectOption.ToLower() == markedOption.ToLower())
            {
                return true;
            }
            return false;
        }
        public Result<string> CheckAnswer(string question, string markedOption)
        {
            var result = new Result<string>();
            var resource = GetEntity(question);
            if (resource == null) return result.ResultError("Nao foi encontrada a Questao!");

            if (resource.CorrectOption.ToLower() == markedOption.ToLower())
            {
                return result.ResultResponse("Resposta Correta");
            }
            return result.ResultResponse("Resposta Errada");
        }

        /// <summary>
        /// get Questions 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Questions GetEntity(Guid id)
        {
            Questions question = _IQuestions.GetEntity(id);

            return question;
        }
        public Questions GetEntity(string question)
        {
            Questions questionObj = new Questions();
            if (!string.IsNullOrEmpty(question))
                questionObj = List().FirstOrDefault(x => x.Question.Equals(question, StringComparison.OrdinalIgnoreCase));

            return questionObj;
        }
        public int Add(Questions Entity)
        {
            return this._IQuestions.Add(Entity);
        }

        public Result<Questions> AddEntity(Questions Entity)
        {
            var result = new Result<Questions>();

            if (this.List().Any(x => x.Question.Equals(Entity.Question, StringComparison.OrdinalIgnoreCase)
            ))
            {
                return result.ResultError("This Question has already been registered!");
            }

            Entity.Id = Guid.NewGuid();

            var save = this.Add(Entity);
            return (save > 0 ? result.ResultResponse(
                this.List()
                .First(x => x.Id == Entity.Id))
                : result.ResultError("Could not save this Question!"));
        }

        public int Delete(Questions Entity)
        {
            return this._IQuestions.Delete(Entity);
        }

        public int? Delete(Guid id)
        {
            var entity = this.GetEntity(id);
            if (entity == null) return null;
            return this.Delete(entity);
        }

        public int Update(Questions Entity)
        {
            return this._IQuestions.Update(Entity);
        }

        public Result<Questions> Update(Guid identifier, Questions entity)
        {
            var result = new Result<Questions>();
             
            var resource = GetEntity(identifier);
            if (resource == null) return result.ResultError("Resource not found!");

            foreach (var entityProp in entity.GetType().GetProperties())
            {
                var entityValue = entityProp.GetValue(entity);

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


            if (this.Update(resource) <= 0) return result.ResultError("The resource was not updated!");
            else return result.ResultResponse(resource);
        }

        public static object GetPropertyValue(object src, string propName)
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

