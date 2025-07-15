using System.ComponentModel.DataAnnotations;
using Moq;

using BLL;
using Entity;
using Entity.DTOs;

namespace wtw_server.Tests;

public class RequestBusinessLogicTest
{
    private const string VALID_JSON_VACATION = """{"employeeId":"EMP001","startDate":"2024-01-15","endDate":"2024-01-20","reason":"Personal vacation"}""";
    private const string INVALID_JSON_DATA = """{"invalidProperty":"invalidValue"}""";
    private const string VALIDATION_ERROR_MESSAGE = "Validation failed";
    private const string PROPERTY_NAME_EMPLOYEE = "employeeId";
    private const string PROPERTY_VALUE_EMPLOYEE = "EMP001";

    private readonly Mock<DAL.IRequest> _mockDalRequest;
    private readonly BLL.Request _businessLogic;
    private readonly Guid _validRequestTypeId;
    private readonly Guid _validRequestStatusId;
    private readonly Guid _validRequestId;

    public RequestBusinessLogicTest()
    {
        _mockDalRequest = new Mock<DAL.IRequest>();
        _businessLogic = new BLL.Request(_mockDalRequest.Object);
        _validRequestTypeId = Guid.NewGuid();
        _validRequestStatusId = Guid.NewGuid();
        _validRequestId = Guid.NewGuid();
    }

    [Fact]
    public async Task CreateRequestAsync_WithValidVacationData_CallsDalAndReturnsRequest()
    {
        // Arrange
        var requestDto = CreateValidVacationRequestDto();
        var expectedRequest = CreateTestRequest();
        
        _mockDalRequest.Setup(d => d.CreateRequestAsync(requestDto))
            .ReturnsAsync(expectedRequest);

        // Act
        var result = await _businessLogic.CreateRequestAsync(requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedRequest.reqId, result.reqId);
        Assert.Equal(expectedRequest.rtyId, result.rtyId);
        Assert.Equal(expectedRequest.resId, result.resId);
        _mockDalRequest.Verify(d => d.CreateRequestAsync(requestDto), Times.Once);
    }

    [Fact]
    public async Task CreateRequestAsync_WithInvalidJsonSchema_ThrowsValidationException()
    {
        // Arrange
        var invalidRequestDto = CreateInvalidRequestDto();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() => _businessLogic.CreateRequestAsync(invalidRequestDto));
        
