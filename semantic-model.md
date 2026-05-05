# Semantic Model

## Entities

### Customer
- Properties: `Id`, `FirstName`, `LastName`, `PhoneNumber`, `Email`, `Address`, `RegisteredAt`
- Relationships:
  - One customer has many vehicles
  - One customer has many service orders

### Vehicle
- Properties: `Id`, `Vin`, `LicensePlate`, `Manufacturer`, `Model`, `Year`, `MileageKm`, `FuelType`, `LastServiceDate`, `CustomerId`
- Relationships:
  - Each vehicle belongs to one customer
  - One vehicle can have many service orders

### Mechanic
- Properties: `Id`, `FirstName`, `LastName`, `Specialty`, `IsCertified`, `ExperienceYears`, `HourlyRate`, `EmployedSince`
- Relationships:
  - One mechanic can handle many service orders
  - One mechanic can have many appointment slots

### ServiceCategory
- Properties: `Id`, `Name`, `Description`, `CreatedAt`
- Relationships:
  - One category contains many service items

### ServiceItem
- Properties: `Id`, `Name`, `Description`, `BasePrice`, `EstimatedDurationMinutes`, `RequiresParts`, `ServiceCategoryId`
- Relationships:
  - Each service item belongs to one service category
  - One service item can appear in many order lines

### ServiceOrder
- Properties: `Id`, `OrderNumber`, `CreatedAt`, `ScheduledAt`, `Status`, `Notes`, `DiscountPercent`, `CustomerId`, `VehicleId`, `MechanicId`
- Relationships:
  - Each service order belongs to one customer
  - Each service order belongs to one vehicle
  - Each service order belongs to one mechanic
  - One service order contains many order lines
  - One service order can be linked from one appointment slot

### OrderLine
- Properties: `Id`, `ServiceOrderId`, `ServiceItemId`, `Quantity`, `UnitPrice`, `PartCost`
- Relationships:
  - Each order line belongs to one service order
  - Each order line references one service item

### AppointmentSlot
- Properties: `Id`, `MechanicId`, `StartAt`, `EndAt`, `IsReserved`, `ServiceOrderId`
- Relationships:
  - Each appointment slot belongs to one mechanic
  - Each appointment slot can optionally reference one service order

## Relationship Summary
- `Customer` 1-N `Vehicle`
- `Customer` 1-N `ServiceOrder`
- `Vehicle` 1-N `ServiceOrder`
- `Mechanic` 1-N `ServiceOrder`
- `Mechanic` 1-N `AppointmentSlot`
- `ServiceCategory` 1-N `ServiceItem`
- `ServiceOrder` 1-N `OrderLine`
- `ServiceItem` 1-N `OrderLine`
- `ServiceOrder` 1-0/1 `AppointmentSlot`

## Notes
- The database is mapped through `WorkshopDbContext`.
- Decimal values use explicit precision to avoid rounding issues in SQL Server.
- The model is seeded at startup through `WorkshopSeedData` after migrations are applied.