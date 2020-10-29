using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ToDoTutorial.Models
{
    public class Todo
    {
        [Required]
        [JsonPropertyName("userId")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int UserId { get; set; }
        [JsonPropertyName("id")]
        public int TodoId { get; set; }
        [JsonPropertyName("title")]
        [Required, MaxLength(128)]
        public string Title { get; set; }
        [Required]
        [JsonPropertyName("completed")]
        public bool IsCompleted { get; set; }
    }
}
