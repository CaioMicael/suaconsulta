import { useState } from "react";
import PasswordInput from './PasswordInput';
import Input from './Input';

const SignUpMedico = () => {
    //const [showSignUp, setShowSignUp] = useState(false);

    return (
        <div className='SignUp-form-container'>
            <div className='SignUp-form'>
                <h2>Cadastrar-se como médico</h2>
                <Input type='text' labelDescription='E-mail' name='email' />
                <Input type='text' labelDescription='Nome' name='name' />
                <Input type='text' labelDescription='CRM' name='crm' />
                <PasswordInput />
            </div>
        </div>
    )
}

export default SignUpMedico;