using IntegrationTestingWithDockerDemo.Models;
using IntegrationTestingWithDockerDemo.Services;

namespace IntegrationTestingWithDockerDemo.Endpoints.Students.Update;

public static class Endpoint
{
    public static WebApplication MapPutUpdateStudent(this WebApplication app)
    {
        app.MapPut("student/update", async (Student student, IStudentsService studentService) =>
        {
            try
            {
                Student updatedStudent = await studentService.Update(student).ConfigureAwait(false);
                return Results.Ok(updatedStudent);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        return app;
    }
}