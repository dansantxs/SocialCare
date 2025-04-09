CREATE DATABASE "SocialCare"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'pt_BR.UTF-8'
    LC_CTYPE = 'pt_BR.UTF-8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

\c "SocialCare"

CREATE TABLE "Pessoas" (
    "id" SERIAL PRIMARY KEY,
    "nome" VARCHAR(255) NOT NULL,
    "cidade" VARCHAR(100) NOT NULL,
    "bairro" VARCHAR(100) NOT NULL,
    "endereco" VARCHAR(255) NOT NULL,
    "numero" VARCHAR(10) NOT NULL,
    "email" VARCHAR(255),
    "telefone" VARCHAR(20),
    "tipo" CHAR(1) NOT NULL
);

CREATE TABLE "Pessoas_Fisicas" (
    "id" INTEGER PRIMARY KEY,
    "cpf" CHAR(11) NOT NULL UNIQUE,
    "data_nascimento" DATE NOT NULL,
    CONSTRAINT "fk_pessoas_fisicas" FOREIGN KEY ("id") 
        REFERENCES "Pessoas"("id") ON DELETE CASCADE
);

CREATE TABLE "Pessoas_Juridicas" (
    "id" INTEGER PRIMARY KEY,
    "cnpj" CHAR(14) NOT NULL UNIQUE,
    "razao_social" VARCHAR(255) NOT NULL,
    CONSTRAINT "fk_pessoas_juridicas" FOREIGN KEY ("id") 
        REFERENCES "Pessoas"("id") ON DELETE CASCADE
);

CREATE TABLE "Produtos" (
    "id" SERIAL PRIMARY KEY,
    "nome" VARCHAR(50) NOT NULL,
    "preco" DECIMAL(10, 2) NOT NULL,
    "estoque" INTEGER NOT NULL
);

CREATE TABLE "Compras" (
    "id" SERIAL PRIMARY KEY,
    "idPessoa" INTEGER NOT NULL,
    "dataCompra" TIMESTAMP NOT NULL,
    "total" DECIMAL(10, 2) NOT NULL,
    CONSTRAINT "FK_Compras_Pessoas" FOREIGN KEY ("idPessoa") 
        REFERENCES "Pessoas"("id")
);

CREATE TABLE "ItensCompra" (
    "id" SERIAL PRIMARY KEY,
    "idCompra" INTEGER NOT NULL,
    "idProduto" INTEGER NOT NULL,
    "quantidade" INTEGER NOT NULL,
    "precoUnitario" DECIMAL(10, 2) NOT NULL,
    "subtotal" DECIMAL(10, 2) NOT NULL,
    CONSTRAINT "FK_ItensCompra_Compras" FOREIGN KEY ("idCompra") 
        REFERENCES "Compras"("id"),
    CONSTRAINT "FK_ItensCompra_Produtos" FOREIGN KEY ("idProduto") 
        REFERENCES "Produtos"("id")
);

CREATE TABLE "ContasPagar" (
    "id" SERIAL PRIMARY KEY,
    "idPessoa" INTEGER NOT NULL,
    "idCompra" INTEGER,
    "data" TIMESTAMP NOT NULL,
    "valor" DECIMAL(10, 2) NOT NULL,
    "dataVencimento" TIMESTAMP NOT NULL,
    "dataPagamento" TIMESTAMP,
    CONSTRAINT "FK_ContasPagar_Pessoas" FOREIGN KEY ("idPessoa") 
        REFERENCES "Pessoas"("id"),
    CONSTRAINT "FK_ContasPagar_Compras" FOREIGN KEY ("idCompra") 
        REFERENCES "Compras"("id")
);

CREATE INDEX "IX_Compras_idPessoa" ON "Compras"("idPessoa");
CREATE INDEX "IX_ContasPagar_idPessoa" ON "ContasPagar"("idPessoa");
CREATE INDEX "IX_ContasPagar_idCompra" ON "ContasPagar"("idCompra");
CREATE INDEX "IX_ItensCompra_idCompra" ON "ItensCompra"("idCompra");
CREATE INDEX "IX_ItensCompra_idProduto" ON "ItensCompra"("idProduto");