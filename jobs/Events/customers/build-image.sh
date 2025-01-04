eval $(minikube -p customer-data-streaming docker-env) &&
docker build . -t customer-events:2.1.0