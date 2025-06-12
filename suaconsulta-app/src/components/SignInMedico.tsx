import PasswordInput from './PasswordInput';
import Input from './Input';

const SignInMedico = () => {

    return (
        <div className='SignIn-form-container'>
            <div className='SignIn-form'>
                <h2>Entrar como Médico</h2>
                <Input type='text' labelDescription='E-mail' name='email' />
                <PasswordInput />
            </div>
        </div>
    )
}

export default SignInMedico;