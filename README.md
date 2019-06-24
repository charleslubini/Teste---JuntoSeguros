# Teste - JuntoSeguros (Backend)

Este projeto contêm uma API com os microserviços de cadastro, modificação e deleção de usuários, juntamente com o serviço responsável pelo geranciamento do login dos usuários. 
Para atender a demanda de gerenciamento de usuários, este projeto utiliza o banco de dados relacional Postgres, e todo o endpoint relativo a usuário deve ser mandando na requisição um token, válido, disponibilizado apartir do endpoint de login.

Assim, O projeto está parcionado da seguinte forma:

## TesteJuntoSeguros
  Projeto que contêm o desenvolvimento da solução. O mesmo possui as configurações relativas ao banco de dados de dados, juntamente com a integração do banco com os modelos(Models). 
  E também possui os controllers relativos ao CRUD de usuários e gerenciamento de login. Sendo eles:
  
  ### UserController
  Com os seguintes endpoints, para o funcionamento dos mesmo, é necessário mandar o token gerado pod LoginController.CreateToken:
  
   #### GetUser (GET)
   Endpoint relativo ao retorno de todos os usuários cadastrados no sistema;
   
   #### GetUser(id) (GET)
   Endpoint relativo ao retorno de um determinado usuário;
   
   #### PostUser (POST)
   Endpoint relativo a criação de um usuário;
   
   #### PutUser (POST)
   Endpoint relativo a modificação de um usuário;
   
   #### DeleteUser (POST)
   Endpoint relativo a deleção de um usuário;
  

  
  ### LoginController
  Com os seguintes endpoints:
  
   #### CreateToken (POST)
   Endpoint relativo a validação do usuário (username) e senha (password) informados, para a criação de um token de autenticação no      sistema.

   #### CreateTokenPassword (POST)
   Valida se o usuário que está solicitando a mudança de senha, possui cadastro no sistema, e caso possuir, cria um token com 4 posições para ser informado no serviço de mudança de senha. 

   #### ChangePassword (POST)
   Serviço relativo a mudança de senha, para o mesmo ser efetuado com sucesso, é validado se o token relativo da mudança de senha (gerado no CreateTokenPassword) é o mesmo que foi gerado pelo sistema, e também valida se a nova senha é a mesma senha do campo de confirmação de senha.
    
    
## TesteJuntoSeguros.Tests
  No qual contêm os testes automatizados realtivos a API.
