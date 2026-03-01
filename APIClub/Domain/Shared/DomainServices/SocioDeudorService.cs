using APIClub.Application.Dtos.Socios;
using APIClub.Application.Services;
using APIClub.Domain.ModuloGestionCuotas.Models;

namespace APIClub.Domain.Shared.DomainServices
{
    /// <summary>
    /// Proporciona servicios relacionados con el cálculo de deudas de los socios.
    /// </summary>
    public class SocioDeudorService
    {
        private readonly CalculatorPeriodoProvider _calculatorPeriodoProvider;
        public SocioDeudorService(CalculatorPeriodoProvider calculatorPeriodoProvider)
        {
            _calculatorPeriodoProvider = calculatorPeriodoProvider;
        }


        /// <summary>
        /// Determina cuáles períodos están efectivamente adeudados aplicando la regla de vencimiento.
        ///
        /// Regla:
        ///   - Períodos anteriores al actual → si no están pagos, son deuda (sin excepción).
        ///   - Período actual → es deuda solo si no está pago Y ya pasó el día de vencimiento
        ///     configurado por la asociación (DiaVencimiento de ConfiguracionCuotas).
        ///
        /// Los días de gracia quedan fuera del alcance de este método por el momento.
        /// </summary>
        public async Task<bool> IsDeudor(ICollection<Cuota> historialCuotasSocios, DateOnly fechaAsociacionSocio)
        {
            var PeriodoCalculator = await _calculatorPeriodoProvider.GetCalculator();

            var hoy = DateOnly.FromDateTime(DateTime.Now);

            var periodosPagados = new HashSet<(int, int)>
              (historialCuotasSocios.Select(c => (c.Anio, c.NumeroPeriodo)));

            var TodosLosPeriodosDelSocio = PeriodoCalculator.GenerarPeriodosDesdeAsociacion(fechaAsociacionSocio);

            var PeriodoActual = PeriodoCalculator.ObtenerPeriodoActual();

            foreach (var periodo in TodosLosPeriodosDelSocio)
            {
                var clave = (periodo.Anio, periodo.Periodo);

                if (periodosPagados.Contains(clave)) continue;


                bool esPeriodoActual = periodo.Anio == hoy.Year && periodo.Periodo == PeriodoActual;

                // periodo anterior => Deudor directo
                if (!esPeriodoActual)
                {
                    return true;
                }

                // Período actual sin pagar → solo es deuda si ya venció.
                var fechaVencimiento = PeriodoCalculator.ObtenerFechaVencimientoPeriodo(periodo.Anio, periodo.Periodo);
                if (hoy > fechaVencimiento)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Obtiene la lista de períodos adeudados desde la fecha de asociación hasta el período actual.
        /// No tiene en cuenta la fecha de vencimiento, simplemente verifica si existe un registro de pago.
        /// </summary>
        public async Task<List<PeriodoAdeudadoDto>> GetPeriodosAdeudados(ICollection<Cuota> historialCuotasSocios, DateOnly fechaAsociacionSocio)
        {
            var PeriodoCalculator = await _calculatorPeriodoProvider.GetCalculator();

            var periodosPagados = new HashSet<(int, int)>
                (historialCuotasSocios.Select(c => (c.Anio, c.NumeroPeriodo)));

            var TodosLosPeriodosDelSocio = PeriodoCalculator.GenerarPeriodosDesdeAsociacion(fechaAsociacionSocio);

            return TodosLosPeriodosDelSocio
                .Where(p => !periodosPagados.Contains((p.Anio, p.Periodo)))
                .Select(p => new PeriodoAdeudadoDto
                {
                    Anio = p.Anio,
                    NumeroPeriodo = p.Periodo
                })
                .ToList();
        }
    }
}
