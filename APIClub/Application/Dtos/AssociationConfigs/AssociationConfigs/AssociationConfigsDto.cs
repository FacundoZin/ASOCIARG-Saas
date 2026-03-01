using APIClub.Application.Dtos.AssociationConfigs.ConfigurationsCuotas;
using APIClub.Domain.Enums;

namespace APIClub.Application.Dtos.AssociationConfigs.AssociationConfigs
{
    public class AssociationConfigsDto
    {
        public int Id { get; set; }
        public string NombreAsociacion { get; set; }
        public ConfigurationCuotasDto? ConfigurationCuotas { get; set; }
        public TipoAsociacion tipoAsociacion { get; set; }

    }
}
