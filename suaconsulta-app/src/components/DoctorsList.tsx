import React, { useEffect, useState } from "react";
import ButtonSeeDoctorSchedule from "./ButtonSeeDoctorSchedule";
import ButtonSeeDoctorProfile from "./ButtonSeeDoctorProfile";

const DoctorsList = () => {

    type Doctor = {
        id: number;
        nome: string;
        especialidade: string;
        crm: string;
        telefone: string;
        email: string;
    };

    const [doctors, setDoctors] = useState<Doctor[]>([]);
    
    useEffect(() => {
        setTimeout(() => {
            setDoctors([
                {id: 1, nome: "Dr. João Silva", especialidade: "Cardiologia", crm: "123456", telefone: "(11) 98765-4321", email: "teste@gmail.com"},
                {id: 2, nome: "Dr. Maria Pereira", especialidade: "Cardiologia", crm: "123456", telefone: "(11) 98765-4321", email: "teste@gmail.com"}
            ]);
        },1000)
    }); 

    return (
        <div className="">
            <div>
                <h2>Médicos</h2>
                <button onClick={() => setDoctors}>Buscar Dados</button>
            </div>
            <div className="flex flex-wrap justify-center">
                {doctors.length === 0 ? (
                    <div className="inline grid grid-cols-3 gap-4">
                        <p>Carregando...</p>
                        <p>Nome:</p>
                        <p>Especialidade:</p>
                        <p>CRM:</p>
                        <p>Telefone:</p>
                        <p>Email:</p>
                    </div>
                ) : (
                    doctors.map((doctor) => (
                        <div className="inline-grid grid-cols-1 gap-2" key={doctor.id}>
                            <div className="border-2 border-gray-700 focus:border-pink-600 rounded-md shadow-lg m-2 py-2 px-1 w-60">
                                <img className="w-10 h-10 rounded-full" src="/download.jpg" alt="Rounded avatar"></img>
                                <p>Id: {doctor.id}</p>
                                <p>Nome: {doctor.nome}</p>
                                <p>Especialidade: {doctor.especialidade}</p>
                                <p>CRM: {doctor.crm}</p>
                                <p>Telefone: {doctor.telefone}</p>
                                <p>Email: {doctor.email}</p>
                                <div className="flex flex-row justify-center">
                                    <ButtonSeeDoctorSchedule 
                                        labelDescription="Horários" 
                                        name="button-agendar" 
                                        type="button" 
                                        DoctorId={doctor.id} 
                                        nome={doctor.nome}
                                        especialidade={doctor.especialidade}
                                        crm={doctor.crm} 
                                        telefone={doctor.telefone}
                                        email={doctor.email}
                                    />
                                    <ButtonSeeDoctorProfile 
                                        doctorId={doctor.id} 
                                        description="Perfil" 
                                        name="button-see-doctor-profile" 
                                    />
                                </div>
                            </div>
                        </div>
                    ))
                )}
            </div>

        </div>
    );
}

export default DoctorsList;