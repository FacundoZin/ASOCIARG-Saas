using APIClub.Application.Common;
using APIClub.Application.Dtos.AssociationConfigs.AssociationConfigs;
using APIClub.Application.Dtos.AssociationConfigs.ConfigurationsCuotas;
using APIClub.Domain.ClientConfigs.UseCases;
using APIClub.Domain.ModuloGestionCuotas.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIClub.Contrrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientConfigsController : ControllerBase
    {

        private readonly IConfigsService _configService;

        public ClientConfigsController(IConfigsService configsService)
        {
            _configService = configsService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<ConfiguracionCuotas>>> GetConfiguracionesGenerales()
        {
            var configs = await _configService.GetAssociationConfigs();
            return Ok(configs);
        }

        [HttpPut("ConfiguacionGeneral")]
        public async Task<ActionResult<Result<ConfiguracionCuotas>>> ActualizarConfiguracionGeneral([FromBody] UpdateAssociationConfigsDto dto)
        {
            var result = await _configService.UpdateGeneralConfigs(dto);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok();
        }

        [HttpPut("ConfiguacionDeCuotas")]
        public async Task<ActionResult<Result<ConfiguracionCuotas>>> ActualizarConfiguracionDeCuotas([FromBody] UpdateConfigurationCuotasDto dto)
        {
            var result = await _configService.UpdateConfigurationCuotas(dto);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok();
        }

        [HttpPost("ConfiguacionGeneral")]
        public async Task<ActionResult<Result<ConfiguracionCuotas>>> CrearConfiguracionGeneral([FromBody] CreateAssociationConfigsDto dto)
        {
            var result = await _configService.CreateGeneralConfigs(dto);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok();
        }

        [HttpPost("ConfiguacionDeCuotas")]
        public async Task<ActionResult<Result<ConfiguracionCuotas>>> CrearConfiguracionDeCuotas([FromBody] CreateConfigurationCuotasDto dto)
        {
            var result = await _configService.CreateConfiguracionCuotas(dto);

            if (!result.Exit) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok();
        }
    }
}
