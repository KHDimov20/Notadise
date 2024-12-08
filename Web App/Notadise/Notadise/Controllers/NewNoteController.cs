using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Notadise.Controllers
{
    public class NotesController : Controller
    {
        // Съхранение на бележки в паметта 
        private static List<Note> Notes = new List<Note>();
        private static readonly object NotesLock = new object();

        // Изобразяване на страницата "NewNote"
        public IActionResult NewNote()
        {
            return View();
        }

        // Изграждане на страницата „MyNotes"
        public IActionResult MyNotes()
        {
            return View();
        }

        // API за запазване или актуализиране на бележка 
        [HttpPost]
        public IActionResult SaveNote([FromBody] Note newNote)
        {
            if (!string.IsNullOrEmpty(newNote.Title) &&
                !string.IsNullOrEmpty(newNote.Content) &&
                !string.IsNullOrEmpty(newNote.Category))
            {
                newNote.LastEdited = DateTime.Now; // Задаване на времевия печат на последната редакция 

                // Използвайте нишка, за да запазите бележката 
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

        // API за извличане на всички бележки 
        [HttpGet]
        public IActionResult GetNotes()
        {
            Thread logThread = new Thread(() =>
            {
                Console.WriteLine($"[{DateTime.Now}] All notes fetched. Total count: {Notes.Count}");
            });
            logThread.IsBackground = true;
            logThread.Start();

            return Json(Notes);
        }

        // API за изтриване на бележка по индекс 
        [HttpPost]
        public IActionResult DeleteNote(int index)
        {
            if (index >= 0 && index < Notes.Count)
            {
                // Използвайте нишка, за да изтриете бележката 
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

        // API за получаване на бележки по категории за бърз достъп/Quick Access
        [HttpGet]
        public IActionResult GetNotesByCategory(string category)
        {
            var filteredNotes = Notes.Where(note => string.Equals(note.Category, category, StringComparison.OrdinalIgnoreCase)).ToList();

            // Използване на нишка за регистриране на специфично за категорията извличане 
            Thread logThread = new Thread(() =>
            {
                Console.WriteLine($"[{DateTime.Now}] Retrieved {filteredNotes.Count} notes for category '{category}'.");
            });
            logThread.IsBackground = true;
            logThread.Start();

            return Json(filteredNotes.Count > 0 ? filteredNotes : new List<Note>());
        }
    }

    public class Note
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public DateTime LastEdited { get; set; }
    }
}
