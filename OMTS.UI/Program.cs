using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NToastNotify;
using OMTS.DAL.Data;
using OMTS.DAL.Repository;
using OMTS.DAL.Repository.Interfaces;

namespace OMTS.UI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			// Add services to the container.
			builder.Services.AddControllersWithViews()
				.AddNToastNotifyToastr(new ToastrOptions()
				{
					ProgressBar = false,
					PositionClass = ToastPositions.TopRight,
					ShowDuration = 5000
				}
);

			// Add services to the container.
			builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
			builder.Services.AddDbContext<OMTSDbContext>(
				opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			IFileProvider physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
			builder.Services.AddSingleton<IFileProvider>(physicalProvider);
			builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts => {

				opts.Password.RequireDigit = true;
				opts.Password.RequireLowercase = false;
				opts.Password.RequireNonAlphanumeric = false;
				opts.Password.RequireUppercase = false;
				opts.Password.RequiredLength = 6;
				opts.User.RequireUniqueEmail = true;

			}).AddEntityFrameworkStores<OMTSDbContext>();



			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			app.UseNToastNotify();
			app.MapControllerRoute(
			  name: "areas",
			  pattern: "{area:exists}/{controller=Account}/{action=LogIn}/{id?}"
			);
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Account}/{action=LogIn}/{id?}");

			app.Run();
		}
	}
}
