import { useState } from "react";
import PasswordInput from '../components/PasswordInput';
import Input from '../components/Input';
import { DoctorApi } from "@/interfaces";
import ButtonSignUpDoctor from "../components/ButtonSignUpDoctor";

/**
 * Retorna um componente de cadastro de médico
 * @returns JSX.Elemente
 */
const SignUpMedico = () => {
    const [password, setPassword] = useState('');
    const [doctor, setDoctor] = useState<DoctorApi>({
            Name: "",
            Email: "",
            Specialty: "",
            CRM: "",
            Phone: "",
            City: "",
            State: "",
            Country: ""
    });

    const handleInputChange = (field: keyof DoctorApi, value: string) => {
            setDoctor(prev => ({
                ...prev,
                [field]: value
            }));
        };

    return (
        <div className='SignUp-form-container'>
            <div className='SignUp-form'>
                <h2>Cadastrar-se como médico</h2>
                <Input type='text' labelDescription='E-mail' name='email' onChange={(e) => handleInputChange('Email', e.target.value)} />
                <Input type='text' labelDescription='Nome' name='name' onChange={(e) => handleInputChange('Name', e.target.value)} />
                <Input type='text' labelDescription='Especialidade' name='especialidade' onChange={(e) => handleInputChange('Specialty', e.target.value)} />
                <Input type='text' labelDescription='CRM' name='crm' onChange={(e) => handleInputChange('CRM', e.target.value)} />
                <Input type='text' labelDescription='Telefone' name='telefone' onChange={(e) => handleInputChange('Phone', e.target.value)} />
                <Input type='text' labelDescription='Cidade' name='cidade' onChange={(e) => handleInputChange('City', e.target.value)} />
                <Input type='text' labelDescription='Estado' name='estado' onChange={(e) => handleInputChange('State', e.target.value)} />
                <Input type='text' labelDescription='País' name='pais' onChange={(e) => handleInputChange('Country', e.target.value)} />
                <PasswordInput onChange={(e) => setPassword(e.target.value)} />
            </div>
            <ButtonSignUpDoctor
                Description="Cadastrar-se"
                Name="signupdoctorbutton"
                doctor={doctor}
                password={password}
            />
        </div>
    )
}

export default SignUpMedico;