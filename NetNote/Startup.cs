using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using guoguo.Domain.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using guoguo.Domain.ApplicationService;
using AutoMapper;
using NetNote.Middleware;
using Microsoft.AspNetCore.Identity;
using System.IO;

namespace NetNote
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
           

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddIdentity<NoteUser, IdentityRole>().AddEntityFrameworkStores<NoteDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper();

            services.AddDbContext<NoteDbContext>(options => options.UseSqlite("Filename=NoteTest.db"));


            //services.AddDbContext<NoteDbContext>(
            //    options=>options.UseSqlServer("Server=10.10.31.186;DataBase=NetNoteTest;Uid=sa;pwd=Admin2012;",
            //    dd=>dd.MigrationsAssembly(typeof(NoteDbContext).Assembly.FullName))
            //    );           

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddSingleton<NoteDbContext>();
            services.AddTransient<INoteRepository, NoteRepository>()
                .AddTransient<INoteTypeRepository,NoteTypeRepository>()
                .AddTransient<IBasicUserRepository,BasicUserRepository>();



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "NetNoteTestBySwagger",Version="v1" });
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, "NetNoteSwagger.xml");
                //c.IncludeXmlComments(xmlPath, true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //app.UseAuthentication();

            //app.UseBasicMiddleware();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            InitData(app.ApplicationServices);

            //app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=NoteAPI}/{action=Index}/{id?}");
            //});
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=AAA}/{action=Index}");
            });

            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "test V1");
                c.RoutePrefix = "";
            });
        }

        private void InitData(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<NoteDbContext>();
                db.Database.EnsureCreated();
                if(db.NoteTypes.Count()<=0)
                {
                    var noteTypeList = new List<NoteType>()
                    {
                        new NoteType{ Name="日常记录"},
                        new NoteType{ Name="代码博客"},
                        new NoteType{ Name="消费账单"},
                        new NoteType{ Name="网站收藏"}
                    };
                    db.NoteTypes.AddRange(noteTypeList);
                    db.SaveChanges();
                }


            }


        }

    }
}
