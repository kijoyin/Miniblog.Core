namespace Miniblog.Brains.Models.Dto
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System;
    using System.Collections.Generic;

    public class PostDto
    {
        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public string Excerpt { get; set; } = string.Empty;

        [Required]
        public string ID { get; set; } = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);

        public bool IsPublished { get; set; } = true;

        public DateTime LastModified { get; set; } = DateTime.UtcNow;

        public DateTime PubDate { get; set; } = DateTime.UtcNow;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Categories { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;

        public IList<CommentDto> Comments { get; } = new List<CommentDto>();

    }
}
