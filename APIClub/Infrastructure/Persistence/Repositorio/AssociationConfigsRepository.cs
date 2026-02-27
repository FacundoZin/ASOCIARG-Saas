using APIClub.Domain.ClientConfigs.Models;
using APIClub.Domain.ClientConfigs.Repositories;
using APIClub.Domain.ModuloGestionCuotas.Models;
using APIClub.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace APIClub.Infrastructure.Persistence.Repositorio
{
    public class AssociationConfigsRepository : IAssociationConfigsRepository
    {
        private readonly AppDbcontext _context;

        public AssociationConfigsRepository (AppDbcontext context)
        {
            _context = context;
        }

        public async Task<bool> CrearConfiguracionCuotas(ConfiguracionCuotas configCuotas)
        {
            _context.ConfiguracionCuotas.Add(configCuotas);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> CreateAssociationConfig(GeneralAssociationConfigs associationConfigs)
        {
            _context.configuracionAsociacion.Add(associationConfigs);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<GeneralAssociationConfigs?> GetAssociationConfigs()
        {
            return await _context.configuracionAsociacion.FirstAsync();
        }

        public async Task<GeneralAssociationConfigs?> GetAssociationConfigsWhitRelations()
        {
            return await _context.configuracionAsociacion.Include(c => c.configuracionDeCuotas).AsNoTracking().FirstAsync();
        }

        public async Task<ConfiguracionCuotas?> GetConfigurationCuotas()
        {
            return await _context.ConfiguracionCuotas.FirstAsync();
        }

        public async Task<bool> SaveChanges()
        {
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }
    }
}
