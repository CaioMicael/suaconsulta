import { useState } from "react";

interface InputProps {
    labelDescription: string;
    type: string;
    name: string;
    placeholder?: string;
    value?: string;
    onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
    className?: string;
}

const Input = ({ labelDescription, type, name, placeholder = "", value, onChange, className }: InputProps) => {
    return (
        <div>
            <label htmlFor={name}>{labelDescription}</label>
            <input 
                type={type} 
                id={name} 
                name={name} 
                placeholder={placeholder}
                value={value}
                onChange={onChange}
                className={className} />
        </div>
    );
};
export default Input;