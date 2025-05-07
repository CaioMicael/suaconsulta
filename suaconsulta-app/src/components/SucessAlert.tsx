interface SucessAlertProps {
    message?: string;
    onClose: () => void;
}

const SucessAlert = ({ message ="Inserido com sucesso!", onClose }: SucessAlertProps) => {
    return (
        <div className="p-4 mb-4 text-sm text-green-800 rounded-lg bg-green-50 dark:bg-gray-800 dark:text-green-400" role="alert">
            <span className="font-medium">Success alert!</span> {message}
        </div>
    )
}
