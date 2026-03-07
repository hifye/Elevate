using System.Data;
using Application.Features.Auth.Responses;
using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Dapper;
using Domain.Entities.Catalog;

namespace Infrastructure.Persistance.Repositories.Catalog;

public class CourseRepository : ICourseRepository
{
    #region Dependencies
    
    private readonly IDbConnection _contextDapper;
    private readonly IUnitOfWork _unitOfWork;

    public CourseRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork)
    {
        _contextDapper = contextDapper;
        _unitOfWork = unitOfWork;
    }

    #endregion

    /// <summary>
    /// Recupera todos os cursos associados a todos os instrutores existentes no sistema.
    /// Os dados incluem informações detalhadas sobre os cursos e seus respectivos instrutores.
    /// </summary>
    /// <returns>
    /// Uma coleção de objetos do tipo <c>CourseResponse</c>, contendo detalhes dos cursos,
    /// incluindo título, descrição, preço e dados do instrutor responsável.
    /// </returns>
    public async Task<IEnumerable<CourseResponse>> GetAllCoursesByAllInstructors()
    {
        var courseDictionary = new Dictionary<Guid, CourseResponse>();  

        await _contextDapper.QueryAsync<CourseResponse, InstructorResponse, CourseResponse>(
            @"select c.id as Id,
                         c.title as Title,
                         c.description as Description,
                         c.price as Price,
                         u.id as Id,
                         u.name as Name,
                         u.email as Email
                from auth.users u 
                inner join catalog.courses c 
  	                on u.id = c.instructor_id",
            (course, instructor) =>
            {
                if (!courseDictionary.TryGetValue(course.Id, out var existingCourse))
                {
                    existingCourse = course;
                    courseDictionary[course.Id] = existingCourse;
                }
                return existingCourse;
            },
            splitOn: "Id");
        return courseDictionary.Values;
    }


    /// <summary>
    /// Recupera todos os cursos com seus módulos e aulas associadas.
    /// Os dados são extraídos das tabelas de cursos, módulos e aulas.
    /// </summary>
    /// <returns>
    /// Uma coleção de objetos do tipo <c>CourseResponse</c>, incluindo
    /// detalhes sobre os cursos, seus módulos e aulas associadas.
    /// </returns>
    public async Task<IEnumerable<CourseResponse>> GetAllWithModulesAndLessons()
    {
        var courseDictionary = new Dictionary<Guid, CourseResponse>();
        var moduleDictionary = new Dictionary<int, ModuleResponse>();

        await _contextDapper.QueryAsync<CourseResponse, ModuleResponse, LessonResponse, CourseResponse>(
            @"select c.id as Id,
                 c.title as Title,
                 c.description as Description,
                 c.price as Price,
                 m.id as Id,
                 m.title as Title,
                 m.order_number as OrderNumber,
                 l.id as Id,
                 l.title as Title,
                 l.order_number as OrderNumber
            from catalog.courses c 
            inner join catalog.modules m 
                on c.id = m.course_id 
            inner join catalog.lessons l 
                on l.module_id = m.id
            order by m.order_number, l.order_number",
            (course, module, lesson) =>
            {
                if (!courseDictionary.TryGetValue(course.Id, out var existingCourse))
                {
                    existingCourse = course;
                    courseDictionary[course.Id] = existingCourse;
                }

                if (!moduleDictionary.TryGetValue(module.Id, out var existingModule))
                {
                    existingModule = module;
                    moduleDictionary[module.Id] = existingModule;
                    existingCourse.Modules.Add(existingModule);
                }

                existingModule.Lessons.Add(lesson);
                return existingCourse;
            },
            splitOn: "Id,Id");
        return courseDictionary.Values;
    }


    /// <summary>
    /// Recupera detalhes de um curso específico, incluindo seus módulos e lições,
    /// com base no identificador único do curso fornecido.
    /// Os dados incluem informações detalhadas sobre o curso, seus módulos,
    /// suas lições e a ordem associada de cada módulo e lição.
    /// </summary>
    /// <param name="id">
    ///     O identificador único (<c>Guid</c>) do curso para o qual se deseja obter detalhes.
    /// </param>
    /// <returns>
    /// Um objeto do tipo <c>CourseResponse</c> contendo os detalhes do curso, incluindo
    /// informações sobre módulos e lições associados.
    /// </returns>
    public async Task<CourseResponse> GetWithModulesAndLessons(Guid id)
    {
        var courseDictionary = new Dictionary<Guid, CourseResponse>();
        var moduleDictionary = new Dictionary<int, ModuleResponse>();

        await _contextDapper.QueryAsync<CourseResponse, ModuleResponse, LessonResponse, CourseResponse>(
            @"select c.id as Id,
                 c.title as Title,
                 c.description as Description,
                 c.price as Price,
                 m.id as Id,
                 m.title as Title,
                 m.order_number as OrderNumber,
                 l.id as Id,
                 l.title as Title,
                 l.order_number as OrderNumber
            from catalog.courses c 
            inner join catalog.modules m 
                on c.id = m.course_id 
            inner join catalog.lessons l 
                on l.module_id = m.id
            where c.id = @Id
            order by m.order_number, l.order_number",
            (course, module, lesson) =>
            {
                if (!courseDictionary.TryGetValue(course.Id, out var existingCourse))
                {
                    existingCourse = course;
                    courseDictionary[course.Id] = existingCourse;
                }

                if (!moduleDictionary.TryGetValue(module.Id, out var existingModule))
                {
                    existingModule = module;
                    moduleDictionary[module.Id] = existingModule;
                    existingCourse.Modules.Add(existingModule);
                }

                existingModule.Lessons.Add(lesson);
                return existingCourse;
            },
            splitOn: "Id,Id",
            param: new { Id = id });
        return courseDictionary.Values.FirstOrDefault()!;
    }

    /// <summary>
    /// Recupera todos os cursos associados a um instrutor específico com base no identificador fornecido.
    /// Os dados incluem informações detalhadas sobre os cursos, como título, descrição, preço,
    /// bem como os dados do instrutor responsável.
    /// </summary>
    /// <param name="instructorId">
    ///     Identificador único do instrutor para o qual os cursos devem ser recuperados.
    /// </param>
    /// <returns>
    /// Um objeto do tipo <c>CourseResponse</c> contendo os detalhes do curso associado ao instrutor.
    /// </returns>
    public async Task<CourseResponse> GetByInstructorId(Guid instructorId)
    {
        var courseDictionary = new Dictionary<Guid, CourseResponse>();

        await _contextDapper.QueryAsync<CourseResponse, InstructorResponse, CourseResponse>(
            @"select c.id as Id,
                         c.title as Title,
                         c.description as Description,
                         c.price as Price,
                         u.id as Id,
                         u.name as Name,
                         u.email as Email
                from auth.users u 
                inner join catalog.courses c 
  	                on u.id = c.instructor_id
  	                where u.id = @InstructorId",
            (course, instructor) =>
            {
                if (!courseDictionary.TryGetValue(course.Id, out var existingCourse))
                {
                    existingCourse = course;
                    courseDictionary[course.Id] = existingCourse;
                }

                return existingCourse;
            },
            splitOn: "Id",
            param: new { InstructorId = instructorId });
        return courseDictionary.Values.FirstOrDefault()!;
    }

    public async Task<Course> GetById(Guid id) => (await _contextDapper.QueryFirstOrDefaultAsync<Course>(
                                                           @"select id as Id,
	                                                              title as Title,
	                                                        description as Description,
	                                                              price as Price,
	                                                      instructor_id as InstructorId
                                                          from catalog.courses 
                                                          where id = @Id", new { Id = id }))!;

    public async Task Create(Course course) => await _contextDapper.ExecuteAsync(
                                           @"insert into catalog.courses(title, description, price, instructor_id)
	                                                    values(@Title,@Description,@Price, @InstructorId)",
                                                new
                                                   {
                                                       course.Title,
                                                       course.Description,
                                                       course.Price,
                                                       course.InstructorId
                                                   }, _unitOfWork.Transaction);

    public async Task<bool> Update(Course course)
    {
        var rowsAffected = await _contextDapper.ExecuteAsync(
                                @"update catalog.courses
                                    set title = @Title,
	                                description = @Description,
	                                price = @Price
                                    where id = @Id",
                            new
                                      {
                                      course.Title,
                                      course.Description,
                                      course.Price,
                                      course.Id
                                      }, _unitOfWork.Transaction);
        return rowsAffected > 0;

    }
    public async Task<bool> Delete(Guid id)
    {
            var rowsAffected = await _contextDapper.ExecuteAsync(
                              @"delete from catalog.courses
                                    where id = @Id", new { Id = id }, _unitOfWork.Transaction);
            return rowsAffected > 0;
    }
}