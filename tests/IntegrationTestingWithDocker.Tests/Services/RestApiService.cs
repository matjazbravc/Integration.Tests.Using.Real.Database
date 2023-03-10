using IntegrationTestingWithDockerDemo.Models;

namespace IntegrationTestingWithDocker.Tests.Services;

public class RestApiService : HttpClientBase, IRestApiService
{
    public RestApiService(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public async Task<Student?> CreateStudentAsync(Student student, CancellationToken cancellationToken = default)
    {
        return await PostAsync("/student/create", student, cancellationToken).ConfigureAwait(false);
    }

    public async Task<bool> DeleteStudentAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DeleteAsync($"/student/delete?id={id}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<IList<Student>?> GetAllStudentsAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<IList<Student>>("/student/get-all", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Student?> GetStudentByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<Student?>($"/student/get-by-id?id={id}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Student?> GetStudentByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await GetAsync<Student?>($"/student/get-by-name?name={name}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Student?> UpdateStudentAsync(Student student, CancellationToken cancellationToken = default)
    {
        return await PutAsync<Student, Student?>("/student/update", student, cancellationToken).ConfigureAwait(false);
    }
}
