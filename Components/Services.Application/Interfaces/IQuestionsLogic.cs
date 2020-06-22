using Services.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Services.Application.Interfaces
{
    public interface IQuestionsLogic : IGenericLogic<Questions>, IQuestionnaireLogic
    {
        int? Delete(Guid id);
        Result<Questions> AddEntity(Questions entity);
        List<Questions> List(string question);
        Result<Questions> Update(Guid identifier, Questions entity);
    }
}
