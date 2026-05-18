using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Extensions;
using WarehouseManagementSystem.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger()
                .AddFluentValidation()
                .AddWarehouseDbContext(builder.Configuration)
                .AddIdentityAndDb(builder.Configuration)
                .AddJwtAuthenticationAndAuthorization(builder.Configuration)
                .AddCorsPolicy()
                .AddAutoMapperAndOtherServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WarehouseManagementDBContext>();
    db.Database.Migrate();

    await app.EnsureRolesSeededAsync();
}

app.UseWarehouseManagementPipeline();


app.Run();
