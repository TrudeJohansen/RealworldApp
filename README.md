# Ratings System

This repo is a fork of [Realworld app](https://github.com/adr1enbe4udou1n/aspnetcore-realworld-example-app) intended for a brights course on dotnet.<br/>

## Table of Contents

- [Project Setup](#project-setup)
  - [Prerequisities](#prerequisites)
  - [Start the app](#start-the-app)
- [Data Model](#data-model)
  - [Rating Model](#rating-model)
  - [Relations](#relations)
  - [Migrations](#migrations)
- [Controller](#controller)
  - [End Points](#end-points)
  - [Create Rating](#create-ratings)
  - [List Ratings](#list-ratings)
  - [Delete Rating](#delete-ratings)
  - [Data Transfer Objects](#data-transfer-objects)
- [Further Work](#further-work)

<br/><br/>

## Project Setup

### Prerequisities

Ensure you have the following dependencies installed:

- [.NET 7.0.x](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Docker](https://docs.docker.com/get-docker/)

If you wish to inspect the database as well, you can download

- [pgadmin](https://www.pgadmin.org/download/)

### Start the app

This project uses PostgreSQL for its database. To start the database, simply do `docker-compose up -d` from the project folder.

To populate the database with sample data (seeding), run `make seed`. This is only necessary the first time you set up the program.

Finally, to start the server, use `make run`

Now you should be able to access swagger and inspect the existing API at http://localhost:5000/api

Start by reviewing the existing methods and endpoint in Swagger, and then try to find their implementations in the code. Become familiar with the project architecture and try to determine the patterns used. Once comfortable with the solution, move on to the data model.

<br/>
<br/>

## Data Model

The first part of the project is to modify the existing data model to allow for storing ratings. A user should be able to review each article and give a rating between 1 and 5 to each one. They cannot have more than one review per article. There are several ways you could implement this, start by thinking of how you would structure the relational database.
<br/>

### Rating Model

To add a new table to the database, you will have to create a new entity using Entity Framework. Define your entity class in the correct folder with the necessary properties.

### Relations

With the class and its properties defined, its relations to other entites must also be declared. Set up the necessary relations in the _DbContext_.

### Migrations

To apply the changes in the data model to the PostgreSQL database, you will have to create a migration. You can use the `dotnet ef` tool for this. To verify that the migration worked as intended, you can use a tool like pgadmin or the postgres CLI to inspect the database.

<br/>
<br/>

## Controller
Now that we have updated our data model, it´s time to move on to the controller responsible for handling rating requests. Start by creating a new controller in the correct folder.

### End Points

Before we start implementing the desired rating functionality, we should ensure that our desired endpoints are reachable. Thus, you should start by creating methods handling the http calls. You can start with returning 200 response codes.

The controller should support the following methods:

- POST
- GET
- DELETE

At the url `http://localhost:5000/api/articles/{article_slug}/ratings`

### Create Rating

Now that we have a skeleton for the incoming requests, it´s time to implement the actual logic. The first step is to define the create method. Create the necessary commands and update the controller with new logic to support the creation of new ratings.

> **N.B.** Remember to validate the rating! It should only allow integers between 1 and 5.

### List Ratings for Article

So we are able to create new ratings, but how can we retrieve them? Moreover, for each article, how can we see all the ratings it received? The answer: a **list** method.

Modify the list endpoint so that it returns all the ratings for a given article. You will have to create a new query for this.

### Delete Rating

Opinions can change over time, and perhaps an article you rated a 5 one year ago feels more like a 3 today. Thus, we want a way to delete ratings as well. Expand the previously defined delete endpoint to support this.

### Data Transfer Objects

To improve the security of our application, and not expose all the fields of the ratings, we should use Data Transfer Objects (DTOs). Define data transfer objects and modify the existing code to support these.

<br/><br/>

## Further Work

Congrats 👏 You have completed the provided tasks! What´s next? Try adding new methods or entities to the API, your imagination is the only limit.
