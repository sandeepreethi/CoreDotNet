using System.Diagnostics.CodeAnalysis;
using Assignment.Application.Interfaces.Services;

namespace Assignment.Infrastructure.Services;

[ExcludeFromCodeCoverage]
public class DateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
}
