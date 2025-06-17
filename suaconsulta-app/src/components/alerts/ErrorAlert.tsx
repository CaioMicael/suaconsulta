interface ErrorAlertProps {
    message?: string;
    onClose?: () => void;
}

/**
 * Mensagem de erro. Deve ser usado em exceptions, não para alerta ao usuário.
 * @param message Mensagem que será exibida no alerta
 * @param onClose Função que será chamada ao fechar o alerta
 * @returns JSX.Element
 */
const ErrorAlert = ({ message = "Ocorreu um erro!", onClose }: ErrorAlertProps) => {
    return (
        <div className="p-4 mb-4 text-sm text-red-800 rounded-lg bg-red-50 dark:bg-gray-800 dark:text-red-400" role="alert">
            <span className="font-medium">Erro! </span> {message}
        </div>
    )
}

export default ErrorAlert;