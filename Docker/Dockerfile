# Use a imagem base do RabbitMQ
FROM rabbitmq:3.8.14-management

# Copie o arquivo de configuração personalizado para o diretório do RabbitMQ
# COPY rabbitmq.conf /etc/rabbitmq/

# Copie o arquivo de definição da exchange e da routing key para o diretório do RabbitMQ
COPY rabbitmq-config.json /etc/rabbitmq/

# Execute o comando para habilitar a definição de configuração da exchange e da routing key
RUN rabbitmq-plugins enable rabbitmq_management rabbitmq_shovel rabbitmq_shovel_management

# Exponha as portas padrão do RabbitMQ (5672 para AMQP e 15672 para o painel de controle)
EXPOSE 5672 15672

# Execute o RabbitMQ Server
CMD ["rabbitmq-server"]