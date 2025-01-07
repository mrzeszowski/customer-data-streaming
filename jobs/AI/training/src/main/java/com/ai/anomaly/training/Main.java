package com.ai.anomaly.training;

import com.ai.anomaly.shared.TransactionDeserializationSchema;
import com.ai.anomaly.shared.TransactionEvent;
import org.apache.flink.api.common.eventtime.WatermarkStrategy;
import org.apache.flink.api.common.functions.MapFunction;
import org.apache.flink.connector.kafka.source.KafkaSource;
import org.apache.flink.connector.kafka.source.enumerator.initializer.OffsetsInitializer;
import org.apache.flink.ml.clustering.kmeans.KMeans;
import org.apache.flink.ml.clustering.kmeans.KMeansModel;
import org.apache.flink.ml.linalg.Vector;
import org.apache.flink.ml.linalg.Vectors;
import org.apache.flink.streaming.api.datastream.DataStream;
import org.apache.flink.streaming.api.environment.StreamExecutionEnvironment;
import org.apache.flink.table.api.Table;
import org.apache.flink.table.api.bridge.java.StreamTableEnvironment;
import org.apache.kafka.clients.consumer.OffsetResetStrategy;

public class Main {
    public static void main(String[] args) throws Exception {
        // Initialize Flink streaming environment
        final StreamExecutionEnvironment env = StreamExecutionEnvironment.getExecutionEnvironment();
        final StreamTableEnvironment tableEnv = StreamTableEnvironment.create(env);

        env.enableCheckpointing(10_000);

        // Step 1: Kafka Source to read training data
        KafkaSource<TransactionEvent> kafkaSource = KafkaSource.<TransactionEvent>builder()
                .setBootstrapServers("localhost:9092")
                .setTopics("transactions.events")
                .setGroupId("anomaly-detection-model-training")
                .setStartingOffsets(OffsetsInitializer.committedOffsets(OffsetResetStrategy.EARLIEST))
                .setDeserializer(new TransactionDeserializationSchema())
                .build();

        DataStream<Vector> transactions = env.fromSource(kafkaSource, WatermarkStrategy.noWatermarks(), "Kafka Source")
                .filter(record -> record.amount != null)
                .map((MapFunction<TransactionEvent, Vector>) value -> Vectors.dense(value.amount.value));

        // Convert DataStream to Table
        Table transactionTable = tableEnv.fromDataStream(transactions).as("features");

        // Step 3: StandardScaler for normalization
        // StandardScaler scaler = new StandardScaler();
        // StandardScalerModel scalerModel = scaler.fit(transactionTable);
        // Table scaledTable = scalerModel.transform(transactionTable)[0];

        // Step 4: KMeans for clustering
        KMeans kMeans = new KMeans()
                .setK(2) // Two clusters: normal and anomalous
                .setMaxIter(10)
                .setFeaturesCol("features")
                .setPredictionCol("predictions");
        KMeansModel kMeansModel = kMeans.fit(transactionTable);

        // Save the trained model
        kMeansModel.save("/opt/flink/usrlib/checkpoint");

        // Execute the Flink job
        env.execute("Anomaly Detection Model Training");
    }
}

