using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IGroupServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.GroupServices;

public class GroupsCRUDService : IGroupsCRUDService
{

	private readonly IGroupsRepository _groupsRepository;
	public GroupsCRUDService(IGroupsRepository groupsRepository)
	{
		_groupsRepository = groupsRepository;
	}
	public Task Delete(int? id) => _groupsRepository.Delete(id);

	public async Task<Group>? Details(int? id) => await _groupsRepository.Details(id);

	public async Task Edit(Group? group) => await _groupsRepository.Edit(group);

	public async Task<List<Group>>? GetAll() => await _groupsRepository.GetAll();

	public async Task<Group>? GetById(int? id) => await _groupsRepository.GetById(id);
	public async Task<int> GetGroupId(string? groupVerifyKey) => await _groupsRepository.GetGroupId(groupVerifyKey);

	public bool GroupExists(string? id) => _groupsRepository.GroupExists(id);

	public async Task Inset(Group? group) => await _groupsRepository.Inset(group);

	public async Task SaveAsync() => await _groupsRepository.SaveAsync();
}
