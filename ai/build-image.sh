eval $(minikube -p customer-data-streaming docker-env) &&   
docker build . -t anomaly-detection:1.4.0