using APIClub.Domain.GestionSocios.Models;
using System.Linq.Expressions;
using APIClub.Domain.Shared.DomainServices;

namespace APIClub.Domain.ModuloGestionSocios.Specification
{
    /// <summary>
    /// Proporciona especificaciones para realizar filtros y consultas sobre la entidad Socio.
    /// </summary>
    public static class SocioSpecifications
    {
        /// <summary>
        /// Define una especificación para determinar si un socio es deudor comparando 
        /// la cantidad de periodos que deberian haber pagado con la cantidad de periodos que pagaron 
        /// </summary>
        /// <param name="calc">Servicio para el cálculo de periodos y fechas de vencimiento.</param>
        /// <returns>Una expresión booleana evaluable por un filtro</returns>
        public static Expression<Func<Socio, bool>> EsDeudor(
            PeriodoCalculator calc, bool ContemplaVencimientoCuota)
        {
            var fechaHoy = DateOnly.FromDateTime(DateTime.Now);
            var anioActual = fechaHoy.Year;
            var numeroPeriodoActual = calc.ObtenerPeriodoActual();
            var fechaVencimiento = calc.ObtenerFechaVencimientoPeriodo(anioActual, numeroPeriodoActual);

            int offset = 0;

            if (ContemplaVencimientoCuota)
            {
                offset = fechaHoy > fechaVencimiento ? 1 : 0;
            }
            else
            {
                // Si NO se tiene en cuenta vencimiento,
                // el período actual siempre es exigible.
                offset = 1;
            }

            int mesesPorPeriodo = calc.MesesPorPeriodo;
            int periodosPorAnio = calc.PeriodosPorAnio;

            return s =>
                (
                    ((anioActual - s.FechaAsociacion.Year) * periodosPorAnio)
                    + (numeroPeriodoActual - (((s.FechaAsociacion.Month - 1) / mesesPorPeriodo) + 1))
                    + offset
                )
                >
                s.HistorialCuotas.Count(c =>
                    (
                        c.Anio > s.FechaAsociacion.Year
                        || (c.Anio == s.FechaAsociacion.Year
                            && c.NumeroPeriodo >= (((s.FechaAsociacion.Month - 1) / mesesPorPeriodo) + 1))
                    )
                    &&
                    (
                        c.Anio < anioActual
                        || (c.Anio == anioActual && c.NumeroPeriodo <= numeroPeriodoActual)
                    )
                );
        }


    }
}

