import PasswordInput from './PasswordInput';
import Input from './Input';
import api from '../services/api';
import { useAlert } from '../providers/AlertProvider';
import ButtonLogin from './ButtonLogin';

/**
 * Formulário de login para pacientes.
 * @returns JSX.Element
 */
const SignInPaciente = () => {
    const { showAlert } = useAlert();

    return (
        <div className='SignIn-form-container'>
            <div className='SignIn-form'>
                <h2>Entrar como paciente</h2>
                <Input type='text' labelDescription='E-mail' name='email' />
                <PasswordInput />
            </div>
            <ButtonLogin
                Description='Entrar'
                Name='button-entrar'
                disabled={false}
            />
        </div>
    )
}

export default SignInPaciente;