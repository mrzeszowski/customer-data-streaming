eval $(minikube -p customer-data-streaming docker-env) &&   
docker build . -t shipment-events:2.1.0