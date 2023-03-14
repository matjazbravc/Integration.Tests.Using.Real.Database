﻿using IntegrationTestingWithDockerDemo.Mediator.Commands;
using IntegrationTestingWithDockerDemo.Models;
using IntegrationTestingWithDockerDemo.Services.Repositories;
using MediatR;

namespace IntegrationTestingWithDockerDemo.Mediator.Handlers;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Student?>
{
    private readonly IStudentsRepository _studentsRepository;

    public UpdateStudentCommandHandler(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public async Task<Student?> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        Student student = new Student(request.Name, request.Address, request.Email, request.DateOfBirth, request.Active)
        {
            Id = request.Id
        };
        return await _studentsRepository.UpdateAsync(student, cancellationToken).ConfigureAwait(false);
    }
}