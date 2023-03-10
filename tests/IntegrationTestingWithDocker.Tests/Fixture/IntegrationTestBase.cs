using IntegrationTestingWithDocker.Tests.Services;
using IntegrationTestingWithDockerDemo.Models;
using IntegrationTestingWithDockerDemo.Services.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTestingWithDocker.Tests.Fixture;

/// <summary>
/// Base class for creating HttpClient and repositories.
/// </summary>
public class IntegrationTestBase : MySqlTestBase
{
    private WebApplicationFactory<Program>? _application;

    public HttpClient HttpClient { get; private set; }

    public RestApiService RestApiService { get; private set; }

    public StudentsRepository StudentsRepository { get; private set; }

    protected override string DockerComposeFileFullPath()
    {
        string composeFile = Path.Combine(Directory.GetCurrentDirectory(), @"Fixture\docker-compose.yml");
        if (!File.Exists(composeFile))
        {
            throw new FileNotFoundException($"docker-compose file {composeFile} not found.");
        }
        return composeFile;
    }

    protected override string TestSettingsFileFullPath()
    {
        string settingsFile = Path.Combine(Directory.GetCurrentDirectory(), @"Fixture\test-settings.json");
        if (!File.Exists(settingsFile))
        {
            throw new FileNotFoundException($"Settings file {settingsFile} not found.");
        }
        return settingsFile;
    }

    protected override void OnContainerInitialized()
    {
        base.OnContainerInitialized();
        StudentsRepository = new StudentsRepository(MySqlDbContext);
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => 
                builder.ConfigureServices(services =>
                    services.AddScoped<IStudentsRepository>(_ => StudentsRepository)));
        HttpClient = _application.CreateClient();
        RestApiService = new RestApiService(HttpClient);
        SeedData();
    }

    protected override void OnContainerTearDown()
    {
        base.OnContainerTearDown();
        StudentsRepository = null;
        _application?.Dispose();
    }

    private void SeedData()
    {
        if (MySqlDbContext?.Students != null && !MySqlDbContext.Students.Any())
        {
            MySqlDbContext.Students.AddRange(GetPreconfiguredStudents());
            MySqlDbContext.SaveChanges();
        }
    }

    // Demo dataset
    private static IEnumerable<Student> GetPreconfiguredStudents()
    {
        return new List<Student>()
        {
            new("Tonny Blatt", "Str. 1c", "tony@gmail.com", new DateTime(1991, 10, 7)),
            new("Anitta Goldman", "Str. 2c", "Anita@gmail.com", new DateTime(1975, 5, 31)),
            new("Alan Ford", "Str. 3c", "alan@gmail.com", new DateTime(2000, 8, 26)),
            new("Jim Beam", "Str. 4c", "jim@gmail.com", new DateTime(1984, 1, 12)),
            new("Suzanne White", "Str. 5c", "suzanne@gmail.com", new DateTime(1992, 3, 10)),
        };
    }
}