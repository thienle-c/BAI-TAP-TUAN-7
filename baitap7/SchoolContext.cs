using baitap7.model;
using System.Data.Entity;

namespace SchoolDB.DAL
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
            : base("name=Student")
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
