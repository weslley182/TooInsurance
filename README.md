# TooInsurance

docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management

dotnet ef migrations add InitialMigrations

dotnet ef database update
