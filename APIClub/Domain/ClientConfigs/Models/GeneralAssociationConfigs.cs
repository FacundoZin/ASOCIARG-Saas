using APIClub.Domain.Enums;
using APIClub.Domain.ModuloGestionCuotas.Models;

namespace APIClub.Domain.ClientConfigs.Models
{
    public class GeneralAssociationConfigs
    {
        public int Id { get; set; }
        public string NombreAsociacion { get; set; }
        public TipoAsociacion tipoAsociacion { get; set; }

        // public modulosSeleccionados...


        //relacion  con la configuracion de cuotas
        public int IdConfiguarcionPeriodoPago { get; set; }
        public ConfiguracionCuotas configuracionDeCuotas { get; set; }
        
    }
}
