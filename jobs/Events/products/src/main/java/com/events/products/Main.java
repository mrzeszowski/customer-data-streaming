package com.events.products;

import com.events.shared.Configuration;
import com.events.shared.ConfigurationProvider;
import com.events.shared.Event;
import com.events.shared.EventSerializationSchema;
import org.apache.flink.api.common.eventtime.WatermarkStrategy;
import org.apache.flink.cdc.connectors.base.source.jdbc.JdbcIncrementalSource;
import org.apache.flink.cdc.connectors.postgres.source.PostgresSourceBuilder;
import org.apache.flink.cdc.debezium.DebeziumDeserializationSchema;
import org.apache.flink.cdc.debezium.JsonDebeziumDeserializationSchema;
import org.apache.flink.connector.kafka.sink.KafkaSink;
import org.apache.flink.streaming.api.datastream.DataStream;
import org.apache.flink.streaming.api.environment.StreamExecutionEnvironment;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.Properties;

public class Main {

    private static final Logger Log = LoggerFactory.getLogger(Main.class);

    public static void main(String[] args) throws Exception {

        Configuration configuration = new ConfigurationProvider(Log).getConfiguration();

        StreamExecutionEnvironment env = StreamExecutionEnvironment.getExecutionEnvironment();

        DebeziumDeserializationSchema<String> deserializer =
                new JsonDebeziumDeserializationSchema();

        JdbcIncrementalSource<String> postgresIncrementalSource =
                PostgresSourceBuilder.PostgresIncrementalSource.<String>builder()
                        .hostname(configuration.getPostgres())
                        .port(5432)
                        .database("products_db")
                        .schemaList("products")
                        .tableList("products.product")
                        .username("postgres")
                        .password("postgres")
                        .slotName("flink_products")
                        .decodingPluginName("pgoutput") // use pgoutput for PostgreSQL 10+
                        .deserializer(deserializer)
                        .debeziumProperties(getDebeziumProperties())
                        .splitSize(100) // the split size of each snapshot split
                        .build();

        env.enableCheckpointing(30_000);

        DataStream<String> rawCdcStream = env.fromSource(
                        postgresIncrementalSource,
                        WatermarkStrategy.noWatermarks(),
                        "ProductsPostgresParallelSource")
                .setParallelism(configuration.getParallelism());

        DataStream<Event> customerEvents = rawCdcStream.flatMap(new ProductFlatMapFunction(Log));

        KafkaSink<Event> kafkaSink = KafkaSink.<Event>builder()
                .setBootstrapServers(configuration.getKafka()) // Change to your Kafka broker
                .setRecordSerializer(new EventSerializationSchema("products.events"))
                .build();

        customerEvents.sinkTo(kafkaSink);

        env.execute("Product Events");
    }

    private static Properties getDebeziumProperties() {

        Properties properties = new Properties();
        properties.setProperty("decimal.handling.mode", "string");
        properties.setProperty("snapshot.mode", "initial_only");
        return properties;
    }
}