import { Navigate, useNavigate } from 'react-router-dom';
import { useAlert } from "../providers/AlertProvider";
import api from "../services/api";
import { Patient } from '../interfaces';
import { UserType } from '../enum/EnumTypeUser';

interface ButtonSignUpPacienteProps {
    Description: string;
    Name: string;
    disabled?: boolean;
    className?: string;
    patient: Patient
    password: string;
}

/**
 * Componente de botão de cadastrar paciente.
 * @param {ButtonSignUpPacienteProps} ButtonSignUpPacienteProps
 * @returns JSX.Element
 */
const ButtonSignUpPaciente = ({Description, Name, password, className, disabled = false, patient}: ButtonSignUpPacienteProps) => {
    const { showAlert } = useAlert();
    const navigate = useNavigate();

    const handleSubmitSignUpPaciente = async (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        if (password.length < 6) {
            showAlert("A senha deve ter pelo menos 6 caracteres", "warning");
            return;
        }

        await api.post('Auth/SignUp', {
            mail: patient.email,
            pass: password,
            typeUser: UserType.PATIENT,
        })
        .then(response => {
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('role', response.data.role);
            const {id, ...patientWithOutId} = patient;
            api.post('Patient/CreatePatient', {
                ...patientWithOutId
            })
            .then(response => {
                showAlert("Cadastrado com sucesso, estamos logando no sistema para você", "success");
                navigate('/');
            })
            .catch(error => {
                showAlert("Ocorreu um erro: "+ error, "error");
            });
        })
        .catch(error => {
            if (error.status == 409) {
                showAlert("Já existe um usuário cadastrado com este e-mail", "warning");
                return;
            }
            showAlert("Ocorreu um erro: "+ error, "error");
        });
    }

    return (
        <button
            type="submit"
            name={Name}
            onClick={handleSubmitSignUpPaciente}
            disabled={disabled}
            className={className ? className : "text-white bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-blue-300 dark:focus:ring-blue-800 shadow-lg shadow-blue-500/50 dark:shadow-lg dark:shadow-blue-800/80 font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2"}>
            {Description}
        </button>
    )
}

export default ButtonSignUpPaciente