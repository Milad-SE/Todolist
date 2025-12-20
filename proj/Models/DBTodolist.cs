using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proj.Models;
using Microsoft.EntityFrameworkCore;
namespace proj.Models
{
public class DBTodolist : DbContext
{
    public DBTodolist(DbContextOptions<DBTodolist> options) : base(options) { }

    public DbSet<TaskItem> Tasks { get; set; }
}

}