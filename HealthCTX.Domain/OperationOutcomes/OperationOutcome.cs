﻿using HealthCTX.Domain.OperationOutcomes.Interfaces;
using System.Collections.Immutable;

namespace HealthCTX.Domain.OperationOutcomes;

public record OperationOutcome(ImmutableList<OutcomeIssue> Issues) : IOperationOutcome;
