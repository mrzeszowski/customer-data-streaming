# Step 1: Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files to the container
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application files and build the application
COPY . ./
RUN dotnet publish -c Release -o /publish

# Step 2: Use the official ASP.NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /publish .

# Expose the application port (default for ASP.NET is 80)
EXPOSE 8080

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "Customer.DataStreaming.Source.dll"]
