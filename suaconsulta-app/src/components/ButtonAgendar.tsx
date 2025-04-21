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
        <div className="items-center justify-center rounded-md transition-all font-medium 2xl:px-6 2xl:py-3.5 px-5 py-3 bg-blurple overflow-hidden hover:bg-blurple/80 active:bg-blurple/90 focus:outline-none focus:ring-2 focus:ring-blurple/50 focus:ring-offset-2 focus:ring-offset-gray-100 shadow-md shadow-blurple/30">
            <button type={type} name={name} onClick={handleClick} disabled={isLoading}>
                {isLoading ? "Carregando..." : labelDescription}
            </button>

        {showOverlay && (
            <div className="overlay fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
                <div className="bg-white p-4 rounded shadow-lg">
                    <DoctorSchedule DoctorId={DoctorId} />
                    <button onClick={toggleOverlay} className="mt-4 text-red-500">Fechar</button>
                </div>
            </div>
        )}
        </div>
    );
}

export default ButtonAgendar;