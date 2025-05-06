import { useState } from "react";
import { Interface } from "readline";
import DoctorSchedule from "./DoctorSchedule";
import ButtonDefault from "./ButtonDefault";

interface ButtonAgendarProps {
    labelDescription: string;
    name: string;
    type: "button" | "submit" | "reset";
    DoctorId: number;
}

const ButtonAgendar = ({ labelDescription, name, type, DoctorId }: ButtonAgendarProps) => {
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
        <div className="justify-center border-gray-700  rounded-md shadow-lg">
            <ButtonDefault Description={labelDescription} Name="button-agendar" Type={type} onClick={handleClick} disabled={isLoading} />
                {isLoading ? "Carregando..." : labelDescription}

        {showOverlay && (
            <div className="overlay fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
                <div className="bg-white p-4 rounded shadow-lg">
                    <DoctorSchedule DoctorId={DoctorId} nome={name} />
                    <ButtonDefault Description="Fechar" Name="button-agendar" Type="button" onClick={toggleOverlay} />
                </div>
            </div>
        )}
        </div>
    );
}

export default ButtonAgendar;