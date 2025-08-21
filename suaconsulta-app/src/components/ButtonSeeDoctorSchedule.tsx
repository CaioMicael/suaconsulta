import { useState } from "react";
import DoctorSchedule from "../pages/DoctorSchedule";
import ButtonDefault from "./ButtonDefault";
import ButtonClose from "./ButtonClose";
import ButtonAgendar from "./ButtonAgendar";
import { DoctorApi } from "@/interfaces";

interface ButtonSeeDoctorScheduleProps {
    labelDescription: string;
    name: string;
    type: "button" | "submit" | "reset";
    doctor: DoctorApi;
}

const ButtonSeeDoctorSchedule = ({ labelDescription, name, type, doctor }: ButtonSeeDoctorScheduleProps) => {
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
                            doctor={doctor} 
                        />
                        <div className="flex items-center justify-between gap-4">
                            <ButtonAgendar 
                                doctor={doctor} 
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