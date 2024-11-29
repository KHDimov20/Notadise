using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    public class NotesController : Controller
    {
        public IActionResult NewNote()
        {
            // Example: Fetch or create a new note model
            var note = new NoteModel
            {
                Title = "Sample Note",
                LastEdited = DateTime.Now,
                FileSizeKB = 875, // Example size
                CreationDate = new DateTime(2023, 11, 11),
                Sections = new List<NoteSection>
                {
                    new NoteSection { Id = 1, Title = "Point 1", Content = "Sample content for point 1" },
                    new NoteSection { Id = 2, Title = "Point 2", Content = "Sample content for point 2" }
                }
            };

            // Pass the model to the view
            return View(note);
        }
    }
}
