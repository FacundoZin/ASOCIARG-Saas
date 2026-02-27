using APIClub.Domain.Enums;
using APIClub.Domain.ModuloGestionCuotas.Models;

namespace APIClub.Domain.Shared.DomainServices
{
    public class PeriodoCalculator
    {
        private readonly ConfiguracionCuotas _config;

        public PeriodoCalculator(ConfiguracionCuotas config)
        {
            _config = config;
        }

        /// <summary>
        /// Dado un mes del año (1-12), devuelve el número de período (1-based).
        /// Ejemplo: Config=Semestral → Enero=1, Julio=2
        ///          Config=Trimestral → Enero=1, Abril=2, Julio=3, Octubre=4
        /// </summary>
        public int ObtenerNumeroPeriodo(int mes)
        {
            return ((mes - 1) / _config.MesesPorPeriodo) + 1;
        }

        /// <summary>
        /// Devuelve el número de período actual basado en la fecha actual.
        /// </summary>
        public int ObtenerPeriodoActual()
        {
            return ObtenerNumeroPeriodo(DateTime.Now.Month);
        }

        /// <summary>
        /// Devuelve el número de período en el que un socio se asoció.
        /// </summary>
        public int ObtenerPeriodoDeAsociacion(DateOnly fechaAsociacion)
        {
            return ObtenerNumeroPeriodo(fechaAsociacion.Month);
        }

        /// <summary>
        /// Genera todos los períodos que un socio debería haber pagado
        /// desde su fecha de asociación hasta el período actual.
        /// Reemplaza: los loops `for (int anio = anioInicio; anio <= anioActual; anio++) { for (int sem = semestreDesde; sem <= semestreHasta; sem++) {...} }`
        /// </summary>
        public List<(int Anio, int Periodo)> GenerarPeriodosDesdeAsociacion(
            DateOnly fechaAsociacion, int anioActual, int periodoActual)
        {
            var periodos = new List<(int Anio, int Periodo)>();
            int anioInicio = fechaAsociacion.Year;
            int periodoInicio = ObtenerPeriodoDeAsociacion(fechaAsociacion);
            int periodosPorAnio = _config.PeriodosPorAnio;

            for (int anio = anioInicio; anio <= anioActual; anio++)
            {
                int desde = (anio == anioInicio) ? periodoInicio : 1;
                int hasta = (anio == anioActual) ? periodoActual : periodosPorAnio;

                for (int p = desde; p <= hasta; p++)
                {
                    periodos.Add((anio, p));
                }
            }

            return periodos;
        }

        /// <summary>
        /// Calcula cuántos períodos deberían haberse pagado antes de un período dado.
        /// Reemplaza: `((token.anio - AnioAsociacion) * 2) + (token.semestre - SemestreAsociacion)`
        /// </summary>
        public int CalcularPeriodosAnteriores(
            DateOnly fechaAsociacion, int anioObjetivo, int periodoObjetivo)
        {
            int anioAsociacion = fechaAsociacion.Year;
            int periodoAsociacion = ObtenerPeriodoDeAsociacion(fechaAsociacion);
            int periodosPorAnio = _config.PeriodosPorAnio;

            return ((anioObjetivo - anioAsociacion) * periodosPorAnio)
                   + (periodoObjetivo - periodoAsociacion);
        }

        /// <summary>
        /// Genera un texto descriptivo del período.
        /// Reemplaza: `GetSemestreText()` → "primer semestre" / "segundo semestre"
        /// </summary>
        public string ObtenerTextoPeriodo(int numeroPeriodo)
        {
            return _config.TipoPeriodo switch
            {
                PeriodosPagoCuota.Mensual => ObtenerNombreMes(numeroPeriodo),
                PeriodosPagoCuota.Bimestral => $"bimestre {numeroPeriodo}",
                PeriodosPagoCuota.Trimestral => $"trimestre {numeroPeriodo}",
                PeriodosPagoCuota.Cuatrimestral => $"cuatrimestre {numeroPeriodo}",
                PeriodosPagoCuota.Semestral => numeroPeriodo == 1 ? "primer semestre" : "segundo semestre",
                PeriodosPagoCuota.Anual => "cuota anual",
                _ => $"período {numeroPeriodo}"
            };
        }

        /// <summary>
        /// Obtiene el mes de inicio de un período específico.
        /// Útil para filtros en queries.
        /// </summary>
        public int ObtenerMesInicioPeriodo(int numeroPeriodo)
        {
            return ((numeroPeriodo - 1) * _config.MesesPorPeriodo) + 1;
        }

        /// <summary>
        /// Obtiene el mes de fin de un período específico.
        /// </summary>
        public int ObtenerMesFinPeriodo(int numeroPeriodo)
        {
            return numeroPeriodo * _config.MesesPorPeriodo;
        }

        private string ObtenerNombreMes(int mes) => mes switch
        {
            1 => "enero",
            2 => "febrero",
            3 => "marzo",
            4 => "abril",
            5 => "mayo",
            6 => "junio",
            7 => "julio",
            8 => "agosto",
            9 => "septiembre",
            10 => "octubre",
            11 => "noviembre",
            12 => "diciembre",
            _ => $"mes {mes}"
        };
    }
}
