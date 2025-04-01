import styles from './Card.module.css';

interface CardProsps{
    email: string;
    situacao: number;
    nome: string;
    qtnde: number;
};

export function Card({ email, situacao, nome, qtnde }: CardProsps) {

    const statusMap: Record<number, string> = {
        2: "Devendo",
        3: "Cancelado"
    };

    const statusTexto = statusMap[situacao] || "Indefinido";

    return (
        <div className={styles.wrapper}>
            <div className={styles.imagem}>
                <img src="/caminho/para/imagem.jpg" alt="Item NFT" className={styles.foto} />
               
            </div>

            <div className={styles.info}>
                <div className={styles.linhaTitulo}>
                    <span className={styles.titulo}>{email}</span>
                    <span className={styles.emDia}>{statusTexto}</span>
                </div>

                <div className={styles.rodape}>
                    <div className={styles.criador}>
                        <div className={styles.avatar}></div>
                        <div>
                            <small>Criador</small>
                            <p>{nome}</p>
                        </div>
                    </div>
                    <div className={styles.lance}>
                        <small>Qntde Dividas</small>
                        <p>{qtnde}</p>
                    </div>                   
                </div>
                <div className={styles.acoes}>
                        <button className={styles.botaoPrimario}>
                            🛍️ Adicionar Dividas
                        </button>
                        <button className={styles.botaoSecundario}>
                            🔄 Dividas
                        </button>
                    </div>
            </div>
        </div>
    );
}
