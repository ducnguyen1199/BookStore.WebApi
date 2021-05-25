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
using BookStore.Core.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookStore.Application.IService;
using BookStore.Application.Service;

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
			services.AddScoped<IUserReponsitory, UserReponsitory>();
			services.AddScoped<IOrderReponsitory, OrderResponsitory>();

			services.AddScoped<IBookService, BookService>();
			services.AddScoped<IAuthorService, AuthorService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IOrderService, OrderService>();

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("Default")));
			services.AddControllers();

			services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

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

			services.Configure<IdentityOptions>(options =>
			{
				// Default Password settings.
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 0;
				options.Password.RequiredUniqueChars = 0;
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore", Version = "v1" });
			});
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				 options.SaveToken = true;
				 options.RequireHttpsMetadata = false;
				 options.TokenValidationParameters = new TokenValidationParameters()
				 {
					 ValidateIssuer = true,
					 ValidateAudience = true,
					 ValidateIssuerSigningKey = true,
					 ValidIssuer = Configuration["JWT:Issuer"],
					 ValidAudience = Configuration["JWT:Audience"],
					 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
				 };
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
			app.UseStaticFiles();

			app.UseRouting();
			app.UseCors("MyOrigins");


			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
