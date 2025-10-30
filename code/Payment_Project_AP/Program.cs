using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Payment_Project_AP.Data;
using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Repositories;
using Payment_Project_AP.Repositories.Interface;
using Payment_Project_AP.Service;
using Payment_Project_AP.Service.Interface;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

namespace Payment_Project_AP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Adding DBContext
            builder.Services.AddDbContext<CorporateBankingDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("myconn"));
            });

            // Bind Cloudinary settings from appsettings.json
            //builder.Services.Configure<CloudinarySettingsDTO>(
            //    builder.Configuration.GetSection("CloudinarySettings"));

            //Adding Repositories
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IBankUserRepository, BankUserRepository>();
            builder.Services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            //builder.Services.AddScoped<ICloudinaryRepository, CloudinaryRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<ISalaryDisbursementRepository, SalaryDisbursementRepository>();
            builder.Services.AddScoped<ISalaryPaymentRepository, SalaryPaymentRepository>();
            builder.Services.AddScoped<IEmailRepository, EmailRepository>();
            builder.Services.AddScoped<IBankRepository, BankRepository>();
            builder.Services.AddScoped<ISupportRepository, SupportRepository>();


            //Adding Services
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IBankUserService, BankUserService>();
            builder.Services.AddScoped<IBeneficiaryService, BeneficiaryService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IUserService, UserService>();
          //  builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ISalaryDisbursementService, SalaryDisbursementService>();
            builder.Services.AddScoped<ISalaryPaymentService, SalaryPaymentService>();
            builder.Services.AddScoped<IBankService, BankService>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ISupportService, SupportService>();

            //builder.Services.Configure<RecaptchaSettings>(
            //    builder.Configuration.GetSection("RecaptchaSettings")
            //);
            builder.Services.AddHttpClient();

            //Adding AutoMapper
            builder.Services.AddAutoMapper(options =>
            {
                options.CreateMap<UserRegisterDTO, User>();
                options.CreateMap<UserResponseDTO, User>();
                options.CreateMap<User, UserResponseDTO>();
                options.CreateMap<Account, AccountDTO>();
                options.CreateMap<AccountDTO, Account>();
                options.CreateMap<AccountRegisterDTO, Account>();
                options.CreateMap<ClientRegisterDTO, Client>();
                options.CreateMap<Client, ClientResponseDTO>();
                options.CreateMap<ClientResponseDTO, Client>();
                options.CreateMap<BankUserRegisterDTO, BankUser>();
                options.CreateMap<BankUserResponseDTO, BankUser>();
                options.CreateMap<BankUser, BankUserResponseDTO>();
                options.CreateMap<TransactionRegisterDTO, Transaction>();
                options.CreateMap<DocumentDTO, Document>();
                options.CreateMap<EmployeeDTO, Employee>();
                options.CreateMap<BeneficiaryDTO, Beneficiary>();
                options.CreateMap<EmployeeDTO, Employee>();
                options.CreateMap<CreateSalaryDisbursmentDTO, SalaryDisbursement>();
                options.CreateMap<SalaryDisbursement, SalaryResponseDTO>();
                options.CreateMap<BankDTO, Bank>();
            });

            //Logger Configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, shared: true)
                .CreateLogger();

            //builder.Host.UseSerilog(); // here we changed the default logger to serilog

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            //builder.Services.AddAuthentication(opt =>
            //{
            //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = "https://localhost:7030",
            //        ValidAudience = "https://localhost:7030",
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetConnectionString("secretkey")))

            //    };
            //});

            builder.Services.AddSwaggerGen(options =>
            {
                //options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                //{
                //    Version = "v1",
                //    Description = "Banking Payments Application"
                //});

                //var securityScheme = new OpenApiSecurityScheme
                //{
                //    Name = "Authorization",
                //    Description = "Enter Jwt Web token Only",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.Http,
                //    Scheme = "bearer",
                //    BearerFormat = "JWT",
                //    Reference = new OpenApiReference
                //    {
                //        Id = JwtBearerDefaults.AuthenticationScheme,
                //        Type = ReferenceType.SecurityScheme
                //    }
                //};
                //options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                //options.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {securityScheme,new string[] {}}
                //});
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    builder => builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking Payments Application");
                    options.EnablePersistAuthorization();
                });
            }

            app.UseHttpsRedirection();
            //app.UseExceptionHandler();
           // app.UseMiddleware<GlobalExceptionMiddleware>();


            app.UseCors("AllowFrontend");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
