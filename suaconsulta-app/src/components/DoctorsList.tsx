import React, { useEffect, useState } from "react";
import ButtonSeeDoctorSchedule from "./ButtonSeeDoctorSchedule";
import ButtonSeeDoctorProfile from "./ButtonSeeDoctorProfile";
import ButtonDefault from "./ButtonDefault";
import SearchInput from "./SearchInput";
import Pagination from "./Pagination";
import api from "../services/api";
import { DoctorApi } from "../interfaces";
import LoadingSpin from "./LoadingSpin";
import NotFoundDoctors from "./NotFoundDoctors";
import { PagedConsult } from "@/types/PagedConsult";
import { useAlert } from "providers/AlertProvider";

const DoctorsList = () => {
    const [doctors, setDoctors] = useState<DoctorApi[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [searchTerm, setSearchTerm] = useState<string>("");
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [totalPages, setTotalPages] = useState<number>(0);
    const [totalCount, setTotalCount] = useState<number>(0);
    const [pageSize] = useState<number>(8); // 8 médicos por página para ficar 2x4 no grid
    const { showAlert } = useAlert();

    const loadDoctors = async (page: number = 1, search: string = "") => {
        try {
            setLoading(true);
            
            // Parâmetros da requisição
            const params = new URLSearchParams({
                Page: page.toString(),
                PageSize: pageSize.toString(),
            });
            
            // Adicionar parâmetro de pesquisa se fornecido
            if (search.trim()) {
                params.append('name', search.trim());
            }
            
            const response = await api.get<PagedConsult<DoctorApi>>(`Doctor/DoctorPage?${params}`);
            
            if (response.status === 204 || !response.data || response.data.items.length === 0) {
                if (search.trim()) {
                    showAlert(`Nenhum médico encontrado para "${search}"`, "warning");
                } else {
                    showAlert("Não foram encontrados médicos", "warning");
                }
                setDoctors([]);
                setTotalPages(0);
                setTotalCount(0);
                setLoading(false);
                return;
            }
            
            setDoctors(response.data.items);
            setCurrentPage(Number(response.data.pageNumber));
            setTotalPages(Number(response.data.totalPages) || Math.ceil(Number(response.data.totalCount) / pageSize));
            setTotalCount(Number(response.data.totalCount));
            setLoading(false);
        } catch (error) {
            showAlert("Erro ao buscar médicos: " + error, "error");
            setDoctors([]);
            setTotalPages(0);
            setTotalCount(0);
            setLoading(false);
        }
    }

    const handleSearch = (searchValue: string) => {
        console.log("Pesquisando por:", searchValue);
        setSearchTerm(searchValue);
        setCurrentPage(1); // Reset para primeira página ao pesquisar
        loadDoctors(1, searchValue);
    };

    const handlePageChange = (page: number) => {
        setCurrentPage(page);
        loadDoctors(page, searchTerm);
    };

    const handleRefresh = () => {
        setSearchTerm("");
        setCurrentPage(1);
        loadDoctors(1, "");
    };
    
    useEffect(() => {
        loadDoctors();
    }, []);

    return (
        <div className="min-h-screen bg-gradient-to-br from-blue-50 via-white to-purple-50 p-6">
            <div className="max-w-7xl mx-auto">
                <div className="text-center mb-8">
                    <h1 className="text-4xl font-bold text-gray-800 mb-4">
                        Nossos <span className="text-blue-600">Médicos</span>
                    </h1>
                    <p className="text-gray-600 text-lg mb-6">
                        Encontre o profissional ideal para suas necessidades de saúde
                    </p>
                    
                    {/* Barra de controles: Pesquisa + Atualizar */}
                    <div className="flex flex-col sm:flex-row items-center justify-center gap-4 mb-8">
                        <SearchInput 
                            placeholder="Pesquisar médicos por nome..."
                            onSearch={handleSearch}
                            disabled={loading}
                        />
                        <ButtonDefault
                            Description="🔄 Atualizar Lista" 
                            Name="buscar-dados" 
                            Type="button"
                            onClick={handleRefresh}
                        />
                    </div>
                </div>


                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6 min-h-[400px]">
                    {loading ? (
                        <div className="col-span-full flex justify-center">
                            <LoadingSpin textSpinner="Carregando médicos..." />
                        </div>
                    ) : doctors.length === 0 ? (
                        <NotFoundDoctors />
                    ) : (
                        doctors.map((doctor) => (
                            <div 
                                key={doctor.id}
                                className="bg-white rounded-2xl shadow-lg hover:shadow-xl transition-all duration-300 transform hover:-translate-y-2 border border-gray-100 overflow-hidden group"
                            >
                                <div className="bg-gradient-to-r from-blue-500 to-purple-600 px-6 py-8 text-center relative">
                                    <div className="absolute inset-0 bg-black opacity-10"></div>
                                    <div className="relative">
                                        <img 
                                            className="w-20 h-20 rounded-full mx-auto border-4 border-white shadow-lg object-cover" 
                                            src="/download.jpg" 
                                            alt={`Dr. ${doctor.name}`}
                                        />
                                        <h3 className="text-white font-bold text-xl mt-3">
                                            Dr. {doctor.name}
                                        </h3>
                                        <span className="inline-block bg-white bg-opacity-20 text-white text-sm px-3 py-1 rounded-full mt-2">
                                            {doctor.specialty}
                                        </span>
                                    </div>
                                </div>

                                <div className="p-6">
                                    <div className="space-y-3">
                                        <div className="flex items-center text-gray-600">
                                            <span className="text-blue-500 mr-2">🏥</span>
                                            <span className="text-sm font-medium">CRM:</span>
                                            <span className="ml-auto font-semibold">{doctor.crm}</span>
                                        </div>
                                        <div className="flex items-center text-gray-600">
                                            <span className="text-green-500 mr-2">📞</span>
                                            <span className="text-sm font-medium">Telefone:</span>
                                            <span className="ml-auto font-semibold text-sm">{doctor.phone}</span>
                                        </div>
                                        <div className="flex items-center text-gray-600">
                                            <span className="text-purple-500 mr-2">✉️</span>
                                            <span className="text-sm font-medium">Email:</span>
                                            <span className="ml-auto font-semibold text-sm truncate max-w-32" title={doctor.email}>
                                                {doctor.email}
                                            </span>
                                        </div>
                                    </div>


                                    <div className="flex gap-2 mt-6 pt-4 border-t border-gray-100">
                                        <div className="flex-1">
                                            <ButtonSeeDoctorSchedule 
                                                labelDescription="📅 Horários" 
                                                name="button-agendar" 
                                                type="button" 
                                                doctor={doctor}
                                            />
                                        </div>
                                        <div className="flex-1">
                                            <ButtonSeeDoctorProfile 
                                                doctor={doctor}
                                                description="👨‍⚕️ Perfil" 
                                                name="button-see-doctor-profile" 
                                            />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        ))
                    )}
                </div>

                {/* Footer Section */}
                {!loading && doctors.length > 0 && (
                    <div className="mt-8 border-t border-gray-200">
                        <Pagination
                            currentPage={currentPage}
                            totalPages={totalPages}
                            totalCount={totalCount}
                            pageSize={pageSize}
                            onPageChange={handlePageChange}
                            disabled={loading}
                        />
                    </div>
                )}

                {/* Mensagem quando não há resultados */}
                {!loading && doctors.length === 0 && searchTerm && (
                    <div className="text-center mt-8">
                        <p className="text-gray-500">
                            Nenhum médico encontrado para "<strong>{searchTerm}</strong>"
                        </p>
                        <button 
                            onClick={handleRefresh}
                            className="mt-2 text-blue-600 hover:text-blue-800 transition-colors"
                        >
                            Limpar pesquisa
                        </button>
                    </div>
                )}
            </div>
        </div>
    );
}

export default DoctorsList;