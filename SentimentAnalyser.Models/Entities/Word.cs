﻿using System.ComponentModel.DataAnnotations;

namespace SentimentAnalyser.Models.Entities
{
    public class Word
    {
        [Key]
        public string Text { get; set; }

        public float Sentiment { get; set; }
    }
}