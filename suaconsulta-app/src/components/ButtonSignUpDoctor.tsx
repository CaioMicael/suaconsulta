import { UserType } from "../enum/EnumTypeUser";
import { Doctor, DoctorApi } from "../interfaces";
import { useAlert } from "../providers/AlertProvider";
import api from "../services/api";
import apiAuth from "../services/apiAuth";
import { useNavigate } from "react-router-dom";


interface ButtonSignUpDoctorProps {
    Description: string;
    Name: string;
    disabled?: boolean;
    className?: string;
    doctor: DoctorApi
    password: string;
}

/**
 * Componente de botão de cadastrar médicos.
 * @param {ButtonSignUpDoctorProps} ButtonSignUpPacienteProps
 * @returns JSX.Element
 */
const ButtonSignUpDoctor = ({Description, Name, password, className, disabled = false, doctor}:ButtonSignUpDoctorProps ) => {
    const { showAlert } = useAlert();
    const navigate = useNavigate();

    const handleSubmitSignUpDoctor = async (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        if (password.length < 6) {
            showAlert("A senha deve ter pelo menos 6 caracteres", "warning");
            return;
        }

        await apiAuth.post('/SignUp', {
            mail: doctor.Email,
            pass: password,
            typeUser: UserType.DOCTOR,
        })
        .then(response => {
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('role', response.data.role);
            const {id, ...doctorWithOutId} = doctor;
            api.post('Doctor/CreateDoctor', {
                ...doctorWithOutId
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
            showAlert("Ocorreu um erro: "+ error.description, "error");
        });
    }

    return (
        <button
            type="submit"
            name={Name}
            onClick={handleSubmitSignUpDoctor}
            disabled={disabled}
            className={className ? className : "text-white bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-blue-300 dark:focus:ring-blue-800 shadow-lg shadow-blue-500/50 dark:shadow-lg dark:shadow-blue-800/80 font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2"}>
            {Description}
        </button>
    );
}

export default ButtonSignUpDoctor;