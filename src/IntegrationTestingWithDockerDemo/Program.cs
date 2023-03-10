using IntegrationTestingWithDockerDemo.Data;
using IntegrationTestingWithDockerDemo.Endpoints.Students.Create;
using IntegrationTestingWithDockerDemo.Endpoints.Students.Delete;
using IntegrationTestingWithDockerDemo.Endpoints.Students.GetAll;
using IntegrationTestingWithDockerDemo.Endpoints.Students.GetById;
using IntegrationTestingWithDockerDemo.Endpoints.Students.GetByName;
using IntegrationTestingWithDockerDemo.Endpoints.Students.Update;
using IntegrationTestingWithDockerDemo.Services.Repositories;
using IntegrationTestingWithDockerDemo.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registers the given context as a service.
// Add services to the container.
builder.Services.AddDbContext<MySqlDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), opt =>
    {
        opt.CommandTimeout((int)TimeSpan.FromSeconds(60).TotalSeconds);
        opt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);
    });
});

// Registers handlers and mediator types from the specified assemblies.
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});

builder.Services.AddTransient<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<IStudentsService, StudentsService>();

WebApplication app = builder.Build();

// Migrate database
using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;
    MySqlDbContext context = services.GetRequiredService<MySqlDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map endpoints
app.MapGetAllStudents()
    .MapGetByIdStudent()
    .MapGetByNameStudent()
    .MapPostCreateStudent()
    .MapPutUpdateStudent()
    .MapDeleteStudent();

// Redirection to Swagger UI
app.MapGet("", context =>
{
    context.Response.Redirect("./swagger/index.html", permanent: false);
    return Task.FromResult(0);
});

app.Run();

public partial class Program { }
