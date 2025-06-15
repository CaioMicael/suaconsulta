import { useAlert } from "../providers/AlertProvider";
import api from "../services/api";

interface ButtonLoginProps {
    Description: string;
    Name: string;
    disabled?: boolean;
    className?: string;
}

/**
 * Componente de botão para login.
 * @param {ButtonLoginProps} props - ButtonLoginProps.
 * @returns 
 */
const ButtonLogin = ({Description, Name, disabled = false, className}: ButtonLoginProps) => {
    const { showAlert } = useAlert();

    const handleSubmitLogin = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        const form = event.currentTarget.closest('form');
        if (form) {
            api.post('Auth/Login', {
                email: form.email.value,
                password: form.password.value
            }).then(response => {
                showAlert("Login feito com sucesso!", "success");
            }).catch(error => {

            })
        }
    }

    return (
        <button
            type="submit"
            name={Name}
            onClick={handleSubmitLogin}
            disabled={disabled}
            className={className ? className : "text-white bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-blue-300 dark:focus:ring-blue-800 shadow-lg shadow-blue-500/50 dark:shadow-lg dark:shadow-blue-800/80 font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2"}>
            {Description}
        </button>
    )
}

export default ButtonLogin;