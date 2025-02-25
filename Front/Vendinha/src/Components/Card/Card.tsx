import styles from './Card.module.css';

interface CardProps {
    id: number;
    nome: string;
    email: string;
    cpfcnpj: string;
    telefone: string;
    CountDividas: number;
    status: number;
}

export function Card({ id, nome, email, cpfcnpj, telefone, CountDividas, status }: CardProps) {

    const  StatusClasse = status == 1 ? styles.EmDia :
                        status == 2 ? styles.Cancelado :
                        status == 3 ? styles.ComDivida:
                        '';

    return (
        <div className={`${styles.card} ${StatusClasse}`}>
            <div>
                <img src="" alt="Imagem" />
                <div>
                    <h3>{id}</h3>
                    <h4>{nome}</h4>
                    <h4>{email}</h4>
                    <h4>{cpfcnpj}</h4>
                    <h4>{telefone}</h4>
                    <h4>{CountDividas}</h4>
                </div>
            </div>
        </div>
    );
}
