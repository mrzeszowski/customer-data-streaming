eval $(minikube -p customer-data-streaming docker-env) &&
docker build . -t product-events:2.1.0