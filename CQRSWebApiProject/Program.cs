using MediatR;
using AutoMapper;
using CQRSWebApiProject.Business.MapProfiles;
using CQRSWebApiProject.DAL.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using CQRSWebApiProject.Business.Validators.General;
using Kanbersky.Customer.Business.Extensions;
using CQRSWebApiProject.DAL.Concrete.EntityFramework.GenericRepository;
using Common.Messaging;
using System;
using CQRSWebApiProject.DAL.Concrete.Redis.Concrete;
using CQRSWebApiProject.DAL.Concrete.Redis.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddMvc(options =>
{
    options.Filters.Add(new ResponseValidator());
});

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(opt =>
    {
        opt.SuppressModelStateInvalidFilter = true;
    })
    .AddFluentValidation();

//builder.Services.AddDbContext<WriteDbContext>(options =>
//    options.UseInMemoryDatabase("InMem"));

//builder.Services.AddDbContext<ReadDbContext>(options =>
//        options.UseInMemoryDatabase("InMem"));

builder.Services.AddDbContext<ReadDbContext>(opt =>
      opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));

builder.Services.AddDbContext<WriteDbContext>(opt =>
      opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));


//Aþaðýdaki 2 þekilde de ekleme yapýlabilir. 
builder.Services.AddSingleton(
    MessageQueueFactory.CreateProvider());// 
//builder.Services.AddSingleton<IMessageQueueProvider>(sp =>
//    MessageQueueProviderFactory.CreateProvider(sp));// 
//builder.Services.AddSingleton<ICacheService, CacheService>();

#region service aboneliði yaklaþýmý
// clean code yaklaþýmý aþaðýdaki yapýyý Extensions klasörü altýna aldým. bu sayede sadace aþaðýdaki iki satýr kod ile baya iþlem halletmiþ olduk...
//Fluentvalidation kütüphanesi ile gelen istekleri kolay ve kod kalabalýðý olmadan validation yapmamýza yarayan kütüphanemiz var
//apimizi bu servislere de abone ediyoruz ve daha fazla servis iþini de orada halledebiliyoruz.
//Core ve Customer servislerindeki startup.cs içerisindeki karýþýklýðý bu yapý ile parçalayýp daha temiz bir hale getirebiliriz....
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//builder.Services.AddSingleton<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();
//builder.Services.AddSingleton<IValidator<UpdateCustomerRequest>, UpdateCustomerRequestValidator>();
builder.Services.RegisterHandlers();
builder.Services.RegisterValidators();
builder.Services.RedisConfiguration();
#endregion


var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMappingProfiels());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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

app.Run();
