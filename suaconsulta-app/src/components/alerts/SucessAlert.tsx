interface SucessAlertProps {
    message?: string;
    onClose?: () => void;
}

/**
 * Mensagem de sucesso. Deve ser usado para indicar que uma ação foi concluída com sucesso.
 * @param message Mensagem que será exibida no alerta
 * @param onClose Função que será chamada ao fechar o alerta
 * @returns JSX.Element
 */
const SucessAlert = ({ message ="Inserido com sucesso!", onClose }: SucessAlertProps) => {
    return (
        <div className="fixed bottom-0 w-full p-4 mb-4 text-sm text-green-800 rounded-lg bg-green-50 dark:bg-gray-800 dark:text-green-400" role="alert">
            <span className="font-medium">Sucesso!</span> {message}
        </div>
    )
}

export default SucessAlert;