using WhatsapNet.Api.Service.WhatsappCloud.SendMessage;
using WhatsapNet.Api.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IWhatsappCloudSendMessage, WhatsappCloudSendMessage>();
builder.Services.AddSingleton<IUtil, Util>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
