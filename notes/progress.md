# Curriculum Progress & Assessment Scope

The examiner skill MUST read this file before issuing any assessment task. It defines what the
candidate has actually been taught. Assessment tasks may ONLY require topics listed as **in
scope**. Anything under **out of scope** must not appear in task requirements, acceptance
criteria, or grading — even if scaffolding for it exists in the repo (e.g. the xUnit project was
scaffolded during setup but testing has not been taught).

## Current stage

Completed: Stage 1 (C# domain model), Stage 2 (Collections & LINQ).
Next up: Stage 3 (ASP.NET Core API).

## In scope (may be assessed)

- C# fundamentals: classes, enums, properties (auto-properties, private setters), constructors,
  fields, `readonly`, namespaces, naming conventions
- Nullable reference/value types (`string?`, `DateOnly?`, `.HasValue`)
- Exceptions for business-rule enforcement (`ArgumentException`, `InvalidOperationException`)
- Collections: `List<T>`, `Dictionary<K,V>`, `IEnumerable<T>`, arrays
- LINQ (method syntax): `Where`, `Select`, `OrderBy`/`OrderByDescending`, `ThenBy`, `GroupBy`,
  `ToDictionary`, `ToList`, `FirstOrDefault`, `Any`, `Count`; lazy evaluation concept
- `DateTime`/`DateOnly` basics
- Encapsulation, domain-model business rules, state-machine style transition guards
- Git basics: init, add, commit, branch, remotes, push

## Out of scope (NOT yet taught — must not be assessed)

- Unit testing / xUnit (Stage 7) — test project exists but is scaffolding only
- ASP.NET Core: controllers, routing, DI, DTOs, model binding, HTTP status codes (Stage 3)
- SQL (Stage 4)
- Entity Framework Core (Stage 5)
- Service layer / SOLID beyond basic encapsulation (Stage 6)
- async/await (Stage 8)
- React / TypeScript (Stage 9)
- Validation/error-handling middleware (Stage 10)
- AuthN/AuthZ (Stage 11)
- Reporting/advanced SQL (Stage 12)
- CI/CD (Stage 14)

## Notes for the examiner

- The repo's `TicketApp.Api` is an ASP.NET Core project template, but only plain C#
  classes/LINQ in `Models/` have been taught — the web layer has not.
- `TicketApp.Tests` contains only the default xUnit template; ignore it entirely.
