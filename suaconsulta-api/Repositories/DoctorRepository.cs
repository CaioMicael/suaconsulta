using System.Web.Http.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Model;

namespace suaconsulta_api.Repositories
{
    /// <summary>
    /// Repository para o modelo de Doctor
    /// </summary>
    public class DoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> UpdateDoctor(ModelDoctor Doctor)
        {
            if (Doctor == null) throw new ArgumentNullException(nameof(Doctor));
            var existing = await _context.Doctor.FindAsync(Doctor.Id);
            if (existing == null) return false;

            _context.Entry(existing).State = EntityState.Detached;
            _context.Doctor.Update(Doctor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}