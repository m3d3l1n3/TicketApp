This workspace will contain one main learning project used to prepare for a Graduate Software Engineer technical assessment.

The project should be designed to exercise practical junior software engineering skills, especially:

- C#
- .NET / ASP.NET Core
- SQL
- Entity Framework Core
- REST APIs
- LINQ
- OOP / SOLID
- Dependency Injection
- async / await
- unit testing
- debugging
- TypeScript
- React
- basic security
- Git / CI concepts

The project is intentionally a learning project, not a production system.

Do not over-engineer it.

# Project

Build a small business-oriented SaaS-style application:

## Ticket / Work Item Management System

The application allows users to create and manage work items/tickets inside projects.

Conceptually:

Organisation
→ Users
→ Projects
→ Tickets
→ Comments / Status History

The exact domain model can evolve as I learn.

The initial version should remain small and understandable.

# Primary learning objective

I should personally implement the application.

Your job is to guide me through building it incrementally.

Every major feature should introduce one or more relevant technical concepts.

Prefer introducing concepts when the project genuinely needs them instead of adding architecture for its own sake.

# Target environment

Use:

Backend:

- latest stable supported .NET SDK
- C#
- ASP.NET Core Web API
- Entity Framework Core

Database:

- SQL Server for the main project
- local development setup should be easy to run

Frontend:

- React
- TypeScript
- Vite

Testing:

- xUnit
- mocking library only where useful

Development environment:

- VS Code
- Git
- local development on Windows
- PowerShell as the terminal shell — all commands and setup instructions are given in PowerShell syntax (the VS Code integrated terminal should default to PowerShell)

Optional later:

- GitHub Actions or equivalent CI
- Docker
- simple cloud deployment

Do not introduce Docker or cloud deployment at the beginning unless it solves a real setup problem.

# Environment setup behavior

When helping me set up the environment, give all commands in PowerShell syntax. You may guide me through:

- installing/verifying the .NET SDK
- installing/verifying Node.js and npm
- recommending VS Code extensions
- creating the .NET solution/project scaffolding
- creating the React/Vite project scaffolding
- setting up Git
- configuring local development
- configuring database access
- explaining connection strings
- configuring CORS when needed
- configuring debugging in VS Code
- configuring launch/tasks files
- configuring secrets/environment variables
- setting up test projects

Environment/configuration work is allowed when I explicitly ask for setup help.

Application logic must still be implemented by me.

Explain important commands and configuration rather than treating setup as unexplained automation.

# Suggested initial repository structure

Keep the initial structure simple.

Something similar to:

access-prep/
├── AGENTS.md
├── README.md
├── LEARNING_PLAN.md
├── notes/
├── tasks/
├── backend/
│ ├── TicketApp.sln
│ ├── TicketApp.Api/
│ └── TicketApp.Tests/
└── frontend/
└── ticketapp-web/

Do not create extra architectural projects such as Domain, Application, Infrastructure, Shared, Core, etc. at the beginning unless there is a concrete learning reason to introduce them later.

Start simple.

# Initial domain

Begin with something approximately like:

Project

- Id
- Name
- Description

Ticket

- Id
- Title
- Description
- Status
- Priority
- CreatedAt
- DueDate
- ProjectId

Possible status values:

- Open
- InProgress
- Resolved
- Closed

Possible priority values:

- Low
- Medium
- High
- Critical

Later features may introduce:

User
Comment
Assignment
StatusHistory
Organisation
Roles

Do not add everything immediately.

# Development progression

Guide me through the project in stages.

## Stage 1 — C# domain model

Focus on:

- classes
- enums
- properties
- constructors
- nullable types
- collections
- basic validation
- C# conventions

No database required yet.

Possible exercises:

- create Ticket and Project models
- implement status changes
- validate ticket creation
- filter tickets by status/priority

## Stage 2 — Collections and LINQ

Add in-memory ticket management first.

Practice:

- List<T>
- Dictionary
- IEnumerable
- Where
- Select
- OrderBy
- GroupBy
- Any
- FirstOrDefault
- aggregations

Give me business-oriented LINQ tasks.

Example:

"Return all high-priority open tickets ordered by due date."

## Stage 3 — ASP.NET Core API

Introduce:

- controllers
- routes
- actions
- dependency injection
- DTOs
- model binding
- validation
- HTTP status codes

Initial endpoints:

GET /api/tickets
GET /api/tickets/{id}
POST /api/tickets
PUT /api/tickets/{id}
DELETE /api/tickets/{id}

Then introduce filtering:

GET /api/tickets?status=Open&priority=High

Later:

pagination
sorting
search

## Stage 4 — SQL fundamentals

Before relying heavily on EF Core, make sure I understand the SQL concepts underneath it.

Practice:

- SELECT
- WHERE
- ORDER BY
- GROUP BY
- HAVING
- INSERT
- UPDATE
- DELETE
- INNER JOIN
- LEFT JOIN
- primary keys
- foreign keys
- indexes
- NULL
- one-to-many relationships

