using Microsoft.EntityFrameworkCore;
using notify.Data;
using notify.Hubs;
using notify.Interfaces;
using notify.Middlewares;
using notify.Repositories;
using notify.Services;
using notify.Subjects;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<ApiContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddSignalR();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IPreferenceService, PreferenceService>();
builder.Services.AddScoped<IPreferenceRepository, PreferenceRepository>();

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddSingleton<IPublisher, NotificationPublisher>();

builder.Services.AddScoped<ISubscriber, EmailNotificationSubscriber>();
builder.Services.AddScoped<ISubscriber, InAppNotificationSubscriber>();
builder.Services.AddScoped<ISubscriber, PushNotificationSubscriber>();
builder.Services.AddScoped<ISubscriber, SMSNotificationSubscriber>();

builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHubClient>("/notifications");

app.UseMiddleware<GlobalExceptionHandler>();

app.Run();
