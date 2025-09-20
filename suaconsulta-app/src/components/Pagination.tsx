import React from "react";

interface PaginationProps {
    currentPage: number;
    totalPages: number;
    totalCount: number;
    pageSize: number;
    onPageChange: (page: number) => void;
    disabled?: boolean;
}

const Pagination: React.FC<PaginationProps> = ({
    currentPage,
    totalPages,
    totalCount,
    pageSize,
    onPageChange,
    disabled = false
}) => {
    // Calcular itens sendo mostrados
    const startItem = (currentPage - 1) * pageSize + 1;
    const endItem = Math.min(currentPage * pageSize, totalCount);

    // Gerar números das páginas para mostrar
    const getPageNumbers = () => {
        const pages: (number | string)[] = [];
        const maxPagesToShow = 5;

        // Mostrar todas as páginas se forem poucas
        if (totalPages <= maxPagesToShow) {
            for (let i = 1; i <= totalPages; i++) {
                pages.push(i);
            }
        } else {
            // Lógica para mostrar páginas com ellipsis
            if (currentPage <= 3) {
                for (let i = 1; i <= 4; i++) {
                    pages.push(i);
                }
                pages.push('...');
                pages.push(totalPages);
            } else if (currentPage >= totalPages - 2) {
                pages.push(1);
                pages.push('...');
                for (let i = totalPages - 3; i <= totalPages; i++) {
                    pages.push(i);
                }
            } else {
                pages.push(1);
                pages.push('...');
                for (let i = currentPage - 1; i <= currentPage + 1; i++) {
                    pages.push(i);
                }
                pages.push('...');
                pages.push(totalPages);
            }
        }

        return pages;
    };

    const handlePageClick = (page: number | string) => {
        if (typeof page === 'number' && page !== currentPage && !disabled) {
            onPageChange(page);
        }
    };

    const handlePrevious = () => {
        if (currentPage > 1 && !disabled) {
            onPageChange(currentPage - 1);
        }
    };

    const handleNext = () => {
        if (currentPage < totalPages && !disabled) {
            onPageChange(currentPage + 1);
        }
    };

    if (totalPages <= 1) {
        return (
            <div className="text-center py-4">
                <p className="text-gray-500">
                    Mostrando {totalCount} resultado{totalCount !== 1 ? 's' : ''}
                </p>
            </div>
        );
    }

    return (
        <div className="flex flex-col sm:flex-row items-center justify-between gap-4 py-6">
            {/* Informações dos resultados */}
            <div className="text-sm text-gray-700">
                Mostrando <span className="font-medium">{startItem}</span> a{" "}
                <span className="font-medium">{endItem}</span> de{" "}
                <span className="font-medium">{totalCount}</span> resultado{totalCount !== 1 ? 's' : ''}
            </div>

            {/* Controles de paginação */}
            <div className="flex items-center space-x-2">
                {/* Botão Anterior */}
                <button
                    onClick={handlePrevious}
                    disabled={currentPage === 1 || disabled}
                    className={`
                        px-3 py-2 rounded-lg text-sm font-medium transition-all duration-200
                        ${currentPage === 1 || disabled
                            ? 'bg-gray-100 text-gray-400 cursor-not-allowed'
                            : 'bg-white text-gray-700 border border-gray-300 hover:bg-gray-50 hover:border-gray-400'
                        }
                    `}
                >
                    ← Anterior
                </button>

                {/* Números das páginas */}
                <div className="flex items-center space-x-1">
                    {getPageNumbers().map((page, index) => (
                        <button
                            key={index}
                            onClick={() => handlePageClick(page)}
                            disabled={disabled}
                            className={`
                                px-3 py-2 rounded-lg text-sm font-medium transition-all duration-200
                                ${page === '...'
                                    ? 'cursor-default text-gray-400'
                                    : page === currentPage
                                        ? 'bg-blue-600 text-white shadow-lg'
                                        : disabled
                                            ? 'bg-gray-100 text-gray-400 cursor-not-allowed'
                                            : 'bg-white text-gray-700 border border-gray-300 hover:bg-gray-50 hover:border-gray-400'
                                }
                            `}
                        >
                            {page}
                        </button>
                    ))}
                </div>

                {/* Botão Próximo */}
                <button
                    onClick={handleNext}
                    disabled={currentPage === totalPages || disabled}
                    className={`
                        px-3 py-2 rounded-lg text-sm font-medium transition-all duration-200
                        ${currentPage === totalPages || disabled
                            ? 'bg-gray-100 text-gray-400 cursor-not-allowed'
                            : 'bg-white text-gray-700 border border-gray-300 hover:bg-gray-50 hover:border-gray-400'
                        }
                    `}
                >
                    Próximo →
                </button>
            </div>
        </div>
    );
};

export default Pagination;