using IntegrationTestingWithDockerDemo.Mediator.Queries;
using IntegrationTestingWithDockerDemo.Models;
using IntegrationTestingWithDockerDemo.Services.Repositories;
using MediatR;

namespace IntegrationTestingWithDockerDemo.Mediator.Handlers;

public class GetStudentByNameQueryHandler : IRequestHandler<GetStudentByNameQuery, Student?>
{
    private readonly IStudentsRepository _studentsRepository;

    public GetStudentByNameQueryHandler(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public async Task<Student?> Handle(GetStudentByNameQuery request, CancellationToken cancellationToken)
    {
        return await _studentsRepository.GetByNameAsync(request.Name, cancellationToken).ConfigureAwait(false);
    }
}
