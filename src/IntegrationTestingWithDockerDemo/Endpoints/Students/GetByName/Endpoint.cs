using IntegrationTestingWithDockerDemo.Models;
using IntegrationTestingWithDockerDemo.Services;

namespace IntegrationTestingWithDockerDemo.Endpoints.Students.GetByName;

public static class Endpoint
{
    public static WebApplication MapGetByNameStudent(this WebApplication app)
    {
        app.MapGet("student/get-by-name", async (string name, IStudentsService studentService) =>
        {
            try
            {
                Student? existingStudent = await studentService.GetByName(name).ConfigureAwait(false);
                return existingStudent != null ? Results.Ok(existingStudent) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        return app;
    }
}