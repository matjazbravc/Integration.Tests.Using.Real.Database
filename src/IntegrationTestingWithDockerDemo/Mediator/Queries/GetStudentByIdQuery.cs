using IntegrationTestingWithDockerDemo.Models;
using MediatR;

namespace IntegrationTestingWithDockerDemo.Mediator.Queries;

public class GetStudentByIdQuery : IRequest<Student>
{
    public int Id { get; set; }
}