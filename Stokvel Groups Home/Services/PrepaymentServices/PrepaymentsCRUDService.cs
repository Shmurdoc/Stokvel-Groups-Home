using Microsoft.AspNetCore.Mvc.Rendering;
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

		public Task<Prepayment>? Detail(int? id)
		{
			throw new NotImplementedException();
		}

		public async Task<Prepayment>? GetById(int? id)
		{
			var result = await _prepaymentsRepository.GetById(id);
			return result;
		}
		public Task Edit(Prepayment? prepayment)
		{
			throw new NotImplementedException();
		}

		public Task<List<Prepayment>>? GetAll()
		{
			throw new NotImplementedException();
		}

		public Task Inset(Prepayment? prepayment)
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
