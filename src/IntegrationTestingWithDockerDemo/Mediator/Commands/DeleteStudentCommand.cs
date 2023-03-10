using MediatR;

namespace IntegrationTestingWithDockerDemo.Mediator.Commands;

public class DeleteStudentCommand : IRequest<bool>
{
    public int Id { get; set; }
}