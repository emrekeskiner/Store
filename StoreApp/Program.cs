using StoreApp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
//Tek Controller kullanacağımızı ifade ediyoruz.
builder.Services.AddControllers()
        .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
//Controller ile görünüm nesnelerinin beraber kullanılacağını ifade ediyoruz.
builder.Services.AddControllersWithViews();
//Razorpage leri kullanacağımızı ifade ediyoruz.
builder.Services.AddRazorPages();

//ServiceExtentions
builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.ConfigureIdentity();

//ServiceExtentions
builder.Services.ConfigureSession();

builder.Services.ConfigureRepositoryRegistration();

builder.Services.ConfigureServiceRegistration();
//Routing konfigürasyonu urlleri küçük yapma
builder.Services.ConfigureRouting();

builder.Services.ConfigureApplicationCookie();
//Automapper servis kaydı
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();


app.UseStaticFiles();
//sesionları kullanabilmek için
app.UseSession();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        areaName: "Admin",
        name: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");
    
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    //Razorpage endpoints
    endpoints.MapRazorPages();

    //Api için endpoints yazıyoruz.
    endpoints.MapControllers();
});
// bir migrations varsa otomatik database update yapacak
app.ConfigureAndCheckMigration();
//Localization metodumu burada çağırıyorum. tr-TR
app.ConfigureLocalization();
app.ConfigureDefaultAdminUser();
app.Run();
