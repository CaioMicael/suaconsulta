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
    const [isLoading, setIsLoading] = useState(false);

    const handleClick = () => {
        setIsLoading(true);
        <DoctorSchedule DoctorId={DoctorId} />
        setTimeout(() => {
            setIsLoading(false);
        }, 2000); // Simula um delay de 2 segundos
    };

    return (
        <div className="items-center justify-center rounded-md transition-all font-medium 2xl:px-6 2xl:py-3.5 px-5 py-3 bg-blurple overflow-hidden hover:bg-blurple/80 active:bg-blurple/90 focus:outline-none focus:ring-2 focus:ring-blurple/50 focus:ring-offset-2 focus:ring-offset-gray-100 shadow-md shadow-blurple/30">
            <button type={type} name={name} onClick={handleClick} disabled={isLoading}>
                {isLoading ? "Carregando..." : labelDescription}
            </button>
        </div>
    );
}

export default ButtonAgendar;