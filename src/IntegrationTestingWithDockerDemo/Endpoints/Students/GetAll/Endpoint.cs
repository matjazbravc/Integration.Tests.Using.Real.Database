using IntegrationTestingWithDockerDemo.Models;
using IntegrationTestingWithDockerDemo.Services;

namespace IntegrationTestingWithDockerDemo.Endpoints.Students.GetAll;

public static class Endpoint
{
    public static WebApplication MapGetAllStudents(this WebApplication app)
    {
        app.MapGet("student/get-all", async (IStudentsService studentService) =>
        {
            try
            {
                IList<Student>? existingStudents = await studentService.GetAll().ConfigureAwait(false);
                return existingStudents != null ? Results.Ok(existingStudents) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        return app;
    }
}