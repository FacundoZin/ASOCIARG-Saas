using APIClub.Domain.ClientConfigs.Repositories;
using APIClub.Domain.Shared.DomainServices;

namespace APIClub.Application.Services
{
    public class CalculatorPeriodoProvider
    {
        private readonly IAssociationConfigsRepository _ConfigRepo;
        private PeriodoCalculator? _Calculator;

        public CalculatorPeriodoProvider(IAssociationConfigsRepository repo)
        {
            _ConfigRepo = repo;
        }

        public async Task<PeriodoCalculator> GetCalculator()
        {
            if (_Calculator == null)
            {
                var config = await _ConfigRepo.GetConfigurationCuotas();

                if (config == null)
                    throw new Exception("La configuracion de cuotas no existe.");

                _Calculator = new PeriodoCalculator(config!);
            }
            return _Calculator;
        }

        public async Task Refresh()
        {
            var config = await _ConfigRepo.GetConfigurationCuotas();

            if (config == null)
                throw new Exception("La configuracion de cuotas no existe.");

            _Calculator = new PeriodoCalculator(config);
        }
    }
}
