import { useState } from "react";
import DoctorSchedule from "./DoctorSchedule";
import ButtonDefault from "./ButtonDefault";
import ButtonClose from "./ButtonClose";
import ButtonAgendar from "./ButtonAgendar";

interface ButtonSeeDoctorScheduleProps {
    labelDescription: string;
    name: string;
    type: "button" | "submit" | "reset";
    DoctorId: number;
    nome: string;
    especialidade: string;
    crm: string;
    telefone: string;
    email: string;
}

const ButtonSeeDoctorSchedule = ({ labelDescription, name, type, DoctorId, nome, especialidade, crm, telefone, email }: ButtonSeeDoctorScheduleProps) => {
    const [showOverlay, setShowOverlay] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    const toggleOverlay = () => {
        setShowOverlay(!showOverlay);
      };

    const handleClick = () => {
        setIsLoading(true);
        setShowOverlay(true);
        setTimeout(() => {
            setIsLoading(false);
        }, 1000); // Simula um delay de 1 segundos
    };

    return (
        <div>
            <ButtonDefault 
                Description={isLoading ? "Carregando..." : labelDescription} 
                Name="button-agendar" 
                Type={type} 
                onClick={handleClick} 
                disabled={isLoading} 
            />
    
            {showOverlay && (
                <div className="fixed inset-0 flex items-center justify-center bg-black/60 z-50">
                    <div className="bg-white px-8 py-6 rounded-2xl shadow-2xl w-full max-w-md space-y-6">
                        <DoctorSchedule 
                            DoctorId={DoctorId} 
                            nome={nome} 
                            crm={crm}
                            email={email}
                            especialidade={especialidade}
                            telefone={telefone}
                        />
                        <div className="flex items-center justify-between gap-4">
                            <ButtonAgendar 
                                DoctorId={DoctorId} 
                            />
                            <ButtonClose
                                onClick={toggleOverlay}
                            />
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}

export default ButtonSeeDoctorSchedule;