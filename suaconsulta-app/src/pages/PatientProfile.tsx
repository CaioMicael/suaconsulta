import React, { useState, useEffect } from "react";
import Input from "../components/Input";
import ButtonDefault from "../components/ButtonDefault";
import { ApiResponse, Patient, UserInformationResponse } from "../interfaces";
import api from "../services/api";
import { useAlert } from "../providers/AlertProvider";
import { useNavigate } from "react-router-dom";

/**
 * PatientProfile component that allows users to view and edit their profile information.
 * @returns JSX.Element
 */
const PatientProfile = () => {
    const { showAlert } = useAlert();
    const navigate = useNavigate();

    const [patient, setPatient] = useState<Patient>({
        name: "",
        email: "",
        birthday: "",
        phone: "",
        city: "",
        state: "",
        country: ""
    });

    const handleInputChange = (field: keyof Patient, value: string) => {
        setPatient(prev => ({
            ...prev,
            [field]: value
        }));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        console.log(event);
        showAlert("Funcionalidade de edição de perfil ainda não implementada.", "warning");
    }

    const loadPatientData = async () => {
        await api.get<UserInformationResponse>('Auth/tokenInformation?justUser=false')
        .then((response) => {
            if (response.status === 200) {
                if (response.data.patient) {
                    const patientData: Patient = response.data.patient;
                    setPatient({
                        name: patientData.name,
                        email: patientData.email,
                        birthday: patientData.birthday,
                        phone: patientData.phone,
                        city: patientData.city,
                        state: patientData.state,
                        country: patientData.country
                    });
                } else if (response.data.doctor) {
                    showAlert("Você está logado como médico, não é possível acessar o perfil de paciente.", "warning");
                    navigate("/doctor/profile");
                }
            } else {
                showAlert("Erro ao carregar os dados do paciente: " + response.status, "error");
            }
        });
    }

    useEffect(() => {
        if (patient.name === "") {
            loadPatientData();
        }
    }, [patient.name]);

    return (
        <div className="max-w-2xl mx-auto p-6 bg-white rounded-lg shadow-md">
            <h1 className="text-2xl font-bold mb-6 text-center">Seu Perfil</h1>
            
            <div className="flex justify-center mb-6">
                <img className="w-32 h-32 rounded-full" src="/download.jpg" alt="Foto do paciente" />
            </div>
            
            <form onSubmit={handleSubmit}>
                <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                    <div className="mb-4">
                        <Input
                            labelDescription="Nome"
                            name="patient-name"
                            type="text"
                            disabled={false}
                            value={patient.name}
                            required={true}
                            size={100}
                            onChange={() => handleInputChange}
                        />
                    </div>

                    <div className="mb-4">
                        <Input
                            labelDescription="E-mail"
                            name="patient-email"
                            type="email"
                            disabled={false}
                            value={patient.email}
                            required={true}
                            onChange={() => handleInputChange}
                        />                        
                    </div>

                    <div className="mb-4">
                        <Input
                            labelDescription="Data de Nascimento"
                            name="patient-birthday"
                            type="date"
                            disabled={false}
                            value={patient.birthday}
                            required={true}
                            onChange={() => handleInputChange}
                        />                           
                    </div>
                    
                    <div className="mb-4">
                        <Input
                            labelDescription="Telefone"
                            name="patient-telefone"
                            type="tel"
                            disabled={false}
                            value={patient.phone}
                            required={true}
                            onChange={() => handleInputChange}
                        />                          
                    </div>
                    
                    <div className="mb-4">
                        <Input
                            labelDescription="Cidade"
                            name="patient-city"
                            type="text"
                            disabled={false}
                            value={patient.city}
                            required={true}
                            onChange={() => handleInputChange}
                        />                            
                    </div>
                    
                    <div className="mb-4">
                        <Input
                            labelDescription="Estado"
                            name="patient-state"
                            type="text"
                            disabled={false}
                            value={patient.state}
                            required={true}
                            onChange={() => handleInputChange}
                        />                          
                    </div>
                    
                    <div className="mb-4 md:col-span-2">
                        <Input
                            labelDescription="País"
                            name="patient-country"
                            type="text"
                            disabled={false}
                            value={patient.country}
                            required={true}
                            onChange={() => handleInputChange}
                        />                         
                    </div>
                </div>
                
                <div className="mt-6 flex justify-end">
                    <ButtonDefault
                        Description="Salvar Alterações"
                        Name="button-salvar"
                        Type="submit"
                        disabled={false}
                    />
                </div>
            </form>
        </div>
    );
};

export default PatientProfile;