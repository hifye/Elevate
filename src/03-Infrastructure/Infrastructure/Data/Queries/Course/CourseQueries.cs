namespace Infrastructure.Data.Queries.Course;

public static class CourseQueries
{
    public const string GetAll = """
                                 select id as CourseId,
                                 title as CourseTitle,
                                 description as CourseDescription,
                                 price as CoursePrice,
                                 instructor_id as InstructorId
                                 from catalog.courses
                                 """;

    public const string GetCoursesByTitle = """
                                            select id as CourseId,
                                            title as CourseTitle,
                                            description as CourseDescription,
                                            price as CoursePrice,
                                            instructor_id as InstructorId
                                            from catalog.courses
                                            where title = @Title
                                            """;

    public const string GetInstructorByName = """
                                              select id as InstructorId,
                                              name as InstructorName,
                                              email as InstructorEmail
                                              from auth.users
                                              where name = @Name
                                              """;

    public const string GetById = """
                                  select id as Id,
                                  title as Title,
                                  description as Description,
                                  price as Price,
                                  instructor_id as InstructorId
                                  from catalog.courses 
                                  where id = @Id
                                  """;

    public const string Create = """
                                 insert into catalog.courses(
                                 title, 
                                 description, 
                                 price, 
                                 instructor_id)
                                 values(
                                 @Title,
                                 @Description,
                                 @Price, 
                                 @InstructorId)
                                 """;

    public const string Update = """
                                 update catalog.courses
                                 set title = @Title,
                                 description = @Description,
                                 price = @Price
                                 where id = @Id
                                 """;

    public const string Delete = """
                                 delete from catalog.courses
                                 where id = @Id
                                 """;
}