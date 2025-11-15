RentalPE Platform — REST API Technical Stories
Overview

This document contains API-focused technical stories intended for frontend or mobile developers integrating with the RentalPE REST API.

The platform is organized into these bounded contexts:

User — registro y autenticación básicos.

Profile — perfil extendido del usuario (fuera de este set de historias).

Space Management — propiedades y espacios, más mantenimiento.

Payment — pagos, gastos, presupuestos y recordatorios.

Monitoring — monitoreo IoT, lecturas, incidentes y notificaciones.

Combo Management — combos/paquetes de propiedades.

Dashboard & Reports (read models) — vistas agregadas para el usuario.

Common conventions

Base path recomendada: /api/v1

Algunos endpoints heredados usan rutas específicas como /api/users/*.

Los códigos de respuesta (200, 201, 202, 204, 400, 401, 403, 404, 409) se listan en cada historia según el comportamiento esperado.

Bounded Context: User

El contexto User expone comandos básicos de registro y autenticación.

TS-USER-001 — User Registration

As a frontend developer, I want to register new users through the API so that I can implement the sign-up flow, create accounts securely, and handle duplicate emails in the UI.

Acceptance criteria:

Scenario: Successful registration

Given a POST request to /api/users/register is received with a body containing valid attributes: email, password

When the API validates and persists the new user

Then the API responds 201 Created and stores the user in the database

Scenario: Email already registered

Given a POST request to /api/users/register is received with an email that is already registered

When the API detects the conflict

Then the API responds 409 Conflict and returns an error payload indicating that the email is already in use

TS-USER-002 — User Authentication

As a frontend developer, I want to authenticate users through the API so that I can implement sign-in flows and obtain an auth token for protected requests.

Acceptance criteria:

Scenario: Valid credentials

Given a POST request to /api/users/login is received with valid email and password

When the API verifies the credentials

Then the API responds 200 OK and returns an authentication token (e.g., JWT) plus minimal user info

Scenario: Invalid credentials

Given a POST request to /api/users/login is received with invalid email or password

When the API fails to authenticate the user

Then the API responds 401 Unauthorized and returns an error payload

Bounded Context: Space Management

El contexto Space Management gestiona los espacios/proiedades y su mantenimiento.

TS-SPACE-001 — Create a Space

As a frontend developer, I want to create spaces through the API so that I can implement flows for registering new properties/spaces in the system.

Acceptance criteria:

Scenario: Successful create

Given a POST request to /api/v1/space is received with a body containing a valid Space payload (all required attributes)

When the API validates and persists the new space

Then the API responds 201 Created and returns the created Space resource

Scenario: Validation error

Given a POST request to /api/v1/space is received with incomplete or invalid attributes

When the API rejects the payload

Then the API responds 400 Bad Request with validation error details

TS-SPACE-002 — List Spaces

As a frontend developer, I want to list spaces so that I can build catalogue screens and admin views for spaces.

Acceptance criteria:

Scenario: List spaces

Given a GET request to /api/v1/space is received, optionally with pagination parameters

When the API retrieves spaces

Then the API responds 200 OK with a paginated array of Space resources (the array may be empty)

TS-SPACE-003 — Get a Space by id

As a frontend developer, I want to fetch a space by {spaceId} so that I can show its details and handle not-found states.

Acceptance criteria:

Scenario: Found

Given a GET request to /api/v1/space/{id} is received with an existing id

When the API finds the space

Then the API responds 200 OK and returns the corresponding Space resource

Scenario: Not found

Given a GET request to /api/v1/space/{id} is received with a non-existent id

When the API cannot find the space

Then the API responds 404 Not Found

TS-SPACE-004 — Update a Space

As a frontend developer, I want to update an existing space so that I can edit its attributes from admin screens.

Acceptance criteria:

Scenario: Successful update

Given a PUT request to /api/v1/space/{id} is received with an existing id and a valid updated Space payload

When the API validates and applies the changes

Then the API responds 200 OK and returns the updated Space resource

Scenario: Space not found

Given a PUT request to /api/v1/space/{id} is received with a non-existent id

When the API cannot find the space

Then the API responds 404 Not Found

TS-SPACE-005 — Delete a Space

As a frontend developer, I want to delete a space so that I can build flows for removing spaces that are no longer needed.

Acceptance criteria:

Scenario: Successful delete

Given a DELETE request to /api/v1/space/{id} is received with an existing id

When the API deletes the space

Then the API responds 204 No Content and the space is removed from the database

Scenario: Space has active references

Given a DELETE request to /api/v1/space/{id} is received for a space that has active references (e.g., bookings, monitoring, payments)

When the API detects that deletion is not allowed

Then the API responds 409 Conflict or 403 Forbidden and returns a payload describing why the space cannot be deleted

TS-SPACE-006 — Property Maintenance Calendar

(From US15 — Calendario de Mantenimiento de Propiedades)

As a frontend developer, I want to manage maintenance tasks per property so that users can schedule, track, and complete maintenance actions.

Acceptance criteria:

Scenario: Create maintenance task

Given a POST request to /api/v1/maintenance is received with a body containing: propertyId, type, date, and optional provider

When the API validates and persists the task

Then the API responds 201 Created and returns a MaintenanceTask resource with status = pending

Scenario: Reminder notification

Given a maintenance task scheduled for a certain date

And a background job/CRON runs at date - 7 days

When the job processes upcoming tasks

Then the system sends a reminder notification using the user’s preferred channel(s)

Scenario: Mark task as done

Given a PATCH request to /api/v1/maintenance/{id}/done is received, optionally with an evidence file attached

When the API finds the maintenance task

Then the API updates its status to completed, optionally stores the evidence, and responds 200 OK with the updated task

Bounded Context: Payment

El contexto Payment cubre pagos, gastos, presupuestos y recordatorios.

TS-PAY-001 — Create a Payment

(From US59 — Creación de Pago)

As a frontend developer, I want to create payments through the API so that I can start payment flows and persist payment aggregates in PENDING status.

Acceptance criteria:

Scenario: Successful create

Given a POST request to /api/v1/payments is received with a valid payment payload

When the API validates and creates the payment aggregate

Then the API responds 201 Created and returns the created Payment resource with status = PENDING

Scenario: Invalid payment data

Given a POST request to /api/v1/payments is received with invalid data (e.g., negative amount)

When the API rejects the request

Then the API responds 400 Bad Request with validation errors

TS-PAY-002 — Get and Search Payments

(From US60 — Consulta de Pagos)

As a frontend developer, I want to retrieve payments by id and via search filters so that I can implement detail views and listing screens.

Acceptance criteria:

Scenario: Get payment by id

Given a GET request to /api/v1/payments/{id} is received for an existing payment

When the API finds the payment

Then the API responds 200 OK and returns a Payment DTO

Scenario: Search payments

Given a GET request to /api/v1/payments is received with filter parameters (e.g., status, date range, propertyId)

When the API applies filters and pagination

Then the API responds 200 OK with a paginated list of Payment DTOs (possibly empty)

TS-PAY-003 — Initiate and Confirm Payment

(From US61 — Flujo de Iniciación y Confirmación)

As a frontend developer, I want to transition payments from PENDING to INITIATED and COMPLETED so that I can coordinate UI flows with the payment lifecycle.

Acceptance criteria:

Scenario: Initiate payment

Given a payment currently in PENDING status

When a POST request to /api/v1/payments/{id}/initiate is received

Then the API transitions the payment to INITIATED

And responds 202 Accepted with the updated payment state

Scenario: Confirm payment

Given a payment currently in INITIATED status

When a POST request to /api/v1/payments/{id}/confirm is received

Then the API transitions the payment to COMPLETED

And responds 200 OK with the updated payment state

TS-PAY-004 — Cancel and Refund Payment

(From US62 — Flujo de Reversión)

As a frontend developer, I want to cancel or refund payments so that I can implement reversal flows in the UI according to business rules.

Acceptance criteria:

Scenario: Cancel payment

Given a payment eligible for cancellation

When a POST request to /api/v1/payments/{id}/cancel is received

Then the API transitions the payment to CANCELLED

And responds 200 OK with the updated payment state

Scenario: Refund payment

Given a payment in COMPLETED status that meets business rules for refund

When a POST request to /api/v1/payments/{id}/refund is received

Then the API transitions the payment to REFUNDED

And responds 202 Accepted with the updated payment state

TS-PAY-005 — Property Expenses: Create and List

(From US12 — Monitoreo de Gastos por Propiedad)

As a frontend developer, I want to create and list expenses per property so that users can track monthly costs (water, electricity, maintenance, others).

Acceptance criteria:

Scenario: Create expense

Given a POST request to /api/v1/expenses is received with a JSON body containing: propertyId, category, amount, date, optional attachment

When the API validates and creates the expense

Then the API responds 201 Created, persists the expense, and updates relevant monthly totals/aggregations

Scenario: List expenses with filters

Given a GET request to /api/v1/expenses is received with optional filters (propertyId, category, dateRange, pagination)

When the API processes the query

Then the API responds 200 OK with a paginated list of expenses plus aggregated values (e.g., totals per month and per category)

Scenario: Download attachment

Given an expense with a stored attachment

When a request is made to download it (e.g., GET /api/v1/expenses/{expenseId}/attachment)

Then the API returns the file in a secure manner (authorized access, correct content-type)

TS-PAY-006 — Monthly Budgets and Overspend Alerts

(From US13 — Presupuesto y Alertas de Sobre-Gasto)

As a frontend developer, I want to manage monthly budgets per property so that I can display budget usage and overspend alerts.

Acceptance criteria:

Scenario: Create or update monthly budget

Given a POST or PUT request to /api/v1/budgets/{propertyId} is received with a monthlyAmount

When the API validates and saves the budget

Then the budget becomes active from the first day of the next month

And the API responds 200 OK or 201 Created with the budget resource

Scenario: Preventive alert (≥ 90%)

Given expenses for a property are updated

And the total for the current month reaches ≥ 90% of the active budget

When the system processes the update

Then it triggers a preventive alert to the user

Scenario: Critical alert (> 100%)

Given expenses for a property are updated

And the total for the current month is > 100% of the active budget

When the system processes the update

Then it triggers a critical alert

Scenario: Get budget summary

Given a GET request to /api/v1/budgets/{propertyId} is received

When the API finds the budget

Then it responds 200 OK with: budget amount, consumption to date, and percentage used

TS-PAY-007 — Export Payment and Expense History

(From US20 — Exportar Historial de Pagos y Gastos)

As a frontend developer, I want to request exports of payment and expense history so that users can download CSV/XLSX/PDF files for accounting.

Acceptance criteria:

Scenario: Request export

Given a POST request to /api/v1/exports/financial-history is received with filters (propertyId, dateRange, category/status, desired format: CSV/XLSX/PDF)

When the API validates the request

Then it enqueues an export job and responds 202 Accepted with a job identifier and a pending download URL or status endpoint

Scenario: Large dataset (> 100k records)

Given the export corresponds to more than 100k records

When the job runs

Then the system exports in batches and consolidates the final file before making it available

Scenario: Download link and expiration

Given the export job completes successfully

When the user accesses the provided download URL

Then the system serves the file

And if more than 7 days have passed since generation, the link has expired, the file is purged, and the API responds with an appropriate error (e.g., 410 Gone)

TS-PAY-008 — Bulk Import of Expenses

(From US38 — Importación Masiva de Gastos)

As a frontend developer, I want to import expenses from CSV/XLSX so that users can quickly load historical data.

Acceptance criteria:

Scenario: Successful import

Given a POST request to /api/v1/expenses/import is received with a CSV/XLSX file containing valid columns

When the API validates the structure and rows

Then it creates expense records for valid rows and returns an import summary (total rows, created, skipped, errors)

Scenario: Row-level validation and error report

Given some rows contain invalid data

When the API processes the file

Then it skips invalid rows and includes detailed error information per row in the response (or via downloadable report)

Scenario: Duplicate detection

Given some rows represent duplicate expenses (same hash or unique constraint)

When the API detects duplicates

Then it ignores them and includes them in the report as duplicates, without creating new records

TS-PAY-009 — Pending Payments and Reminders

(From US39 — Listado y Recordatorios de Pagos Pendientes)

As a frontend developer, I want to list pending payments and show due dates so that users can avoid late fees, and the system can send reminders.

Acceptance criteria:

Scenario: List pending payments

Given a GET request to /api/v1/payments?status=pending is received

When the API retrieves pending payments for the authenticated user

Then it responds 200 OK with items that include: amount, due date, remaining days, and payment link

Scenario: Reminder CRON (T-3 / T-1)

Given a background job/CRON runs daily

When it finds payments with due date in 3 days or 1 day

Then it sends reminder notifications via the user’s preferred channel(s)

Scenario: PSP webhook confirmation

Given the payment service provider (PSP) calls a webhook endpoint (e.g., POST /api/v1/payments/webhooks/{provider})

When the webhook confirms a payment

Then the API updates the payment status to a non-pending state (e.g., COMPLETED)

And the payment no longer appears in the pending list

Bounded Context: Monitoring

El contexto Monitoring abarca proyectos de monitoreo, lecturas IoT, dispositivos, incidentes y notificaciones.

TS-MON-001 — Register Monitoring Project

(From US63 — Registro de Proyecto de Monitoreo)

As a frontend developer, I want to register monitoring projects so that new monitored properties can be configured in the system.

Acceptance criteria:

Scenario: Successful create

Given a POST request to /api/v1/monitoring/projects is received with all required project data

When the API validates and creates the project aggregate

Then it responds 201 Created and returns the Project DTO

Scenario: Duplicate project identifier

Given a POST request to /api/v1/monitoring/projects is received with an identifier that already exists

When the API detects the conflict

Then it responds 409 Conflict with details

TS-MON-002 — Get Monitoring Project by id

(From US64 — Consulta de Proyecto por ID)

As a frontend developer, I want to fetch a monitoring project by {projectId} so that I can show its configuration.

Acceptance criteria:

Scenario: Found

Given a GET request to /api/v1/monitoring/projects/{id} is received for an existing project

When the API finds the project

Then it responds 200 OK with the Project DTO

Scenario: Not found

Given a GET request to /api/v1/monitoring/projects/{id} is received with a non-existent id

When the API does not find the project

Then it responds 404 Not Found

TS-MON-003 — Single Reading Ingestion and Consumption Time Series

(From US19 — Ingesta de Lecturas (medidores) / Consumo)

As a frontend developer or IoT client, I want to send individual consumption readings and query aggregated series so that I can feed reports and alerts.

Acceptance criteria:

Scenario: Ingest signed reading

Given a POST request to /api/v1/readings is received with a signed payload containing: propertyId, type, value, timestamp

When the API validates the signature, normalizes the data, and saves the reading

Then it responds 201 Created (or 202 Accepted if async) and stores the reading

And if the value is out of expected range, the system emits a ReadingOutOfRange event

Scenario: Query readings with range

Given a GET request to /api/v1/readings is received with a range parameter (e.g., date range) and optional grouping options

When the API aggregates readings per day/month

Then it responds 200 OK with time-series data and aggregates

TS-MON-004 — Batch Sensor Readings Ingestion

(From US65 — Ingesta de Lecturas de Sensores)

As a frontend / integration developer, I want to send batches of sensor readings so that ingestion is efficient and can be processed asynchronously.

Acceptance criteria:

Scenario: Valid batch, async processing

Given a POST request to /api/v1/monitoring/readings is received with a batch of valid readings

When the API validates and enqueues them for processing/storage

Then it responds 202 Accepted and the readings are queued for asynchronous handling

Scenario: Invalid or unauthorized batch

Given a POST request to /api/v1/monitoring/readings is received with invalid data or from a non-authorized source

When the API validates the payload and checks authorization

Then it responds 400 Bad Request for bad data or 403 Forbidden for unauthorized source

TS-MON-005 — Create Monitoring Task

(From US66 — Creación de Tarea de Monitoreo)

As a frontend developer, I want to create monitoring tasks so that I can configure periodic monitoring rules for projects.

Acceptance criteria:

Scenario: Successful create

Given a POST request to /api/v2/monitoring/tasks is received with all required parameters (e.g., projectId, metric, threshold, periodicity)

When the API validates the configuration

Then it responds 201 Created and stores the Task aggregate

Scenario: Invalid configuration

Given a POST request to /api/v2/monitoring/tasks is received with invalid parameters (e.g., negative periodicity)

When the API validates the payload

Then it responds 400 Bad Request with details

TS-MON-006 — Get Monitoring Task by id

(From US67 — Consulta de Tarea por ID)

As a frontend developer, I want to get a monitoring task by {taskId} so that I can display its configuration and execution status.

Acceptance criteria:

Given a GET request to /api/v2/monitoring/tasks/{id} is received

When the task exists

Then the API responds 200 OK with the Task DTO (including configuration and status)

When the task does not exist

Then the API responds 404 Not Found

TS-MON-007 — Register IoT Device

(From US68 — Registro de Dispositivo IoT)

As a frontend or provisioning tool developer, I want to register IoT devices so that they can be associated with monitoring projects.

Acceptance criteria:

Scenario: Successful create

Given a POST request to /api/v1/monitoring/io-t-devices is received with all required device data and a valid projectId

When the API validates and creates the device aggregate

Then it responds 201 Created and returns the IoTDevice DTO

Scenario: Duplicate device id

Given a POST request with a device identifier that already exists

When the API detects the conflict

Then it responds 409 Conflict

TS-MON-008 — List Devices by Project

(From US69 — Consulta de Dispositivos por Proyecto)

As a frontend developer, I want to list IoT devices by project so that I can build management screens per monitored project.

Acceptance criteria:

Scenario: Devices found

Given a GET request to /api/v1/monitoring/io-t-devices/project/{projectId} is received with a valid projectId

When the API finds devices associated to that project

Then it responds 200 OK with a list of IoTDevice DTOs

Scenario: No devices

Given a valid projectId with no associated devices

When the API processes the request

Then it responds 200 OK with an empty list

TS-MON-009 — List Incidents by Project

(From US70 — Consulta de Incidentes por Proyecto)

As a frontend developer, I want to list incidents per project so that users can see active and historical monitoring issues.

Acceptance criteria:

Scenario: Incidents found

Given a GET request to /api/v1/monitoring/incidents/project/{projectId} is received

When the API finds incidents

Then it responds 200 OK with a paginated, filtered list of Incident DTOs

Scenario: No incidents

Given a GET request to the same endpoint for a project with no incidents

When processed

Then it responds 200 OK with an empty list

TS-MON-010 — Acknowledge Incident

(From US71 — Reconocimiento de Incidente)

As a frontend developer, I want to acknowledge incidents so that recurring notifications stop once someone is handling the issue.

Acceptance criteria:

Scenario: Successful acknowledge

Given an existing incident that is not yet closed

When a PATCH request to /api/v1/monitoring/incidents/{id}/acknowledge is received

Then the API updates the incident state to acknowledged

And responds 200 OK with the updated incident

Scenario: Incident not found

Given a PATCH request to the same endpoint with a non-existent id

When the API cannot find the incident

Then it responds 404 Not Found

TS-MON-011 — List Notifications by Project

(From US72 — Consulta de Notificaciones por Proyecto)

As a frontend developer, I want to list notifications per project so that I can show alert history in the UI.

Acceptance criteria:

Scenario: Notifications found

Given a GET request to /api/v1/monitoring/notifications/project/{projectId} is received

When the API finds notifications

Then it responds 200 OK with a paginated, filtered list of Notification DTOs

Scenario: No notifications

Given the same request for a project with no notifications

Then the API responds 200 OK with an empty list

Bounded Context: Combo Management

El contexto Combo Management administra combos o paquetes de propiedades con precio, vigencia y estado.

TS-COMBO-001 — Create and Update Property Combos

(From US06 — Catálogo de Combos de Propiedades)

As a frontend developer, I want to create and edit property combos so that admins can offer promotions and sell property packages.

Acceptance criteria:

Scenario: Create combo

Given a POST request to /api/v1/combos is received with a valid payload containing: name, propertyIds, price, validityPeriod, and initial state (e.g., draft)

When the API validates data, ensures referenced properties are valid, and persists the combo

Then it responds 201 Created and stores the combo with versioning and state (draft / published)

Scenario: Update combo

Given a PUT request to /api/v1/combos/{comboId} is received with updated fields

When the API validates and creates a new version of the combo (or updates according to the versioning strategy)

Then it responds 200 OK and returns the updated combo including its state and version

Scenario: Auto-expire published combo

Given a combo in published state with a defined validity period

When the current date falls outside the validity period

Then the system transitions the combo state to expired (e.g., via scheduled job) and it should no longer be offered in active listings

Scenario: Prevent publishing with inactive properties

Given a combo that references one or more inactive properties

When a request tries to publish the combo (e.g., POST /api/v1/combos/{id}/publish)

Then the API blocks the operation, responds with an error (e.g., 400 or 409), and returns details of the inactive properties

Cross-context: Dashboard & Reports (Read Models)

Este “contexto” agrupa read models que consumen datos de Space Management, Payment y Monitoring.

TS-DASH-001 — User Dashboard Widgets

(From US45 — Servicio de Dashboard)

As a frontend developer, I want to fetch a dashboard with KPIs and widgets so that users can see an overview of their properties, alerts, consumption, and pending payments.

Acceptance criteria:

Scenario: Get dashboard

Given a GET request to /api/v1/dashboard is received for an authenticated user

When the API composes data from underlying bounded contexts

Then it responds 200 OK with a payload containing widgets such as:

Properties summary (count, progress)

Last alerts (up to 5)

Monthly energy/consumption series (12 months)

Pending payments (N items)

Scenario: Filtered dashboard

Given the same GET endpoint is called with filters (e.g., propertyId, dateRange)

When the API applies those filters

Then it recalculates and returns only the relevant widget data

Scenario: Degraded widget

Given one or more underlying services or data sources are temporarily unavailable

When the dashboard is requested

Then the API responds with a degraded dashboard where some widgets are marked with metadata (e.g., meta.staleUntil) indicating that the data is stale but still usable, instead of failing the entire response