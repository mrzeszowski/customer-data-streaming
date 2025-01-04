eval $(minikube -p customer-data-streaming docker-env) &&   
docker build . -t customer-data-streaming-source:3.2.0