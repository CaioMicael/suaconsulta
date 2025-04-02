import React from 'react';
import { useState } from "react";
import PasswordInput from './PasswordInput';
import Input from './Input';
import SignUpMedico from './SignUpMedico';
import SignUpPaciente from './SignUpPaciente';


const LoginForm = () => {
    const [showSignUpMedico, setShowSignUpMedico] = useState(false);
    const [showSignUpPaciente, setShowSignUpPaciente] = useState(true);

    return (
        <div className='Login-form-container'>
            <div className='Login-form'>
                <h2>Login</h2>
                <Input type='text' labelDescription='E-mail' name='email' />
                <PasswordInput />
            </div>
            <div className='Login-form'>
                <h2>Cadastrar-se</h2>
                <button onClick={ () => (
                    setShowSignUpMedico(true), 
                    setShowSignUpPaciente(false)
                    )}>Médico</button>
                    
                <button onClick={ () => (
                    setShowSignUpPaciente(true),
                    setShowSignUpMedico(false)
                    )}>Paciente</button>
                { showSignUpMedico && !showSignUpPaciente && <SignUpMedico />}
                { showSignUpPaciente && !showSignUpMedico && <SignUpPaciente />}
            </div>
        </div>
    )
}

export default LoginForm;