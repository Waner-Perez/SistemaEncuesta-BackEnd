using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApiForm.Capa_de_Servicio;
using WebApiForm.Middleware;
using WebApiForm.Repository;
using WebApiForm.Interfaces;
using WebApiForm.Capa_de_Servicio.Exportacion;

var builder = WebApplication.CreateBuilder(args);

// Configurar Kestrel para permitir conexiones externas
//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(7190); // Permite conexiones desde cualquier IP en el puerto 7190
//});

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(7190, listenOptions =>
//    {
//        listenOptions.UseHttps(); // Habilita HTTPS en Kestrel
//    });
//});


// Add services to the container.
builder.Services.AddDbContext<FormEncuestaDbContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

// Agrega el servicio de controladores
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

//Configuracion del Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Definir el emisor del token
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Utiliza la clave secreta obtenida del entorno
    };
});

// Registrar el servicio en vace a stored procedure
builder.Services.AddScoped<FormularioRepository>();
//--------------------------------------------------------
builder.Services.AddScoped<EstacionPorLineaService>();
builder.Services.AddScoped<PreguntaCompletaService>();
builder.Services.AddScoped<RespuestaService>();
builder.Services.AddScoped<FormularioServices>();
builder.Services.AddScoped<ExcelExportService>();
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
builder.Services.AddScoped<PasswordRecoveryService>();
//--------------------------------------------------------

// Registrar el servicio de env�o de correo electr�nico
//builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();
//builder.Services.AddTransient<PasswordRecoveryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//para que se pueda visualizar en el hosting
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Usa CORS
app.UseCors("AllowAll");

// Usa autenticaci�n
app.UseAuthentication();

// Usa el middleware de lista negra de tokens
app.UseTokenBlacklist(); // Aseg�rate de que este llamado est� despu�s de la autenticaci�n

// Usa la autorizaci�n
app.UseAuthorization();

app.MapControllers();

app.Run();
