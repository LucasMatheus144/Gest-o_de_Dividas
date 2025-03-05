import { Card } from '../Components/Card/Card';
import { Navegation } from '../Components/Navegation/Navegation';
import estilo from '../styles/Home.module.css'

export function Home(){
    return <>
    
      <div className={estilo.menu}>
        <div className={estilo.submenu}>
          <div className={estilo.filtro}>
            <select name="opcoes" className={estilo.selecionado}>
                <option value="1">Nome</option>
                <option value="2">CPF/CNPJ</option>
                <option value="3">Id</option>
            </select>
            <div className={estilo.juncao}>
              <input type="text" name="" id="" className={estilo.envio} />
              <a href="#" className={estilo.btn}>🔍</a>
            </div>
            <a className={estilo.formulario}>Cadastro</a>
          </div>
        </div>
      </div>



      <section className={estilo.Sessao}>
        <div className={estilo.wrapper}>
          <Card  id={12} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437.581.234.23"} telefone={"1499582345"} CountDividas={2} status={1}/>
          <Card  id={13} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437.581.234.23"} telefone={"1499582345"} CountDividas={0} status={2}/>
          <Card  id={14} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437.581.234.23"} telefone={"1499582345"} CountDividas={5} status={3}/>
          <Card  id={15} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437.581.234.23"} telefone={"1499582345"} CountDividas={1} status={2}/>
          <Card  id={15} nome={"Lucas"} email ={"lucas.marques@interfocus.com"} cpfcnpj={"437.581.234.23"} telefone={"1499582345"} CountDividas={1} status={2}/>
        </div>

        <div className={estilo.nave}>
          <Navegation numInicio={3} numFinalTotal={30} numPagina={1} numPaginalTotal={25}/>
        </div>
      </section>

       
        
    </>
}