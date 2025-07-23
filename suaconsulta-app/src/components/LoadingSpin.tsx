import React, { useEffect, useState } from "react";

interface LoadingSpinProps {
    textSpinner?: string;
}

/**
 * Componente de loading spinner, para carregamento de tela/dados.
 * @returns JSX.Element
 */
const LoadingSpin = ({ textSpinner }: LoadingSpinProps) => {

    return (
        <div className="col-span-full flex flex-col items-center justify-center py-16">
            <div className="animate-spin rounded-full h-16 w-16 border-b-2 border-blue-600 mb-4"></div>
            <p className="text-gray-500 text-lg">{textSpinner ? textSpinner : "Carregando..."}</p>
        </div>
    );
}

export default LoadingSpin;