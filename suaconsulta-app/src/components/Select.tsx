import React from "react";

interface SelectProps {
    labelDescription: string;
    name: string;
    id?: string;
    labelClass?: string;
    class?: string;
    options: { value: string|number; label: string }[];
    onChange?: (event: React.ChangeEvent<HTMLSelectElement>) => void;
    onSelect?: (event: React.ChangeEvent<HTMLSelectElement>) => void;
    disabled?: boolean;
}

const Select = ({
    labelDescription,
    name,
    id,
    labelClass = "block text-sm font-medium text-gray-700 mb-1",
    class: className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-146 p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500",
    options,
    onChange,
    onSelect,
    disabled = false}
:SelectProps) => {
    return (
        <div className="w-64">
          <label className={labelClass}>{labelDescription}</label>
          <select
            id={id}
            name={name}
            className={className}
            onSelect={onSelect}
            onChange={onChange}
            disabled={disabled}
          >
            <option value="" disabled selected>
              Selecione uma opção
            </option>
            {options.map((option) => {
                return (
                    <option key={option.value} value={option.value}>
                        {option.label}
                    </option>
                );
            })}
          </select>
        </div>
    );
}

export default Select;