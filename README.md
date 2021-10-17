## Author
- [@TheAlgy](https://www.github.com/TheAlgy)

# Projeto Tempo - SPRO

Criar uma API que faça o armazenamento de cidades (crud).

Estrutura:

Na classe BDContext existe um método que realiza a importação de dados de todas as cidades brasileiras da API do IBGE.
Nota: A primeira execução pode demorar alguns segundos!

No crud foram feitas as seguintes validações:

Cadastrar cidade 
* Comparação de nome da cidade com a base do IBGE para se certificar seja uma cidade real;
* Verifica se uma cidade já está cadastrada para evitar duplicidade.

Sobrescrever cidade 
* Compara o novo nome da cidade com a base importada do IBGE para se certificar de que seja uma cidade real;
* Verifica as cidades cadastradas para evitar duplicidade.

Deletar cidade 
* Verifica na base se o ID preenchido existe.

Listar cidade
* Verifica na base de cidades cadastradas se o ID preenchido existe;
* Caso o ID exista, apresenta o nome da cidade e realiza uma consulta de informações do tempo utilizando como base a API OpenWeather, os dados retornados na busca são:

→ Nome da cidade;

→ Temperatura Mínima; 

→ Temperatura Máxima; 

→ Temperatura Atual; 

→ Humidade; 

→ Sensação Térmica; 

→ Descrição.
