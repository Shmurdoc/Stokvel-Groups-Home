﻿using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices
{
	public interface IPaymentLogsService
	{
		Task<List<PaymentLog>>? GetAll();

		Task<PaymentLog>? GetById(int? id);
		Task Insert(PaymentLog? paymentLog);

		Task Edit(PaymentLog? paymentLog);

		Task Delete(string? id);

		Task SaveAsync();

		bool PaymentLogExists(string? id);

	}
}
