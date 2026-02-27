using APIClub.Domain.Enums;
using APIClub.Application.Dtos.Socios;
using APIClub.Domain.GestionSocios.Models;
using APIClub.Domain.GestionSocios.Repositories;
using APIClub.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using APIClub.Application.Helpers;
using APIClub.Domain.ModuloGestionCuotas.Models;
using APIClub.Domain.Shared.DomainServices;
using APIClub.Application.Services;

namespace APIClub.Infrastructure.Persistence.Repositorio
{
    public class SociosRepository : ISocioRepository
    {
        private readonly AppDbcontext _Dbcontext;
        private readonly CalculatorPeriodoProvider _calculatorPeriodoProvider;

        public SociosRepository(AppDbcontext dbcontext, CalculatorPeriodoProvider providerCalculator)
        {

            _Dbcontext = dbcontext;
            _calculatorPeriodoProvider = providerCalculator;
        }

        public async Task cargarSocio(Socio socio)
        {
            _Dbcontext.Socios.Add(socio);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task<Socio?> GetSocioByDni(string dni)
        {
            return await _Dbcontext.Socios
                .Include(s => s.HistorialCuotas)
                .Include(s => s.Lote)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Dni == dni);
        }

        public async Task<Socio?> GetSocioByDniIgnoreFilter(string dni)
        {
            return await _Dbcontext.Socios
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Dni == dni);
        }

        public async Task<bool> SocioExists(string dni)
        {
            return await _Dbcontext.Socios.IgnoreQueryFilters().AnyAsync(s => s.Dni == dni);
        }

        public async Task<bool> SocioExistsForUpdate(string dni, int id)
        {
            return await _Dbcontext.Socios.IgnoreQueryFilters().AnyAsync(s => s.Dni == dni && s.Id != id);
        }

        public async Task<Socio?> GetSocioById(int id)
        {
            return await _Dbcontext.Socios
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateSocio(Socio socio)
        {
            _Dbcontext.Socios.Update(socio);
            await _Dbcontext.SaveChangesAsync();
        }

        public void UpdateSocioWhitoutSave(Socio socio)
        {
            _Dbcontext.Socios.Update(socio);
        }

        public async Task<Socio?> GetSocioByIdIgnoreFilter(int id)
        {
            return await _Dbcontext.Socios.IgnoreQueryFilters().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Socio?> GetSocioByIdWithCuotas(int id)
        {
            var socioData = await _Dbcontext.Socios
                .Select(s => new { s.Id, s.FechaAsociacion })
                .FirstOrDefaultAsync(s => s.Id == id);

            if (socioData == null) return null;

            var calculator = await _calculatorPeriodoProvider.GetCalculator();

            int periodoIngreso = calculator.ObtenerPeriodoDeAsociacion(socioData.FechaAsociacion);

            return await _Dbcontext.Socios
                .Include(s => s.HistorialCuotas
                .Where(c => c.Anio > socioData.FechaAsociacion.Year ||
                (c.Anio == socioData.FechaAsociacion.Year && c.NumeroPeriodo >= periodoIngreso)))
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Socio>> GetSociosDeudores(int anioActual, int numeroPeriodoActual)
        {
            var deudores = await _Dbcontext.Socios
                .Where(s => !s.HistorialCuotas.Any(c => c.Anio == anioActual && c.NumeroPeriodo == numeroPeriodoActual))
                .AsNoTracking()
                .ToListAsync();

            return deudores;
        }

        public async Task<(List<Socio> Items, int TotalCount)> GetSociosDeudoresPaginado(int anioActual, int numeroPeriodoActual, int pageNumber, int pageSize)
        {
            var query = _Dbcontext.Socios
                .Include(s => s.Lote)
                .AsNoTracking()
                .Where(s => !s.HistorialCuotas.Any(c => c.Anio == anioActual && c.NumeroPeriodo == numeroPeriodoActual));

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(s => s.Apellido)
                .ThenBy(s => s.Nombre)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<(List<Socio> Items, int TotalCount)> GetSociosPaginado(int pageNumber, int pageSize)
        {
            var query = _Dbcontext.Socios
                .Include(s => s.Lote)
                .AsNoTracking();

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(s => s.Apellido)
                .ThenBy(s => s.Nombre)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task RemoveSocios(Socio socio)
        {
            socio.IsActivo = false;
            socio.FechaDeBaja = DateOnly.FromDateTime(DateTime.Now);
            _Dbcontext.Socios.Update(socio);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task<List<Cuota>> GetCuotasSocioById(int socioId)
        {
            return await _Dbcontext.Cuotas
            .Where(c => c.SocioId == socioId)
            .OrderByDescending(c => c.FechaPago)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<(List<PreviewSocioForCobranzaDto> Items, int TotalCount)> GetSociosDeudoresByLote(int IdLote, int anioActual, int numeroPeriodoActual, int pageNumber, int pageSize)
        {
            var query = _Dbcontext.Socios.
                Where(s => s.LoteId == IdLote && s.PreferenciaDePago == MetodosDePago.Cobrador && !s.HistorialCuotas
                .Any(c => c.Anio == anioActual && c.NumeroPeriodo == numeroPeriodoActual))
                .AsNoTracking();

            int totalCount = await query.CountAsync();

            // Proyectamos a un tipo anónimo para traer solo lo necesario de la DB
            var socios = await query
                .OrderBy(s => s.Apellido)
                .ThenBy(s => s.Nombre)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new
                {
                    s.Id,
                    s.Nombre,
                    s.Apellido,
                    s.Dni,
                    s.Telefono,
                    s.Direcccion,
                    s.FechaAsociacion,
                    CuotasPagas = s.HistorialCuotas.Select(c => new { c.Anio, NumeroPeriodo = c.NumeroPeriodo }).ToList()
                })
                .ToListAsync();

            var calc = await _calculatorPeriodoProvider.GetCalculator();
            // Transformamos al DTO final
            var dtoSocios = socios.Select(s =>
            {
                var todosLosPeriodos = calc.GenerarPeriodosDesdeAsociacion(s.FechaAsociacion, anioActual, numeroPeriodoActual);

                var periodosAdeudados = todosLosPeriodos
                    .Where(p => !s.CuotasPagas.Any(c => c.Anio == p.Anio && c.NumeroPeriodo == p.Periodo))
                    .Select(p => new PeriodoAdeudadoDto { Anio = p.Anio, NumeroPeriodo = p.Periodo })
                    .ToList();

                return new PreviewSocioForCobranzaDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    Dni = s.Dni,
                    Telefono = s.Telefono?.FormatearForUserVisibility(),
                    Direcccion = s.Direcccion,
                    PeriodosAdeudados = periodosAdeudados
                };

            }).ToList();

            return (dtoSocios, totalCount);
        }

        public async Task<List<Socio>> GetSociosDeudoresWithPreferenceLinkDePagoPaginado(int anioActual, int numeroPeriodoActual, int pageNumber, int pageSize)
        {
            var items = await _Dbcontext.Socios
                .Where(s => s.PreferenciaDePago == MetodosDePago.LinkDePago &&
                !s.HistorialCuotas.Any(c => c.Anio == anioActual && c.NumeroPeriodo == numeroPeriodoActual))
                .AsNoTracking()
                .OrderBy(s => s.Apellido)
                .ThenBy(s => s.Nombre)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return items;
        }
    }
}
