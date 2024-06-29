import { useNavigate, useSearchParams } from "react-router-dom"
import { FerramentasDaListagem } from "../../shared/components"
import { LayoutBaseDePagina } from "../../shared/layouts"
import { useEffect, useMemo, useState } from "react";
import { IListagemCliente, ClientesService } from "../../shared/services/api/clientes/ClientesService";
import { useDebouce } from "../../shared/hooks";
import { Icon, IconButton, LinearProgress, Pagination, Paper, Table, TableBody, TableCell, TableContainer, TableFooter, TableHead, TableRow } from "@mui/material";
import { Environment } from "../../shared/environments";

export const ListagemDeClientes:React.FC = () => {
    const [searchParams, setSearchParams] = useSearchParams();
    const { debounce } = useDebouce();
    const navigate = useNavigate();

    const [rows, setRows] = useState<IListagemCliente[]>([])
    const [totalCount, setTotalCount] = useState(0)
    const [isLoading, setIsLoading] = useState(true)
    
    const busca = useMemo(() => {
        return searchParams.get('busca') || '';
    }, [searchParams]);

    const pagina = useMemo(() => {
        return Number(searchParams.get('pagina') || '1');
    }, [searchParams]);

    useEffect(() => {
        setIsLoading(true)

        debounce(() => {
            ClientesService.getAll(pagina, busca)
            .then((result) => {
                setIsLoading(false)

                if (result instanceof Error) {
                    alert(result.message)
                } else {
                    console.log(result)

                    setTotalCount(result.data.length)
                    setRows(result.data)
                }
            })
        })
    }, [busca, pagina]);

    const handleDelete = (id: number) => {
        //if (confirm('Realmente deseja apagar?')){
            ClientesService.deleteById(id)
            .then(result => {
                if (result instanceof Error){
                    alert(result.message)
                } else {
                    setRows(oldRows => [
                        ...oldRows.filter(oldRow => oldRow.id !== id),
                    ])
                    alert('Registro apagado com sucesso!')
                }
            })
        //}
    }

    return(
        <LayoutBaseDePagina
            titulo="Listagem de Clientes"
            barraDeFerramentas={
                <FerramentasDaListagem 
                    mostrarInputBusca
                    textoDaBusca={busca}
                    textoBotaoNovo="Nova"
                    aoMudarTextoDeBusca={texto => setSearchParams({busca: texto, pagina: '1' }, {replace: true})}
                />
            }
        >
            <TableContainer component={Paper} variant="outlined" sx={{ m: 1, width: 'auto'}}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Nome Empresa</TableCell>
                            <TableCell>Porte</TableCell>
                            <TableCell>Ações</TableCell>
                        </TableRow>
                    </TableHead>

                    <TableBody>
                        {
                            rows.map(row => (
                                <TableRow key={row.id}>
                                    <TableCell>{row.nomeEmpresa}</TableCell>
                                    <TableCell>{row.porte}</TableCell>
                                    <IconButton size="small" onClick={() => navigate(`/clientes/detalhe/${row.id}`)}>
                                        <Icon>edit</Icon>
                                    </IconButton>
                                    <IconButton size="small" onClick={() => handleDelete(row.id)}>
                                        <Icon>delete</Icon>
                                    </IconButton>
                                </TableRow>
                            ))
                        }
                    </TableBody>

                    {totalCount === 0 && !isLoading && (
                        <caption>{Environment.LISTAGEM_VAZIA}</caption>
                    )}

                    <TableFooter>
                        {isLoading && (
                            <TableRow>
                                <TableCell colSpan={3}>
                                        <LinearProgress variant='indeterminate'/>
                                </TableCell>
                            </TableRow>
                        )}
                        {totalCount > 0 && totalCount > Environment.LIMITE_DE_LINHAS && (
                            <TableRow>
                                <TableCell colSpan={3}>
                                        <Pagination 
                                            page={pagina}
                                            count={Math.ceil(totalCount / Environment.LIMITE_DE_LINHAS)} 
                                            onChange={(_, newPage) => setSearchParams({busca, pagina: newPage.toString()}, {replace: true})}
                                        />
                                </TableCell>
                            </TableRow>
                        )}
                    </TableFooter>
                </Table>
            </TableContainer>
        </LayoutBaseDePagina>
    )
}