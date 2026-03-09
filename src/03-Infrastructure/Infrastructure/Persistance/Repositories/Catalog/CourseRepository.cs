using System.Data;
using Application.Features.Auth.Responses;
using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Dapper;
using Domain.Entities.Catalog;
using Infrastructure.Persistance.Dapper.Queries.Course;

namespace Infrastructure.Persistance.Repositories.Catalog;

public class CourseRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork) : ICourseRepository
{
    public async Task<IEnumerable<CourseResponse>> GetAll()
        => await contextDapper.QueryAsync<CourseResponse>(CourseQueries.GetAll);

    public async Task<IEnumerable<CourseResponse>> GetCoursesByTitle(string title)
        => await contextDapper.QueryAsync<CourseResponse>(CourseQueries.GetCoursesByTitle, new { Title = title }) ;

    public async Task<InstructorResponse> GetInstructorByName(string name)
        => (await contextDapper.QueryFirstOrDefaultAsync<InstructorResponse>(CourseQueries.GetInstructorByName, new { Name = name }))!;

    public async Task<Course> GetById(Guid id) 
        => (await contextDapper.QueryFirstOrDefaultAsync<Course>(CourseQueries.GetById, new { Id = id }))!;

    public async Task Create(Course course) => await contextDapper.ExecuteAsync(CourseQueries.Create,
        new { course.Title, course.Description, course.Price, course.InstructorId }, unitOfWork.Transaction);

    public async Task<bool> Update(Course course)
    {
        var rowsAffected = await contextDapper.ExecuteAsync(CourseQueries.Update,
            new { course.Title, course.Description, course.Price, course.Id }, unitOfWork.Transaction);
        
        return rowsAffected > 0;
    }
    public async Task<bool> Delete(Guid id)
    {
            var rowsAffected = await contextDapper.ExecuteAsync(CourseQueries.Delete,
                new { Id = id }, unitOfWork.Transaction);
            
            return rowsAffected > 0;
    }
}