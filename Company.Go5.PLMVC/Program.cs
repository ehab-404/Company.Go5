using Company.Go5.BLL.Interfaces;
using Company.Go5.BLL.Repositories;
using Company.Go5.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Company.Go5.PLMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //ask clr to create object of DepartmentRepository
            //when assigning it reference of type of IDepartmentRepository

            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();

            builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));



            //allow DI for CompanyDbContext 
            builder.Services.AddDbContext<CompanyDbContext>(

                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                            

            );



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
