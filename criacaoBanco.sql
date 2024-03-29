-- Active: 1689037465891@@127.0.0.1@3306@bibliotec

docker run --name some-mysql -e MYSQL_ROOT_PASSWORD=my-secret-pw -v /caminho/no/host:/caminho/no/container -p 127.0.0.1:3306:3306 -d mysql:tag
docker exec -it bibliotec mysql -u root -p
CREATE DATABASE bibliotec;

CREATE TABLE t_aluno(
    id_matricula VARCHAR(15) PRIMARY KEY,
    nm_aluno VARCHAR(50),
    nm_professor VARCHAR(50),
    vl_sala VARCHAR(3),
    vl_turno CHAR(1),
    vl_serie CHAR(1)
);


CREATE Table t_emprestimo(
    id_emprestimo INT AUTO_INCREMENT PRIMARY KEY,
    cd_registro VARCHAR(13),
    cd_matricula VARCHAR(15),
    dt_emprestimo DATE
);

CREATE TABLE t_livro(
    id_registro VARCHAR(15) PRIMARY KEY,
    nm_titulo VARCHAR(50),
    nm_autor VARCHAR(50),
    nm_editora VARCHAR(20),
    fl_genero VARCHAR(20),
    nm_prateleira VARCHAR(10)
)
