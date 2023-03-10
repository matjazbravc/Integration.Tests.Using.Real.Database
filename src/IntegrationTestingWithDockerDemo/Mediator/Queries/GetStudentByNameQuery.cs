using IntegrationTestingWithDockerDemo.Models;
using MediatR;

namespace IntegrationTestingWithDockerDemo.Mediator.Queries;

public class GetStudentByNameQuery : IRequest<Student>
{
    public string Name { get; set; }
}