using Amazon.Extensions.NETCore.Setup;
using Microsoft.AspNetCore.Builder;
using WebAdvert.AdvertApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IAdvertStorageService, DynamoDBAdvertStorage>();

#if DEBUG
AWSOptions awsOptions = new AWSOptions
{
    Profile = "udemy"
};
builder.Services.AddDefaultAWSOptions(awsOptions);
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
