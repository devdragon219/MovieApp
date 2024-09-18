# MovieAppBackend

## Overview
MovieAppBackend is a simple backend application built using C# .NET 8 that allows users to view and create movies with a title, description, genre, cover image, and movie file. The movie files are uploaded to AWS S3.

## Features
- View a list of movies
- Create a new movie
- Upload movie files to SQL Server (LocalDB or SQL Server Express recommended)

## Technologies Used
- .NET 8
- Entity Framework Core
- SQL Server (LocalDB or SQL Server Express recommended)
- AutoMapper

## Setup Instructions

1. **Clone the repository:**
   ```bash
Restore dependencies:


dotnet restore
Apply database migrations:


dotnet ef database update
Run the server:


dotnet run


