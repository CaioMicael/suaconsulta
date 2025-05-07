import { useState } from "react";

interface InputProps {
    labelDescription: string;
    type: string;
    name: string;
    placeholder?: string;
    value?: any;
    disabled?: boolean;
    onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
    className?: string;
}

const Input = ({ labelDescription, type, name, placeholder = "", value, disabled, onChange, className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" }: InputProps) => {
    return (
        <div>
            <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white" htmlFor={name}>{labelDescription}</label>
            <input 
                type={type} 
                id={name} 
                name={name} 
                placeholder={placeholder}
                value={value}
                onChange={onChange}
                disabled={disabled}
                className={className} />
        </div>
    );
};
export default Input;