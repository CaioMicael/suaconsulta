import ButtonDefault from "./ButtonDefault";

/**
 * Componente a ser apresentado quando não forem encontrados médicos para demonstrar na página
 * @returns JSX.Element
 */
const NotFoundDoctors = () => {
    return (
        <div className="col-span-full text-center py-12">
            <div className="bg-white rounded-2xl shadow-lg p-8 max-w-md mx-auto">
                <div className="text-6xl mb-4">🏥</div>
                <h3 className="text-2xl font-bold text-gray-800 mb-2">
                    Nenhum médico encontrado
                </h3>
                <p className="text-gray-600 mb-6">
                    Não há médicos cadastrados no momento. Tente novamente mais tarde.
                </p>
            </div>
        </div>
    )
}

export default NotFoundDoctors;