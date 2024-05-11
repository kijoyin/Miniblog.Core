namespace Miniblog.Core.Models.Dto
{
    using System.ComponentModel.DataAnnotations;
    using System;

    public class CommentDto
    {
        [Required]
        public string Author { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        public bool IsAdmin { get; set; } = false;

        [Required]
        public DateTime PubDate { get; set; } = DateTime.UtcNow;
    }
}
