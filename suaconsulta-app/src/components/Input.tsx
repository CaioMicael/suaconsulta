import { useState } from "react";

interface InputProps {
    labelDescription: string;
    type: string;
    name: string;
    placeholder?: string; // Deixamos opcional usando "?"
}

const Input = ({ labelDescription, type, name, placeholder = "" }: InputProps) => {
    return (
        <div>
            <label htmlFor={name}>{labelDescription}</label>
            <input type={type} id={name} name={name} placeholder={placeholder} />
        </div>
    );
};
export default Input;