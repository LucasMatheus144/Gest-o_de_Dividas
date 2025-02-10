create schema vendinha;

create sequence vendinha.cliente_id_seq;

create table vendinha.clientes(
	id integer primary key default("vendinha.cliente_id_seq"),
	nome varchar(25) not null,
	email varchar(30) not null unique,
	cpf_cnpj varchar(16) not null unique,
	d_datanascimeto date not null,
	d_datacadastro date default(current_date),
	telefone varchar(15) not null,
	observacao varchar(100)
);

create sequence vendinha.divida_id_seq;

create table vendinha.divida(
	id integer primary key default("vendinha.divida_id_seq"),
	cliente_id integer references vendinha.clientes(id) not null,
	descricao varchar(100),
	valor decimal not null,
	situacao integer not null,
);

create sequence vendinha.parametro_id_seq;

create table vendinha.parametrochavevalor(
	id integer primary key default ("vendinha.parametro_id_seq"),
	nome varchar(20),
	valor integer,
	situacao boolean
);

insert into vendinha.parametrochavevalor VALUES ('IdadeMinimaPermitida',18,TRUE);
insert into vendinha.parametrochavevalor VALUES ('ValorMaximo',300,TRUE);



CREATE OR REPLACE FUNCTION vendinha.valida_newdivida(id INT)
RETURNS BOOLEAN AS $$
DECLARE
    valormaximo INT;
    total INT := 0; 
BEGIN

    SELECT valor INTO valormaximo FROM vendinha.parametrochavevalor WHERE id = 2;
    
    SELECT COALESCE(SUM(d.valor), 0) INTO total FROM vendinha.divida d WHERE d.cliente_id = id;

    IF total >= valormaximo THEN
        RAISE EXCEPTION 'O cliente já bateu o limite de dívidas.';
    END IF;

    RETURN TRUE;
END;
$$ LANGUAGE plpgsql;
