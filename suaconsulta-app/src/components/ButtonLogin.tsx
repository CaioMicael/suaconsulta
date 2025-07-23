import { Navigate, useNavigate } from 'react-router-dom';
import { useAlert } from "../providers/AlertProvider";
import api from "../services/api";
import { UserType } from '../enum/EnumTypeUser';

interface ButtonLoginProps {
    Description: string;
    Name: string;
    disabled?: boolean;
    className?: string;
    email: string;
    password: string;
}

/**
 * Componente de botão para login.
 * @param {ButtonLoginProps} props - ButtonLoginProps.
 * @returns 
 */
const ButtonLogin = ({Description, Name, disabled = false, className, email, password}: ButtonLoginProps) => {
    const { showAlert } = useAlert();
    const navigate = useNavigate();

    const handleSubmitLogin = (event: React.MouseEvent<HTMLButtonElement>) => {
        api.post('Auth/Login', {
            email,
            password
        }).then(response => {
            showAlert("Login feito com sucesso!", "success");
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('role', response.data.role);
            response.data.role == UserType.PATIENT ? navigate('/') : navigate('/doctorProfile');
        }).catch(error => {
            showAlert("Email ou Senha Inválidos!", "warning");
        });
    }

    return (
        <button
            type="button"
            name={Name}
            onClick={handleSubmitLogin}
            disabled={disabled}
            className={className ? className : "text-white bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-blue-300 dark:focus:ring-blue-800 shadow-lg shadow-blue-500/50 dark:shadow-lg dark:shadow-blue-800/80 font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2"}>
            {Description}
        </button>
    )
}

export default ButtonLogin;