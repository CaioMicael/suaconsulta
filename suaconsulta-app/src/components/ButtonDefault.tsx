interface ButtonProps {
    Description: string;
    Name: string;
    Type: "button" | "submit" | "reset";
    onClick?: () => void;
    disabled?: boolean;
    className?: string;
}

const ButtonDefault = ({ Description, Name, Type, onClick, disabled, className }: ButtonProps) => {
    return (
        <button
            type={Type}
            name={Name}
            onClick={onClick}
            disabled={disabled}
            className={className ? className : "text-white bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-blue-300 dark:focus:ring-blue-800 shadow-lg shadow-blue-500/50 dark:shadow-lg dark:shadow-blue-800/80 font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2"}>
            {Description}
        </button>
    )
}

export default ButtonDefault;