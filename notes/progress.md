# Curriculum Progress & Assessment Scope

The examiner skill MUST read this file before issuing any assessment task. It defines what the
candidate has actually been taught. Assessment tasks may ONLY require topics listed as **in
scope**. Anything under **out of scope** must not appear in task requirements, acceptance
criteria, or grading — even if scaffolding for it exists in the repo (e.g. the xUnit project was
scaffolded during setup but testing has not been taught).

## Current stage

Completed: Stage 1 (C# domain model), Stage 2 (Collections & LINQ), Stage 3 (ASP.NET Core API).
Next up: Stage 4 (SQL fundamentals).

## In scope (may be assessed)

- C# fundamentals: classes, enums, properties (auto-properties, private setters), constructors,
  constructor chaining, fields, `readonly`, namespaces, naming conventions
- Nullable reference/value types (`string?`, `DateOnly?`, `.HasValue`), null-conditional `?.`
- Exceptions for business-rule enforcement (`ArgumentException`, `InvalidOperationException`),
  try/catch, exception-type-driven error mapping
- Collections: `List<T>`, `Dictionary<K,V>`, `IEnumerable<T>`, arrays
- LINQ (method syntax): `Where`, `Select`, `OrderBy`/`OrderByDescending`, `ThenBy`, `GroupBy`,
  `ToDictionary`, `ToList`, `FirstOrDefault`, `Any`, `Count`, `Max`; lazy evaluation and
  conditional query composition
- `DateTime`/`DateOnly` basics, `Interlocked.Increment`
- Encapsulation, domain-model business rules, state-machine style transition guards
- ASP.NET Core Web API: controllers, attribute routing (`[Route]`, `[HttpGet]`/`[HttpPost]`/
  `[HttpPut]`/`[HttpDelete]`, route parameters, literal-vs-parameter precedence), `ControllerBase`,
  `ActionResult<T>`/`IActionResult`, dependency injection (constructor injection, singleton
  lifetime, `builder.Services`, `MapControllers`), DTOs (`required` modifier), model binding
  (body, route, query string), enum binding by name, HTTP status codes (200/201/204/400/404/409),
  ProblemDetails responses, REST semantics (sub-action endpoints vs field updates, idempotency)
- Git basics: init, add, commit, branch, remotes, push; GitHub CLI auth

## Out of scope (NOT yet taught — must not be assessed)

- Unit testing / xUnit (Stage 7) — test project exists but is scaffolding only
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

- The API persists nothing; the in-memory `TicketStore` is a deliberate Stage 3 design, not a gap.
- `TicketApp.Tests` contains only the default xUnit template; ignore it entirely.
