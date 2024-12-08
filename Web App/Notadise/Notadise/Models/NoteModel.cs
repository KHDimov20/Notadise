using System;
using System.Collections.Generic;

namespace Notadise.Models
{
    public class NoteModel
    {
        public string Title { get; set; } = string.Empty; // Иницииране на празен низ 
        public DateTime LastEdited { get; set; } = DateTime.Now; // По подразбиране е текущата дата и час 
        public DateTime CreationDate { get; set; } = DateTime.Now; // По подразбиране е текущата дата и час 
        public List<NoteSection> Sections { get; set; } = new(); // Иницииране на празен списък 
    }

    public class NoteSection
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; // Иницииране на празен низ 
        public string Content { get; set; } = string.Empty; // Иницииране на празен низ 
    }
}
