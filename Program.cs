using DOTNETPROJECT.EndPoints;
using DOTNETPROJECT.Repositiries;
using DOTNETPROJECT.Utilities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IUserData, UserData>();

builder.Services.AddScoped<IUserCompanyexp, UserCompanyExp>();

builder.Services.AddOutputCache();

builder.Services.AddAutoMapper(typeof(AddAutoMapperProfiles));


builder.Services.AddCors((options) =>
{
    options.AddPolicy(("MyPolicy"), policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseOutputCache();

// app.UseCors("MyPolicy");


// app.MapGet("/", () => "Hello World!");


app.MapGroup("/userEntity").EndPointsUserData();
app.MapGroup("/userExp").EndPointsExpUsers();



app.Run();

