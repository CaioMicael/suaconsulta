import PasswordInput from './PasswordInput';
import Input from './Input';
import { useAlert } from '../providers/AlertProvider';
import ButtonLogin from './ButtonLogin';
import { useState } from 'react';

/**
 * Formulário de login para pacientes.
 * @returns JSX.Element
 */
const SignIn = () => {
    const { showAlert } = useAlert();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    return (
        <div className='SignIn-form-container'>
            <div className='SignIn-form'>
                <Input type='text' labelDescription='E-mail' name='email' onChange={e => setEmail(e.target.value)} />
                <PasswordInput onChange={e => setPassword(e.target.value)} />
            </div>
            <ButtonLogin
                email={email}
                password={password}
                Description='Entrar'
                Name='button-entrar'
                disabled={false}
            />
        </div>
    )
}

export default SignIn;