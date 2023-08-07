using AutoMapper;
using Assignment.Application.Mapping;
using Xunit;

namespace Assignment.Application.Tests.Mapping;

public class ProductTests
{
    [Fact]
    public void VerifyConfiguration()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>());

        configuration.AssertConfigurationIsValid();
    }
}
