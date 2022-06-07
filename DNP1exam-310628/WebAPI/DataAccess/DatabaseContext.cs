using Microsoft.EntityFrameworkCore;

namespace WebAPI.DataAccess; 

public class DatabaseContext :DbContext {



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

        // please use your own root path...
        optionsBuilder.UseSqlite(
            @"Data Source =C:\Users\nepal\OneDrive\Desktop\DNP-Exam-310628\DNP1exam-310628\WebAPI\database.db");
    }
}