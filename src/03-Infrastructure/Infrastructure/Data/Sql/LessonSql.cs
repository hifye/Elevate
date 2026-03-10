namespace Infrastructure.Data.Sql;

public class LessonSql
{
    public const string GetAllLessons = """
                                        select id as LessonId,
                                        title as LessonTitle,
                                        video_url as LessonVideoUrl,
                                        order_number as LessonOrderNumber
                                        from catalog.lessons
                                        """;

    public const string GetById = """
                                  select id as Id,
                                  module_id as ModuleId,
                                  title as Title,
                                  video_url as VideoUrl,
                                  order_number as OrderNumber
                                  from catalog.lessons
                                  where id = @Id
                                  """;

    public const string Create = """
                                 insert into catalog.lessons(
                                 module_id, 
                                 title, 
                                 video_url, 
                                 order_number)
                                 values(
                                 @ModuleId, 
                                 @Title,
                                 @VideoUrl, 
                                 @OrderNumber)
                                 """;

    public const string Update = """
                                 update catalog.lessons
                                 set title = @Title ,
                                 video_url = @VideoUrl,
                                 order_number = @OrderNumber
                                 where id = @Id
                                 """;

    public const string Delete = """
                                 delete from catalog.lessons 
                                 where id = @Id
                                 """;
}