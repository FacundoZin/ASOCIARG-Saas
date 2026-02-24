using APIClub.Domain.ClientConfigs.Models;

namespace APIClub.Domain.ClientConfigs.Repositories
{
    public interface IAssociationConfigsRepository
    {
        Task CreateAssociationConfig(AssociationConfigs configs);
        Task UpdateAssociationConfig(AssociationConfigs updatedConfigs);
        Task<AssociationConfigs> GetAssociationConfigs();
    }
}
