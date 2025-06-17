interface WarningAlertProps {
    message?: string;
    onClose?: () => void;
}

/**
 * Mensagem de alerta. Deve ser usado para alertar o usuário sobre algo importante.
 * @param message Mensagem que será exibida no alerta
 * @param onClose Função que será chamada ao fechar o alerta
 * @returns JSX.Element
 */
const WarningAlert = ({ message = "Atenção!", onClose }: WarningAlertProps) => {
    return (
        <div className="p-4 mb-4 text-sm text-yellow-800 rounded-lg bg-yellow-50 dark:bg-gray-800 dark:text-yellow-300" role="alert">
            <span className="font-medium">Atenção! </span> {message}
        </div>
    )
}

export default WarningAlert;