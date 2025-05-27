import { useState } from "react";
import ButtonDefault from "../components/ButtonDefault";

const DoctorProfile = () => {
    const [doctor, setDoctor] = useState({
        name: "",
        email: "",
        specialty: "",
        phone: "",
        city: "",
        state: "",
        country: ""
    });

    return (
        <div className="max-w-2xl mx-auto p-6 bg-white rounded-lg shadow-md">
            <h1 className="text-2xl font-bold mb-6 text-center">Seu Perfil</h1>
            
            <div className="flex justify-center mb-6">
                <img className="w-32 h-32 rounded-full" src="/download.jpg" alt="Foto do paciente" />
            </div>
            
            <form >
                <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                    <div className="mb-4">
                        
                    </div>

                    <div className="mb-4">
                                                
                    </div>

                    <div className="mb-4">
                                                  
                    </div>
                    
                    <div className="mb-4">
                                                  
                    </div>
                    
                    <div className="mb-4">
                        
                    </div>
                    
                    <div className="mb-4">
                        
                    </div>
                    
                    <div className="mb-4 md:col-span-2">

                    </div>
                </div>
                
                <div className="mt-6 flex justify-end">
                    <ButtonDefault
                        Description="Salvar Alterações"
                        Name="button-salvar"
                        Type="button"
                        disabled={false}
                    />
                </div>
            </form>
        </div>
    )
}

export default DoctorProfile;