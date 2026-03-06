using System.Data;
using Application.Contracts.UnitOfWork;
using Application.Interfaces.Repositories.Catalog;
using Dapper;
using Domain.Entities.Catalog;

namespace Infrastructure.Persistance.Repositories.Catalog;

public class LessonRepository : ILessonRepository
{
    #region Dependencies
    
    private readonly IDbConnection _contextDapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public LessonRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork)
    {
	    _contextDapper = contextDapper;
	    _unitOfWork = unitOfWork;
    }
    
    #endregion

    public async Task<Lesson> GetById(int id) => (await _contextDapper.QueryFirstOrDefaultAsync<Lesson>(
											    @"select id as Id,
	   												  module_id as ModuleId,
	   													  title as Title,
	   												  video_url as VideoUrl,
	   											   order_number as OrderNumber
												   from catalog.lessons
												   where id = @Id", new { Id = id }))!;

    public async Task Create(Lesson lesson) => await _contextDapper.ExecuteAsync(
										   @"insert into catalog.lessons(module_id, title, video_url, order_number)
                    							values(@ModuleId, @Title, @VideoUrl, @OrderNumber", 
										   new
										   {
											   lesson.ModuleId, 
											   lesson.Title, 
											   lesson.VideoUrl, 
											   lesson.OrderNumber
										   }, _unitOfWork.Transaction);

    public async Task<bool> Update(Lesson lesson)
    {
	   var rowsAffected = await  _contextDapper.ExecuteAsync(
								@"update catalog.lessons
										set title = @Title ,
										    video_url = @VideoUrl,
										order_number = @OrderNumber
									where id = @Id", 
								new
								{
									lesson.Title, 
									lesson.VideoUrl,
									lesson.OrderNumber,
									lesson.Id
								}, _unitOfWork.Transaction);
	   return rowsAffected > 0;

    }
    public async Task<bool> Delete(int id)
    {
		var rowsAffected =  await _contextDapper.ExecuteAsync(
							@"delete from catalog.lessons 
       								where id = @Id", new { Id = id }, _unitOfWork.Transaction);
		return rowsAffected > 0;
    }
}