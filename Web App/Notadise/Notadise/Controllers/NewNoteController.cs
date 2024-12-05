using Microsoft.AspNetCore.Mvc;
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
        public IActionResult School()
        {
            // Filter notes by category "School"
            var schoolNotes = Notes.Where(note => note.Category == "School").ToList();

            // Pass the filtered notes to the view
            return View(schoolNotes);
        }

        // API to save a note
        [HttpPost]
        public IActionResult SaveNote([FromBody] Note newNote)
        {
            if (!string.IsNullOrEmpty(newNote.Title) && !string.IsNullOrEmpty(newNote.Content) && !string.IsNullOrEmpty(newNote.Category))
            {
                Notes.Add(newNote);
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
    }

    // Updated Note model
    public class Note
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; } // New property for categorization
    }
}
