using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		protected ApplicationDbContext(DbContextOptions contextOptions)
		: base(contextOptions)
		{
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Message>()
				.HasOne<AppUser>(a => a.Sender)
				.WithMany(d => d.Messages)
				.HasForeignKey(d => d.UserID);

			/*//AdminAccountUser to Account One to Many
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>()
                .HasOne<AdminAccountUser>(au => au.AdminAccountUser)
                .WithMany(a => a.AdminAccounts)
                .HasForeignKey(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);*/

			//AccountUser to Account One to Many
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Account>()
				.HasOne<AccountUser>(au => au.AccountUser)
				.WithMany(a => a.UserAccounts)
				.HasForeignKey(a => a.Id)
				.OnDelete(DeleteBehavior.Cascade);

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Prepayment>()
				.Property(p => p.Amount)
				.HasColumnType("decimal(18,4)");



			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Wallet>()
				.Property(a => a.Amount)
				.HasColumnType("decimal(18,4)");

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<GroupMembers>()
			   .HasKey(au => new { au.AccountId, au.GroupId });

			//Account and group Many to Many
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<GroupMembers>()
					.HasOne(gm => gm.Group)
					.WithMany(Group => Group.GroupMembers)
					.HasForeignKey(gm => gm.GroupId)
					.OnDelete(DeleteBehavior.Cascade);

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<GroupMembers>()
					.HasOne(gm => gm.Account)
					.WithMany(Account => Account.GroupMembers)
					.HasForeignKey(gm => gm.AccountId)
					.OnDelete(DeleteBehavior.Cascade);

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<MemberInvoice>()
			   .HasKey(au => new { au.AccountId, au.InvoceId });

			//Account and Invoice Many to Many
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<MemberInvoice>()
					.HasOne(mi => mi.Invoice)
					.WithMany(Group => Group.MemberInvoices)
					.HasForeignKey(mi => mi.InvoceId)
					.OnDelete(DeleteBehavior.Cascade);

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<MemberInvoice>()
					.HasOne(mi => mi.Account)
					.WithMany(Account => Account.MemberInvoices)
					.HasForeignKey(mi => mi.AccountId)
					.OnDelete(DeleteBehavior.Cascade);


			//Invoice to Deposit One to Many
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Account>()
				.HasOne<AccountUser>(au => au.AccountUser)
				.WithMany(au => au.UserAccounts)
				.HasForeignKey(a => a.Id)
				.OnDelete(DeleteBehavior.Cascade);


			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<PenaltyFee>()
				.Property(pa => pa.PenaltyAmount)
				.HasColumnType("decimal(18,4)");

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Invoice>()
				.Property(ta => ta.TotalAmount)
				.HasColumnType("decimal(18,4)");

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Deposit>()
				.Property(d => d.DepositAmount)
				.HasColumnType("decimal(18,4)");

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<AccountProfile>()
				.Property(tp => tp.TotalAmoutDeposited)
				.HasColumnType("decimal(18,4)");

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<AccountProfile>()
				.Property(tp => tp.TotalPenaltyFee)
				.HasColumnType("decimal(18,4)");



			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<InvoiceDetails>()
				.Property(p => p.CreditAmount)
				.HasColumnType("decimal(18,4)");

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<PenaltyFee>()
				.Property(p => p.PenaltyAmount)
				.HasColumnType("decimal(18,4)");

		}

		public virtual DbSet<AccountProfile> AccountProfiles { get; set; }
		public virtual DbSet<AccountUser>? AccountUsers { get; set; }
		/*public virtual DbSet<AccountUserPersonal>? AccountUserPersonals { get; set; }*/
		public virtual DbSet<AdminAccountUser>? AdminAccountUsers { get; set; }
		public virtual DbSet<Group>? Groups { get; set; }
		public virtual DbSet<GroupMembers>? GroupMembers { get; set; }
		public virtual DbSet<Account>? Accounts { get; set; }
		public virtual DbSet<Invoice>? Invoices { get; set; }
		public virtual DbSet<MemberInvoice>? MemberInvoices { get; set; }
		public virtual DbSet<InvoiceDetails>? InvoiceDetails { get; set; }
		public virtual DbSet<PenaltyFee>? PenaltyFees { get; set; }
		public virtual DbSet<Deposit>? Deposits { get; set; }
		public virtual DbSet<Prepayment>? Prepayments { get; set; }
		public virtual DbSet<PaymentStatus>? PaymentStatuses { get; set; }
		public virtual DbSet<PaymentLog>? PaymentLog { get; set; }
		public virtual DbSet<PaymentMethod>? PaymentMethods { get; set; }
		public virtual DbSet<BankDetails>? BankDetails { get; set; }
		public virtual DbSet<Wallet>? Wallets { get; set; }
		public virtual DbSet<ApplicationUser>? ApplicationUser { get; set; }
		public virtual DbSet<Message>? Messages { get; set; }

	}


}
