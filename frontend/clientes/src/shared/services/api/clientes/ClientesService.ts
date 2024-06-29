import { Environment } from "../../../environments";
import { Api } from "../axios-config";

export interface IListagemCliente {
    id: number;
    nomeEmpresa: string;
    porte: string;
}

export interface IDetalheCliente {
    id: number;
    nomeEmpresa: string;
    porte: string;
}

type TClientesComTotalCount = {
    data: IListagemCliente[];
    totalCount: number;
}

const getAll = async (page = 1, filter = ''): Promise<TClientesComTotalCount | Error> => { 
    try {
        const urlRelativa = `/clientes`
        const { data, headers } = await Api.get(urlRelativa);

       if (data) {
            return {
                data,
                totalCount: Number(headers['x-total-count']) || Environment.LIMITE_DE_LINHAS,
            }
        }

        return new Error('Erro ao listar os registros');
    } catch(error) {
        console.error(error);

        return new Error((error as {message: string }).message || 'Erro ao listar os registros');
    }
}

const getById = async (id: number): Promise<IDetalheCliente | Error> => { 
    try {
        const { data } = await Api.get(`/clientes/${id}`);

        if (data) {
            return data;
        }

        return new Error('Erro ao consultar o registro');
    } catch(error) {
        console.error(error);

        return new Error((error as {message: string }).message || 'Erro ao consultar o registro');
    }
}

const create = async (cliente: Omit<IDetalheCliente, 'id'>): Promise<number | Error> => { 
    try {
        const { data } = await Api.post<IDetalheCliente>('/clientes', cliente);

        if (data) {
            return data.id;
        }

        return new Error('Erro ao consultar o registro');
    } catch(error) {
        console.error(error);

        return new Error((error as {message: string }).message || 'Erro ao consultar o registro');
    }
}

const updateById = async (id: number, dados: IDetalheCliente): Promise<void | Error> => {
    try {
       await Api.put(`/clientes/${id}`, dados);
    } catch(error) {
        console.error(error);

        return new Error((error as {message: string }).message || 'Erro ao alterar o registro');
    }
 }

const deleteById = async (id: number): Promise<void | Error> => { 
    try {
        await Api.delete(`/clientes/${id}`);
    } catch(error) {
        console.error(error);

        return new Error((error as {message: string }).message || 'Erro ao excluir o registro');
    }
}


export const ClientesService = {
    getAll,
    getById,
    create,
    updateById,
    deleteById,
}