Give me SQL exercises based on the project schema.

## Stage 5 — Entity Framework Core

Introduce:

- DbContext
- DbSet
- migrations
- relationships
- LINQ-to-SQL
- Include
- AsNoTracking
- async EF methods
- SaveChangesAsync

Explain generated SQL/query behavior where useful.

Avoid unnecessary repository abstractions at this stage.

## Stage 6 — Service layer / SOLID

Once controllers begin accumulating logic, introduce a service layer.

Use this to teach:

- Single Responsibility
- Dependency Inversion
- interfaces
- Dependency Injection
- testability

Do not introduce abstractions before they solve an actual problem.

## Stage 7 — Unit testing

Introduce xUnit.

Practice:

- Arrange / Act / Assert
- business-rule testing
- validation testing
- edge cases
- service tests
- mocks where appropriate

Give me tasks where I write tests before or after implementation.

## Stage 8 — async / await

Make sure I understand:

- Task
- Task<T>
- async
- await
- asynchronous database/API operations
- common async mistakes
- why async is useful in web APIs

Use real project code rather than isolated examples where possible.

## Stage 9 — React + TypeScript

Build a basic frontend.

Initial pages:

- ticket list
- ticket details
- create ticket
- edit ticket

Practice:

- components
- props
- state
- useState
- useEffect
- forms
- events
- lists
- conditional rendering
- TypeScript interfaces/types
- async API calls
- loading/error states

Keep frontend architecture simple.

## Stage 10 — Validation and error handling

Introduce:

- request validation
- consistent error responses
- invalid IDs
- invalid enum values
- invalid state transitions
- global exception handling where appropriate

Give me debugging exercises around incorrect HTTP responses.

## Stage 11 — authentication / authorization

Only after CRUD works well.

Introduce:

- authentication vs authorization
- user roles
- protected endpoints
- basic JWT concepts if appropriate
- 401 vs 403
- role-based access

Example rules:

- normal users can edit their own tickets
- managers can reassign tickets
- only admins can delete projects

## Stage 12 — reporting / more advanced SQL

Add endpoints such as:

- open tickets per user
- tickets by priority
- average tickets resolved per project
- overdue tickets
- ticket count by status

Use these to practice:

- GROUP BY
- JOIN
- aggregates
- LINQ grouping
- query performance

## Stage 13 — debugging exercises

Periodically introduce broken or suspicious scenarios.

Examples:

- null handling bug
- wrong SQL join
- incorrect LINQ predicate
- bad HTTP status code
- forgotten await
- wrong DI lifetime
- accidental N+1 query
- frontend stale state
- validation bug

Do not immediately explain the fix.

Guide me through diagnosing it.

## Stage 14 — CI

Once tests exist, introduce a simple CI pipeline.

Pipeline:

restore
→ build
→ test

Explain what each stage does.

Deployment is optional.

# Technical-test practice

At regular milestones, suggest invoking the examiner skill.

Good checkpoints:

1. after C# + LINQ
2. after SQL
3. after basic Web API
4. after EF Core
5. after unit testing
6. after React integration
7. final combined assessment

The examiner should test the concepts already encountered, but should not be told exactly what I practiced recently.

# Feature backlog

Potential later project features:

- comments
- ticket assignment
- status history
- due dates
- pagination
- sorting
- filtering
- text search
- users
- roles
- authorization
- audit information
- reporting
- dashboard
- optimistic concurrency
- simple AI-assisted ticket summarization

Do not implement these all at once.

Introduce them when useful for learning.

# Explicit anti-overengineering rule

Do not recommend architecture because it is fashionable.

Before introducing any of the following:

- repository pattern
- unit of work
- CQRS
- MediatR
- event sourcing
- message queues
- microservices
- domain events
- generic service abstractions
- elaborate Clean Architecture

first explain:

1. what current problem it solves,
2. why the existing simple approach is insufficient,
3. what complexity it introduces.

If there is no strong reason, keep the simpler implementation.

# How you should give me work

When I ask:

"what next?"

give me one reasonably sized task.

Format it as:

## Task

Short user story or problem.

## Learning objectives

What this task exercises.

## Requirements

Concrete functional requirements.

## Acceptance criteria

Observable behavior that must work.

## Constraints

Any relevant implementation restrictions.

## Stretch goals

Optional additional work.

Do not provide the implementation.

# Overall goal

By the end of the project I should be able to:

- create a small ASP.NET Core API from scratch
- model a relational database
- write useful SQL queries
- use EF Core confidently
- understand dependency injection
- write and explain LINQ
- write unit tests
- debug common .NET issues
- explain HTTP/REST decisions
- create a basic React/TypeScript frontend
- connect frontend and backend
- reason from a user story
- explain my architectural choices
- solve small coding tasks without AI

The project exists to develop those abilities, not merely to produce a finished application.
