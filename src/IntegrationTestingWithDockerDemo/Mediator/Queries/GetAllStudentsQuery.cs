using IntegrationTestingWithDockerDemo.Models;
using MediatR;

namespace IntegrationTestingWithDockerDemo.Mediator.Queries;

public class GetAllStudentsQuery : IRequest<IList<Student>>
{
}