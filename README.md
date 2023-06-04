
# TooInsurance
## Diagram
![solution](../../diagrama_teste.png)
## Rabbit info
 - User: guest
 - Password: guest

## On Windows, After install Docker Desktop, run line below
 - docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management

## On Library DadaBaseModel Run these commands 
 - dotnet ef database update
