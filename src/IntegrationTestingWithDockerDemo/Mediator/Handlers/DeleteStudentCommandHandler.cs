using IntegrationTestingWithDockerDemo.Mediator.Commands;
using IntegrationTestingWithDockerDemo.Services.Repositories;
using MediatR;

namespace IntegrationTestingWithDockerDemo.Mediator.Handlers;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
{
    private readonly IStudentsRepository _studentsRepository;

    public DeleteStudentCommandHandler(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        return await _studentsRepository.RemoveAsync(request.Id, cancellationToken).ConfigureAwait(false);
    }
}