namespace EsercizioU5S2.Services
{
    public interface IAuthService
    {
        Task<User> AuthenticateUserAsync(string username, string password);
        Task SignInAsync(HttpContext httpContext, User user);
    }
}
