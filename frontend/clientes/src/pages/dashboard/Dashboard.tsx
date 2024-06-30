import { Box, Card, CardContent, Grid, Typography } from "@mui/material"
import { LayoutBaseDePagina } from "../../shared/layouts"

export const Dashboard = () => {
    return(
        <LayoutBaseDePagina 
            titulo="Página Inicial" 
         >
            <Box width='100%' display='flex'>
                <Grid container margin={2}>

                    <Grid item xs={12} sm={12} md={12} lg={12} xl={12}>

                        <Card>
                            <CardContent />
                            <Typography variant='h1' align='center'>
                                Bem Vindo
                            </Typography>
                        </Card>
                    </Grid>
                </Grid>
            </Box>
        </LayoutBaseDePagina>
    )
}