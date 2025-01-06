namespace BM.MissionProcessor.Utils;

public interface ISecurityService
{
    Guid CreateCryptographicallySecureGuid();
}