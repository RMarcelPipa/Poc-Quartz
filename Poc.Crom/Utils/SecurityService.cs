using System.Security.Cryptography;

namespace BM.MissionProcessor.Utils;

public class SecurityService : ISecurityService
{
    private readonly RandomNumberGenerator _rand = RandomNumberGenerator.Create();

    public Guid CreateCryptographicallySecureGuid()
    {
        var bytes = new byte[16];
        _rand.GetBytes(bytes);
        return new Guid(bytes);
    }
}