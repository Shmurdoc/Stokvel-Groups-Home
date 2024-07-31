using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IServices.IPrepaymentServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.PrepaymentServices
{
	public class PrepaymentsCRUDService : IPrepaymentsCRUDService
	{
		private readonly IPrepaymentsRepository _prepaymentsRepository;
		public PrepaymentsCRUDService(IPrepaymentsRepository prepaymentsRepository)
		{
			_prepaymentsRepository = prepaymentsRepository;
		}

		public Task Delete(int? id)
		{
			throw new NotImplementedException();
		}

		public Task<PreDeposit>? Detail(int? id)
		{
			throw new NotImplementedException();
		}

		public async Task<PreDeposit>? GetById(int? id)
		{
			var result = await _prepaymentsRepository.GetById(id);
			return result;
		}
		public Task Edit(PreDeposit? prepayment)
		{
			throw new NotImplementedException();
		}

		public async Task<List<PreDeposit>>? GetAll() => await _prepaymentsRepository.GetAll();

        public Task Inset(PreDeposit? prepayment)
		{
			throw new NotImplementedException();
		}

		public List<SelectListItem>? PaymentStatusExtendInclude()
		{
			throw new NotImplementedException();
		}

		public bool PrepaymentExists(int? id)
		{
			throw new NotImplementedException();
		}

		public Task SaveAsync()
		{
			throw new NotImplementedException();
		}
	}
}
