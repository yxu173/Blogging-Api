# Blogging API

A clean architecture-based blogging platform API built with .NET Core 8, Entity Framework Core (EF Core), CQRS, and Domain-Driven Design (DDD). The API provides role-based authentication and authorization, email confirmation, user profiles with posts, profile pictures, bio, and a reporting system. Users can follow each other to stay updated on their latest posts.

## Features

- **User Authentication and Authorization**
    - Role-based access control (Admin, User, etc.).
    - JWT-based authentication.
    - Email confirmation using EmailFluent.
- **User Profiles**
    - Users can manage their profiles, including:
        - Profile picture.
        - Bio.
        - List of user posts.
- **Follow System**
    - Users can follow and unfollow other users.
    - Users can see a list of followers and people they follow.
- **Posts and Comments**
    - Users can create, read, update, and delete posts.
    - Every post can have tags.
    - Users can comment on posts.
    - Users can like posts.
- **Tags**
    - Posts can have multiple tags to categorize them.
- **Reporting System**
    - Users can report inappropriate posts or comments.

## Technologies Used

- **.NET Core 8**
    - Backend framework for building scalable APIs.
- **Entity Framework Core (EF Core)**
    - Object-relational mapper (ORM) for interacting with a PostgreSQL database.
- **CQRS (Command Query Responsibility Segregation)**
    - A pattern to separate write and read operations for optimized scalability.
- **Clean Architecture**
    - Follows the principles of separation of concerns and maintainability, with distinct layers for domain, application, and infrastructure.
- **Domain-Driven Design (DDD)**
    - Follows DDD practices to model business logic and domain entities.
- **EmailFluent**
    - A library used for sending email confirmations during user registration.
