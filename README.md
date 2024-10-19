# Stock Photo Marketplace

## Overview

Stock Photo Marketplace is a dynamic platform that connects photographers with buyers, allowing photographers to upload and manage their stock images while providing buyers with an easy-to-use search and purchasing system. This project focuses on giving users distinct roles—buyers and sellers—with features tailored to each role to enhance the overall user experience.

We used **ASP.NET Core MVC** for the web app and **MS SQL Server** to store photo details. For the images, we saved them in the `wwwroot/images` folder on the server. To make uploads fast and secure, we added file size limits, format checks (like JPEG or PNG), and used **Bootstrap** for a smooth user interface.

## Features

- **Role-Based Access Control (RBAC)**:
  - Buyers: Can search for, browse, and purchase images.
  - Sellers: Can upload, edit, and delete their images.
- **Image Upload and Management**: 
  - Sellers can upload large image files, with automatic validation for format, size, and resolution.
- **Secure Payments**: 
  - Integration with payment systems for smooth transactions between buyers and sellers.



## Project Structure

Stock Photo Marketplace
│
├── Controllers/                 # Handles incoming HTTP requests and interacts with Models and Views
├── Data/                        # Handles data access logic
├── Database/                    # Contains database-related files
├── Migrations/                  # Handles database migrations
├── Models/                      # Defines data models used in the application
├── Properties/                  # Contains project metadata and settings
├── Services/                    # Contains business logic and service classes
├── Views/                       # Contains Razor Views for rendering UI
├── wwwroot/                     # Contains static files (CSS, JS, images, etc.)
├── Program.cs                   # Entry point of the application
├── Stock_Photo_Marketplace.csproj # Project file for the .NET application
├── appsettings.Development.json # Configuration settings for development
└── appsettings.json             # General application configuration
