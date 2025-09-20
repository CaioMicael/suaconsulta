import React, { useState } from "react";

interface SearchInputProps {
    placeholder?: string;
    onSearch: (searchTerm: string) => void;
    disabled?: boolean;
}

const SearchInput: React.FC<SearchInputProps> = ({ 
    placeholder = "Pesquisar médicos...", 
    onSearch, 
    disabled = false 
}) => {
    const [searchTerm, setSearchTerm] = useState<string>("");

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = e.target.value;
        setSearchTerm(value);
        onSearch(value);
    };

    const handleClear = () => {
        setSearchTerm("");
        onSearch("");
    };

    return (
        <div className="relative flex-1 max-w-md">
            <div className="relative">
                <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                    <span className="text-gray-400 text-xl">🔍</span>
                </div>
                <input
                    type="text"
                    value={searchTerm}
                    onChange={handleInputChange}
                    placeholder={placeholder}
                    disabled={disabled}
                    className={`
                        w-full pl-12 pr-10 py-3 
                        border border-gray-300 rounded-xl 
                        focus:ring-2 focus:ring-blue-500 focus:border-transparent 
                        transition-all duration-300
                        shadow-sm hover:shadow-md
                        ${disabled ? 'bg-gray-100 cursor-not-allowed' : 'bg-white'}
                        placeholder-gray-400 text-gray-700
                    `}
                />
                {searchTerm && (
                    <button
                        onClick={handleClear}
                        className="absolute inset-y-0 right-0 pr-3 flex items-center text-gray-400 hover:text-gray-600 transition-colors"
                        disabled={disabled}
                    >
                        <span className="text-xl">✖️</span>
                    </button>
                )}
            </div>
        </div>
    );
};

export default SearchInput;