package com.events.shared;

public class Configuration {
    private final String postgres; // "postgres.default.svc.cluster.local"
    private final String kafka; // "kafka.confluent.svc.cluster.local:9092"
    private final int parallelism; // 1

    public Configuration(String postgres, String kafka, int parallelism){
        this.postgres = postgres;
        this.kafka = kafka;
        this.parallelism = parallelism;
    }

    public String getPostgres() {
        return postgres;
    }

    public String getKafka() {
        return kafka;
    }

    public int getParallelism() {
        return parallelism;
    }
}
