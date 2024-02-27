# Project Pitch 4

Project Pitch 4 is a web application built with ASP.NET Core for managing products, regions, and quizzes.

## Table of Contents
1. [Introduction](#introduction)
2. [Project Structure](#project-structure)
3. [Installation](#installation)
4. [Usage](#usage)
5. [API Endpoints](#api-endpoints)
6. [Configuration](#configuration)
7. [Deployment](#deployment)
8. [Contributing](#contributing)

## Introduction

This project is aimed at providing functionalities to manage products, regions, and quizzes. It includes APIs for listing, adding, updating, and deleting products, regions, and quiz questions. The application is built using ASP.NET Core and MongoDB for data storage.

## Project Structure

The project is organized into the following directories:

- `Controllers`: Contains the controller classes for handling HTTP requests.
- `Models`: Contains the model classes representing the entities in the application.
- `Services`: Contains the service classes used for interacting with external services or databases.
- `Properties`: Contains project properties.
- `bin` and `obj`: Directories for output files and build artifacts.

## Installation

To run the project locally, you need to have the .NET SDK and MongoDB installed on your machine. Follow these steps:

1. Clone the repository: `git clone <repository_url>`
2. Navigate to the project directory: `cd ProjectPitch4`
3. Restore dependencies: `dotnet restore`
4. Start the MongoDB server: `mongod`
5. Run the project: `dotnet run`

## Usage

Once the project is running, you can access the APIs using a tool like Postman or Swagger UI. The base URL for the APIs will be `https://localhost:5001`.

## API Endpoints

The following are the available API endpoints:

- `/api/Product/ProductList`: Get a paginated list of products.
- `/api/Product/GetByProductID/{productID}`: Get product description by product ID.
- `/api/Product/AddProduct`: Add a new product.
- `/api/Product/UpdateProduct/{id}`: Update an existing product.
- `/api/Product/DeleteProduct/{id}`: Delete a product.

- `/api/Region/RegionList`: Get a paginated list of regions.
- `/api/Region/GetRegion/{id}`: Get region details by ID.
- `/api/Region/GetByProvinceCode/{stateProvinceCode}`: Get regions by state/province code.
- `/api/Region/AddRegion`: Add a new region.
- `/api/Region/UpdateRegion/{id}`: Update an existing region.
- `/api/Region/DeleteRegion/{id}`: Delete a region.

- `/api/Quiz`: Endpoints for managing quiz questions.
  - `GET`: Retrieve all quiz questions.
  - `GET /{id}`: Retrieve a specific quiz question by ID.
  - `POST`: Add a new quiz question.
  - `PUT /{id}`: Update an existing quiz question.
  - `DELETE /{id}`: Delete a quiz question.

## Configuration

The application can be configured using the `appsettings.json` and `appsettings.Development.json` files. You can specify MongoDB connection settings, logging configuration, and other application settings in these files.

## Deployment

The application has been deployed on Azure Web Service. You can access the deployed Swagger UI [here](https://opeyemidemoproject.azurewebsites.net/).

## Contributing

Contributions are welcome! Please feel free to open issues or submit pull requests to improve the project.


