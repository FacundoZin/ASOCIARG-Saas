using APIClub.Domain.Enums;

namespace APIClub.Application.Dtos.AssociationConfigs
{
    public class UpdateAssociationConfigsDto
    {
        public int Id { get; set; }
        public string NombreAsociacion { get; set; }
        public PeriodosPagoCuota periodosCobroDeCuotas { get; set; }
        public TipoAsociacion tipoAsociacion { get; set; }
    }
}
