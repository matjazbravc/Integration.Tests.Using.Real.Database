using IntegrationTestingWithDockerDemo.Mediator.Queries;
using IntegrationTestingWithDockerDemo.Models;
using IntegrationTestingWithDockerDemo.Services.Repositories;
using MediatR;

namespace IntegrationTestingWithDockerDemo.Mediator.Handlers;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Student?>
{
    private readonly IStudentsRepository _studentsRepository;

    public GetStudentByIdQueryHandler(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public async Task<Student?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _studentsRepository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
    }
}
