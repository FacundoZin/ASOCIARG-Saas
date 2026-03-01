using APIClub.Application.Dtos.AssociationConfigs.ConfigurationsCuotas;
using APIClub.Domain.Enums;

namespace APIClub.Application.Dtos.AssociationConfigs.AssociationConfigs
{
    public class UpdateAssociationConfigsDto
    {
        public int Id { get; set; }
        public string NombreAsociacion { get; set; }
        public TipoAsociacion tipoAsociacion { get; set; }
    }
}
