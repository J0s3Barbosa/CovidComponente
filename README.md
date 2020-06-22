# CovidComponente 

Aplicação console que obtem dados atualizados do corona virus do site da google.
Tambem tem a opção de salvar os dados no banco mongodb e realizar pesquisas e montar statisticas.

# Tecnologias e patterns utilizados:
    ⦁ BACK-END - .Net Core 3.1
    ⦁ PATTERNS – DDD e Injeção de Dependência, Clean code, clean architecture, 
    
# Manual de Instalação da aplicação

    ## para executar o projeto
        - Faça o download do codigo
        - Instales as dependencias
            -  ComponentsLib
            -  Service.Domain
            -  CovidComponent
            -  HtmlAgilityPack
            -  MongoDB.Driver
            - ...
                            
        - execute o console app
        - criar uma conta no mongo caso queira armazenar os dados das pesquisas
        - crie uma variável de ambiente no windows com a connectionstring do seu banco conforme abaixo ou Coloque no appsettings.json do CrossCutting
            Variable Name = MongoDB
            Variable value = sua connectionString
 

# Layers:

    - BackEnd
        CovidConsoleApp
        CrossCutting
       
        
   
