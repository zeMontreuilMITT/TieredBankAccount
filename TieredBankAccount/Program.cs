using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TieredBankAccount.Data;
using TieredBankAccount.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TieredBankAccountContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TieredBankAccountContext") ?? throw new InvalidOperationException("Connection string 'TieredBankAccountContext' not found.")));

builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IRepository<Address>, AddressRepository>();
builder.Services.AddScoped<IRepository<CustomerAddress>, CustomerAddressRepository>();
builder.Services.AddScoped<IRepository<BankAccount>, BankAccountRepository>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
