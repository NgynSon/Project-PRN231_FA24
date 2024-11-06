namespace Diary_Client
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllersWithViews();

			// Add session support
			builder.Services.AddSession(options =>
			{
				options.Cookie.HttpOnly = true; // Cookie không th? truy c?p b?ng JavaScript
				options.Cookie.IsEssential = true; // ??m b?o r?ng cookie session luôn ???c g?i ?i
			});
			// Add services to the container.
			builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
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
            app.UseSession();
			app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}