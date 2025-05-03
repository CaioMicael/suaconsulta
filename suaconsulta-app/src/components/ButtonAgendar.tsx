import { useState } from "react";
import { Interface } from "readline";
import DoctorSchedule from "./DoctorSchedule";

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
        //<DoctorSchedule DoctorId={DoctorId} />
        setTimeout(() => {
            setIsLoading(false);
        }, 1000); // Simula um delay de 1 segundos
    };

    return (
        <div className="justify-center border-2 border-gray-700 focus:border-pink-600 rounded-md shadow-lg">
            <button type={type} name={name} onClick={handleClick} disabled={isLoading}>
                {isLoading ? "Carregando..." : labelDescription}
            </button>

        {showOverlay && (
            <div className="overlay fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
                <div className="bg-white p-4 rounded shadow-lg">
                    <DoctorSchedule DoctorId={DoctorId} nome={name} />
                    <button onClick={toggleOverlay} className="mt-4 text-red-500">Fechar</button>
                </div>
            </div>
        )}
        </div>
    );
}

export default ButtonAgendar;