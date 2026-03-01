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
        /// Define una especificación para determinar si un socio es deudor basándose en la fecha de vencimiento del periodo actual.
        /// </summary>
        /// <param name="calc">Servicio para el cálculo de periodos y fechas de vencimiento.</param>
        /// <returns>Una expresión booleana evaluable por un proveedor de datos.</returns>
        public static Expression<Func<Socio, bool>> EsDeudorTeniendoEnCuentaFechaDeVencimiento(
            PeriodoCalculator calc)
        {
            var fechaHoy = DateOnly.FromDateTime(DateTime.Now);
            var anioActual = fechaHoy.Year;
            var numeroPeriodoActual = calc.ObtenerPeriodoActual();
            var fechaVencimiento = calc.ObtenerFechaVencimientoPeriodo(anioActual, numeroPeriodoActual);

            // Si la fecha de hoy es mayor a la fecha de vencimiento, se considera que el socio adeuda la cuota del perido actual.
            int offset = fechaHoy > fechaVencimiento ? 1 : 0;

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

