import PasswordInput from './PasswordInput';
import Input from './Input';

const SignUpPaciente = () => {

    return (
        <div className='SignUp-form-container'>
            <div className='SignUp-form'>
                <h2>Cadastrar-se como paciente</h2>
                <Input type='text' labelDescription='E-mail' name='email' />
                <Input type='text' labelDescription='Nome' name='name' />
                <PasswordInput />
            </div>
        </div>
    )
}

export default SignUpPaciente;