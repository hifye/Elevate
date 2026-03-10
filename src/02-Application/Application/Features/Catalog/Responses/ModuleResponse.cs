using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Catalog.Responses;

public record ModuleResponse(int ModuleId, Guid CourseId, string ModuleTitle, int ModuleOrderNumber, [property: JsonIgnore] string LessonsJson)
{
    public List<LessonResponse> Lessons => JsonSerializer.Deserialize<List<LessonResponse>>(LessonsJson) ?? [];
}