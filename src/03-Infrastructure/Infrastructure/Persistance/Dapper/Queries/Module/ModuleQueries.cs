namespace Infrastructure.Persistance.Dapper.Queries.Module;

public class ModuleQueries
{
    public const string GetModulesAndLessons = """
        select m.id as ModuleId,
               m.course_id as CourseId,
            m.title as ModuleTitle,
        	   m.order_number as ModuleOrderNumber,
        	       
        			(select json_agg(json_build_object(
        			       'LessonId', l.id,
        			       'LessonTitle', l.title,
        			       'LessonVideoUrl', l.video_url,
        			       'LessonOrderNumber', l.order_number
        					)
        			       order by l.order_number
        			       )
        			       from catalog.lessons l
        			       where l.module_id = m.id
        			       ) as Lessons
        			from catalog.modules m
        			order by m.order_number
        """;

    public const string GetById = """
                                  select id as Id,
                                  course_id as CourseId,
                                  title as Title,
                                  order_number as OrderNumber
                                  from catalog.modules
                                  where id = @Id
                                  """;

    public const string Create = """
                                 insert into catalog.modules(
                                 course_id,
                                 title,
                                 order_number)
                                 values(
                                 @CourseId,
                                 @Title,
                                 @Number)
                                 """;

    public const string Update = """
                                 update catalog.modules
                                 set title = @Title,
                                 order_number = @OrderNumber
                                 where id = @Id
                                 """;

    public const string Delete = """
                                 delete from catalog.modules
                                 where id = @Id
                                 """;
}
