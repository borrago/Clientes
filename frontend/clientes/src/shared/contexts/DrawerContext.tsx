import { createContext, useCallback, useContext, useState } from "react";

interface IDrawerContextData {
    isDrawerOpen: boolean;
    toggleDrawerOpen: () => void;
    drawerOptions: IDrawerOption[];
    setDrawerOptions: (newDrawerOptions: IDrawerOption[]) => void;
}

const DrawerContextProvider = createContext({} as IDrawerContextData);

interface IDrawerContextProps{
    children: React.ReactNode;
}

interface IDrawerOption {
    icon: string;
    path: string;
    label: string;
}

export const useDrawerContext = () => {
    return useContext(DrawerContextProvider);
}

export const DrawerContext: React.FC<IDrawerContextProps> = ({ children }) => {
    const [isDrawerOpen, setIsDrawerOpen] = useState(false);
    const [drawerOptions, setdrawerOptions] = useState<IDrawerOption[]>([]);

    const toggleDrawerOpen = useCallback(() => {
        setIsDrawerOpen(oldDrawerOpen => !oldDrawerOpen)
    }, []);

    const handleSetdrawerOptions = useCallback((newDrawerOptions: IDrawerOption[]) => {
        setdrawerOptions(newDrawerOptions)
    }, []);

    return (
        <DrawerContextProvider.Provider value={{ isDrawerOpen, drawerOptions, toggleDrawerOpen, setDrawerOptions: handleSetdrawerOptions}}>
            {children}
        </DrawerContextProvider.Provider>
    )
}