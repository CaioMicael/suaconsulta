import React, { useState } from "react";
import Input from "../components/Input";
import ButtonDefault from "../components/ButtonDefault";

const PatientProfile = () => {
    const [patient, setPatient] = useState({
        name: "",
        email: "",
        birthday: "",
        phone: "",
        city: "",
        state: "",
        country: ""
    });

    return (
        <div className="max-w-2xl mx-auto p-6 bg-white rounded-lg shadow-md">
            <h1 className="text-2xl font-bold mb-6 text-center">Seu Perfil</h1>
            
            <div className="flex justify-center mb-6">
                <img className="w-32 h-32 rounded-full" src="/download.jpg" alt="Foto do paciente" />
            </div>
            
            <form >
                <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                    <div className="mb-4">
                        <Input
                            labelDescription="Nome"
                            name="patient-name"
                            type="text"
                            disabled={false}
                            value={patient.name}
                            required={true}
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
                        />                         
                    </div>
                </div>
                
                <div className="mt-6 flex justify-end">
                    <ButtonDefault
                        Description="Salvar Alterações"
                        Name="button-salvar"
                        Type="button"
                        disabled={false}
                    />
                </div>
            </form>
        </div>
    );
};

export default PatientProfile;