using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Entities;
using MyExams.Models;

namespace MyExams.Repositories
{
    public class TeachersRepository : GenericRepository<Teacher>
    {
        public List<CoursesOfTeacher> GetCoursesOfTeacher(int id)
        {
            var query = from c in base.db.Courses
                        where c.TeacherId == id
                        select new CoursesOfTeacher
                        {
                            Course = c.Name,
                            Duration = c.Duration,
                            Credits = c.Credits
                        };

            return query.ToList();
        }
    }
}