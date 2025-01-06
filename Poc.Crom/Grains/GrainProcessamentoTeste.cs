namespace Poc.Crom.Grains;

public class GrainProcessamentoTeste : Grain, IGrainProcessamentoTeste
{
    private readonly ILogger<RunningJob> _logger;

    public GrainProcessamentoTeste(ILogger<RunningJob> logger)
    {
        _logger = logger;
    }

    public async Task Processar()
    {
           _logger.LogInformation($"Processando {DateTime.Now.ToLongTimeString()}");
    }
}