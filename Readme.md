# TeleAppBot


Antes de executar, crie um arquivo *.env* na raiz do projeto contendo a variável de ambiente **TELEGRAM_BOT_TOKEN** preenchida com o token de um bot do Telegram. Para mais informações sobre como gerar um token, acesse: https://core.telegram.org/bots

Após a configuração da variável de ambiente, execute o comando:

`docker-compose --env-file ./{ARQUIVO ENV}.env up --build`

Acesse o bot criado no Telegram. Ele irá copiar todas as mensagens enviadas e as enviará de volta. O comando **/invert** irá inverter todas as mensagens enviadas de volta.

## Kubernetes

A última atualização envolveu o uso do Kubernetes para executar e orquestrar os contêiners da aplicação. Para o MongoDB foi utilizada a versão cloud, Atlas, e para o Kafka o Confluent Cloud. Ao executar, é necessário os valores para a connection string do Mongo, o broker do Kafka e o usuário e senha do Kafka.