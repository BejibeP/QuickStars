# Use the official Python image as the base image
FROM python:3.8-alpine

# Set the working directory inside the container
WORKDIR /app

# Copy the requirements file and install dependencies
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copy the rest of the application files into the container
COPY . .

# Expose the port that the application will listen on
EXPOSE 7210

# Start the application when the container is run
CMD ["python", "weather_api.py"]
