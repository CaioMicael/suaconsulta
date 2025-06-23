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

export interface Doctor {
    id?: string;
    name: string;
    email: string;
    specialty: string;
    crm: string;
    phone: string;
    city: string;
    state: string;
    country: string;
    createdAt?: Date;
    updatedAt?: Date;
}

export interface Consultation {
    id?: string;
    patientId: string;
    doctorId: string;
    date: string;
    time: string;
    status: 'scheduled' | 'completed' | 'cancelled';
    notes?: string;
}

export interface ApiResponse<T> {
    data: T;
    message: string;
    success: boolean;
}

export interface LoginCredentials {
    email: string;
    password: string;
}

export interface User {
    id: string;
    email: string;
    name: string;
    role: 'patient' | 'doctor' | 'admin';
    token?: string;
}