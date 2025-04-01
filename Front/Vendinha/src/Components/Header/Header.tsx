import styles from '../Header/Header.module.css';

export function Header(){
    return(
        <>
            <header className={styles.cabecalho}>

                <p className={styles.txt}>Vendinha Interfocus</p>

                <nav>
                    <a href="#" className={styles.menu}>Home</a>
                </nav>
            
            </header>  
        
        </>
    );
}