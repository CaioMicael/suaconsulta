import { useState } from "react";

interface InputProps {
    labelDescription: string;
    type: string;
    name: string;
    placeholder?: string;
    value?: any;
    defaultValue?: any;
    disabled?: boolean;
    required?: boolean;
    onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
    onSelect?: (event: React.ChangeEvent<HTMLInputElement>) => void;
    className?: string;
    size?: number;
}


const Input = ({ 
                    labelDescription, 
                    type, 
                    name, 
                    placeholder = "", 
                    value, 
                    defaultValue,
                    disabled = false, 
                    onChange, 
                    onSelect,
                    required = false, 
                    size = 69,
                    className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" 
                }: InputProps) => {
    return (
        <div className={`w-${size}`}>
            <label className='block mb-2 text-sm font-medium text-gray-900 dark:text-white' htmlFor={name}>{labelDescription}</label>
            <input 
                type={type} 
                id={name} 
                name={name} 
                placeholder={placeholder}
                value={value}
                defaultValue={defaultValue}
                onChange={onChange}
                onSelect={onSelect}
                disabled={disabled}
                required={required}
                className={disabled ? "w-full px-4 py-2 rounded-lg border border-gray-400 bg-gray-200 text-gray-600 placeholder-gray-500 shadow-sm cursor-not-allowed opacity-100 dark:bg-gray-700 dark:border-gray-600 dark:text-gray-400" : className} />
        </div>
    );
};

export default Input;