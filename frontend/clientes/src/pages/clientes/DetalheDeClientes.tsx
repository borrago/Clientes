import { useEffect, useState } from 'react';
import { Box, Grid, LinearProgress, MenuItem, Paper, Typography } from '@mui/material';
import { useNavigate, useParams } from 'react-router-dom';
import * as yup from 'yup';

import { ClientesService } from '../../shared/services/api/clientes/ClientesService';
import { VTextField, VForm, useVForm, IVFormErrors, VSelect } from '../../shared/forms';
import { FerramentasDeDetalhe } from '../../shared/components';
import { LayoutBaseDePagina } from '../../shared/layouts';


interface IFormData {
    nomeEmpresa: string;
    porte: number;
}

const formValidationSchema: yup.SchemaOf<IFormData> = yup.object().shape({
    porte: yup.number().required(),
    nomeEmpresa: yup.string().required().min(3),
});

export const DetalheDeClientes: React.FC = () => {
    const { formRef, save, saveAndClose, isSaveAndClose } = useVForm();
    const { id = 'nova' } = useParams<'id'>();
    const navigate = useNavigate();

    const [isLoading, setIsLoading] = useState(false);
    const [nome, setNome] = useState('');
    const [porte, setPorte] = useState(0);

    useEffect(() => {
        if (id !== 'nova') {
            setIsLoading(true);

            ClientesService.getById(id)
                .then((result) => {
                    setIsLoading(false);

                    if (result instanceof Error) {
                        alert(result.message);
                        navigate('/clientes');
                    } else {
                        setNome(result.nomeEmpresa);
                        setPorte(mapPorteToValueSelect(result.porte));
                        formRef.current?.setData(result);
                    }
                });
        } else {
            formRef.current?.setData({
                nomeEmpresa: '',
                porte: 0,
            });
        }
    }, [id]);


    const mapPorteToValue = (porteString: string): number => {
        switch (porteString + 1) {
            case 'Media':
                return 1;
            case 'Grande':
                return 2;
            default:
                return 0; // Pequena
        }
    };

    const mapPorteToValueSelect = (porteString: string): number => {
        switch (porteString) {
            case 'Media':
                return 2;
            case 'Grande':
                return 3;
            default:
                return 1; // Pequena
        }
    };

    const handleSave = (dados: IFormData) => {
        dados.porte = mapPorteToValueSelect(dados.porte.toString())

        formValidationSchema.
            validate(dados, { abortEarly: false })
            .then((dadosValidados) => {
                setIsLoading(true);

                dados.porte = dados.porte - 1;

                if (id === 'nova') {
                    ClientesService
                        .create(dadosValidados)
                        .then((result) => {
                            setIsLoading(false);

                            if (result instanceof Error) {
                                alert(result.message);
                            } else {
                                if (isSaveAndClose()) {
                                    navigate('/clientes');
                                } else {
                                    navigate(`/clientes/detalhe/${result}`);
                                }
                            }
                        });
                } else {
                    ClientesService
                        .updateById(id, { id: id, ...dadosValidados })
                        .then((result) => {
                            setIsLoading(false);

                            if (result instanceof Error) {
                                alert(result.message);
                            } else {
                                if (isSaveAndClose()) {
                                    navigate('/clientes');
                                }
                            }
                        });
                }
            })
            .catch((errors: yup.ValidationError) => {
                const validationErrors: IVFormErrors = {};

                errors.inner.forEach(error => {
                    if (!error.path) return;

                    validationErrors[error.path] = error.message;
                });

                formRef.current?.setErrors(validationErrors);
            });
    };

    const handleDelete = (id: string) => {
        //if (confirm('Realmente deseja apagar?')) {
        ClientesService.deleteById(id)
            .then(result => {
                if (result instanceof Error) {
                    alert(result.message);
                } else {
                    alert('Registro apagado com sucesso!');
                    navigate('/clientes');
                }
            });
        //}
    };


    return (
        <LayoutBaseDePagina
            titulo={id === 'nova' ? 'Novo cliente' : nome}
            barraDeFerramentas={
                <FerramentasDeDetalhe
                    textoBotaoNovo='Novo'
                    mostrarBotaoSalvarEFechar
                    mostrarBotaoNovo={id !== 'nova'}
                    mostrarBotaoApagar={id !== 'nova'}

                    aoClicarEmSalvar={save}
                    aoClicarEmSalvarEFechar={saveAndClose}
                    aoClicarEmVoltar={() => navigate('/clientes')}
                    aoClicarEmApagar={() => handleDelete(id)}
                    aoClicarEmNovo={() => navigate('/clientes/detalhe/nova')}
                />
            }
        >

            <VForm ref={formRef} onSubmit={handleSave} placeholder="Cadastro de Clientes" onPointerEnterCapture={() => { }} onPointerLeaveCapture={() => { }}>
                <Box margin={1} display="flex" flexDirection="column" component={Paper} variant="outlined">

                    <Grid container direction="column" padding={2} spacing={2}>

                        {isLoading && (
                            <Grid item>
                                <LinearProgress variant='indeterminate' />
                            </Grid>
                        )}

                        <Grid container item direction="row" spacing={2}>
                            <Grid item xs={12} sm={12} md={12} lg={12} xl={12}>
                                <VTextField
                                    fullWidth
                                    name='nomeEmpresa'
                                    disabled={isLoading}
                                    label='Nome empresa'
                                    onChange={e => setNome(e.target.value)}
                                />
                            </Grid>
                        </Grid>

                        <Grid item xs={12}>
                            <VSelect
                                fullWidth
                                disabled={isLoading}
                                label="Porte"
                                name="porte"
                                onChange={e => setPorte(mapPorteToValue(e.target.value))}
                                options={[
                                    { id: "1", label: 'Pequena' },
                                    { id: "2", label: 'Media' },
                                    { id: "3", label: 'Grande' },
                                  ]}
                            />
                        </Grid>

                    </Grid>

                </Box>
            </VForm>
        </LayoutBaseDePagina>
    );
};