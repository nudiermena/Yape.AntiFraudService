## Yape AntiFraud Service Setup
This sample project shows how to use MassTransit with Kafka Event Bus for Communication Purposes.

## Requirements

## Database Setup

The following database and tables need to be created on PostgreSQL before proceeding with running the solution:
For this go and follow the instructions on the 
https://github.com/nudiermena/Yape.TransactionServiceAPI/blob/master/README.md

```sql
CREATE DATABASE TransactionDB;

CREATE TABLE Antifraud (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    sourceaccountid UUID NOT NULL,
    value DECIMAL(18, 2) NOT NULL,
    createdat TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);
```

1. Clone the solution and deploy the project into the docker desktop using the DockerFile:
https://github.com/nudiermena/Yape.AntiFraudService/blob/master/src/AntiFraudService.Worker/Dockerfile
