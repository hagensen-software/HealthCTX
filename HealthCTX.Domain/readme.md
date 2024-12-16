# HealthCTX.Domain

## Resources
Resources are created by implementing the interface of the resource type.

The following snippet shows how to create a Patient resource:

```csharp
public record Patient(MaritalStatus? MaritalStatus) : IPatient;
```

## CodeableConcept

