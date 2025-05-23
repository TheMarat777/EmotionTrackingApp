using DTO;
using Grpc.Core;
using Grpc.Net.Client;
using Protobuf.Services.Interfaces;
using Protobuf.Users;

namespace Protobuf.Services;

public class UsersService : IUsersService
{
  private readonly Users.UsersService.UsersServiceClient _client;
  private readonly GrpcChannel _channel;

  public UsersService()
  {
    _channel = GrpcChannel.ForAddress("http://localhost:8888");
    _client = new(_channel);
  }

  public async Task<UserReturnDto> Create(UserRegisterDto user)
  {
    var reply = await _client.CreateAsync(new UserCreate
    {
      Username = user.Username,
      Password = user.Password,
      Email = user.Email
    });

    DateTime.TryParse(reply.CreatedAt, out var createdAt);
    DateTime.TryParse(reply.UpdatedAt, out var updatedAt);

    return new UserReturnDto
    {
      Id = Convert.ToInt32(reply.Id),
      Username = reply.Username,
      Email = reply.Email,
      Streak = reply.Streak,
      CreatedAt = createdAt,
      UpdatedAt = updatedAt
    };
  }

  public async Task<UserReturnDto?> GetByUsernameAndPassword(string username, string password)
  {
    var reply = await _client.GetByUsernameAndPasswordAsync(new UsernameAndPassword
    {
      Username = username,
      Password = password
    });

    if (reply.Id == 0)
    {
      return null;
    }

    DateTime.TryParse(reply.CreatedAt, out var createdAt);
    DateTime.TryParse(reply.UpdatedAt, out var updatedAt);

    return new UserReturnDto
    {
      Id = Convert.ToInt32(reply.Id),
      Username = reply.Username,
      Email = reply.Email,
      Streak = reply.Streak,
      CreatedAt = createdAt,
      UpdatedAt = updatedAt
    };
  }

  public async Task<UserReturnDto?> GetByUsername(string username)
  {
    var reply = await _client.GetByUsernameAsync(new Username { Username_ = username });

    if (reply.Id == 0)
    {
      return null;
    }

    DateTime.TryParse(reply.CreatedAt, out var createdAt);
    DateTime.TryParse(reply.UpdatedAt, out var updatedAt);

    return new UserReturnDto
    {
      Id = Convert.ToInt32(reply.Id),
      Username = reply.Username,
      Email = reply.Email,
      Streak = reply.Streak,
      CreatedAt = createdAt,
      UpdatedAt = updatedAt
    };
  }

  public async Task<UserReturnDto> ChangePassword(int userId, ChangePasswordDto changePasswordDto)
  {
    var user = await _client.GetByIdAsync(new UserId { Id = userId });

    if (user == null)
    {
      throw new Exception("User not found");
    }

    var reply = await _client.UpdateAsync(new UserUpdate
    {
      Id = user.Id,
      Username = user.Username,
      Password = changePasswordDto.NewPassword,
      Email = user.Email
    });

    DateTime.TryParse(reply.CreatedAt, out var createdAt);
    DateTime.TryParse(reply.UpdatedAt, out var updatedAt);

    return new UserReturnDto
    {
      Id = Convert.ToInt32(reply.Id),
      Username = reply.Username,
      Email = reply.Email,
      Streak = reply.Streak,
      CreatedAt = createdAt,
      UpdatedAt = updatedAt
    };
  }

  public async Task<UserReturnDto?> GetById(int id)
  {
    var reply = await _client.GetByIdAsync(new UserId { Id = id });

    DateTime.TryParse(reply.CreatedAt, out var createdAt);
    DateTime.TryParse(reply.UpdatedAt, out var updatedAt);

    if (reply.Id == 0)
    {
      return null;
    }

    return new UserReturnDto
    {
      Id = Convert.ToInt32(reply.Id),
      Username = reply.Username,
      Email = reply.Email,
      Streak = reply.Streak,
      CreatedAt = createdAt,
      UpdatedAt = updatedAt
    };
  }
}