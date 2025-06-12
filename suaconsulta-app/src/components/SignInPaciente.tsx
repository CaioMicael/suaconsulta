import PasswordInput from './PasswordInput';
import Input from './Input';

const SignInPaciente = () => {

    return (
        <div className='SignIn-form-container'>
            <div className='SignIn-form'>
                <h2>Entrar como paciente</h2>
                <Input type='text' labelDescription='E-mail' name='email' />
                <PasswordInput />
            </div>
        </div>
    )
}

export default SignInPaciente;