namespace Poc.Crom.Grains;

public interface IGrainProcessamentoTeste: IGrainWithGuidKey
{
    Task Processar();
}