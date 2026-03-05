using System.Data;
using Application.Contracts.Repositories.Catalog;
using Application.Contracts.UnitOfWork;
using Dapper;
using Domain.Entities.Catalog;

namespace Infrastructure.Repositories.Catalog;

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

    public async Task Create(Lesson lesson) => await _contextDapper.ExecuteAsync(
										   @"insert into catalog.lessons(
	                     							 module_id as ModuleId,
	                      							 title as Title,
	                      							 video_url as VideoUrl,
	                      							 order_number as OrderNumber)
                    							values(
	                        							@ModuleId,
	                        							@Title,
	                        							@VideoUrl,
	                        							@OrderNumber", lesson, _unitOfWork.Transaction);

    public void Update(Lesson lesson) => _contextDapper.ExecuteAsync(
									@"update catalog.lessons
											set title = @Title ,
											order_number = @OrderNumber
											where id = @Id", lesson, _unitOfWork.Transaction);

    public async Task<bool> Delete(int id)
    {
		var rowsAffected =  await _contextDapper.ExecuteAsync(
							@"delete from catalog.lessons 
       								where id = @Id", new { Id = id }, _unitOfWork.Transaction);
		return rowsAffected > 0;
    }
}