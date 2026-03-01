using APIClub.Application.Common;
using APIClub.Application.Dtos.Socios;

namespace APIClub.Domain.GestionSocios
{
    public interface ISociosManagmentService
    {
        Task<Result<PagedResult<SocioCardDto>>> GetSociosDeudores(int pageNumber, int pageSize); //revisado
        Task<Result<PagedResult<socioCardSinEstadoDto>>> GetPadronSocios(int pageNumber, int PageSize); //revisado
        Task<Result<object?>> cargarSocio(CreateSocioDto _dto); //no hay cambios
        Task<Result<SocioDebtPreviewDto>> GetSocioByDni(string dni); //revisado
        Task<Result<object>> UpdateSocio(int id, UpdateSocio dto); //no hay cambios
        Task<Result<object>> RemoveSocio(int id); //no hay cambios
        Task<Result<object>> ReactivarSocio(int id); //no hay cambios
        Task<Result<PreviewSocioDto>> GetSocioById(int id); //no hay cambios
        Task<Result<FullSocioDto>> GetFullSocioById(int id); //revisado

    }
}
