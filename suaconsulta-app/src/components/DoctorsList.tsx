import { useEffect, useState } from "react";

const DoctorsList = () => {

    type Doctor = {
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
                {nome: "Dr. João Silva", especialidade: "Cardiologia", crm: "123456", telefone: "(11) 98765-4321", email: "teste@gmail.com"},
                {nome: "Dr. Maria Pereira", especialidade: "Cardiologia", crm: "123456", telefone: "(11) 98765-4321", email: "teste@gmail.com"}
            ]);
        },1000)
    });

    return (
        <div className="medico-disponivel-container">
            <h2>Dados do Médico</h2>
            <button onClick={() => setDoctors}>Buscar Dados</button>
            {doctors.length === 0 ? (
                <div className="medico-disponivel-info">
                    <p>Nome:</p>
                    <p>Especialidade:</p>
                    <p>CRM:</p>
                    <p>Telefone:</p>
                    <p>Email:</p>
                </div>
            ) : (
                doctors.map((doctor) => (
                    <div className="medico-disponivel-info" key={doctor.crm}>
                        <p>Nome: {doctor.nome}</p>
                        <p>Especialidade: {doctor.especialidade}</p>
                        <p>CRM: {doctor.crm}</p>
                        <p>Telefone: {doctor.telefone}</p>
                        <p>Email: {doctor.email}</p>
                    </div>
                ))
            )}
        </div>
    );
}

export default DoctorsList;