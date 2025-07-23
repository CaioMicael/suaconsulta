import React, { useEffect, useState } from "react";
import ButtonSeeDoctorSchedule from "./ButtonSeeDoctorSchedule";
import ButtonSeeDoctorProfile from "./ButtonSeeDoctorProfile";
import ButtonDefault from "./ButtonDefault";
import api from "../services/api";
import SucessAlert from "./alerts/SucessAlert";
import { Doctor } from "../interfaces";
import LoadingSpin from "./LoadingSpin";

const DoctorsList = () => {
    const [doctors, setDoctors] = useState<Doctor[]>([]);

    const loadDoctors = async () => {
        try {
            api.get('Doctor/ListDoctor')
                .then(response => {
                    const formattedDoctors = response.data.map((doctorData: any) => ({
                        id: doctorData.id,
                        nome: doctorData.name,
                        especialidade: doctorData.specialty,
                        crm: doctorData.crm,
                        telefone: doctorData.phone,
                        email: doctorData.email
                    }));
                    setDoctors(formattedDoctors);
                    <SucessAlert message="Dados carregados com sucesso!" />
                })
        } catch (error) {
            console.error("Erro ao buscar médicos:", error);
        }
    }
    
    useEffect(() => {
        loadDoctors();
    }, []);

    return (
        <div className="min-h-screen bg-gradient-to-br from-blue-50 via-white to-purple-50 p-6">
            <div className="max-w-7xl mx-auto">
                <div className="text-center mb-8">
                    <h1 className="text-4xl font-bold text-gray-800 mb-4">
                        Nossos <span className="text-blue-600">Médicos</span>
                    </h1>
                    <p className="text-gray-600 text-lg mb-6">
                        Encontre o profissional ideal para suas necessidades de saúde
                    </p>
                    <div className="flex justify-center">
                        <ButtonDefault
                            Description="🔄 Atualizar Lista" 
                            Name="buscar-dados" 
                            Type="button"
                            onClick={() => loadDoctors()}
                        />
                    </div>
                </div>


                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
                    {doctors.length === 0 ? (
                        <LoadingSpin textSpinner="Carregando médicos..." />
                    ) : (
                        doctors.map((doctor) => (
                            <div 
                                key={doctor.id}
                                className="bg-white rounded-2xl shadow-lg hover:shadow-xl transition-all duration-300 transform hover:-translate-y-2 border border-gray-100 overflow-hidden group"
                            >
                                <div className="bg-gradient-to-r from-blue-500 to-purple-600 px-6 py-8 text-center relative">
                                    <div className="absolute inset-0 bg-black opacity-10"></div>
                                    <div className="relative">
                                        <img 
                                            className="w-20 h-20 rounded-full mx-auto border-4 border-white shadow-lg object-cover" 
                                            src="/download.jpg" 
                                            alt={`Dr. ${doctor.nome}`}
                                        />
                                        <h3 className="text-white font-bold text-xl mt-3">
                                            Dr. {doctor.nome}
                                        </h3>
                                        <span className="inline-block bg-white bg-opacity-20 text-white text-sm px-3 py-1 rounded-full mt-2">
                                            {doctor.especialidade}
                                        </span>
                                    </div>
                                </div>

                                <div className="p-6">
                                    <div className="space-y-3">
                                        <div className="flex items-center text-gray-600">
                                            <span className="text-blue-500 mr-2">🏥</span>
                                            <span className="text-sm font-medium">CRM:</span>
                                            <span className="ml-auto font-semibold">{doctor.crm}</span>
                                        </div>
                                        <div className="flex items-center text-gray-600">
                                            <span className="text-green-500 mr-2">📞</span>
                                            <span className="text-sm font-medium">Telefone:</span>
                                            <span className="ml-auto font-semibold text-sm">{doctor.telefone}</span>
                                        </div>
                                        <div className="flex items-center text-gray-600">
                                            <span className="text-purple-500 mr-2">✉️</span>
                                            <span className="text-sm font-medium">Email:</span>
                                            <span className="ml-auto font-semibold text-sm truncate max-w-32" title={doctor.email}>
                                                {doctor.email}
                                            </span>
                                        </div>
                                    </div>


                                    <div className="flex gap-2 mt-6 pt-4 border-t border-gray-100">
                                        <div className="flex-1">
                                            <ButtonSeeDoctorSchedule 
                                                labelDescription="📅 Horários" 
                                                name="button-agendar" 
                                                type="button" 
                                                doctor={doctor}
                                            />
                                        </div>
                                        <div className="flex-1">
                                            <ButtonSeeDoctorProfile 
                                                doctor={doctor}
                                                description="👨‍⚕️ Perfil" 
                                                name="button-see-doctor-profile" 
                                            />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        ))
                    )}
                </div>

                {/* Footer Section */}
                {doctors.length > 0 && (
                    <div className="text-center mt-12 pt-8 border-t border-gray-200">
                        <p className="text-gray-500">
                            Mostrando {doctors.length} médico{doctors.length !== 1 ? 's' : ''} disponív{doctors.length !== 1 ? 'eis' : 'el'}
                        </p>
                    </div>
                )}
            </div>
        </div>
    );
}

export default DoctorsList;