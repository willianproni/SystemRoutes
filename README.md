<h1>
  Sistema de Rotas (WillRotas)
  </h1>
  
  <p>Objetivo, Desenvolver um Projeto para Gerenciar rotas de se serviço vinda(Upload) de um arquivo .xlsx, com as informações de rotas gerar um relatório Word(.doc) a respeito do dia atual, mostrando todas as rotas realizadas naquele determinado dia.</p>
  <h4>
  Descrição do Fluxo do Programa:
  </h4>
  <p>
O usuário deverá fazer o login no aplicativo. Ao fazer o login, deverá fazer o
upload do arquivo em formato XLSX. <br>
O cabeçalho da planilha deve ser exibido como uma lista de seleções para
que o usuário selecione as colunas de dados que serão utilizadas para gerar
o arquivo DOCX. <br>
A seguir, o usuário poderá aplicar um filtro relacionado ao tipo de serviço a
ser executado e qual cidade será criada a rota.<br>
O arquivo DOCX gerado deve seguir o template abaixo. Os dados adicionais
que poderão ser utilizados que não constam no modelo podem ser inseridos
na sequência.
 
  <hr>       
        <h3>
  Banco Utilizado:
  </h3>
  
  <li>
  SQL Server para Identity
  <li>
    MongoDB para criação das API
  
  <hr>
        
  <h3>
    Concluído:
    </h3>

   <li>
    API de Pessoas
                <li>
    API de Cidades
                  <li>
    API do Times
                      <li>
    Model View Controler
                   <li>
    Validações
                                        <li>
                                              Upload
                                        <li>
                                          download do documento gerado 

    
  
  <hr>
                                          
<h3>
    Falta Fazer:
  </h3>
    
<li>
   Editar pessoa na API, não edita o nome da mesma em Equipe
  
  <hr>
  <h4>Obs:</h4> 
  Ao cadastrar no MVC o novo regristro pode não aparecer, realizar a atualização da página para aparecer o novo registro<br>
  Lembrar de Alterar o arquivo AppSettings.Json para a rota da máquina utilizada
  
  <hr>
  
  ### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
[Banco de Dados MongoDB](https://www.mongodb.com/). 
Além disto é bom ter um editor para trabalhar com o código como [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/)

### 🎲 Rodando o Back End (servidor)

```bash
# Clone este repositório

# Acesse os micro serviços

# Vá para o arquivo appsettings.json

# Altere as conexões para o padrão do seu MongoDB
$ ConnectionString

# Execute a aplicação em modo de desenvolvimento

# O servidor inciará na porta:(Aleatória)
```
  
  <hr>
  
  <hr>
    <img src="https://img.shields.io/static/v1?label=System&message=WillRotas&color=7159c1&style=for-the-badge&logo=ghost"/>
    
