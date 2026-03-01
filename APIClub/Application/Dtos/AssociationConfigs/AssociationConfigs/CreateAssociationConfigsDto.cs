using APIClub.Application.Dtos.AssociationConfigs.ConfigurationsCuotas;
using APIClub.Domain.Enums;
using APIClub.Domain.ModuloGestionCuotas.Models;

namespace APIClub.Application.Dtos.AssociationConfigs.AssociationConfigs
{
    public class CreateAssociationConfigsDto
    {
        public string NombreAsociacion { get; set; }
        public TipoAsociacion tipoAsociacion { get; set; }
    }
}
