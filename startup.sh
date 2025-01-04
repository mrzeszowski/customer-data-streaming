minikube start -p customer-data-streaming --addons=["metrics-server", "storage-provisione", "default-storageclass"]
minikube -p customer-data-streaming tunnel