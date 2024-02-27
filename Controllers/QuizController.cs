using Microsoft.AspNetCore.Mvc;
using ProjectPitch4.models;

namespace ProjectPitch4.Controllers
{
    /// <summary>
    /// Controller for managing quiz questions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly MySqlContext _context;

        /// <summary>
        /// Constructor initializing the MySQL context.
        /// </summary>
        public QuizController(MySqlContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all quiz questions.
        /// </summary>
        /// <remarks>Returns a list of all quiz questions.</remarks>
        /// <returns>All quiz questions.</returns>
        [HttpGet]
        public IEnumerable<CcquizQuestion> Get()
        {
            return _context.CcquizQuestions.ToList();
        }

        /// <summary>
        /// Retrieves a specific quiz question by ID.
        /// </summary>
        /// <remarks>Returns the quiz question with the specified ID.</remarks>
        /// <param name="id">The ID of the quiz question to retrieve.</param>
        /// <returns>The quiz question with the specified ID.</returns>
        [HttpGet("{id}")]
        public CcquizQuestion Get(int id)
        {
            return _context.CcquizQuestions.Find(id);
        }

        /// <summary>
        /// Adds a new quiz question.
        /// </summary>
        /// <remarks>Adds a new quiz question to the database.</remarks>
        /// <param name="value">The quiz question to add.</param>
        [HttpPost]
        public void Post([FromBody] CcquizQuestion value)
        {
            _context.CcquizQuestions.Add(value);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates an existing quiz question.
        /// </summary>
        /// <remarks>Updates an existing quiz question in the database.</remarks>
        /// <param name="id">The ID of the quiz question to update.</param>
        /// <param name="value">The updated quiz question.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CcquizQuestion value)
        {
            _context.CcquizQuestions.Update(value);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes a quiz question by ID.
        /// </summary>
        /// <remarks>Deletes a quiz question from the database by its ID.</remarks>
        /// <param name="id">The ID of the quiz question to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.CcquizQuestions.Remove(Get(id));
            _context.SaveChanges();
        }

        // Scaffold-DbContext 'server=localhost;user=root;password=Ademidun98!;database=quiz_master;'
        // Pomelo.EntityFrameworkCore.MySql -OutputDir models -Context MySqlContext -f
    }
}
