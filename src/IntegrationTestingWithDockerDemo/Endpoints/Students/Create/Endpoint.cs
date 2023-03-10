using IntegrationTestingWithDockerDemo.Models;
using IntegrationTestingWithDockerDemo.Services;
using MiniValidation;

namespace IntegrationTestingWithDockerDemo.Endpoints.Students.Create;

public static class Endpoint
{
    public static WebApplication MapPostCreateStudent(this WebApplication app)
    {
        app.MapPost("student/create", async (Student student, IStudentsService studentService) =>
            !MiniValidator.TryValidate(student, out IDictionary<string, string[]>? errors)
                ? Results.ValidationProblem(errors)
                : Results.Ok(await studentService.Create(student).ConfigureAwait(false)));
        return app;
    }
}