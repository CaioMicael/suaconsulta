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
        <button type={type} name={name} onClick={handleClick} disabled={isLoading}>
            {isLoading ? "Carregando..." : labelDescription}
        </button>
    );
}

export default ButtonAgendar;