using DotNetDemo.Service.Dtos;

namespace DotNetDemo.Service
{
    public interface IGroupService
    {
        Task<IEnumerable<GetGroupDto>> GetAll();
        Task<GetGroupDto> Get(int id);
        Task<int> Create(CreateGroupDto input);
        Task<int> Update(UpdateGroupDto input);
        Task<int> Delete(int id);
        Task<IEnumerable<GetGroupDto>> GetAllForAssigning();
    }
}
