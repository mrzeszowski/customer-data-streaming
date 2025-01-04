package com.events.shared;

import org.slf4j.Logger;

public class ConfigurationProvider {

    private final Logger log;

    public ConfigurationProvider(Logger log) {
        this.log = log;
    }

    public Configuration getConfiguration() {
        String postgres = getEnvironmentVariable("POSTGRES_HOST", "localhost");
        if (postgres == null || postgres.isEmpty()) {
            throw new IllegalStateException("POSTGRES_HOST environment variable is required.");
        }

        String kafka = getEnvironmentVariable("KAFKA_HOST", "localhost:9092");
        if (kafka == null || kafka.isEmpty()) {
            throw new IllegalStateException("KAFKA_HOST environment variable is required.");
        }

        Integer parallelism;
        try {
            parallelism = Integer.parseInt(getEnvironmentVariable("PARALLELISM", "1"));
        } catch (NumberFormatException e) {
            log.warn("Invalid PARALLELISM value, using default: 1");
            parallelism = 1;
        }

        return new Configuration(postgres, kafka, parallelism);
    }

    private String getEnvironmentVariable(String key, String defaultValue) {
        String value = System.getenv(key);
        if (value == null) {
            log.warn("Environment variable not found: {}, using default: {}", key, defaultValue);
            return defaultValue;
        }
        return value;
    }
}
