﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Stokvel_Groups_Home.Interface.IServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services
{
	public class PaymentMethodsService : IPaymentMethodsService
	{
		public Task Delete(int? id)
		{
			throw new NotImplementedException();
		}

		public Task<DepositMethod>? Details(int? id)
		{
			throw new NotImplementedException();
		}

		public Task Edit(DepositMethod? paymentMethod)
		{
			throw new NotImplementedException();
		}

		public Task<List<DepositMethod>>? GetAll()
		{
			throw new NotImplementedException();
		}

		public Task Inset(DepositMethod? paymentMethod)
		{
			throw new NotImplementedException();
		}

		public bool PaymentMethodExists(int? id)
		{
			throw new NotImplementedException();
		}

		public List<SelectListItem>? PaymentMethodExtendInclude()
		{
			throw new NotImplementedException();
		}

		public Task SaveAsync()
		{
			throw new NotImplementedException();
		}
	}
}
