namespace Infrastructure.Persistance.Dapper.Queries.Learning;

public class EnrollmentQueries
{
    public const string GetAllEnrollmentsByStudents = """
        select u.id as StudentId,
             u.name as StudentName,
              u.email as StudentEmail,
              
              (select json_agg(json_build_object(
                     'CourseId', c.id,
                     'CourseTitle', c.title,
                     'EnrolledAt', e.enrolled_at
                         )
                     )
                     from learning.enrollments e 
                     inner join catalog.courses c 
                     on c.id = e.course_id
                     where e.user_id = u.id
                     ) as Courses
                from auth.users u
        """;

    public const string GetById = """
                                  select user_id as UserId, 
                                  course_id as CourseId, 
                                  enrolled_at as EnrolledAt 
                                  from learning.enrollments
                                  where id = @UserId
                                  """;

    public const string Create = """
                                 insert into learning.enrollments(
                                 user_id,
                                 course_id)
                                 values(
                                 @UserId, 
                                 @CourseId)
                                 """;

    public const string Delete = """
                                 delete from learning.enrollments 
                                 where user_id = @UserId 
                                 """;
}
