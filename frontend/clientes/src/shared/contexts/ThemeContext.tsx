import { createContext, useCallback, useMemo, useContext, useState } from "react";
import { ThemeProvider } from "@emotion/react";
import { DarkTheme, LightTheme } from "../themes";
import { Box } from "@mui/system";

interface IThemeContextData {
    themeName: 'light' | 'dark';
    toggleTheme: () => void;
}

const ThemeContextProvider = createContext({} as IThemeContextData);

interface IThemeContextProps{
    children: React.ReactNode;
}

export const useThemeContext = () => {
    return useContext(ThemeContextProvider);
}

export const ThemeContext: React.FC<IThemeContextProps> = ({ children }) => {
    const [themeName, setThemeName] = useState<'light' | 'dark'>('light');

    const toggleTheme = useCallback(() => {
        setThemeName(oldThemeName => oldThemeName === 'light' ? 'dark' : 'light')
    }, []);

    const theme = useMemo(() => {
        if (themeName === 'light') return LightTheme;

        return DarkTheme;
    }, [themeName]);

    return (
        <ThemeContextProvider.Provider value={{ themeName, toggleTheme}}>
            <ThemeProvider theme={theme}>
                <Box width="100vw" height="100vh" bgcolor={theme.palette.background.default}>
                    {children}
                </Box>
            </ThemeProvider>
        </ThemeContextProvider.Provider>
    )
}