
namespace Shared
{
    public interface IAuthClientHelper
    {
        HttpClient? GetAuthorizedHttpClient(string providedApiKey);
    }
}