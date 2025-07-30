import React, { useState, useEffect } from "react";
import api from "../services/api";
import { useAlert } from "../providers/AlertProvider";
import LoadingSpin from "../components/LoadingSpin";
import ButtonDefault from "../components/ButtonDefault";
import { ConsultationStatus } from "../enum/EnumStatusConsultation";
import { ConsultationData } from "@/interfaces";
import NotFoundConsultation from "../components/NotFoundConsultation";

/**
 * Page da tela de consultas do paciente
 * @returns JSX.Element
 */
const PatientConsultation = () => {
    const [consultations, setConsultations] = useState<ConsultationData[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [filteredConsultations, setFilteredConsultations] = useState<ConsultationData[]>([]);
    const [statusFilter, setStatusFilter] = useState<string>("all");
    const { showAlert } = useAlert();

    const getStatusText = (status: number): string => {
        switch (status) {
            case ConsultationStatus.Agendada:
                return "Agendada";
            case ConsultationStatus.Concluida:
                return "Concluída";
            case ConsultationStatus.Cancelada:
                return "Cancelada";
            case ConsultationStatus.Reagendada:
                return "Reagendada";
            case ConsultationStatus.EmAndamento:
                return "Em Andamento";
            default:
                return "Desconhecido";
        }
    };

    const getStatusColor = (status: number): string => {
        switch (status) {
            case ConsultationStatus.Agendada:
                return "bg-blue-100 text-blue-800";
            case ConsultationStatus.Concluida:
                return "bg-green-100 text-green-800";
            case ConsultationStatus.Cancelada:
                return "bg-red-100 text-red-800";
            case ConsultationStatus.Reagendada:
                return "bg-yellow-100 text-yellow-800";
            case ConsultationStatus.EmAndamento:
                return "bg-purple-100 text-purple-800";
            default:
                return "bg-gray-100 text-gray-800";
        }
    };

    const formatDate = (dateString: string): string => {
        const date = new Date(dateString);
        return date.toLocaleDateString('pt-BR', {
            day: '2-digit',
            month: '2-digit',
            year: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
    };

    /**
     * Este método realiza o carregamento das consultas do paciente
     * // TODO alterar endpoint pra não passar id via query string
     */
    const loadConsultations = async () => {
        try {
            setLoading(true);
            const userResponse = await api.get('Auth/tokenInformation?justUser=false');
            
            if (userResponse.data.patient) {
                const patientId = userResponse.data.patient.id;
                
                // Buscar todas as consultas (o endpoint atual retorna todas, então vamos filtrar)
                const response = await api.get(`Consultation/PatientConsultations/${patientId}`);
                
                if (response.status === 200 && response.data) {
                    // Filtrar apenas as consultas do paciente atual
                    const patientConsultations = response.data.filter((consultation: ConsultationData) => 
                        consultation.patientId === patientId
                    );
                    
                    setConsultations(patientConsultations);
                    setFilteredConsultations(patientConsultations);
                } else {
                    setConsultations([]);
                    setFilteredConsultations([]);
                }
            } else {
                showAlert("Erro: Você não está logado como paciente.", "error");
                setConsultations([]);
                setFilteredConsultations([]);
            }
            
            setLoading(false);
        } catch (error: any) {
            console.error("Erro ao carregar consultas:", error);
            setConsultations([]);
            setFilteredConsultations([]);
            setLoading(false);
            
            if (error.response?.status === 404) {
                showAlert("Nenhuma consulta encontrada.", "warning");
            } else {
                showAlert("Erro ao carregar suas consultas. Tente novamente.", "error");
            }
        }
    };

    const filterConsultations = (status: string) => {
        setStatusFilter(status);
        
        if (status === "all") {
            setFilteredConsultations(consultations);
        } else {
            const statusNumber = parseInt(status);
            const filtered = consultations.filter(consultation => consultation.status === statusNumber);
            setFilteredConsultations(filtered);
        }
    };

    useEffect(() => {
        loadConsultations();
    }, []);

    const getUpcomingConsultations = () => {
        const now = new Date();
        return filteredConsultations.filter(consultation => {
            const consultationDate = new Date(consultation.date);
            return consultationDate > now && consultation.status === ConsultationStatus.Agendada;
        }).sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime());
    };

    const getPastConsultations = () => {
        const now = new Date();
        return filteredConsultations.filter(consultation => {
            const consultationDate = new Date(consultation.date);
            return consultationDate <= now || consultation.status === ConsultationStatus.Concluida;
        }).sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
    };

    const upcomingConsultations = getUpcomingConsultations();
    const pastConsultations = getPastConsultations();

    return (
        <div className="min-h-screen bg-gradient-to-br from-blue-50 via-white to-purple-50 p-6">
            <div className="max-w-6xl mx-auto">
                {/* Header */}
                <div className="text-center mb-8">
                    <h1 className="text-4xl font-bold text-gray-800 mb-4">
                        Suas <span className="text-blue-600">Consultas</span>
                    </h1>
                    <p className="text-gray-600 text-lg mb-6">
                        Gerencie e acompanhe todas as suas consultas médicas
                    </p>
                    
                    {/* Botão Atualizar */}
                    <div className="flex justify-center mb-6">
                        <ButtonDefault
                            Description="🔄 Atualizar Lista"
                            Name="refresh-consultations"
                            Type="button"
                            onClick={() => loadConsultations()}
                            disabled={loading}
                        />
                    </div>

                    {/* Filtros */}
                    <div className="flex flex-wrap justify-center gap-2 mb-6">
                        <button
                            onClick={() => filterConsultations("all")}
                            className={`px-4 py-2 rounded-full text-sm font-medium transition-colors ${
                                statusFilter === "all" 
                                    ? "bg-blue-600 text-white" 
                                    : "bg-white text-gray-600 hover:bg-blue-50"
                            }`}
                        >
                            Todas
                        </button>
                        <button
                            onClick={() => filterConsultations("1")}
                            className={`px-4 py-2 rounded-full text-sm font-medium transition-colors ${
                                statusFilter === "1" 
                                    ? "bg-blue-600 text-white" 
                                    : "bg-white text-gray-600 hover:bg-blue-50"
                            }`}
                        >
                            Agendadas
                        </button>
                        <button
                            onClick={() => filterConsultations("2")}
                            className={`px-4 py-2 rounded-full text-sm font-medium transition-colors ${
                                statusFilter === "2" 
                                    ? "bg-green-600 text-white" 
                                    : "bg-white text-gray-600 hover:bg-green-50"
                            }`}
                        >
                            Concluídas
                        </button>
                        <button
                            onClick={() => filterConsultations("3")}
                            className={`px-4 py-2 rounded-full text-sm font-medium transition-colors ${
                                statusFilter === "3" 
                                    ? "bg-red-600 text-white" 
                                    : "bg-white text-gray-600 hover:bg-red-50"
                            }`}
                        >
                            Canceladas
                        </button>
                    </div>
                </div>

                {/* Loading */}
                {loading ? (
                    <div className="flex justify-center">
                        <LoadingSpin textSpinner="Carregando suas consultas..." />
                    </div>
                ) : (
                    <>
                        {filteredConsultations.length === 0 ? (
                            <NotFoundConsultation
                                statusConsulta = {statusFilter === "all" ? "agendadas" : "${getStatusText(parseInt(statusFilter))}"}
                            />
                        ) : (
                            <div className="space-y-8">
                                {/* Próximas Consultas */}
                                {upcomingConsultations.length > 0 && (
                                    <section>
                                        <h2 className="text-2xl font-bold text-gray-800 mb-4 flex items-center">
                                            🔜 Próximas Consultas
                                            <span className="ml-2 bg-blue-100 text-blue-800 text-sm font-medium px-2.5 py-0.5 rounded-full">
                                                {upcomingConsultations.length}
                                            </span>
                                        </h2>
                                        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                                            {upcomingConsultations.map((consultation) => (
                                                <div
                                                    key={consultation.id}
                                                    className="bg-white rounded-2xl shadow-lg hover:shadow-xl transition-all duration-300 border border-gray-100 overflow-hidden"
                                                >
                                                    <div className="bg-gradient-to-r from-blue-500 to-blue-600 px-6 py-4 text-white">
                                                        <div className="flex justify-between items-start">
                                                            <div>
                                                                <h3 className="font-bold text-lg">
                                                                    Dr. {consultation.doctor.name}
                                                                </h3>
                                                                <p className="text-blue-100 text-sm">
                                                                    {consultation.doctor.specialty}
                                                                </p>
                                                            </div>
                                                            <span className={`px-2 py-1 rounded-full text-xs font-medium ${getStatusColor(consultation.status)}`}>
                                                                {getStatusText(consultation.status)}
                                                            </span>
                                                        </div>
                                                    </div>
                                                    
                                                    <div className="p-6">
                                                        <div className="space-y-3">
                                                            <div className="flex items-center text-gray-600">
                                                                <span className="text-blue-500 mr-2">📅</span>
                                                                <span className="font-medium">{formatDate(consultation.date)}</span>
                                                            </div>
                                                            <div className="flex items-center text-gray-600">
                                                                <span className="text-green-500 mr-2">🏥</span>
                                                                <span className="text-sm">CRM: {consultation.doctor.crm}</span>
                                                            </div>
                                                            <div className="flex items-start text-gray-600">
                                                                <span className="text-purple-500 mr-2 mt-1">📝</span>
                                                                <span className="text-sm">{consultation.description}</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            ))}
                                        </div>
                                    </section>
                                )}

                                {/* Consultas Passadas */}
                                {pastConsultations.length > 0 && (
                                    <section>
                                        <h2 className="text-2xl font-bold text-gray-800 mb-4 flex items-center">
                                            📋 Histórico de Consultas
                                            <span className="ml-2 bg-gray-100 text-gray-800 text-sm font-medium px-2.5 py-0.5 rounded-full">
                                                {pastConsultations.length}
                                            </span>
                                        </h2>
                                        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                                            {pastConsultations.map((consultation) => (
                                                <div
                                                    key={consultation.id}
                                                    className="bg-white rounded-2xl shadow-lg hover:shadow-xl transition-all duration-300 border border-gray-100 overflow-hidden opacity-90"
                                                >
                                                    <div className="bg-gradient-to-r from-gray-500 to-gray-600 px-6 py-4 text-white">
                                                        <div className="flex justify-between items-start">
                                                            <div>
                                                                <h3 className="font-bold text-lg">
                                                                    Dr. {consultation.doctor.name}
                                                                </h3>
                                                                <p className="text-gray-100 text-sm">
                                                                    {consultation.doctor.specialty}
                                                                </p>
                                                            </div>
                                                            <span className={`px-2 py-1 rounded-full text-xs font-medium ${getStatusColor(consultation.status)}`}>
                                                                {getStatusText(consultation.status)}
                                                            </span>
                                                        </div>
                                                    </div>
                                                    
                                                    <div className="p-6">
                                                        <div className="space-y-3">
                                                            <div className="flex items-center text-gray-600">
                                                                <span className="text-blue-500 mr-2">📅</span>
                                                                <span className="font-medium">{formatDate(consultation.date)}</span>
                                                            </div>
                                                            <div className="flex items-center text-gray-600">
                                                                <span className="text-green-500 mr-2">🏥</span>
                                                                <span className="text-sm">CRM: {consultation.doctor.crm}</span>
                                                            </div>
                                                            <div className="flex items-start text-gray-600">
                                                                <span className="text-purple-500 mr-2 mt-1">📝</span>
                                                                <span className="text-sm">{consultation.description}</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            ))}
                                        </div>
                                    </section>
                                )}

                                {/* Resumo */}
                                {filteredConsultations.length > 0 && (
                                    <div className="text-center mt-12 pt-8 border-t border-gray-200">
                                        <p className="text-gray-500">
                                            Mostrando {filteredConsultations.length} consulta{filteredConsultations.length !== 1 ? 's' : ''}
                                            {statusFilter !== "all" && ` com status "${getStatusText(parseInt(statusFilter))}"`}
                                        </p>
                                    </div>
                                )}
                            </div>
                        )}
                    </>
                )}
            </div>
        </div>
    );
};

export default PatientConsultation;