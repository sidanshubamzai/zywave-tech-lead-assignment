# Incident Management System

This project is a simple incident management system built as part of a technical assignment.
The system allows users to:

* Create incidents
* Upload attachments (for example, screenshots)
* Update incident status
* Trigger a notification when a new incident is created

---

## Live Demo

**Frontend UI**
[https://black-water-0dbcc8700.1.azurestaticapps.net](https://black-water-0dbcc8700.1.azurestaticapps.net)

**Backend API (Swagger)**
[https://incidentmanagementsystemapi20260102123745-bzdvfffrgzgtacc3.canadacentral-01.azurewebsites.net/swagger/index.html]


---

## High-level Overview

The solution follows a backend-first approach.

* Backend API built using **ASP.NET Core**
* Attachments stored in **Azure Blob Storage**
* Notifications handled asynchronously using **Azure Functions**
* Backend deployed on **Azure App Service**
* Logs and execution details monitored using **Application Insights**

All components are deployed and verified in Azure.

---

## Frontend Overview

The frontend is a lightweight React application used to demonstrate the backend functionality in a real user flow.

It allows users to:
* View the list of incidents
* Create new incidents with attachments
* Download uploaded attachments
* Update incident status directly from the UI

The UI communicates with the backend API using HTTP calls and reflects changes immediately.
It is deployed using **Azure Static Web** Apps and connected to the live backend API.

---

## Design Overview

### API Layer

Exposes REST endpoints for creating and managing incidents.

### Service Layer

Contains business logic such as incident creation, status updates, file handling, and notification triggering.

### Data Layer

Uses a repository abstraction.
An in-memory implementation is used to keep the solution simple and easy to demonstrate. The design allows switching to a persistent data store without changes to controllers or services.

### Storage

Azure Blob Storage is used for storing uploaded files. Only file metadata is handled by the application.

### Notifications

An Azure Function is triggered when a new incident is created.
The function receives the incident payload and logs the notification using Application Insights.

---

## Features Implemented

### Incident Management

* Create incidents with title, description, and severity
* Retrieve incidents
* Update incident status (`Open`, `In Progress`, `Resolved`)
* Automatic creation and update timestamps

### File Attachments

* Upload attachments linked to an incident
* Files stored in Azure Blob Storage
* Uploads verified using the Azure Portal

### Notifications

* Azure Function triggered on incident creation
* Notification implemented using structured logging
* Logs visible in Application Insights
* Can be extended to email or other channels if required

---

## Azure Services Used

* **Azure App Service** – Hosts the backend API
* **Azure Blob Storage** – Stores incident attachments
* **Azure Functions** – Handles asynchronous notification logic
* **Application Insights** – Logging and monitoring for the API and function executions

---

## Deployment Details

* Backend API deployed to Azure App Service
* Environment-specific configuration stored in App Service settings
* Swagger enabled in the deployed environment
* Azure Function deployed separately and invoked via HTTP

---

## How to Test the Application

1. Open the Swagger URL from the deployed API.
2. Create a new incident using `POST /api/incidents`.
3. Upload an attachment for the incident.
4. Verify that:

   * The incident is created successfully
   * The file appears in Azure Blob Storage
   * A notification log appears in Application Insights

This demonstrates the complete end-to-end flow running in Azure.

---

## Notes on Data Persistence

For this assignment, an **in-memory repository** is used for storing incident metadata.
This keeps the solution lightweight and easy to demonstrate while focusing on architecture and cloud integration.

The data layer is abstracted behind an interface, so replacing the in-memory implementation with **Azure SQL (via EF Core)** would only require:

* Adding a DbContext
* Implementing a SQL-based repository
* Updating dependency injection configuration

No changes would be required in the API or service layers.

