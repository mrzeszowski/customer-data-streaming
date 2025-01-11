# Use an official Python image as the base image
FROM python:3.9-slim

# Set the working directory inside the container
WORKDIR /app

# Copy the requirements file (create this if you haven't)
COPY requirements.txt .

# Install the Python dependencies
RUN pip install --no-cache-dir -r requirements.txt

# Copy the consumer script into the container
COPY consumer.py .

# Expose any required ports (if necessary, for debugging/logging purposes)
# EXPOSE 9092 (Kafka uses this, but you don't need to expose it for consumers)

# Set the default command to execute the consumer script
CMD ["python", "consumer.py"]