        Assert.Contains("Unknown request type", exception.Message);
        _mockDalRequest.Verify(d => d.CreateRequestAsync(It.IsAny<RequestCreateDto>()), Times.Never);
    }

    [Fact]
    public async Task GetAllRequestsAsync_WhenCalled_CallsDalAndReturnsRequestsList()
    {
        // Arrange
        var expectedRequests = CreateTestRequestsCollection();
        _mockDalRequest.Setup(d => d.GetAllRequestsAsync())
            .ReturnsAsync(expectedRequests);

        // Act
        var result = await _businessLogic.GetAllRequestsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedRequests.Count(), result.Count);
        _mockDalRequest.Verify(d => d.GetAllRequestsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetRequestByIdAsync_WithValidId_CallsDalAndReturnsRequest()
    {
        // Arrange
        var expectedRequest = CreateTestRequest();
        _mockDalRequest.Setup(d => d.GetRequestByIdAsync(_validRequestId))
            .ReturnsAsync(expectedRequest);

        // Act
        var result = await _businessLogic.GetRequestByIdAsync(_validRequestId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedRequest.reqId, result.reqId);
        _mockDalRequest.Verify(d => d.GetRequestByIdAsync(_validRequestId), Times.Once);
    }

    [Fact]
    public async Task GetRequestByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var invalidId = Guid.NewGuid();
        _mockDalRequest.Setup(d => d.GetRequestByIdAsync(invalidId))
            .ReturnsAsync((Requests?)null);

        // Act
        var result = await _businessLogic.GetRequestByIdAsync(invalidId);

        // Assert
        Assert.Null(result);
        _mockDalRequest.Verify(d => d.GetRequestByIdAsync(invalidId), Times.Once);
    }

    [Fact]
    public async Task DeleteRequestAsync_WithValidId_CallsDalAndReturnsTrue()
    {
        // Arrange
        _mockDalRequest.Setup(d => d.DeleteRequestAsync(_validRequestId))
            .ReturnsAsync(true);

        // Act
        var result = await _businessLogic.DeleteRequestAsync(_validRequestId);

        // Assert
        Assert.True(result);
        _mockDalRequest.Verify(d => d.DeleteRequestAsync(_validRequestId), Times.Once);
    }

    [Fact]
    public async Task DeleteRequestAsync_WithInvalidId_CallsDalAndReturnsFalse()
    {
        // Arrange
        var invalidId = Guid.NewGuid();
        _mockDalRequest.Setup(d => d.DeleteRequestAsync(invalidId))
            .ReturnsAsync(false);

        // Act
        var result = await _businessLogic.DeleteRequestAsync(invalidId);

        // Assert
        Assert.False(result);
        _mockDalRequest.Verify(d => d.DeleteRequestAsync(invalidId), Times.Once);
    }

    [Fact]
    public async Task GetFilteredRequestsAsync_WithValidFilter_CallsDalAndReturnsFilteredRequests()
    {
        // Arrange
        var filter = CreateValidFilterDto();
        var expectedRequests = CreateTestRequestsCollection();
        _mockDalRequest.Setup(d => d.GetFilteredRequestsAsync(filter))
            .ReturnsAsync(expectedRequests);

        // Act
        var result = await _businessLogic.GetFilteredRequestsAsync(filter);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedRequests.Count(), result.Count);
        _mockDalRequest.Verify(d => d.GetFilteredRequestsAsync(filter), Times.Once);
    }

    [Fact]
    public async Task SearchByJsonPropertyAsync_WithValidParameters_CallsDalAndReturnsResults()
    {
        // Arrange
        var expectedRequests = CreateTestRequestsCollection();
        _mockDalRequest.Setup(d => d.SearchByJsonPropertyAsync(PROPERTY_NAME_EMPLOYEE, PROPERTY_VALUE_EMPLOYEE))
            .ReturnsAsync(expectedRequests);

        // Act
        var result = await _businessLogic.SearchByJsonPropertyAsync(PROPERTY_NAME_EMPLOYEE, PROPERTY_VALUE_EMPLOYEE);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedRequests.Count(), result.Count);
        _mockDalRequest.Verify(d => d.SearchByJsonPropertyAsync(PROPERTY_NAME_EMPLOYEE, PROPERTY_VALUE_EMPLOYEE), Times.Once);
    }

    private Requests CreateTestRequest()
    {
        return new Requests
        {
            reqId = _validRequestId,
            rtyId = _validRequestTypeId,
            resId = _validRequestStatusId,
            createdAt = DateTime.UtcNow,
            data = VALID_JSON_VACATION
        };
    }

    private IEnumerable<Requests> CreateTestRequestsCollection()
    {
        return new List<Requests>
        {
            CreateTestRequest(),
            new Requests
            {
                reqId = Guid.NewGuid(),
                rtyId = _validRequestTypeId,
                resId = _validRequestStatusId,
                createdAt = DateTime.UtcNow.AddDays(-1),
                data = """{"employeeId":"EMP002","amount":5000,"purpose":"Home improvement"}"""
            }
        };
    }

    private RequestCreateDto CreateValidVacationRequestDto()
    {
        var vacationTypeId = new Guid("0617B26B-6583-435F-B32E-35D76F70E39D");
        return new RequestCreateDto
        {
            RequestTypeId = vacationTypeId,
            RequestStatusId = _validRequestStatusId,
            DynamicData = new Dictionary<string, object>
            {
                { "StartDate", "2024-01-15T00:00:00" },
                { "EndDate", "2024-01-20T00:00:00" },
                { "DaysRequested", 5 },
                { "Reason", "Personal vacation" },
                { "IsEmergency", false }
            }
        };
    }

    private RequestCreateDto CreateInvalidRequestDto()
    {
        return new RequestCreateDto
        {
            RequestTypeId = new Guid("753D5B16-E908-4F73-982C-F2FCAF7DB28C"),
            RequestStatusId = _validRequestStatusId,
            DynamicData = new Dictionary<string, object>
            {
                { "invalidProperty", "invalidValue" }
            }
        };
    }

    private RequestFilterDto CreateValidFilterDto()
    {
        return new RequestFilterDto
        {
            RequestTypeId = _validRequestTypeId,
            RequestStatusId = _validRequestStatusId,
            FromDate = DateTime.UtcNow.AddDays(-30),
            ToDate = DateTime.UtcNow,
            JsonProperty = PROPERTY_NAME_EMPLOYEE,
            JsonValue = PROPERTY_VALUE_EMPLOYEE
        };
    }
}