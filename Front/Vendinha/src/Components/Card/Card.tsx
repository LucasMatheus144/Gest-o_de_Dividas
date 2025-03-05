import styles from './Card.module.css';
import foto from '../../../public/imgs/the-incredibles-2-logo-hs-2880x1800.jpg';

interface CardProps {
    id: number;
    nome: string;
    email: string;
    cpfcnpj: string;
    telefone: string;
    CountDividas: number;
    status: number;
}

export function Card({ id, nome, email, cpfcnpj, telefone, CountDividas, status  }: CardProps) {

    const  StatusClasse = status == 1 ? styles.EmDia :
                        status == 2 ? styles.Cancelado :
                        status == 3 ? styles.ComDivida:
                        '';

    return (
        <div className={`${styles.card} ${StatusClasse}`}>
            <div className={styles.container}>
                <img src={foto} alt="Imagem" className={styles.foto} />
                <div className={styles.info}>
                    <div className={styles.identificador}>
                        <h1>{id}</h1>
                    </div>
                    <div className={styles.divida}>
                        <h4>Dividas Existentes <b>{CountDividas}</b></h4>
                    </div>
                    <div className={styles.dados}>
                        <h4>Nome - {nome}</h4>
                        <h4>Email - {email}</h4>
                        <h4>Cpf| Cnpj -{cpfcnpj}</h4>
                        <h4>Telefone - {telefone}</h4>
                    </div>   
                </div>
            </div>
        </div>
    );
}
