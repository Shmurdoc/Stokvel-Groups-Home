using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Hubs;
using Stokvel_Groups_Home.Interface.Infrastructure;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IService;
using Stokvel_Groups_Home.Interface.IServices.AccountProfileServices;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;
using Stokvel_Groups_Home.Interface.IServices.IAccountUserServices;
using Stokvel_Groups_Home.Interface.IServices.IDepositServices;
using Stokvel_Groups_Home.Interface.IServices.IGroupMembersServices;
using Stokvel_Groups_Home.Interface.IServices.IGroupServices;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;
using Stokvel_Groups_Home.Interface.IServices.IInvoiceServices;
using Stokvel_Groups_Home.Interface.IServices.IPrepaymentServices;
using Stokvel_Groups_Home.Interface.IServices.IWalletServices;
using Stokvel_Groups_Home.Interface.IServices.IWithdrawServices;
using Stokvel_Groups_Home.Interface.IServices.PrepaymentServices;
using Stokvel_Groups_Home.Repositories;
using Stokvel_Groups_Home.Services;
using Stokvel_Groups_Home.Services.AccountProfileServices;
using Stokvel_Groups_Home.Services.AccountServices;
using Stokvel_Groups_Home.Services.AccountServices.AccountRequestService;
using Stokvel_Groups_Home.Services.AccountUserServices;
using Stokvel_Groups_Home.Services.DepositServices;
using Stokvel_Groups_Home.Services.GroupMembersServices;
using Stokvel_Groups_Home.Services.GroupServices;
using Stokvel_Groups_Home.Services.HomeService;
using Stokvel_Groups_Home.Services.InvoiceServices;
using Stokvel_Groups_Home.Services.PrepaymentServices;
using Stokvel_Groups_Home.Services.PrepaymentsServices;
using Stokvel_Groups_Home.Services.UnitOfWork;
using Stokvel_Groups_Home.Services.WalletServices;
using Stokvel_Groups_Home.Services.WithdrawServices;
namespace Stokvel_Groups_Home.Controllers
{

	public class Program
	{

		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();



			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnetion")));

			builder.Services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
						.AddEntityFrameworkStores<ApplicationDbContext>();


			builder.Services.AddSignalR();

			builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

			builder.Services.AddTransient<IAccountProfileRepository, AccountProfileRepository>();
			builder.Services.AddTransient<IAccountUserRepository, AccountUserRepository>();
			builder.Services.AddTransient<IInvoicesRepository, InvoicesRepository>();
			builder.Services.AddTransient<IDepositRepository, DepositRepository>();
			builder.Services.AddTransient<IPaymentStatusRepository, PaymentStatusRepository>();
			builder.Services.AddTransient<IPaymentMethodsRepository, PaymentMethodsRepository>();
			builder.Services.AddTransient<IPrepaymentsRepository, PrepaymentsRepository>();
			builder.Services.AddTransient<IAccountsRepository, AccountsRepository>();
			builder.Services.AddTransient<IGroupsRepository, GroupsRepository>();
			builder.Services.AddTransient<IGroupMembersRepository, GroupMembersRepository>();
			builder.Services.AddTransient<IMemberInvoiceRepository, MemberInvoiceRepository>();
			builder.Services.AddTransient<IWithdrawRepository, WithdrawRepository>();
			builder.Services.AddTransient<IPenaltyFeeRepository, PenaltyFeeRepository>();

			builder.Services.AddTransient<IHomeRequestService, HomeRequestService>();
			builder.Services.AddTransient<IAccountUserCRUDService, AccountUserCRUDService>();
			builder.Services.AddTransient<IAccountsCRUDService, AccountsCRUDService>();
			builder.Services.AddTransient<IGroupsCRUDService, GroupsCRUDService>();
			builder.Services.AddTransient<IGroupMembersCRUDService, GroupMembersCRUDService>();
			builder.Services.AddTransient<IAccountProfileCRUDService, AccountProfileCRUDService>();
			builder.Services.AddTransient<IAccountProfileServices, AccountProfileServices>();
			builder.Services.AddTransient<IGroupServices, GroupServices>();
			builder.Services.AddTransient<IPrepaymentServices, PrepaymentServices>();
			builder.Services.AddTransient<IWithdrawServices, WithdrawServices>();
			builder.Services.AddTransient<IWithdrawRequestService, WithdrawRequestService>();
			builder.Services.AddTransient<IGroupRequestServices, GroupRequestServices>();
			builder.Services.AddTransient<IAccountUserRequestServices, AccountUserRequestServices>();
			builder.Services.AddTransient<IAccountUserServices, AccountUserServices>();
			builder.Services.AddTransient<IGroupMembersRequestServices, GroupMembersRequestServices>();
			builder.Services.AddTransient<IGroupMembersServices, GroupMembersServices>();
			builder.Services.AddTransient<IInvoiceRequestServices, InvoiceRequestServices>();
			builder.Services.AddTransient<IInvoicesCRUDService, InvoicesCRUDService>();
			builder.Services.AddTransient<IInvoiceServices, InvoiceServices>();


			builder.Services.AddTransient<IHomeServices, HomeServices>();
			builder.Services.AddTransient<IAccountServices, AccountServices>();
			builder.Services.AddTransient<IDepositServices, DepositServices>();
			builder.Services.AddTransient<IDepositRequestService, DepositRequestService>();
			builder.Services.AddTransient<IAccountProfileRequestServices, AccountProfileRequestServices>();
			builder.Services.AddTransient<IWithdrawCRUDService, WithdrawCRUDService>();
			builder.Services.AddTransient<IAccountRequestService, AccountRequestServices>();
			builder.Services.AddTransient<IDepositCRUDService, DepositCRUDService>();
			builder.Services.AddTransient<IWalletRepository, WalletRepository>();
			builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
			builder.Services.AddTransient<IPrepaymentsCRUDService, PrepaymentsCRUDService>();

			builder.Services.AddTransient<IWalletCRUDService, WalletCRUDService>();
			builder.Services.AddTransient<IWalletRequestServices, WalletRequestServices>();
			builder.Services.AddTransient<IWalletServices, WalletServices>();

			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromSeconds(10);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});
			builder.Services.AddMvc();




			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();

				app.UseMvcWithDefaultRoute();

			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}"
				);

			app.MapHub<ChatHub>("ChatHub");
	

			app.MapRazorPages();

			using (var scope = app.Services.CreateScope())
			{
				var roleManager =
						scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				var roles = new[] { "Admin", "Manager", "SuperUser", "Member" };
				foreach (var role in roles)
				{
					if (!await roleManager.RoleExistsAsync(role))
						await roleManager.CreateAsync(new IdentityRole(role));
				}
			}


			

			app.Run();
		}
	}
}