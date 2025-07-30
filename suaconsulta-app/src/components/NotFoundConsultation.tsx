
interface NotFoundConsultationProps {
    statusConsulta: string;
}

const NotFoundConsultation = ({ statusConsulta }: NotFoundConsultationProps) => {
    return (
    <div className="text-center py-12">
        <div className="bg-white rounded-2xl shadow-lg p-8 max-w-md mx-auto">
            <div className="text-6xl mb-4">📅</div>
            <h3 className="text-2xl font-bold text-gray-800 mb-2">
                Nenhuma consulta encontrada
            </h3>
            <p className="text-gray-600 mb-6">
                {statusConsulta === null 
                    ? "Você ainda não possui consultas agendadas."
                    : `Não há consultas com status "${statusConsulta}".`
                }
            </p>
        </div>
    </div>
    )
}

export default NotFoundConsultation;