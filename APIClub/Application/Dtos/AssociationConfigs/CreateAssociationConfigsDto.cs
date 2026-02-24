using APIClub.Domain.Enums;

namespace APIClub.Application.Dtos.AssociationConfigs
{
    public class CreateAssociationConfigsDto
    {
        public string NombreAsociacion { get; set; }
        public PeriodosPagoCuota periodosCobroDeCuotas { get; set; }
        public TipoAsociacion tipoAsociacion { get; set; }
    }
}
