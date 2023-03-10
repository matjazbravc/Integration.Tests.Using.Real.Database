using IntegrationTestingWithDockerDemo.Models;
using IntegrationTestingWithDockerDemo.Services.Repositories.Base;

namespace IntegrationTestingWithDockerDemo.Services.Repositories;

public interface IStudentsRepository : IBaseRepository<Student>
{
    Task<IList<Student>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Student?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}