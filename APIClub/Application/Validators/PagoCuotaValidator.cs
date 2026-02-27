using APIClub.Application.Common;
using APIClub.Application.Dtos.Socios;
using APIClub.Domain.Auth.Repositories;
using APIClub.Domain.Enums;
using APIClub.Domain.GestionSocios.Models;
using APIClub.Domain.GestionSocios.Repositories;
using APIClub.Domain.ModuloGestionCuotas.Validations;

namespace APIClub.Application.Validators
{
    public class PagoCuotaValidator : IPagoCuotaValidator
    {
        private readonly ISocioRepository _socioRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly ICuotaRepository _cuotaRepository;

        public PagoCuotaValidator(ISocioRepository socioRepository, IUsuariosRepository usuariosRepository, ICuotaRepository cuotaRepository)
        {
            _socioRepository = socioRepository;
            _usuariosRepository = usuariosRepository;
            _cuotaRepository = cuotaRepository;
        }

        public async Task<Result<Socio>> ValidarPagoConCobrador(int idSocio, int idUsuarioSistema, List<PeriodoAdeudadoDto> periodos)
        {
            var usuario = await _usuariosRepository.GetById(idUsuarioSistema);

            if (usuario == null)
                return Result<Socio>.Error("Usuario no encontrado.", 404);

            if (usuario.Rol != RolUsuario.Cobrador)
                return Result<Socio>.Error("solamente los cobradores pueden registrar este tipo de pagos", 403);

            var socio = await _socioRepository.GetSocioById(idSocio);
            if (socio == null)
                return Result<Socio>.Error("Socio no encontrado.", 404);

            foreach (var periodo in periodos)
            {
                if (await _cuotaRepository.PeriodoYaPagado(idSocio, periodo.Anio, periodo.NumeroPeriodo))
                    return Result<Socio>.Error($"El periodo {periodo.Anio}/{periodo.NumeroPeriodo} ya se encuentra pago.", 400);
            }

            return Result<Socio>.Exito(socio);
        }

        public async Task<Result<Socio>> ValidarPagoEnEstablecimeinto(int idSocio, List<PeriodoAdeudadoDto> periodos)
        {
            var socio = await _socioRepository.GetSocioById(idSocio);
            if (socio == null)
                return Result<Socio>.Error("Socio no encontrado.", 404);

            foreach (var periodo in periodos)
            {
                if (await _cuotaRepository.PeriodoYaPagado(idSocio, periodo.Anio, periodo.NumeroPeriodo))
                    return Result<Socio>.Error($"El periodo {periodo.Anio}/{periodo.NumeroPeriodo} ya se encuentra pago.", 400);
            }

            return Result<Socio>.Exito(socio);
        }
    }
}
