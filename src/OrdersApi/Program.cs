
#region Services
///////////////////////////////////////////////////////
// Application services/DI Container configures ///////
///////////////////////////////////////////////////////

using OrdersApi;
using OrdersApi.BusinessLayer.Extensions;
using OrdersApi.BusinessLayer.Orders;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddDatabaseInfrastructure(builder.Configuration);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services
    .AddHttpContextAccessor()
    .AddSingleton<HttpCancellationTokenAccessor>()
    .AddSingleton<IOrderService, OrderServiceResolver>();

#endregion

#region App
///////////////////////////////////////////////////////
// Application middlewares and entrypoint /////////////
///////////////////////////////////////////////////////

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Migrate();
app.Run();

#endregion
