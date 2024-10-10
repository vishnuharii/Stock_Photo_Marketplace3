using Stock_Photo_Marketplace.Models;

public interface IUserService
{
    void Register(User user);
    User Login(string username);
}
