import { useState } from "react";
import ButtonDefault from "./ButtonDefault";
import { Doctor } from "@/interfaces";

interface ButtonSeeDoctorProfileProps {
    doctor: Doctor;
    description?: string;
    name?: string;
    onClick?: () => void;
}

const ButtonSeeDoctorProfile = ({ doctor, description = 'Ver Perfil', name = 'button-see-doctor-profile', onClick }: ButtonSeeDoctorProfileProps) => {
    const [isLoading, setIsLoading] = useState(false);

    return (
        <div>
            <ButtonDefault
                Description = {isLoading ? "Carregando..." : description}
                Name = {name}
                Type = {"button"}
            ></ButtonDefault>
        </div>
    )
}

export default ButtonSeeDoctorProfile;