---
name: entity-framework
description: "Use when working on Entity Framework Core in this workshop project: add or change entities, configure DbContext, create or update migrations, seed data, or update the database schema."
user-invocable: true
---

# Entity Framework Skill

## When to Use
- Add or modify EF entity classes under `Models/Entities`
- Configure relationships, precision, keys, or delete behavior in `WorkshopDbContext`
- Create a new migration after a model change
- Update the database with `dotnet ef database update`
- Adjust startup seeding when the schema changes

## Workflow
1. Inspect the affected entity classes and `WorkshopDbContext`.
2. Update the C# model first.
3. Keep relationships explicit when the domain is not trivial.
4. Add or adjust precision for decimal properties that will be stored in SQL Server.
5. Generate a migration after the model is stable.
6. Apply the migration to the database.
7. Validate the app still builds and the seeded data still loads.

## Rules of Thumb
- Prefer attributes and `OnModelCreating` for clear schema intent.
- Use `HasPrecision` for money-like decimal values.
- Use `Include` in read queries when views need related entities.
- Keep generated migration files under source control, but do not hand-edit them unless a special SQL step is required.
- If a schema change affects seed data, update the seed code in the same iteration.

## Project-Specific Targets
- `Data/WorkshopDbContext.cs`
- `Data/WorkshopSeedData.cs`
- `Models/Entities/*.cs`
- `Migrations/*`
- `Program.cs`

## Verification
- Run a build after model or DbContext changes.
- Generate a migration after each meaningful EF model change.
- Apply the migration and confirm the app still starts with seeded data.
