using Castle.Windsor;
using FluentAssertions;
using Services.Application.Interfaces;
using Services.Domain.Entities;
using System.Collections.Generic;
using Xunit;

namespace Services.Application.Logic.Tests
{

    public class QuestionsLogicTests
    {
        private readonly IWindsorContainer _container;
        private readonly IQuestionsLogic _QuestionsLogic;

        public QuestionsLogicTests()
        {
            _container = new WindsorContainer();
            _container.Install(new BaseInstaller<QuestionsLogic>());
            _QuestionsLogic = _container.Resolve<QuestionsLogic>();
        }

        [Fact(DisplayName = "#00 - Cenário: ")]
        [Trait("Category", "Fail")]
        public void IQuestionsLogic_Test()
        {

            var questionsList = _QuestionsLogic.List();

            // Assert
            var result = questionsList.Should().BeOfType<List<Questions>>().Subject;
            result.Should().NotBeNull();
            result.Should().HaveCountGreaterThan(0);
        }
    }
}