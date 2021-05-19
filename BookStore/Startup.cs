using BookStore.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using BookStore.Core.Repository;
using BookStore.Database.Reponsitory;
using BookStore.Core.ViewModel;

namespace BookStore
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<IBookReponsitory, BookReponsitory>();
			services.AddScoped<IAuthorReponsitory, AuthorReponsitory>();
			services.AddScoped<ICategoryReponsitory, CategoryReponsitory>();


			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("Default")));
			services.AddControllers();

			services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

			services.AddCors(options =>
			{
				options.AddPolicy(name: "MyOrigins",
					builder =>
					{
						builder.AllowAnyOrigin()
							.AllowAnyHeader()
							.AllowAnyMethod();
					});
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseCors("MyOrigins");
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
