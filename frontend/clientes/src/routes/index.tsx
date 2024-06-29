import { Routes, Route, Navigate } from "react-router-dom";
import { useDrawerContext } from "../shared/contexts";
import { useEffect } from "react";
import { Dashboard, ListagemDeClientes } from "../pages";

export const AppRoutes = () => {
    const { setDrawerOptions } = useDrawerContext();

    useEffect(() => {
        setDrawerOptions([
            {
                icon: 'home',
                path: '/home',
                label: 'Página inicial'
            },
            {
                icon: 'people',
                path: '/clientes',
                label: 'Clientes'
            }
        ]);
    });

    return (
        <Routes>
            <Route path="/home" element={<Dashboard />} />
            <Route path="/clientes" element={<ListagemDeClientes />} />
            <Route path="/clientes/detalhe/:id" element={<p>Detalhe</p>} />

            <Route path="*" element={<Navigate to="/home" />} />
        </Routes>
    );
}