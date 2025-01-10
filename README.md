# Cloud-Based Movie Management Platform  

A scalable movie management web application built with **C#**, **ASP.NET**, and **AWS**. This platform allows users to register, log in, upload movies, rate them, and leave comments, all while providing advanced search and filtering capabilities.  

## Features  
- **User Management**: Secure registration and login functionality.  
- **Movie Management**: Upload movies with metadata such as genre, title, and description.  
- **Ratings and Comments**: Rate movies and manage user-generated comments.  
- **Advanced Search**: Filter movies by genre or ratings using efficient indices.  
- **Scalability**: Designed to handle a growing number of users and data.  

## Technologies Used  
- **Backend**: ASP.NET for creating a robust and secure API.  
- **Frontend**: HTML/CSS and JavaScript (details if applicable).  
- **Database**:  
  - Amazon RDS for relational data storage.  
  - DynamoDB for efficient non-relational data access.  
- **Storage**: Amazon S3 for managing movie assets.  
- **Deployment**: AWS Elastic Beanstalk for scalable application hosting.  

## Architecture  
The application follows a **scalable microservices architecture** with a focus on modularity and performance:  
1. **Frontend** communicates with the backend via REST APIs.  
2. **Backend Services** handle user management, movie uploads, ratings, and comments.  
3. **AWS Services** provide the infrastructure for hosting, storage, and database management.  

## Deployment  
The application is deployed on **AWS Elastic Beanstalk**, leveraging auto-scaling capabilities to maintain performance under varying loads.  

## Getting Started  

### Prerequisites  
1. Install .NET SDK (https://dotnet.microsoft.com/download) and Visual Studio (https://visualstudio.microsoft.com/).  
2. Set up an AWS account and configure the following services:
   - Amazon RDS (Used for user storage)
   - Amazon DynamoDB (Create tables Movies and Comments)
   - Amazon S3 (Used for storing Movie files)
   - Elastic Beanstalk (Optional)

### Installation  
1. Clone the repository:  
   ```bash
   git clone https://github.com/HobbyistProgrammer/Cloud-based-Movie-Management-Platform
