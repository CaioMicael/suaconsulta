import React, { useState, useEffect } from "react";
import Input from "../components/Input";
import ButtonDefault from "../components/ButtonDefault";
import { ApiResponse, DoctorApi, UserInformationResponse } from "../interfaces";
import api from "../services/api";
import { useAlert } from "../providers/AlertProvider";
import { useNavigate } from "react-router-dom";

/**
 * DoctorProfile component that allows doctors to view and edit their profile information.
 * @returns JSX.Element
 */
const DoctorProfile = () => {
    const { showAlert } = useAlert();
    const navigate = useNavigate();

    const [doctor, setDoctor] = useState<DoctorApi>({
        name: "",
        email: "",
        specialty: "",
        crm: "",
        phone: "",
        city: "",
        state: "",
        country: ""
    });

    useEffect(() => {
        if (doctor.name === "") {
            loadDoctorData();
        }
    }, [doctor.name]);

    const handleInputChange = (field: keyof DoctorApi, value: string) => {
        setDoctor(prev => ({
            ...prev,
            [field]: value
        }));
    };

    /**
     * Realiza o submit da tela de alteração de dados do médico
     * @param event React.FormEvent
     * @async
     */
    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        try {
            await api.put("Doctor/UpdateDoctor", {
                ...doctor
            })
            .then(response => {
                showAlert("Perfil alterado com sucesso!", "success");
            })
            .catch(error => {
                showAlert("Ocorreu um erro na alteração do perfil: "+ error.response.data, "error");    
            });
        } catch (error) {
            showAlert("Ocorreu um erro na alteração do perfil: "+ error, "error");
        }
    }

    /**
     * Carrega os dados do médico em tela.
     * @async
     */
    const loadDoctorData = async () => {
        await api.get<UserInformationResponse>('Auth/tokenInformation?justUser=false')
        .then((response) => {
            if (response.status === 200) {
                if (response.data.doctor) {
                    const doctorData = response.data.doctor;
                    setDoctor({
                        name: (doctorData as any).name,
                        email: doctorData.email,
                        specialty: (doctorData as any).specialty,
                        crm: doctorData.crm,
                        phone: (doctorData as any).phone,
                        city: (doctorData as any).city,
                        state: (doctorData as any).state,
                        country: (doctorData as any).country
                    });
                } else if (response.data.patient) {
                    showAlert("Você está logado como paciente, não é possível acessar o perfil de médico.", "warning");
                    navigate("/patient/profile");
                }
            } else {
                showAlert("Erro ao carregar os dados do médico: " + response.status, "error");
            }
        });
    }

    return (
        <div className="max-w-2xl mx-auto p-6 bg-white rounded-lg shadow-md">
            <h1 className="text-2xl font-bold mb-6 text-center">Seu Perfil</h1>
            
            <div className="flex justify-center mb-6">
                <img className="w-32 h-32 rounded-full" src="/download.jpg" alt="Foto do médico" />
            </div>
            
            <form onSubmit={handleSubmit}>
                <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                    <div className="mb-4">
                        <Input
                            labelDescription="Nome"
                            name="doctor-name"
                            type="text"
                            disabled={false}
                            value={doctor.name}
                            required={true}
                            size={100}
                            onChange={(e) => handleInputChange("name", e.target.value)}
                        />
                    </div>

                    <div className="mb-4">
                        <Input
                            labelDescription="E-mail"
                            name="doctor-email"
                            type="email"
                            disabled={false}
                            value={doctor.email}
                            required={true}
                            onChange={(e) => handleInputChange("email", e.target.value)}
                        />                        
                    </div>

                    <div className="mb-4">
                        <Input
                            labelDescription="Especialidade"
                            name="doctor-specialty"
                            type="text"
                            disabled={false}
                            value={doctor.specialty}
                            required={true}
                            onChange={(e) => handleInputChange("specialty", e.target.value)}
                        />                           
                    </div>
                    
                    <div className="mb-4">
                        <Input
                            labelDescription="CRM"
                            name="doctor-crm"
                            type="text"
                            disabled={false}
                            value={doctor.crm}
                            required={true}
                            onChange={(e) => handleInputChange("crm", e.target.value)}
                        />                          
                    </div>
                    
                    <div className="mb-4">
                        <Input
                            labelDescription="Telefone"
                            name="doctor-phone"
                            type="tel"
                            disabled={false}
                            value={doctor.phone}
                            required={true}
                            mask="(99)99999-9999"
                            onChange={(e) => handleInputChange("phone", e.target.value)}
                        />                            
                    </div>
                    
                    <div className="mb-4">
                        <Input
                            labelDescription="Cidade"
                            name="doctor-city"
                            type="text"
                            disabled={false}
                            value={doctor.city}
                            required={true}
                            onChange={(e) => handleInputChange("city", e.target.value)}
                        />                          
                    </div>
                    
                    <div className="mb-4">
                        <Input
                            labelDescription="Estado"
                            name="doctor-state"
                            type="text"
                            disabled={false}
                            value={doctor.state}
                            required={true}
                            onChange={(e) => handleInputChange("state", e.target.value)}
                        />                         
                    </div>
                    
                    <div className="mb-4 md:col-span-2">
                        <Input
                            labelDescription="País"
                            name="doctor-country"
                            type="text"
                            disabled={false}
                            value={doctor.country}
                            required={true}
                            onChange={(e) => handleInputChange("country", e.target.value)}
                        />                         
                    </div>
                </div>
                
                <div className="mt-6 flex justify-end">
                    <ButtonDefault
                        Description="Salvar Alterações"
                        Name="button-salvar"
                        Type="submit"
                        disabled={false}
                        onClick={() => handleSubmit}
                    />
                </div>
            </form>
        </div>
    );
};

export default DoctorProfile;