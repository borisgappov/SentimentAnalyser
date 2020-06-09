﻿using System.Linq;
using Faker;
using SentimentAnalyser.Data.Repositories;
using SentimentAnalyser.Models.Entities;
using SentimentAnalyser.Models.UnitTests.Fakes;
using Xunit;

namespace SentimentAnalyzer.Data.UnitTests
{
    public class WordRepositoryUnitTests : BaseRepositoryUnitTests
    {
        [Fact]
        public async void GivenMatchIdAndUserId_RepositoryChecksBetExists()
        {
            // Arrange
            var sentece = Lorem.Sentence();
            var words = Helper.FakeEnumerable(() => new Word().Fake(), 10).ToList();
            words.Add(new Word().Fake(Text: sentece));

            TestDbContext.Database.EnsureDeleted();
            TestDbContext.AddRange(words);
            TestDbContext.SaveChanges();

            // Act
            var result = await new WordRepository(TestDbContext).Exists(sentece).ConfigureAwait(false);

            // Assert
            Assert.True(result);

            // if not exists
            // Act
            result = await new WordRepository(TestDbContext).Exists(string.Empty).ConfigureAwait(false);

            // Assert
            Assert.False(result);
        }
    }
}