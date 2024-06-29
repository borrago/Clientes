import { Button } from "@mui/material";
import { Routes, Route, Navigate } from "react-router-dom";
import { useThemeContext } from "../shared/contexts";

export const AppRoutes = () => {
    const { toggleTheme } = useThemeContext();

    return (
        <Routes>
            <Route path="/" element={<Button variant="contained" color="primary" onClick={toggleTheme}>Trocar tema</Button>} />

            <Route path="*" element={<Navigate to="/" />} />
        </Routes>
    );
}