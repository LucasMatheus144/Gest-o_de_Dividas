import styles from '../Navegation/Navegation.module.css'

interface NavProps{
    numInicio : number;
    numFinalTotal: number;
    numPagina : number;
    numPaginalTotal : number;
}

export function Navegation( {numInicio,numFinalTotal,numPagina, numPaginalTotal } : NavProps){
    return (
    <div className={styles.custompage}>
        <div className={styles.pagination}>Itens {numInicio} - {numFinalTotal}</div>
            <div className={styles.controls}>
                <span>Página </span>
                <a href="#" >&lt;</a>
                <label className={styles.number}>{numPagina}</label>
                <a href="#">&gt;</a>
                <span> de {numPaginalTotal} </span>
            </div>
     </div>
    );
    
}