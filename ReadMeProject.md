# RentalPE Platform

## Summary

RentalPE Platform is a property–rental and monitoring API application built with **C#**, **ASP.NET Core**, **Entity Framework Core** and a **relational database** (e.g., MySQL) for persistence.  
It follows a **Domain-Driven Design** approach and is organized into several bounded contexts such as User, Profile, Properties, Payment, Monitoring, Combo Management and Reports.

The solution also illustrates **OpenAPI documentation** configuration and integration with **Swagger UI**, plus a shared kernel for cross-cutting concerns.

## Features

- RESTful API
- OpenAPI documentation
- Swagger UI
- ASP.NET Core Web API
- Entity Framework Core
- Audit fields for creation and update dates (per aggregate)
- Custom route naming conventions
- Custom ORM naming conventions
- Relational database (MySQL compatible)
- Domain-Driven Design with:
    - Aggregates, Value Objects, Repositories
    - Application Services (commands & queries)
    - Anti-corruption layers between bounded contexts
- Shared kernel for documentation, persistence and mediator infrastructure

---

## Bounded Contexts

This version of RentalPE Platform is divided into several bounded contexts:

- **User**
- **Profile**
- **Properties (Space Management)**
- **Payment**
- **Monitoring**
- **Combo Management**
- **Reports & Dashboard**
- **Shared Kernel**

---

### User Context

The **User** context is responsible for managing the platform users’ identities (the “technical” user account).  
It exposes endpoints used for registration and authentication and coordinates with other contexts via IDs.

Its main capabilities include:

- Register a new user (sign-up).
- Authenticate a user (sign-in) and return an authentication token.
- Get user information by identifier (for internal use).
- Provide user identifiers to other bounded contexts (Profile, Payment, etc.) through its ACL.

Depending on configuration, this context can also handle:

- Password hashing and verification.
- Token generation and validation for authenticated requests.

---

### Profile Context

The **Profile** context is responsible for the **business view of the user**, including personal data and preferences.  
It uses aggregates such as `Profile` and `PreferenceSet`, plus value objects like `Address`, `Avatar`, `NotificationPrefs`, `Phone`, `PrivacySettings`, `QuietHours` and `UserId`.

Its features include:

- Create a new profile associated to an existing user.
- Update profile data (name, address, avatar, phone, etc.).
- Manage notification preferences (channels, quiet hours, language, theme).
- Manage privacy settings for the account.
- Manage payment methods attached to the profile (via `PaymentMethod` entity).
- Query profiles:
    - Get profile by id.
    - Get profile by user id.
    - Get profile by email.
    - Get preference sets filtered by language or theme.

This context also exposes an **anti-corruption layer (ACL)** to other BCs.  
The ACL provides operations such as:

- Create a Profile and return its ID on success.
- Get a Profile or PreferenceSet by key (email, userId, etc.).
- Resolve notification preferences for other contexts (e.g., Monitoring, Reports) when sending alerts.

---

### Properties (Space Management) Context

The **Properties** (or **Space Management**) context is responsible for managing rental **properties and spaces** and operational tasks associated to them.

Its responsibilities include:

- Register and maintain **spaces/properties** (core entity for rentals).
- Expose CRUD operations for spaces:
    - Create a new space.
    - Get a space by id.
    - Get all spaces with pagination/filtering.
    - Update an existing space.
    - Delete a space, validating that there are no blocking references.
- Manage **maintenance tasks** associated to properties:
    - Create maintenance work items (type, date, optional provider).
    - List and update maintenance tasks.
    - Mark tasks as completed and store optional evidence.

This context is the main source of truth for **properties** and is referenced by other BCs such as Combo Management, Payment, Monitoring and Reports.

---

### Payment Context

The **Payment** context is responsible for the lifecycle of **payments and invoices** related to the rental properties.  
Its domain model includes aggregates like `Payment`, `PaymentAudit`, `Invoice`, `InvoiceAudit`, and value objects such as `Money`, `Monitoring` (link to monitoring info) and `PaymentMethodSummary`. It also defines enums for `PaymentStatus`, `InvoiceStatus`, `PaymentMethodType`, and `Currency`.

Its capabilities include:

- **Payments**
    - Create a payment in `PENDING` status.
    - Initiate a payment (`INITIATED`).
    - Confirm a payment (`COMPLETED`).
    - Cancel a payment (`CANCELLED`) according to business rules.
    - Refund a payment (`REFUNDED`) when eligible.
    - Query payments by:
        - ID
        - status
        - userId
        - reference

