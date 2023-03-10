using IntegrationTestingWithDockerDemo.Models;

namespace IntegrationTestingWithDocker.Tests.Services;

public interface IRestApiService
{
    Task<Student?> CreateStudentAsync(Student student, CancellationToken cancellationToken = default);

    Task<bool> DeleteStudentAsync(int id, CancellationToken cancellationToken = default);

    Task<IList<Student>?> GetAllStudentsAsync(CancellationToken cancellationToken = default);

    Task<Student?> GetStudentByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Student?> GetStudentByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<Student?> UpdateStudentAsync(Student student, CancellationToken cancellationToken = default);
}