using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Restaurant.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(services => services.EnableEndpointRouting = false);
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddDbContext<AppDbContext>(DbContext =>
{
    DbContext.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});


builder.Services.AddScoped<IRepository<MasterCategoryMenu>, MasterCategoryMenuRepository>();
builder.Services.AddScoped<IRepository<MasterContactUsInformation>, MasterContactUsInformationRepository>();
builder.Services.AddScoped<IRepository<MasterItemMenu>, MasterItemMenuRepository>();
builder.Services.AddScoped<IRepository<MasterMenu>, MasterMenuRepository>();
builder.Services.AddScoped<IRepository<MasterOffer>, MasterOfferRepository>();
builder.Services.AddScoped<IRepository<MasterPartner>, MasterPartnerRepository>();
builder.Services.AddScoped<IRepository<MasterService>, MasterServiceRepository>();
builder.Services.AddScoped<IRepository<MasterSlider>, MasterSliderRepository>();
builder.Services.AddScoped<IRepository<MasterSocialMedium>, MasterSocialMediumRepository>();
builder.Services.AddScoped<IRepository<MasterWorkingHour>, MasterWorkingHourRepository>();
builder.Services.AddScoped<IRepository<SystemSetting>, SystemSettingRepository>();
builder.Services.AddScoped<IRepository<TransactionBookTable>, TransactionBookTableRepository>();
builder.Services.AddScoped<IRepository<TransactionContactU>, TransactionContactURepository>();
builder.Services.AddScoped<IRepository<TransactionNewsletter>, TransactionNewsletterRepository>();
builder.Services.AddScoped<IRepository<WhatPeopleSay>, WhatPeopleSayRepository>();

builder.Services.Configure<IdentityOptions>(x => {
    x.Password.RequireDigit = false;
    x.Password.RequiredLength = 3;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireLowercase = false;
    x.Password.RequireUppercase = false;
    //x.Password.RequiredUniqueChars = 0;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Admin/Account/Login";
});

var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

//app.MapGet("/", () => "Hello World!");

app.UseEndpoints(app =>
{
    //app.MapControllerRoute(
    //    name: "areas",
    //    pattern: "{area=exists}/{Controller=account}/{action=Login}/{Id?}"
    //    );

    //app.MapControllerRoute(
    //    name: "Default",
    //    pattern: "{controller=Home}/{action=Index}/{Id?}"
    //    );

    app.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}"
                        );

    app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
            );
});

app.Run();
