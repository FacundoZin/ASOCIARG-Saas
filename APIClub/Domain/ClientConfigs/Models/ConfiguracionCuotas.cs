using APIClub.Domain.Enums;

namespace APIClub.Domain.ModuloGestionCuotas.Models
{
    public class ConfiguracionCuotas
    {
        public int Id { get; set; }
        public PeriodosPagoCuota TipoPeriodo { get; set; }
        public int DiaVencimiento { get; set; } // Día del mes en que vence la cuota
        public int DiasGracia { get; set; } // Días extra después de vencimiento
        public int DiasAnticipacionAviso { get; set; } // Días antes para enviar aviso

        // Propiedades calculadas
        public int MesesPorPeriodo => (int)TipoPeriodo;
        public int PeriodosPorAnio => 12 / MesesPorPeriodo;

        // relacion con la tabla de configuracion de asociacion
        public int IdAssociationConfig {  get; set; }
    }
}
