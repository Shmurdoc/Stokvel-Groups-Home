using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models
{
	public class AccountUserPersonal : AccountUser
	{
		[Required]
		public int PhoneNumber { get; set; }

		public string? SecondAddress { get; set; }

		public string? SecondStreetAddress { get; set; }
		public string? SecondCity { get; set; }
		public string? SecondProvince { get; set; }
		public int SecondPostalCode { get; set; }

		[Required]
		public DateOnly DateOfBirth { get; set; }


		[Required]
		public string? EmployerFirstName { get; set; }
		[Required]
		public string? EmployerLastName { get; set; }
		[Required]
		public string? EmailAddress { get; set; }
		[Required]
		public string? EmployerAddress { get; set; }
		[Required]
		public string? EmployerStreetAddress { get; set; }
		[Required]
		public string? EmployerCity { get; set; }
		[Required]
		public string? EmployerProvince { get; set; }
		[Required]
		public string? EmployerPostalCode { get; set; }


		[Required]
		[DisplayName("Annual Income")]
		public int AnnualIncome { get; set; }
		[Required]
		[DisplayName("Rent Payment")]
		public decimal RentPayment { get; set; }
		[Required]
		public decimal Loans { get; set; }
		[Required]
		public LoanPurpose? LoanPurpose { get; set; }

		[NotMapped]
		[DisplayName("Upload File")]
		[DataType(DataType.Upload)]
		[FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg,pdf")]
		public IFormFile? IdFile { get; set; }
		public string? MemberIdPath { get; set; }
		public string? MemberIdFileName { get; set; }

		[NotMapped]
		[DisplayName("Upload File")]
		[DataType(DataType.Upload)]
		[FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg,pdf")]
		public IFormFile? BankStatementFile { get; set; }
		public string? MemberBankStatementPath { get; set; }
		public string? MemberBankStatementFileName { get; set; }
	}
}
