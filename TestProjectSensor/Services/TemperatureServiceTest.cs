using Microsoft.EntityFrameworkCore;
using Moq;
using SensorState.Context;
using SensorState.Models;
using SensorState.Services;

namespace TestProjectSensor.Services;

[TestFixture]
internal class TemperatureServiceTest
{
    public TemperatureService createTemperatureService_Normal()
    {
        var mockSet = new Mock<DbSet<Temperature>>();

        var mockContext = new Mock<DatabaseContext>();
        mockContext.Setup(m => m.Temperatures).Returns(mockSet.Object);

        Mock<IStatusService> statusServiceMock = new();

        statusServiceMock.Setup(s => s.GetStatusBoundModelAsync());
        statusServiceMock.Setup(s => s.RedefineStatusBoundAsync(It.IsAny<int>(), It.IsAny<StatusBound>()));

        return new TemperatureService(mockContext.Object,statusServiceMock.Object);
    }

    [Test]
    public async Task GetTemperatures_ShouldReturnAllTemperatures()
    {
        TemperatureService service = createTemperatureService_Normal();

        //Act
        var result = await service.GetAll();

        //Assert
        Assert.IsNotNull(result);
        Assert.That(result.Count(), Is.GreaterThan(0));
    }

    [Test]
    public async Task BuildTemperatureEntity_ShouldReturnASingleHotTemperature()
    {
        TemperatureService service = createTemperatureService_Normal();

        //Act
        var result = await service.BuildTemperatureEntity(42);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.Status.Description, Is.EqualTo("HOT"));
    }

    [Test]
    public async Task BuildTemperatureEntity_ShouldReturnASingleColdTemperature()
    {
        TemperatureService service = createTemperatureService_Normal();

        //Act
        var result = await service.BuildTemperatureEntity(20);

        //Assert
        Assert.IsNotNull(result);
        Assert.That(result?.Status.Description, Is.EqualTo("COLD"));
    }


    [Test]
    public async Task BuildTemperatureEntity_ShouldReturnASingleWarmTemperature()
    {
        TemperatureService service = createTemperatureService_Normal();

        //Act
        var result = await service.BuildTemperatureEntity(32);

        //Assert
        Assert.IsNotNull(result);
        Assert.That(result?.Status.Description, Is.EqualTo("WARM"));
    }
}
