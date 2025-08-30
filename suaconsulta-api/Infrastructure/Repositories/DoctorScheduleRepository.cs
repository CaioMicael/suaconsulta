using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Infrastructure.Data;

namespace suaconsulta_api.Infrastructure.Repositories
{
    /// <summary>
    /// Repository para a agenda do médico
    /// </summary>
    public class DoctorScheduleRepository
    {
        private readonly AppDbContext _context;

        public DoctorScheduleRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retorna se a data passada está disponível na agenda do médico
        /// </summary>
        /// <param name="date"></param>
        /// <param name="doctorId"></param>
        /// <returns>bool</returns>
        public async Task<bool> IsDateScheduleAvailable(DateTime date, int doctorId)
        {
            bool isAvailable = await _context.DoctorSchedule
            .AsNoTracking()
            .Where(
                D => D.Doctor.Id == doctorId &&
                !_context.Consultation.Any(
                    C => C.Doctor.Id == D.Doctor.Id &&
                    C.Date.Year == D.StartTime.Year &&
                    C.Date.Month == D.StartTime.Month &&
                    C.Date.Day == D.StartTime.Day &&
                    C.Date.Hour == D.StartTime.Hour &&
                    C.Date.Minute == D.StartTime.Minute
                )
            )
            .AnyAsync();

            return isAvailable;
        }
    }
}