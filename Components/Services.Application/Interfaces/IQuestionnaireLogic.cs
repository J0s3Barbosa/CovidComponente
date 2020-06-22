using Services.Domain.DTO;
using Services.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Services.Application.Interfaces
{
    public interface IQuestionnaireLogic
    {
        List<QuestionsDTO> Questionnaire();
        bool CheckAnswer(Guid id, string markedOption);
        Result<string> CheckAnswer(string question, string markedOption);

    }
}
