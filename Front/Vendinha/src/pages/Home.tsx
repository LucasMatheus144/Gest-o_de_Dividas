import { useEffect, useState } from 'react';
import axios from 'axios';
import { Card } from '../Components/Card/Card';
import styles from '../styles/Home.module.css';

interface Cliente {
    email: string;
    situacao: number;
    nome: string;
    qtnde: number;
}

export default function Home() {
    const [clientes, setClientes] = useState<Cliente[]>([]);

    useEffect(() => {
        async function carregarCards() {
            try {
                const response = await axios.get<Cliente[]>('http://localhost:7083/api/Cliente', {
                    headers: {
                        'Accept': 'application/json',
                    },
                });

                setClientes(response.data);
            } catch (error) {
                console.error('Erro ao buscar clientes:', error);
            } finally {
                console.log('Requisição finalizada');
            }
        }

        carregarCards();
    }, []);

    return (
        <section className={styles.agrupamento}>
            {clientes.map((cliente, index) => (
                <Card
                    key={index}
                    email={cliente.email}
                    situacao={cliente.situacao}
                    nome={cliente.nome}
                    qtnde={cliente.qtnde}
                />
            ))}
        </section>
    );
}
