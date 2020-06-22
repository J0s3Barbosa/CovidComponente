using Services.Application.Interfaces;
using Services.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Services.Domain.Interfaces;
using Services.Infra.Repository;
using System.Linq;
using Services.Application.Services;
using Services.Application.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;

namespace Services.Application.Logic.Tests
{
    [TestClass()]
    public class ArticleLogicTests
    {

        private readonly IArticleLogic _ArticleLogic;
        public ArticleLogicTests()
        {
            IOptions<ApiSettings> someOptions = Options.Create(new ApiSettings());
            var _container = new WindsorContainer();
            _container.Register(Component.For<IArticleLogic>().ImplementedBy<ArticleLogic>());
            _container.Register(Component.For<IArticle>().ImplementedBy<RepositoryArticle>());
            _container.Register(Component.For<ILoginServices>().ImplementedBy<LoginServices>());
            _container.Register(Component.For<IOptions<ApiSettings>>().Instance(someOptions));
            _ArticleLogic = _container.Resolve<IArticleLogic>();
        }
        [TestMethod()]
        public void ListArticlesTest()
        {
            var article = new Article()
            {
                UserEmail = "Email@Email.com",
                Description = "Password",
                Text = "Password",
                Access = ArticleAccess.Public,
            };

            Assert.IsNotNull(article);

        }

        [TestMethod()]
        public void GeraRandomNumberTest()
        {

            var number = RandomNumber();

            Assert.IsNotNull(number);

        }

        [TestMethod()]
        public void GeraRandomNameTest()
        {
            int numberLetters = 3;
            int numberWords = 1;
            var name = RandomName(numberLetters, numberWords).FirstOrDefault();

            Assert.AreEqual(3, name.Count());

        }

        [TestMethod()]
        public void Get4DigitsOkTest()
        {
            var digits = Get4Digits();

            Assert.AreEqual(4, digits.Count());

        }
        [TestMethod()]
        public void Get4DigitsNotOkTest()
        {
            var digits = Get4Digits();

            Assert.AreNotEqual(6, digits.Count());

        }

        [TestMethod()]
        public void Get8DigitsOkTest()
        {
            var digits = Get8Digits();

            Assert.AreEqual(8, digits.Count());

        }

        [TestMethod()]
        public void RandomStringTest()
        {
            int length = 3;
            var name = RandomString(length);

            Assert.AreEqual(3, name.Count());

        }

        private static string RandomNumber()
        {
            int _min = 0000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max).ToString();
        }
        public string Get4Digits()
        {

            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 1000;
            return String.Format("{0:D4}", random);
        }
        public string Get8Digits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return String.Format("{0:D8}", random);
        }
        private static IEnumerable<string> RandomName(int numberLetters, int numberWords)
        {
            // Get the number of words and letters per word.
            int num_letters = numberLetters;
            int num_words = numberWords;

            // Make an array of the letters we will use.
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            // Make a random number generator.
            Random rand = new Random();

            // Make the words.
            for (int i = 1; i <= num_words; i++)
            {
                // Make a word.
                string word = "";
                for (int j = 1; j <= num_letters; j++)
                {
                    // Pick a random number between 0 and 25
                    // to select a letter from the letters array.
                    int letter_num = rand.Next(0, letters.Length - 1);

                    // Append the letter.
                    word += letters[letter_num];
                }

                // Add the word to the list.
                yield return word;
            }

        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}