using BM.MissionProcessor.Utils;
using Poc.Crom.Grains;
using Quartz;

namespace Poc.Crom;

public class RunningJob: IJob
{
    private readonly ILogger<RunningJob> _logger;
    private readonly IGrainFactory _grainsFactory;
    private readonly ISecurityService _securityService;
    
    public RunningJob(ILogger<RunningJob> logger, IGrainFactory grainsFactory, ISecurityService securityService)
    {
        _logger = logger;
        _grainsFactory = grainsFactory;
        _securityService = securityService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        // Criar o Grão aqui

        var key = _securityService.CreateCryptographicallySecureGuid();

        var grain = _grainsFactory.GetGrain<IGrainProcessamentoTeste>(key);
        
         await grain.Processar(); 
    }
}        