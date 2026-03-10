using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Catalog.ListItem;

public record ModuleListItem(
    int ModuleId,
    Guid CourseId,
    string ModuleTitle,
    int ModuleOrderNumber,
    [property: JsonIgnore] string LessonsJson
)
{
    public List<LessonListItem> Lessons =>
        JsonSerializer.Deserialize<List<LessonListItem>>(LessonsJson) ?? [];
}
