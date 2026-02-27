using APIClub.Application.Common;
using APIClub.Application.Dtos.AssociationConfigs.AssociationConfigs;
using APIClub.Application.Dtos.AssociationConfigs.ConfigurationsCuotas;

namespace APIClub.Domain.ClientConfigs.UseCases
{
    public interface IConfigsService
    {
        Task<Result<object?>> CreateGeneralConfigs(CreateAssociationConfigsDto dto);
        Task<Result<object?>> UpdateGeneralConfigs(UpdateAssociationConfigsDto dto);
        Task<(AssociationConfigsDto?, ConfigurationCuotasDto?)> GetAssociationConfigs();
        Task<Result<object?>> CreateConfiguracionCuotas(CreateConfigurationCuotasDto dto);
        Task<Result<object?>> UpdateConfigurationCuotas(UpdateConfigurationCuotasDto dto);
        
    }
}
