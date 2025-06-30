import { useState } from "react";
import ButtonDefault from "./ButtonDefault";
import { Doctor } from "@/interfaces";

/**
 * Componente para agendar uma consulta com o médico.
 * @property {Doctor} doctor - Informações do médico.
 * @returns JSX.Element
 */
const ButtonAgendar = ({ doctor }: { doctor: Doctor }) => {
    const [isLoading, setIsLoading] = useState(false);

    const handleClick = () => {
        setIsLoading(true);
        setTimeout(() => {
            setIsLoading(false);
        }, 1000); // Simula um delay de 1 segundos
    };

    return (
        <div>
            <ButtonDefault 
                Description={isLoading ? "Carregando..." : "Agendar"} 
                Name="button-agendar" 
                Type="button"
                onClick={handleClick} 
                disabled={isLoading} 
            />
        </div>
    );
}

export default ButtonAgendar;