import { useState } from "react";
import DoctorSchedule from "./DoctorSchedule";
import ButtonDefault from "./ButtonDefault";
import ButtonAgendar from "./ButtonAgendar";

interface ButtonSeeDoctorScheduleProps {
    labelDescription: string;
    name: string;
    type: "button" | "submit" | "reset";
    DoctorId: number;
}

const ButtonSeeDoctorSchedule = ({ labelDescription, name, type, DoctorId }: ButtonSeeDoctorScheduleProps) => {
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
        <div className="justify-center border-gray-700 rounded-md shadow-lg">
            <ButtonDefault 
                Description={isLoading ? "Carregando..." : labelDescription} 
                Name="button-agendar" 
                Type={type} 
                onClick={handleClick} 
                disabled={isLoading} 
            />
    
            {showOverlay && (
                <div className="overlay fixed inset-0 flex flex-row items-center justify-center bg-black bg-opacity-50 z-50">
                    <div className="bg-white p-4 rounded shadow-lg">
                        <DoctorSchedule 
                            DoctorId={DoctorId} 
                            nome={name} 
                        />
                        <div className="flex flex-row items-center justify-center">
                            <ButtonAgendar 
                                DoctorId={DoctorId} 
                            />
                            <ButtonDefault 
                                Description="Fechar" 
                                Name="button-agendar" 
                                Type="button" 
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