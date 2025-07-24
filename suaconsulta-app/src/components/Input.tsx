import { useState, useEffect } from "react";
import { IndexInfo } from "typescript";

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
    mask?: string;
}

/**
 * Retorna o input padrão do sistema
 * @param InputProps 
 * @returns 
 */
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
                    mask,
                    className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" 
                }: InputProps) => {
    
        const [maskedValue, setMaskedValue] = useState(value || '');

        useEffect(() => {
            if (mask && value) {
                HandleInputMask();
            }
        }, [value, mask]);

        /**
         * Atualiza a máscara do input se informado
         */            
        const HandleInputMask = () => {
            if (mask) {
                var CaracterEspecial:Array<string> = mask.replaceAll('9', '').split('');
                CaracterEspecial.map((caracter:string, idx:number) => {
                    var reg:string = "["+caracter+"]";
                    var regExp:RegExp = new RegExp(reg);
                    var posicaoCaracter:number = mask.search(regExp);
                    if (value && posicaoCaracter != -1) {
                        value = value.substring(0, posicaoCaracter) + caracter + value.substring(posicaoCaracter);
                    }
                });
            }
            setMaskedValue(value);
        }

        return (
            <div className={`w-${size}`}>
                <label className='block mb-2 text-sm font-medium text-gray-900 dark:text-white' htmlFor={name}>{labelDescription}</label>
                <input 
                    type={type} 
                    id={name} 
                    name={name} 
                    placeholder={placeholder}
                    value={maskedValue}
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