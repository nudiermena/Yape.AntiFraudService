using AntiFraudService.Consumers;
using Confluent.Kafka;
using AntiFraudService.Infrastructure;
using AntiFraudService.Worker.Extensions;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureSerilog()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddInfrastructure(hostContext.Configuration);
        services.AddApplication();
        services.AddMassTransit(x =>
        {
            // we aren't using the bus, only Kafka
            x.UsingInMemory();

            x.AddRider(r =>
            {
                r.AddProducer<FraudCheckResultEvent>("transaction-antifraudchecked");

                r.AddConsumer<TransactionReceivedConsumer>();
                r.UsingKafka((context, cfg) =>
                {
                    //cfg.Host(context);
                    cfg.Host("localhost:29092");
                    cfg.ClientId = "transaction-service";                

                    cfg.TopicEndpoint<string, Transaction>("transaction-created", "antifraud.worker", e =>
                    {
                        e.AutoOffsetReset = AutoOffsetReset.Earliest;                        

                        e.ConcurrentMessageLimit = 10;

                        e.UseSampleRetryConfiguration();

                        e.ConfigureConsumer<TransactionReceivedConsumer>(context);
                    });                    
                });
            });
        });
    })
    .Build();

await host.RunAsync();