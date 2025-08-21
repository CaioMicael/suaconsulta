import { UserType } from "./enum/EnumTypeUser";

export interface Patient {
    id?: number;
    name: string;
    email: string;
    birthday: string;
    phone: string;
    city: string;
    state: string;
    country: string;
}

/**
 * @deprecated use DoctorApi
 */
export interface Doctor {
    id?: number;
    nome: string;
    email: string;
    especialidade: string;
    crm: string;
    telefone: string;
    cidade: string;
    estado: string;
    pais: string;
}

export interface DoctorApi {
    id?: number;
    name: string;
    email: string;
    specialty: string;
    crm: string;
    phone: string;
    city: string;
    state: string;
    country: string;
}

export interface Consultation {
    id?: number;  
    patientId: number;  
    doctorId: number;   
    date: string;
    time: string;
    status: number;
    notes?: string;
}

export interface ApiResponse<T> {
    data: T;
    message: string;
    status: number;
    success?: boolean;
}

export interface LoginCredentials {
    email: string;
    password: string;
}

export interface User {
    id: number;  
    email: string;
    name: string;
    role: UserType;
    token?: string;
}

/**
 * Interface específica para a resposta da API que retorna informações do usuário
 */
export interface UserInformationResponse {
    user: {
        id: number;
        externalId: number;
        typeUser: UserType;
        mail: string;
    };
    patient?: Patient;
    doctor?: DoctorApi;
}

export interface ApiError {
    message: string;
    code?: string;
    details?: string;
}

/**
 * Interface para utilização de datas em componentes.
 */
export interface DateOption {
    startTime: string;
    shortDate: string;
    hour: string;
    minute: string;
}

export interface ConsultationData {
    id: number;
    date: string;
    status: number;
    patientId: number;
    doctorId: number;
    description: string;
    patient: {
        id: number;
        name: string;
        email: string;
    };
    doctor: {
        id: number;
        name: string;
        specialty: string;
        crm: string;
        email: string;
    };
}

/**
 * Interface padrão para consultas paginadas.
 */
export interface PagedConsult<T> {
    items:Array<T>;
    pageNumber:Number;
    pageSize:Number;
    totalCount:Number;
    totalPages:Number;
}