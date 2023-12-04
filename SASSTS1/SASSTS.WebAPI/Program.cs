using Microsoft.Extensions.Options;
using SASSTS.Business.Injection;
using SASSTS.WebAPI;
using SASSTS.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddBusinessServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomException();
app.MapControllers();
app.Run();