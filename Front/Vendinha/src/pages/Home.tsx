import { Card } from '../Components/Card/Card';

import estilo from '../styles/Home.module.css'

export function Home(){
    return <>
        
      



      <section className={estilo.Sessao}>
        <div className={estilo.wrapper}>
          <Card id={10} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437,581,234.23"} telefone={"1499582345"} CountDividas={10} status={1}/>
          <Card id={10} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437,581,234.23"} telefone={"1499582345"} CountDividas={10} status={1}/>
          <Card id={10} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437,581,234.23"} telefone={"1499582345"} CountDividas={10} status={1}/>
          <Card id={10} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437,581,234.23"} telefone={"1499582345"} CountDividas={10} status={1}/>
        </div>
        <div className={estilo.wrapper}>
          <Card id={10} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437,581,234.23"} telefone={"1499582345"} CountDividas={10} status={1}/>
          <Card id={10} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437,581,234.23"} telefone={"1499582345"} CountDividas={10} status={1}/>
          <Card id={10} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437,581,234.23"} telefone={"1499582345"} CountDividas={10} status={1}/>
          <Card id={10} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437,581,234.23"} telefone={"1499582345"} CountDividas={10} status={1}/>
        </div>
      </section>

      
        
        
    </>
}