workspace {
    model {
        customer = person "Customer" "An actor interacting with the system"

        customersService = softwareSystem "Customers Service" "Handles customer data"
        productsService = softwareSystem "Products Service" "Manages product information"
        transactionsService = softwareSystem "Transactions Service" "Processes transaction data"
        shipmentsService = softwareSystem "Shipments Service" "Manages shipment stages and details"

        flink = softwareSystem "Apache Flink" "Stream data real-time using CDC and Jobs"
        kafka = softwareSystem "Apache Kafka" "Message broker for event streaming"
        anomalyDetection = softwareSystem "Anomaly Detection AI" "Consumes data from Kafka, executes online training, detects anomalies, and pushes them back to Kafka"

        customer -> customersService "Uses"
        customer -> transactionsService "Uses"
        customer -> shipmentsService "Uses"

        flink -> customersService "Listen using CDC"
        flink -> productsService "Listen using CDC"
        flink -> transactionsService "Listen using CDC"
        flink -> shipmentsService "Listen using CDC"

        flink -> kafka "Pushes processed data"
        kafka -> anomalyDetection "Consumes Transactions data for anomaly detection"
        anomalyDetection -> kafka "Pushes detected anomalies"
    }

    !script groovy {
        //workspace.views.createDefaultViews()
        workspace.views.views.each { it.disableAutomaticLayout() }
    }

    views {
        systemLandscape {
            include *
            # autolayout lr
        }

        themes default https://static.structurizr.com/themes/microsoft-azure-2021.01.26/theme.json https://structurizr.com/share/36141/theme https://static.structurizr.com/themes/amazon-web-services-2022.04.30/theme.json https://static.structurizr.com/themes/google-cloud-platform-v1.5/theme.json
    }
}
