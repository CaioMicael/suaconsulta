import React, { useState } from 'react';
import PasswordInput from '../components/PasswordInput';
import Input from '../components/Input';
import SignUpMedico from '../components/SignUpMedico';
import SignUpPaciente from '../components/SignUpPaciente';
import { motion, AnimatePresence } from 'framer-motion';
import ButtonDefault from '../components/ButtonDefault';
import SignIn from '../components/SignIn';

/**
 * Tela de Login/Cadastro
 * @returns JSX.Element
 */
const LoginForm = () => {
  const [showSignMedico, setShowSignMedico] = useState(false);
  const [showSignPaciente, setShowSignPaciente] = useState(true);
  const [showSignIn, setShowSignIn] = useState(false);
  const [showSignUp, setShowSignUp] = useState(false);

  return (
    <div className="flex h-screen">
      {/* Lado esquerdo: formulário */}
      <div className="w-full md:w-1/2 flex items-center justify-center bg-white">
        <div className="w-full max-w-md p-8">
          <ButtonDefault
            Description="Cadastrar-se"
            Name="button-cadastrar"
            Type="button"
            onClick={() => (setShowSignUp(true), setShowSignIn(false))}
            disabled={false}
            className="text-3xl font-bold text-gray-800 mb-6"
          />
          <ButtonDefault
            Description="Entrar"
            Name="button-entrar"
            Type="button"
            onClick={() => (setShowSignUp(false), setShowSignIn(true))}
            disabled={false}
            className="pl-5 text-3xl font-bold text-gray-800 mb-6"
          />
          { showSignUp && !showSignIn && (
          <div className="flex gap-4 mb-6">
            <ButtonDefault
                onClick={() => (
                    setShowSignMedico(true),
                    setShowSignPaciente(false)
                )}
                Description="Médico"
                Name="Médico"
                Type="button"
                disabled={false}
            />
            <ButtonDefault
                onClick={() => (
                    setShowSignPaciente(true),
                    setShowSignMedico(false)
                )}
                Description="Paciente"
                Name="Paciente"
                Type="button"
                disabled={false}
            />
          </div>
          )}

          <div className="mt-4">
            <AnimatePresence mode="wait">
              {showSignUp && showSignMedico && !showSignPaciente && (
                <motion.div
                  key="medico"
                  initial={{ opacity: 0, y: 10 }}
                  animate={{ opacity: 1, y: 0 }}
                  exit={{ opacity: 0, y: -10 }}
                  transition={{ duration: 0.3 }}
                >
                  <SignUpMedico />
                </motion.div>
              )}
              {showSignUp && showSignPaciente && !showSignMedico && (
                <motion.div
                  key="paciente"
                  initial={{ opacity: 0, y: 10 }}
                  animate={{ opacity: 1, y: 0 }}
                  exit={{ opacity: 0, y: -10 }}
                  transition={{ duration: 0.3 }}
                >
                  <SignUpPaciente />
                </motion.div>
              )}
              {showSignIn && (
                <motion.div
                  key="signInPaciente"
                  initial={{ opacity: 0, y: 10 }}
                  animate={{ opacity: 1, y: 0 }}
                  exit={{ opacity: 0, y: -10 }}
                  transition={{ duration: 0.3 }}
                >
                  <SignIn />
                </motion.div>
              )}
            </AnimatePresence>
          </div>
        </div>
      </div>

      {/* Lado direito: visual */}
      <div className="hidden md:flex w-1/2 bg-blue-600 items-center justify-center text-white p-10">
        <div className="max-w-md">
          <h2 className="text-4xl font-bold mb-4">Explore o sistema</h2>
          <p className="text-lg leading-relaxed">
            Uma plataforma moderna para médicos e pacientes. Gerencie agendamentos, cadastros e muito mais de forma rápida e segura.
          </p>
          <div className="mt-6 flex items-center space-x-2">
            <div className="flex -space-x-2 overflow-hidden">
              <img className="inline-block h-8 w-8 rounded-full ring-2 ring-white" src="https://i.pravatar.cc/40?img=1" alt="" />
              <img className="inline-block h-8 w-8 rounded-full ring-2 ring-white" src="https://i.pravatar.cc/40?img=2" alt="" />
              <img className="inline-block h-8 w-8 rounded-full ring-2 ring-white" src="https://i.pravatar.cc/40?img=3" alt="" />
            </div>
            <span className="text-sm">Mais de <strong>15.000</strong> usuários satisfeitos</span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginForm;
