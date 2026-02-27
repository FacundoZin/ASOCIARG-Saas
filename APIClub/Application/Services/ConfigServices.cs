using APIClub.Application.Common;
using APIClub.Application.Dtos.AssociationConfigs.AssociationConfigs;
using APIClub.Application.Dtos.AssociationConfigs.ConfigurationsCuotas;
using APIClub.Domain.ClientConfigs.Models;
using APIClub.Domain.ClientConfigs.Repositories;
using APIClub.Domain.ClientConfigs.UseCases;
using APIClub.Domain.ModuloGestionCuotas.Models;

namespace APIClub.Application.Services
{
    public class ConfigServices : IConfigsService
    {
        private readonly IAssociationConfigsRepository _associationConfigsRepository;
        private readonly CalculatorPeriodoProvider _calculatorProvider;

        public ConfigServices(IAssociationConfigsRepository associationConfigsRepository, CalculatorPeriodoProvider calculatorProvider)
        {
            _associationConfigsRepository = associationConfigsRepository;
            _calculatorProvider = calculatorProvider;
        }

        public async Task<Result<object?>> CreateConfiguracionCuotas(CreateConfigurationCuotasDto dto)
        {
            var cuotasConfig = new ConfiguracionCuotas
            {
                TipoPeriodo = dto.TipoPeriodo,
                DiaVencimiento = dto.DiaVencimiento,
                DiasGracia = dto.DiasGracia,
                DiasAnticipacionAviso = dto.DiaVencimiento
            };

            var exit = await _associationConfigsRepository.CrearConfiguracionCuotas(cuotasConfig);

            if(!exit) return Result<object?>.Error("lo sentimos algo salio mal", 500);

            // al llamar a este metodo del proveedor de la calculadora de periodos
            // refrecamos la configuracion de cuotas que va a usar la calculadora.
            await _calculatorProvider.Refresh();

            return Result<object?>.Exito(null);
        }

        public async Task<Result<object?>> CreateGeneralConfigs(CreateAssociationConfigsDto dto)
        {
            var configs = new GeneralAssociationConfigs
            {
                NombreAsociacion = dto.NombreAsociacion,
                tipoAsociacion = dto.tipoAsociacion,
            };

            var exit = await _associationConfigsRepository.CreateAssociationConfig(configs);

            if(!exit) return Result<object?>.Error("lo sentimos algo salio mal", 500);

            return Result<object?>.Exito(null);
        }

        public async Task<(AssociationConfigsDto?,ConfigurationCuotasDto?)> GetAssociationConfigs()
        {
            var Generalconfigs = await _associationConfigsRepository.GetAssociationConfigs();
            var CuotasConfigs = await _associationConfigsRepository.GetConfigurationCuotas();

            var DtoGeneralConfig = Generalconfigs == null ? null : new AssociationConfigsDto
            {
                Id = Generalconfigs.Id,
                NombreAsociacion = Generalconfigs.NombreAsociacion,
                tipoAsociacion = Generalconfigs.tipoAsociacion
            };

            var DtoCuotasConfig = Generalconfigs == null ? null : new ConfigurationCuotasDto
            {
                Id = Generalconfigs.configuracionDeCuotas.Id,
                TipoPeriodo = Generalconfigs.configuracionDeCuotas.TipoPeriodo,
                DiaVencimiento = Generalconfigs.configuracionDeCuotas.DiaVencimiento,
                DiasGracia = Generalconfigs.configuracionDeCuotas.DiasGracia,
                DiasAnticipacionAviso = Generalconfigs.configuracionDeCuotas.DiasAnticipacionAviso,
            };

            return (DtoGeneralConfig, DtoCuotasConfig);

        }

        public async Task<Result<object?>> UpdateConfigurationCuotas(UpdateConfigurationCuotasDto dto)
        {
            var cuotasConfig = await _associationConfigsRepository.GetConfigurationCuotas();

            cuotasConfig!.TipoPeriodo = dto.TipoPeriodo;
            cuotasConfig.DiaVencimiento = dto.DiaVencimiento;
            cuotasConfig.DiasGracia = dto.DiasGracia;
            cuotasConfig.DiasAnticipacionAviso = dto.DiasAnticipacionAviso;

            var exit = await _associationConfigsRepository.SaveChanges();

            if(!exit) return Result<object?>.Error("lo sentimos algo salio mal", 500);

            // al llamar a este metodo del proveedor de la calculadora de periodos
            // refrecamos la configuracion de cuotas que va a usar la calculadora.
            await _calculatorProvider.Refresh();

            return Result<object?>.Exito(null);
        }

        public async Task<Result<object?>> UpdateGeneralConfigs(UpdateAssociationConfigsDto dto)
        {
            var configs = await _associationConfigsRepository.GetAssociationConfigs();

            configs!.NombreAsociacion = dto.NombreAsociacion;
            configs.tipoAsociacion= dto.tipoAsociacion;

            var exit = await _associationConfigsRepository.SaveChanges();

            if (!exit) return Result<object?>.Error("lo sentimos algo salio mal", 500);

            return Result<object?>.Exito(null);
        }
    }
}
