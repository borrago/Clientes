import { Box, useTheme, Button, Paper, TextField, Icon } from "@mui/material";
import { Environment } from "../../environments";

interface IFerramentasDaListagemProps {
    textoDaBusca?: string;
    mostrarInputBusca?: boolean;
    aoMudarTextoDeBusca?: (novoTexto: string) => void;
    textoBotaoNovo?: string;
    mostrarBotaoNovo?: boolean;
    aoClicarEmNovo?: () => void;
}

export const FerramentasDaListagem: React.FC<IFerramentasDaListagemProps> = ({
    textoDaBusca = '',
    mostrarInputBusca = false,
    aoMudarTextoDeBusca,
    textoBotaoNovo = 'Novo',
    mostrarBotaoNovo = true,
    aoClicarEmNovo,
}) => {
    const theme = useTheme();

    return(
        <Box 
            gap={1} 
            marginX={1} 
            padding={1} 
            paddingX={1} 
            display="flex" 
            alignItems="center" 
            height={theme.spacing(5)} 
            component={Paper}
        >
            {//mostrarInputBusca && (
                //<TextField 
                //    size="small"
                //    value={textoDaBusca}
                //    placeholder={Environment.INPUT_DE_BUSCA}
                //    onChange={(e) => aoMudarTextoDeBusca?.(e.target.value)}
                ///>
            //)
            }

            <Box flex={1} display="flex" justifyContent="end">
                {mostrarBotaoNovo && (
                    <Button
                        color="primary"
                        disableElevation
                        variant="contained"
                        onClick={aoClicarEmNovo}
                        endIcon={<Icon>add</Icon>}
                    >
                        {textoBotaoNovo}
                    </Button>
                )}
            </Box>
        </Box>
    );
}