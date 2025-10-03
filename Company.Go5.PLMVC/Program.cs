using Company.Go5.BLL;
using Company.Go5.BLL.Interfaces;
using Company.Go5.BLL.Repositories;
using Company.Go5.DAL.Data.Contexts;
using Company.Go5.PLMVC.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //builder.Services.AddScoped<EmployeeProfile>();
            builder.Services.AddAutoMapper(m=>m.AddProfile(new EmployeeProfile() )  );

           


            //dependency injection lifetime 

            //object per each operation or action or method   life time =operation 
            //builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();


            ////object per each request  life time=request time 

            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();



            ////object per each running application  life time=application time

            //builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();







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
