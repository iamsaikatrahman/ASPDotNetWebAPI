using HotelListing.Models;

namespace HotelListing.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginDTO userDTO);
        Task<string> CreateToken();
    }
}
