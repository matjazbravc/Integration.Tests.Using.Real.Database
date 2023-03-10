using FluentAssertions;
using IntegrationTestingWithDocker.Tests.Fixture;
using IntegrationTestingWithDocker.Tests.Services;
using IntegrationTestingWithDockerDemo.Models;

namespace IntegrationTestingWithDocker.Tests.IntegrationTests;

public class RestApiTests : IClassFixture<IntegrationTestBase>
{
    private readonly RestApiService _restApiService;

    public RestApiTests(IntegrationTestBase fixture)
    {
        _restApiService = fixture.RestApiService;
    }

    [Fact]
    public async Task RestApiTest_CreateStudent_Negative()
    {
        var newStudent = new Student("Martin Toluise", "Str. 7789/G", "InvalidEmail",
            new DateTime(1997, 12, 18));

        var student = await _restApiService.CreateStudentAsync(newStudent).ConfigureAwait(false);

        // Assert
        student?.Should().BeNull();
    }

    [Fact]
    public async Task RestApiTest_CreateStudent_Positive()
    {
        var newStudent = new Student("Martin Toluise", "Str. 7789/G", "martin@yahoo.com",
            new DateTime(1997, 12, 18));

        var student = await _restApiService.CreateStudentAsync(newStudent).ConfigureAwait(false);

        // Assert
        student?.Should().NotBeNull();
    }

    [Fact]
    public async Task RestApiTest_DeleteStudent_Negative()
    {
        bool success = await _restApiService.DeleteStudentAsync(999999).ConfigureAwait(false);

        // Assert
        success.Should().BeFalse();
    }

    [Fact]
    public async Task RestApiTest_DeleteStudent_Positive()
    {
        Student newStudent = new Student("Thereza Vuitton", "Str. 1234/H", "thereza@yahoo.com",
            new DateTime(1991, 10, 23));

        Student? student = await _restApiService.CreateStudentAsync(newStudent).ConfigureAwait(false);

        bool success = await _restApiService.DeleteStudentAsync(student.Id).ConfigureAwait(false);

        // Assert
        success.Should().BeTrue();
    }

    [Fact]
    public async Task RestApiTest_GetAllStudents_Positive()
    {
        IList<Student>? students = await _restApiService.GetAllStudentsAsync().ConfigureAwait(false);

        // Assert
        students.Should().NotBeEmpty();
    }

    [Fact]
    public async Task RestApiTest_GetStudentById_Negative()
    {
        Student? student = await _restApiService.GetStudentByIdAsync(99999999).ConfigureAwait(false);

        // Assert
        student?.Should().BeNull();
    }

    [Fact]
    public async Task RestApiTest_GetStudentById_Positive()
    {
        Student? student = null;
        IList<Student>? allStudents = await _restApiService.GetAllStudentsAsync().ConfigureAwait(false);
        if (allStudents != null && allStudents.Any())
        {
            student = await _restApiService.GetStudentByIdAsync(allStudents.First().Id).ConfigureAwait(false);
        }

        // Assert
        student?.Should().NotBeNull();
    }

    [Fact]
    public async Task RestApiTest_GetStudentByName_Negative()
    {
        var student = await _restApiService.GetStudentByNameAsync("bla bla").ConfigureAwait(false);

        // Assert
        student?.Should().BeNull();
    }

    [Fact]
    public async Task RestApiTest_GetStudentByName_Positive()
    {
        Student? student = null;
        IList<Student>? allStudents = await _restApiService.GetAllStudentsAsync().ConfigureAwait(false);
        if (allStudents != null && allStudents.Any())
        {
            student = await _restApiService.GetStudentByNameAsync(allStudents.First().Name).ConfigureAwait(false);
        }

        // Assert
        student?.Should().NotBeNull();
    }

    [Fact]
    public async Task RestApiTest_UpdateStudent_Negative()
    {
        var updatedStudent = await _restApiService.UpdateStudentAsync(null).ConfigureAwait(false);

        // Assert
        updatedStudent?.Should().BeNull();
    }

    [Fact]
    public async Task RestApiTest_UpdateStudent_Positive()
    {
        Student? updatedStudent = null;
        IList<Student>? allStudents = await _restApiService.GetAllStudentsAsync().ConfigureAwait(false);
        if (allStudents != null && allStudents.Any())
        {
            var student = await _restApiService.GetStudentByIdAsync(allStudents.First().Id).ConfigureAwait(false);
            if (student != null)
            {
                student.Email = "new.stundent@tnt.com";
                updatedStudent = await _restApiService.UpdateStudentAsync(student).ConfigureAwait(false);
            }
        }

        // Assert
        updatedStudent?.Should().NotBeNull();
    }
}