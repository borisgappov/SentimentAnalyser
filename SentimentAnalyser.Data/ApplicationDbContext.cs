﻿using System.Linq;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SentimentAnalyser.Models.Entities;

namespace SentimentAnalyser.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Word> Words { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Word>(entity => { entity.ToTable("Lexicon"); });
            
            Seed(builder);

            base.OnModelCreating(builder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>().HasData(
                SeedData.Words.Select(x => new Word
                {
                    Text = x.Key,
                    Sentiment = x.Value
                }).ToArray()
            );
        }
    }
}