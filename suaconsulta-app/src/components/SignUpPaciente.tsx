import PasswordInput from './PasswordInput';
import Input from './Input';
import ButtonSignUpPaciente from './ButtonSignUpPaciente';
import { useEffect, useState } from 'react';
import { Patient } from '../interfaces';

/**
 * Componente de cadastro de paciente
 * @returns JSX.Element
 */
const SignUpPaciente: React.FC = () => {
    const [patient, setPatient] = useState<Patient>({
        name: "",
        email: "",
        birthday: "",
        phone: "",
        city: "",
        state: "",
        country: ""
    });
    const [password, setPassword] = useState('');

    const handleInputChange = (field: keyof Patient, value: string) => {
        setPatient(prev => ({
            ...prev,
            [field]: value
        }));
    };

    return (
        <div className='SignUp-form-container'>
            <div className='SignUp-form'>
                <h2>Cadastrar-se como paciente</h2>
                <Input type='text' labelDescription='E-mail' name='email' onChange={(e) => handleInputChange('email', e.target.value)} />
                <Input type='text' labelDescription='Nome' name='name' onChange={(e) => handleInputChange('name', e.target.value)} />
                <Input type='date' labelDescription='Data Nascimento' name='birthday' onChange={(e) => handleInputChange('birthday', e.target.value)} />
                <Input type='telephone' labelDescription='Número Celular' name='phone' onChange={(e) => handleInputChange('phone', e.target.value)} />
                <Input type='text' labelDescription='País' name='country' onChange={(e) => handleInputChange('country', e.target.value)} />
                <Input type='text' labelDescription='Estado' name='state' onChange={(e) => handleInputChange('state', e.target.value)} />
                <Input type='text' labelDescription='Cidade' name='city' onChange={(e) => handleInputChange('city', e.target.value)} />
                <PasswordInput onChange={(e) => setPassword(e.target.value)} />
            </div>
            <ButtonSignUpPaciente
                Description='Cadastrar'
                Name='signUp'
                patient={patient}
                password={password}
            />
        </div>
    )
}

export default SignUpPaciente;