using FastWiki.Service.Application.Users.Queries;
using FastWiki.Service.Contracts.Users.Dto;
using FastWiki.Service.Domain.Users.Repositories;

namespace FastWiki.Service.Application.Users;

public sealed class UserQueryHandler(IUserRepository userRepository, IMapper mapper)
{
    [EventHandler]
    public async Task UserInfoAsync(UserInfoQuery query)
    {
        var dto = await userRepository.FindAsync(x => x.Account == query.Account);

        if (dto == null)
        {
            throw new UserFriendlyException("账号不存在");
        }

        if (!dto.CheckCipher(query.Pass))
        {
            throw new UserFriendlyException("密码错误");
        }

        if (dto.IsDisable)
        {
            throw new UserFriendlyException("账号已禁用");
        }

        query.Result = mapper.Map<UserDto>(dto);
    }

    [EventHandler]
    public async Task UserListAsync(UserListQuery query)
    {
        var list = await userRepository.GetListAsync(query.Keyword, query.Page, query.PageSize);

        var total = await userRepository.GetCountAsync(query.Keyword);

        query.Result = new PaginatedListBase<UserDto>
        {
            Total = total,
            Result = mapper.Map<List<UserDto>>(list)
        };
    }

    [EventHandler]
    public async Task GetUserAsync(UserQuery query)
    {
        var dto = await userRepository.FindAsync(query.Id);

        query.Result = dto;
    }
}