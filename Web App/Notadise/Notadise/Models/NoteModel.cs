using System;
using System.Collections.Generic;

namespace YourNamespace.Models
{
    public class NoteModel
    {
        public string Title { get; set; } = string.Empty; // Initialize to an empty string
        public DateTime LastEdited { get; set; } = DateTime.Now; // Default to the current date and time
        public double FileSizeKB { get; set; } = 0; // Default file size
        public DateTime CreationDate { get; set; } = DateTime.Now; // Default to the current date and time
        public List<NoteSection> Sections { get; set; } = new(); // Initialize to an empty list
    }

    public class NoteSection
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; // Initialize to an empty string
        public string Content { get; set; } = string.Empty; // Initialize to an empty string
    }
}
