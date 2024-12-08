using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Notadise.Controllers
{
    public class NotesController : Controller
    {
        // In-memory storage for notes
        private static List<Note> Notes = new List<Note>();
        private static readonly object NotesLock = new object(); // For thread safety

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

        // API to save or update a note
        [HttpPost]
        public IActionResult SaveNote([FromBody] Note newNote)
        {
            if (!string.IsNullOrEmpty(newNote.Title) &&
                !string.IsNullOrEmpty(newNote.Content) &&
                !string.IsNullOrEmpty(newNote.Category))
            {
                newNote.LastEdited = DateTime.Now; // Set the last edited timestamp

                // Use a thread to save the note
                Thread saveThread = new Thread(() =>
                {
                    lock (NotesLock)
                    {
                        Notes.Add(newNote);
                    }
                    Console.WriteLine($"[{DateTime.Now}] Saved Note: Title = {newNote.Title}, Category = {newNote.Category}, LastEdited = {newNote.LastEdited}");
                });
                saveThread.IsBackground = true;
                saveThread.Start();

                return Ok();
            }

            return BadRequest("Invalid note data.");
        }

        // API to fetch all notes
        [HttpGet]
        public IActionResult GetNotes()
        {
            // Use a thread to log fetching all notes
            Thread logThread = new Thread(() =>
            {
                Console.WriteLine($"[{DateTime.Now}] All notes fetched. Total count: {Notes.Count}");
            });
            logThread.IsBackground = true;
            logThread.Start();

            return Json(Notes);
        }

        // API to delete a note by index
        [HttpPost]
        public IActionResult DeleteNote(int index)
        {
            if (index >= 0 && index < Notes.Count)
            {
                // Use a thread to delete the note
                Thread deleteThread = new Thread(() =>
                {
                    lock (NotesLock)
                    {
                        Notes.RemoveAt(index);
                    }
                    Console.WriteLine($"[{DateTime.Now}] Deleted note at index {index}.");
                });
                deleteThread.IsBackground = true;
                deleteThread.Start();

                return Ok();
            }
            return BadRequest("Invalid note index.");
        }

        // API to get notes by category for Quick Access
        [HttpGet]
        public IActionResult GetNotesByCategory(string category)
        {
            var filteredNotes = Notes.Where(note => string.Equals(note.Category, category, StringComparison.OrdinalIgnoreCase)).ToList();

            // Use a thread to log category-specific retrieval
            Thread logThread = new Thread(() =>
            {
                Console.WriteLine($"[{DateTime.Now}] Retrieved {filteredNotes.Count} notes for category '{category}'.");
            });
            logThread.IsBackground = true;
            logThread.Start();

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
