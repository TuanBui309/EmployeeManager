using Entity.Data_Access;
using Entity.Models;
using Entity.Repository.Repositories;
using Entity.Repository.Respositories;
using Entity.Respository.Respositories;
using Entity.Services;
using Entity.Services.Interface;
using Entity.Services.Validation;
using Entity.Services.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<EntityDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});


builder.Services.AddControllers().AddNewtonsoftJson(options=>
{
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}

);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Repository
builder.Services.AddScoped<ICityRepository,CityRepository>();
builder.Services.AddScoped<IDistrictRespository, DistrictRepository>();
builder.Services.AddScoped<IWardRepository, WardRespository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<INationRepository, NationRepositoty>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDegreeRepository, DegreeRepository>();


//Service
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<IWardService, WardService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<INationService, NationService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDegreeService,DegreeService>();
//validation
builder.Services.AddScoped<IValidator<CityViewModel>, CityValidator>();
builder.Services.AddScoped<IValidator<DegreeViewModel>, DegreeValidetor>();
builder.Services.AddScoped<IValidator<EmployeeViewModel>, EmployeeValidator>();
builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy",
	builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
