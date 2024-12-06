using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notadise.Controllers
{
    public class NotesController : Controller
    {
        // In-memory storage for notes
        private static List<Note> Notes = new List<Note>();

        // Render the New Note page
        public IActionResult NewNote()
        {
            return View();
        }

        // Render the My Notes page
        public IActionResult MyNotes()
        {
            return View();
        }

        // Render the School Notes page
        [HttpGet]
        public IActionResult School()
        {
            var schoolNotes = Notes.Where(note => string.Equals(note.Category, "School", StringComparison.OrdinalIgnoreCase)).ToList();
            return View(schoolNotes);
        }

        [HttpGet]
        public IActionResult GetSchoolNotes()
        {
            var schoolNotes = Notes.Where(note => string.Equals(note.Category, "School", StringComparison.OrdinalIgnoreCase)).ToList();
            return Json(schoolNotes.Count > 0 ? schoolNotes : new List<Note>());
        }

        // API to save or update a note
        [HttpPost]
        public IActionResult SaveNote([FromBody] Note newNote)
        {
            if (!string.IsNullOrEmpty(newNote.Title) &&
                !string.IsNullOrEmpty(newNote.Content) &&
                !string.IsNullOrEmpty(newNote.Category))
            {
                newNote.LastEdited = DateTime.Now; // Set the last edited timestamp
                Notes.Add(newNote);

                // Debug log to confirm saving
                Console.WriteLine($"Saved Note: Title = {newNote.Title}, Category = {newNote.Category}, LastEdited = {newNote.LastEdited}");
                return Ok();
            }

            return BadRequest("Invalid note data.");
        }


        // API to fetch all notes
        [HttpGet]
        public IActionResult GetNotes()
        {
            return Json(Notes);
        }

        // API to delete a note by index
        [HttpPost]
        public IActionResult DeleteNote(int index)
        {
            if (index >= 0 && index < Notes.Count)
            {
                Notes.RemoveAt(index);
                return Ok();
            }
            return BadRequest("Invalid note index.");
        }

        // API to get notes by category for Quick Access
        [HttpGet]
        public IActionResult GetNotesByCategory(string category)
        {
            var filteredNotes = Notes.Where(note => string.Equals(note.Category, category, StringComparison.OrdinalIgnoreCase)).ToList();
            return Json(filteredNotes.Count > 0 ? filteredNotes : new List<Note>());
        }
    }

    // Updated Note model
    public class Note
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; } // Property for categorization
        public DateTime LastEdited { get; set; } // Timestamp for last edited
    }
}
