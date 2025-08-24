using suaconsulta_api.Domain.Model;
using suaconsulta_api.DTO;
using suaconsulta_api.Repositories;

namespace suaconsulta_api.Domain.Services
{
    public class ConsultationService
    {
        private readonly ConsultationRepository _consultationRepository;

        public ConsultationService(ConsultationRepository consultationRepository)
        {
            _consultationRepository = consultationRepository ?? throw new ArgumentNullException(nameof(_consultationRepository));
        }

        /// <summary>
        /// Retorna as consultas do médico repassado
        /// </summary>
        /// <param name="Doctor">ModelDoctor</param>
        /// <returns>DoctorConsultationsDto</returns>
        public async Task<DoctorConsultationsDto> GetDoctorConsultation(ModelDoctor Doctor)
        {
            return await _consultationRepository.GetDoctorConsultations(Doctor);
        }
    }
}