using Microsoft.EntityFrameworkCore;
using NotesAppWebAPI;
using NotesAppWebAPI.Data;
using NotesAppWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<NoteAppDbContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NotesAppDb")));

builder.Services.AddScoped<INoteRepository, NoteRepository>();

builder.Services.AddScoped<ITagRepository, TagRepository>();

builder.Services.AddScoped<INoteService, NoteService>();

builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
