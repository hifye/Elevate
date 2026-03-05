using Application.Contracts.Responses.Catalog;
using Application.DTOs.Catalog;

namespace Application.Contracts.Services.Catalog;

public interface ILessonService
{
    Task<LessonResponse> Create(LessonRequest request);
    Task<bool> Update(LessonRequest request, int id);
    Task<bool> Delete(int id);
}