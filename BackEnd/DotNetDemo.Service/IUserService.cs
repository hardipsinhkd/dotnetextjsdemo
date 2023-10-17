using DotNetDemo.Service.Dtos;

namespace DotNetDemo.Service
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserDto>> GetAll();
        Task<GetUserDto> Get(int id);
        Task<int> Create(CreateUserDto input);
        Task<int> Update(UpdateUserDto input);
        Task<int> Delete(int id);
        Task<int> AssignGroup(AssignGroupToUserDto input);
    }
}
