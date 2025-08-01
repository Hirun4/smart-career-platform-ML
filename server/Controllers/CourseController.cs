using Microsoft.AspNetCore.Mvc;
using SmartCareerPlatform.Models;
using SmartCareerPlatform.Services;

namespace SmartCareerPlatform.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();
            return Ok(course);
        }

        [HttpGet("recommend")]
        public IActionResult GetRecommendations([FromQuery] int userId)
        {
            if (userId <= 0)
                return BadRequest("Invalid user ID.");
                
            var courses = _courseService.GetRecommendedCoursesAsync(userId);
            return Ok(courses);
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollCourse([FromBody] EnrollmentRequest request)
        {
            var result = await _courseService.EnrollCourseAsync(request.CourseId);
            if (!result)
                return BadRequest("Enrollment failed.");
                
            return Ok(new { Message = "Enrolled successfully." });
        }
    }

    public class EnrollmentRequest
    {
        public int CourseId { get; set; }
       
    }
}