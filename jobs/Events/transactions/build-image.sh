eval $(minikube -p customer-data-streaming docker-env) &&
docker build . -t transaction-events:2.1.0