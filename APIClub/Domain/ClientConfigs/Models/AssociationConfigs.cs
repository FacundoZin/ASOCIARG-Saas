using APIClub.Domain.Enums;

namespace APIClub.Domain.ClientConfigs.Models
{
    public class AssociationConfigs
    {
        public int Id { get; set; }
        public string NombreAsociacion { get; set; }
        public PeriodosPagoCuota periodosCobroDeCuotas {  get; set; }
        public TipoAsociacion tipoAsociacion { get; set; }
    }
}
