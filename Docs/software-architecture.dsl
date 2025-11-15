workspace "rentallPe" "Platform for commercial/residential remodeling with payments, IoT, and reports." {

    model {

        // People
        owner   = person "Owner/Entrepreneur" "Business owner or administrator who publishes and manages projects." "Person"
        client  = person "Client/User" "Contracts remodeling services and tracks progress." "Person"
        admin   = person "System Administrator" "Manages rules, catalogs, and support." "Person"

        // External Systems
        psp        = softwareSystem "PSP / Payment Gateway" "Authorization and capture (cards/wallets)." "ExternalSystem"
        einvoice   = softwareSystem "E-Invoicing" "Issues electronic invoices." "ExternalSystem"
        notifier   = softwareSystem "Notification Service" "Push/Email/SMS (FCM/SES/etc.)." "ExternalSystem"
        iotHub     = softwareSystem "IoT Broker" "Telemetry ingestion (MQTT/HTTP)." "ExternalSystem"

        // Main System
        rentallPe = softwareSystem "rentallPe" "Web app for profiles, payments, IoT, and remodeling reports." "SoftwareSystem" {

            // Frontend and API
            spa        = container "Single-Page Application" "Web UI for owners and clients." "Vue 3 / SPA" "WebApp"
            apiRest    = container "API REST" "Routing, security, and observability." "REST API" "ApiRest"
            apiCmd     = container "Command API" "Commands (REST) - write operations." "Node/.NET/Java" "Container,Service"
            apiQry     = container "Query API"   "Queries (REST) - read operations." "Node/.NET/Java" "Container,Service"

            // Databases (CQRS)
            writeDb = container "Write Database" "OLTP, write consistency." "PostgreSQL" "Database"
            readDb  = container "Read Database"  "OLAP/Elastic, read projections." "PostgreSQL/Elastic" "Database"

            // 1) Identity & Access Management (BC)
            iam = container "Identity & Access Management BC" "Registration, login, roles, sessions, and recovery." "Service (DDD)" "BoundedContext,Container" {
                authController = component "Auth Controller" "REST controller for /auth." "Controller" "Controller"
                authCmdSvc     = component "Auth CommandService" "Credential and role rules." "Service" "Service"
                authQrySvc     = component "Auth QueryService" "User, role, and session reads." "Service" "Service"
                userEnt        = component "User" "Entity." "Entity" "Entity"
                roleEnt        = component "Role" "Entity." "Entity" "Entity"
                sessionEnt     = component "Session" "Entity." "Entity" "Entity"

                apiRest -> authController "Req /auth/*"
                authController -> authCmdSvc "commands"
                authController -> authQrySvc "queries"
                authCmdSvc -> writeDb "writes"
                authQrySvc -> readDb  "reads"
                authCmdSvc -> notifier "Send verification or password reset"
            }

            // 2) Profile & Preferences Management (BC)
            profile = container "Profile & Preferences BC" "Profiles, contact info, and preferences." "Service (DDD)" "BoundedContext,Container" {
                profController = component "Profile Controller" "REST controller /profiles." "Controller" "Controller"
                profCmdSvc     = component "Profile CommandService" "Update profile and preferences." "Service" "Service"
                profQrySvc     = component "Profile QueryService" "Read profile and preferences." "Service" "Service"
                prefEnt        = component "Preferences" "Entity." "Entity" "Entity"

                apiRest -> profController "Req /profiles/*"
                profController -> profCmdSvc "commands"
                profController -> profQrySvc "queries"
                profCmdSvc -> writeDb "writes"
                profQrySvc -> readDb  "reads"
                profCmdSvc -> notifier "Notify profile changes"
            }

            // 3) Payment Management (BC)
            payments = container "Payment Management BC" "Charges, confirmations, reconciliations, and invoicing." "Service (DDD)" "BoundedContext,Container" {
                payController  = component "Payment Controller" "REST controller /payments." "Controller" "Controller"
                payCmdSvc      = component "Payment CommandService" "Authorize, capture, refund." "Service" "Service"
                payQrySvc      = component "Payment QueryService" "Payment status and receipts." "Service" "Service"
                invoicePolicy  = component "Invoicing Policy" "Emit invoice when payment is captured." "Policy" "Policy"

                apiRest -> payController "Req /payments/*"
                payController -> payCmdSvc "commands"
                payController -> payQrySvc "queries"
                payCmdSvc -> psp "Authorize/Capture (webhooks)"
                psp -> payCmdSvc "Status webhook"
                payCmdSvc -> writeDb "writes"
                payQrySvc -> readDb  "reads"
                payCmdSvc -> invoicePolicy "event payment_captured"
                invoicePolicy -> einvoice "Emit invoice"
                payCmdSvc -> notifier "Send receipt"
            }

            // 4) IoT Monitoring & Notifications (BC)
            iot = container "IoT Monitoring & Notifications BC" "Sensor ingestion, thresholds, anomalies, and alerts." "Service (DDD)" "BoundedContext,Container" {
                ingestSub     = component "Ingest Subscriber" "IoT broker subscriber." "Service" "Service"
                ruleEngine    = component "Rule Engine" "Thresholds, severity, and anomaly detection." "Service" "Service"
                alertCmdSvc   = component "Alert CommandService" "Create, Ack, Close alerts." "Service" "Service"
                alertQrySvc   = component "Alert QueryService" "List and filter alerts." "Service" "Service"

                iotHub -> ingestSub "Telemetry"
                ingestSub -> ruleEngine "processes"
                ruleEngine -> alertCmdSvc "creates alert"
                alertCmdSvc -> writeDb "writes"
                alertQrySvc -> readDb  "reads"
                alertCmdSvc -> notifier "Notify"
            }

            // 5) Reports & Advanced Features (BC)
            reports = container "Reports & Advanced Features BC" "PDF/CSV reports, comparisons, and ESG metrics." "Service (DDD)" "BoundedContext,Container" {
                rptController  = component "Reports Controller" "REST controller /reports." "Controller" "Controller"
                rptJob         = component "Report Scheduler" "Scheduled execution." "Service" "Service"
                renderSvc      = component "Render Service" "PDF/CSV/XLSX generator." "Service" "Service"

                apiRest -> rptController "Req /reports/*"
                rptController -> renderSvc "generate"
                rptJob -> renderSvc "scheduled"
                renderSvc -> readDb "reads projections"
                renderSvc -> notifier "Send link or attachment"
            }

            // 6) Space Management (BC)
            spaceMgmt = container "Space Management BC" "Publish, update, pause, and review spaces by owners." "Service (DDD)" "BoundedContext,Container" {
                spaceController = component "Space Controller" "REST controller /spaces." "Controller" "Controller"
                spaceCmdSvc     = component "Space CommandService" "Publish, update, and pause spaces." "Service" "Service"
                spaceQrySvc     = component "Space QueryService" "Read listed, paused, or reserved spaces." "Service" "Service"
                validationPol   = component "Validation Policy" "Validate new space before publishing." "Policy" "Policy"
                ownershipPol    = component "Ownership Policy" "Ensure only the owner can modify a space." "Policy" "Policy"
                spaceEnt        = component "Space" "Entity representing a rentable unit." "Entity" "Entity"
                reviewEnt       = component "Review" "Entity representing tenant feedback." "Entity" "Entity"

                apiRest -> spaceController "Req /spaces/*"
                spaceController -> spaceCmdSvc "commands"
                spaceController -> spaceQrySvc "queries"
                spaceCmdSvc -> validationPol "validate publish"
                spaceCmdSvc -> ownershipPol "check ownership"
                spaceCmdSvc -> writeDb "writes"
                spaceQrySvc -> readDb  "reads"
                spaceCmdSvc -> notifier "Notify updates or pauses"
                iam -> spaceMgmt "Authenticate owner before publishing/updating"
            }

            // Usage
            owner  -> spa "Publishes/manages"
            client -> spa "Contracts/views"
            admin  -> spa "Administers"

            spa -> apiRest "HTTP"
            apiRest -> apiCmd "POST commands"
            apiRest -> apiQry "GET queries"

            // Orchestration (API -> BC)
            apiCmd -> iam "Commands"
            apiCmd -> profile "Commands"
            apiCmd -> payments "Commands"
            apiCmd -> iot "Commands"
            apiCmd -> reports "Commands"
            apiCmd -> spaceMgmt "Commands"

            apiQry -> iam "Queries"
            apiQry -> profile "Queries"
            apiQry -> payments "Queries"
            apiQry -> iot "Queries"
            apiQry -> reports "Queries"
            apiQry -> spaceMgmt "Queries"
        }

        // System Context
        owner  -> rentallPe "Publishes/manages projects and payments"
        client -> rentallPe "Contracts, monitors, and pays"
        admin  -> rentallPe "Manages rules and catalogs"
    }

    views {

        systemContext rentallPe "c1_context" "System Context - rentallPe" {
            include *
            autoLayout lr
        }

        container rentallPe "c2_containers" "Main containers (CQRS + BCs)" {
            include *
            autoLayout lr
        }

        component iam "c3_iam" "Components - Identity and Access" {
            include *
            autoLayout tb
        }

        component profile "c3_profile" "Components - Profile and Preferences" {
            include *
            autoLayout tb
        }

        component payments "c3_payments" "Components - Payment Management" {
            include *
            autoLayout tb
        }

        component iot "c3_iot" "Components - IoT Monitoring and Notifications" {
            include *
            autoLayout tb
        }

        component reports "c3_reports" "Components - Reports and Advanced Features" {
            include *
            autoLayout tb
        }

        component spaceMgmt "c3_spaceMgmt" "Components - Space Management" {
            include *
            autoLayout tb
        }

        styles {

            element "Person" {
                shape Person
                background #7A3CE7
                color #FFFFFF
            }

            element "SoftwareSystem" {
                background #FFFFFF
                color #111111
            }

            element "ExternalSystem" {
                background #FABB32
                color #111111
            }

            element "Container" {
                shape RoundedBox
                background #805B17
                color #FFFFFF
            }

            element "BoundedContext" {
                shape RoundedBox
                background #9A6A28
                color #FFFFFF
            }

            element "Service" {
                shape RoundedBox
                background #2F855A
                color #FFFFFF
            }

            element "Controller" {
                shape RoundedBox
                background #1D75F0
                color #FFFFFF
            }

            element "ApiRest" {
                shape RoundedBox
                background #884A39
                color #FFFFFF
            }

            element "WebApp" {
                shape RoundedBox
                background #2067F5
                color #FFFFFF
            }

            element "Entity" {
                shape Ellipse
                background #42B8C2
                color #FFFFFF
            }

            element "Policy" {
                shape Diamond
                background #C22777
                color #FFFFFF
            }

            element "Database" {
                shape Cylinder
                background #E53E3E
                color #FFFFFF
            }
        }
    }

    configuration {
        scope softwaresystem
      }
}