- **Invoices**
    - Create and issue invoices associated to payments.
    - Send invoice emails.
    - Void invoices when required.
    - Query invoices by id, payment id, status or user id.

The Payment context exposes command and query services (`PaymentCommandService`, `PaymentQueryService`, etc.) and may integrate with external PSPs (Payment Service Providers) via webhooks.

An **anti-corruption layer (ACL)** allows other contexts (Profile, Reports, Monitoring) to work with payment summaries without coupling to internal payment models.

---

### Monitoring Context

The **Monitoring** context manages **IoT monitoring for remodelations and properties**, including projects, devices, readings, incidents and notifications.  
Its domain model has entities like `Project`, `IoTDevice`, `Reading`, `Incident`, `Notification` and `WorkItem`.

Its features include:

- **Projects**
    - Create and configure monitoring projects for properties.
    - Get project information by id.

- **IoT Devices**
    - Register devices associated with projects.
    - List devices by project.

- **Readings**
    - Ingest single or batched sensor readings (via commands like `IngestReadingCommand`).
    - Validate source and normalize data.
    - Detect anomalies via `AnomalyDetectorService`.
    - Raise domain events (e.g., reading out of range) for downstream processing.

- **Incidents**
    - Create incidents based on anomalies or rules.
    - List incidents per project (active or historical).
    - Acknowledge incidents (via `AcknowledgeIncidentCommand`) to stop recurrent notifications.

- **Notifications**
    - Generate notifications linked to incidents or threshold breaches.
    - List notifications per project.
    - Send alerts using `NotificationService`, respecting user preferences from the Profile context when necessary.

The Monitoring context includes infrastructure for persistence (EFC configuration & repositories) and uses an **ACL** to interact with other BCs while protecting its internal model.

---

### Combo Management Context

The **Combo** context (Combo Management) handles **bundles of properties/spaces** sold or rented together with special pricing and conditions.  
Its aggregate `Combo` uses value objects such as `Image`, `InstallDays` and `Price`.

Its capabilities include:

- Create a new combo from a set of properties/spaces.
- Update an existing combo, maintaining versioning and state (draft/published/expired).
- Delete/disable combos when they are no longer valid.
- Query combos:
    - Get combo by id.
    - List combos with filters (e.g., active, by property, etc.).
- Validate combos against referenced properties:
    - Prevent publishing combos that include **inactive** properties.
    - Auto-expire combos whose validity range has passed.

The Combo context integrates with **Properties** (to validate property references) and can be consumed by **Reports** and **Dashboard** to present promotional packages to users.

---

### Reports & Dashboard Context

The **Report** context provides **read models and reporting capabilities** that combine data from multiple BCs (Properties, Payment, Monitoring, Profile). It focuses on queries and background jobs instead of complex domain logic.

Its main responsibilities include:

- **Expenses and Budgets**
    - Record and query expenses by property, category and date.
    - Define monthly budgets per property and compute consumption percentage.
    - Trigger preventive and critical alerts when expenses reach ≥90% or exceed 100% of the defined budget.

- **Imports & Exports**
    - Import expenses in bulk from CSV/XLSX files, validating rows and handling duplicates.
    - Export large histories of payments and expenses to CSV/XLSX/PDF.
    - Process large exports asynchronously using queued jobs, and provide temporary download links with expiration.

- **Pending Payments & Reminders**
    - List pending payments for the user, including due dates and remaining days.
    - Schedule reminder jobs (e.g., T-3/T-1 days before due date) and send notifications through channels defined in the Profile context.
    - React to PSP webhooks to update payment status and remove items from the pending list.

- **Dashboard**
    - Expose a composite `/dashboard` endpoint that aggregates:
        - properties summary
        - recent alerts and incidents
        - monthly consumption series
        - pending payments and financial indicators
    - Support filters by property and date range.
    - Return degraded/stale widgets gracefully when some underlying services are unavailable.

This context is intentionally **read-oriented** and uses DTOs tailored to client needs, shielding consumers from the internal structure of each domain.

---

### Shared Kernel

The **Shared** folder represents a **shared kernel** that contains cross-cutting building blocks reused across bounded contexts.

It includes:

- **Application**
    - Internal abstractions like `IEventHandler` for application-level domain events.

- **Domain**
    - Shared base model and repository interfaces reused by multiple contexts.

- **Infrastructure**
    - Documentation configuration (OpenAPI/Swagger).
    - Common interfaces and abstractions.
    - Mediator integration for dispatching commands, queries and events.
    - Shared persistence helpers (e.g., base EF Core configurations).

The Shared kernel helps maintain consistency across contexts without breaking their independence, and is especially useful for technical concerns (not business rules).
