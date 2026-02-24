using APIClub.Application.Common;
using APIClub.Application.Dtos.AssociationConfigs;
using APIClub.Application.Dtos.Salones;

namespace APIClub.Domain.ClientConfigs.UseCases
{
    public interface IAssociationConfigsService
    {
        Task<Result<object?>> CreateAssociationConfigs(CreateAssociationConfigsDto dto);
        Task<Result<object?>> UpdateAssociationConfigs(UpdateAssociationConfigsDto dto);
        Task<Result<AssociationConfigsDto?>> GetAssociationConfigs();
        Task UploadSalones(CreateSalonDto dto);
        Task SetValorCuotaInicial(decimal valorCuota);

    }
}
