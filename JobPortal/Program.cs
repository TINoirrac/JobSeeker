using JobPortal.Mails;
using JobPortal.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using JobPortal.Services;
using JobPortal.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Connection string 'DefaultConnectionString' not found.");

builder.Services.AddDbContext<JobPortalWebContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<UserProfile>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<JobPortalWebContext>(); ;

// Add services to the container.

//dang ky JobPortalWebContext, su dung ket noi den MS SQL Server
//string connection = builder.Configuration.GetConnectionString("DefaultConnectionString");
//builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<JobPortalWebContext>(options => options.UseSqlServer(connection));

//// Đăng ký các dịch vụ của Identity
//builder.Services.AddIdentity<UserProfile, IdentityRole>()
//    .AddEntityFrameworkStores<JobPortalWebContext>()
//    .AddDefaultTokenProviders();

//Tiến hành đăng ký dịch vụ SendMailService và hệ thống với tên giao diện IEmailSender
builder.Services.AddOptions();                                        // Kích hoạt Options
var mailsettings = builder.Configuration.GetSection("MailSettings");  // đọc config
builder.Services.Configure<MailSettings>(mailsettings);               // đăng ký để Inject
builder.Services.AddTransient<IEmailSender, SendMailService>();        // Đăng ký dịch vụ Mail

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        // Đọc thông tin Authentication:Google từ appsettings.json
        IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");

        // Thiết lập ClientID và ClientSecret để truy cập API google
        options.ClientId = googleAuthNSection["ClientId"];
        options.ClientSecret = googleAuthNSection["ClientSecret"];
        // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
        options.CallbackPath = "/dang-nhap-tu-google";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole(RoleName.Administrator);
    });
    options.AddPolicy("Seeker", policy =>
     {
         policy.RequireAuthenticatedUser();
         policy.RequireRole(RoleName.Seeker);
     });
    options.AddPolicy("Employer", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole(RoleName.Employer);
    });
});

builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = $"/Areas/Identity/Pages/Account/LoginSeeker.cshtml";
    option.LogoutPath = $"/Areas/Identity/Pages/Account/Logout.cshtml";
    option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
    

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();   // Phục hồi thông tin đăng nhập (xác thực)
app.UseAuthorization();   // Phục hồi thông tinn về quyền của User

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
