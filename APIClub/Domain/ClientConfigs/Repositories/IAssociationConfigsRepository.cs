using APIClub.Domain.ClientConfigs.Models;
using APIClub.Domain.ModuloGestionCuotas.Models;

namespace APIClub.Domain.ClientConfigs.Repositories
{
    public interface IAssociationConfigsRepository
    {
        Task<bool> CreateAssociationConfig(GeneralAssociationConfigs associationConfig);
        Task<GeneralAssociationConfigs?> GetAssociationConfigsWhitRelations();
        Task<GeneralAssociationConfigs?> GetAssociationConfigs();
        Task<ConfiguracionCuotas?> GetConfigurationCuotas();
        Task<bool> CrearConfiguracionCuotas(ConfiguracionCuotas cuotaConfig);
        Task<bool> SaveChanges();
    }
}
