using Dokaanah.DataAccessLayer.Data;
using Dokaanah.Models;
using Dokaanah.PresentationLayer.Helpers;
using Dokaanah.Repositories.RepoClasses;
using Dokaanah.Repositories.RepoInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore; 
using NLog.Web;
using System.Security.Claims;

namespace Dokaanah
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region App builder  Region
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Connection String Region
            
            try
            {

              builder.Services.AddDbContext<DokkanahDBContex>(options =>
               options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionForDok"))
              );
            
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
            }

            #region Comment for Add Identity Region

            //// Add Identity with the DbContext
            //builder.Services.AddIdentity<Customer, IdentityRole>()
            //    .AddEntityFrameworkStores<DokkanahDBContex>()
            //    .AddDefaultTokenProviders();

            #endregion


            #region Connection String Comment Region


            //builder.Services.AddDbContext<DokkanahDBContex>(options =>
            //{
            //    //options.UseMySQL("server=localhost;database=DokkanahDataBase;uid=root;pwd=new_password;");
            //    //options.UseNpgsql("Host=localhost;Database=Talabat1aHome;Username=postgres;Password=0kjf;");
            //    options.UseSqlServer
            //  ("Server=DESKTOP-NRGEJ6B\\SQLEXPRESS;Database=DokSql;Trusted_Connection=True;");

            //});

            //builder.Services.AddDbContext<DokkanahDBContex>(options =>
            //options.UseSqlServer
            //  ("Server=DESKTOP-NRGEJ6B\\SQLEXPRESS;Database=DokSql;Trusted_Connection=True;")
            //  );

            //options.UseMySQL("server=localhost;database=DokkanahDataBase;uid=root;pwd=new_password;") );

            #endregion

            #endregion

            #region Inject Classes and Interfaces Region

            builder.Services.AddScoped<ICartRepo, CartRepo>();
            builder.Services.AddScoped<IProductsRepo, ProductsRepo>();
            builder.Services.AddScoped<ICustomersRepo, CustomersRepo>();
            builder.Services.AddScoped<IOrdersRepo, OrdersRepo>();
            builder.Services.AddScoped<ICategoriesRepo, CategoriesRepo>();
            builder.Services.AddScoped<ICartProductRepo, CartProductRepository>();


            #endregion

            builder.Services.AddIdentity<Models.Customer, IdentityRole>(con =>
            {
                con.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<DokkanahDBContex>();
             
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });

            #endregion

            #region Application Region

            #region  log Changes  Region
             
            var logger = NLog.LogManager.GetCurrentClassLogger();
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            #region Add SignalR  Region

            //// أضف خدمات SignalR
            builder.Services.AddSignalR();
            #endregion
             
            #endregion
             
            var app = builder.Build();
            #region Logger Factory Region

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<DokkanahDBContex>();
             var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

            #endregion

            #region Migrate Seed Region

            try
            {
                await _dbcontext.Database.MigrateAsync();
                await StoreDokContextSeed.SeedAsync(_dbcontext);

 
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
                var loggerV = LoggerFactory.CreateLogger<Program>();
                loggerV.LogError(Ex, "error here");
            }

            #endregion

            #endregion

            #region Middlewares Before Run Region

            app.UseSession();

            #region Status and Last Active Middleware User Region

            app.UseMiddleware<UserActivityMiddleware>();

            #endregion
             
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/ErrorHandleForUser");

                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            #region Add map SignalR  Region
            app.MapHub<UserHub>("/userHub");
             
            #endregion
             
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
             
            #endregion
              
            app.Run();
  
        }

    }

}



#region  Comment Region







//builder.Services.AddSignalR(options =>
//{
//    options.AddFilter<CustomUserIdProvider>();
//});

#region  comment Region

//var logger = NLogBuilder.ConfigureNLog(Path.Combine(Directory.GetCurrentDirectory(), "nlog.config")).GetCurrentClassLogger();
// logger.Debug("Application started");

// Configure services...
//builder.Logging.ClearProviders();
//    builder.Host.UseNLog();

//logger = NLog.LogManager.GetCurrentClassLogger();
//logger.Info("Application started successfully.");

#endregion




//var logger = NLog.LogManager.GetCurrentClassLogger();
//builder.Logging.ClearProviders();
//builder.Host.UseNLog();


//builder.Services.AddAuthentication(); 


//builder.Services.AddSingleton<IUserConnectionManager, UserConnectionManager>();


//app.MapHub<UserHub>("/userHub"); // SignalR Hub
//// أضف المسار الخاص بـ SignalR
//app.MapHub<UserHub>("/userHub");



//app.UseEndpoints
//    (endPoint =>
//    {
//        endPoint.MapHub<UserHub>("/Hub");
//    });



//public class CustomUserIdProvider : IUserIdProvider
//{
//    public string GetUserId(HubConnectionContext connection)
//    {
//        return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//    }
//}

// <script>
//         var connection = new signalR.HubConnectionBuilder()
//             .withUrl("/Hub").build();

//         connection.start().catch(function (err) {
//             return console.error(err.toString());
//         }); 

//         // Listen to updates from the server
//         connection.on("ReceiveUserStatus", (userId, status) => {
//             // Find the user's row in the table
//             const userRow = document.querySelector(`[data-user-id="${userId}"]`);
//             if (userRow) {
//                 const statusCell = userRow.querySelector(".status-cell");
//                 if (status === "Online") {
//                     statusCell.innerHTML = '<span class="badge bg-success">Online</span>';
//                 } else {
//                     statusCell.innerHTML = '<span class="badge bg-danger">Offline</span>';
//                 }
//             }
//         });

// </script> *@

//<script>


//    var connection = new signalR.HubConnectionBuilder()
//        .withUrl("/Hub").build();


//    connection.start().catch(function (err) {
//        return console.error(err.toString());
//    });


//    document.getElementById("sendmessage").addEventListener("click",
//        function (event) {
//            var user = document.getElementById("DisplayName").value;
//            var message = document.getElementById("message").value;
//            connection.invoke("SendMessage", user, message).catch(function (err) {
//                return console.error(err.toString());
//            });
//            event.preventDefault();
//        });

//    connection.on("RecievedMessage",
//        function (user, message) {
//            var enCodeMsg = user + ": " + message;
//            var li = document.createElement("li");
//            li.textContent = enCodeMsg;
//            var user = document.getElementById("Discussion").appendChild(li);

//        });
//</script>



#endregion





//webApplicationBuilder.Services.AddControllers();


//builder.Services.AddControllersWithViews();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}"
//);


//app.UseRouting();
