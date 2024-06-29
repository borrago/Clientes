import { createTheme } from "@mui/material"; 
import { grey, purple } from "@mui/material/colors";

export const DarkTheme = createTheme({
    palette: {
        primary: {
            main: purple[700],
            dark: purple[800],
            light: purple[500],
            contrastText: "#fff",
        },
        secondary: {
            main: grey[700],
            dark: grey[800],
            light: grey[500],
            contrastText: "#fff",
        },
        background: {
            paper: "#303134",
            default: "#202124",
        },
    },
    typography: {
        allVariants: {
            color: 'white'
        }
    }
});