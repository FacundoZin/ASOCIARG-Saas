using APIClub.Domain.Enums;

namespace APIClub.Application.Dtos.AssociationConfigs.ConfigurationsCuotas
{
    public class ConfigurationCuotasDto
    {
        public int Id { get; set; }
        public PeriodosPagoCuota TipoPeriodo { get; set; }
        public int DiaVencimiento { get; set; } // Día del mes en que vence la cuota
        public int DiasGracia { get; set; } // Días extra después de vencimiento
        public int DiasAnticipacionAviso { get; set; } // Días antes para enviar aviso
    }
}
