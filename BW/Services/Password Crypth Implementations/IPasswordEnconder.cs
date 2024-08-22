namespace BW2_Team6.Services.Password_Crypth_Implementations
{
    public interface IPasswordEnconder
    {
        string Encode(string password);
        bool IsSame(string plainText, string codedText);
    }
}
