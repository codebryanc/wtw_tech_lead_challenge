using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

using BLL;
using Entity;
using Entity.DTOs;
using wtw_server.Controllers;

namespace wtw_server.Tests;

public class RequestUnitTest
{
    private const string VALID_JSON_DATA = """{"employeeId":"EMP001","startDate":"2024-01-15","endDate":"2024-01-20","reason":"Personal vacation"}""";
    private const string SEARCH_PROPERTY_NAME = "employeeId";
    private const string SEARCH_PROPERTY_VALUE = "EMP001";
    private const string ERROR_MESSAGE_VALIDATION = "Invalid request data";
    
    private readonly Mock<IRequest> _mockRequestService;
    private readonly RequestController _controller;
    private readonly Guid _validRequestId;
    private readonly Guid _validRequestTypeId;
    private readonly Guid _validRequestStatusId;

    public RequestUnitTest()
    {
        _mockRequestService = new Mock<IRequest>();
        _controller = new RequestController(_mockRequestService.Object);
        _validRequestId = Guid.NewGuid();
        _validRequestTypeId = Guid.NewGuid();
        _validRequestStatusId = Guid.NewGuid();
    }

    [Fact]
    public async Task GetAllRequests_WhenCalled_ReturnsOkResultWithRequestsList()
    {
        // Arrange
        var expectedRequests = CreateTestRequestsList();
        _mockRequestService.Setup(s => s.GetAllRequestsAsync())
            .ReturnsAsync(expectedRequests);

        // Act
        var result = await _controller.GetAllRequests();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actualRequests = Assert.IsType<List<Requests>>(okResult.Value);
        Assert.Equal(expectedRequests.Count, actualRequests.Count);
        _mockRequestService.Verify(s => s.GetAllRequestsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetRequestById_WithValidId_ReturnsOkResultWithRequest()
    {
        // Arrange
        var expectedRequest = CreateTestRequest();
        _mockRequestService.Setup(s => s.GetRequestByIdAsync(_validRequestId))
            .ReturnsAsync(expectedRequest);

        // Act
        var result = await _controller.GetRequestById(_validRequestId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actualRequest = Assert.IsType<Requests>(okResult.Value);
        Assert.Equal(expectedRequest.reqId, actualRequest.reqId);
        _mockRequestService.Verify(s => s.GetRequestByIdAsync(_validRequestId), Times.Once);
    }

    [Fact]
    public async Task GetRequestById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var invalidId = Guid.NewGuid();
        _mockRequestService.Setup(s => s.GetRequestByIdAsync(invalidId))
            .ReturnsAsync((Requests?)null);

        // Act
        var result = await _controller.GetRequestById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
        _mockRequestService.Verify(s => s.GetRequestByIdAsync(invalidId), Times.Once);
    }

    [Fact]
    public async Task CreateRequest_WithValidData_ReturnsCreatedAtAction()
    {
        // Arrange
        var requestDto = CreateValidRequestDto();
        var expectedRequest = CreateTestRequest();
        _mockRequestService.Setup(s => s.CreateRequestAsync(requestDto))
            .ReturnsAsync(expectedRequest);

        // Act
        var result = await _controller.CreateRequest(requestDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(RequestController.GetRequestById), createdResult.ActionName);
        Assert.Equal(expectedRequest.reqId, ((Requests)createdResult.Value!).reqId);
        _mockRequestService.Verify(s => s.CreateRequestAsync(requestDto), Times.Once);
    }

    [Fact]
    public async Task CreateRequest_WithInvalidData_ReturnsBadRequest()
    {
        // Arrange
        var requestDto = CreateValidRequestDto();
        _mockRequestService.Setup(s => s.CreateRequestAsync(requestDto))
            .ThrowsAsync(new ValidationException(ERROR_MESSAGE_VALIDATION));

        // Act
        var result = await _controller.CreateRequest(requestDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var errorResponse = badRequestResult.Value;
        Assert.NotNull(errorResponse);
        _mockRequestService.Verify(s => s.CreateRequestAsync(requestDto), Times.Once);
    }

    [Fact]
    public async Task DeleteRequest_WithValidId_ReturnsNoContent()
    {
        // Arrange
        _mockRequestService.Setup(s => s.DeleteRequestAsync(_validRequestId))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteRequest(_validRequestId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRequestService.Verify(s => s.DeleteRequestAsync(_validRequestId), Times.Once);
    }

    [Fact]
    public async Task DeleteRequest_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var invalidId = Guid.NewGuid();
        _mockRequestService.Setup(s => s.DeleteRequestAsync(invalidId))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteRequest(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        _mockRequestService.Verify(s => s.DeleteRequestAsync(invalidId), Times.Once);
    }

    [Fact]
    public async Task SearchByJsonProperty_WithValidParameters_ReturnsOkResult()
    {
        // Arrange
        var expectedRequests = CreateTestRequestsList();
        _mockRequestService.Setup(s => s.SearchByJsonPropertyAsync(SEARCH_PROPERTY_NAME, SEARCH_PROPERTY_VALUE))
            .ReturnsAsync(expectedRequests);

        // Act
        var result = await _controller.SearchByJsonProperty(SEARCH_PROPERTY_NAME, SEARCH_PROPERTY_VALUE);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actualRequests = Assert.IsType<List<Requests>>(okResult.Value);
        Assert.Equal(expectedRequests.Count, actualRequests.Count);
        _mockRequestService.Verify(s => s.SearchByJsonPropertyAsync(SEARCH_PROPERTY_NAME, SEARCH_PROPERTY_VALUE), Times.Once);
    }

    [Fact]
    public async Task SearchByJsonProperty_WithEmptyPropertyName_ReturnsBadRequest()
    {
        // Arrange
        var emptyPropertyName = string.Empty;

        // Act
        var result = await _controller.SearchByJsonProperty(emptyPropertyName, SEARCH_PROPERTY_VALUE);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Contains("PropertyName and Value are required", badRequestResult.Value?.ToString());
        _mockRequestService.Verify(s => s.SearchByJsonPropertyAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    private Requests CreateTestRequest()
    {
        return new Requests
        {
            reqId = _validRequestId,
            rtyId = _validRequestTypeId,
            resId = _validRequestStatusId,
            createdAt = DateTime.UtcNow,
            data = VALID_JSON_DATA
        };
    }

    private List<Requests> CreateTestRequestsList()
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

    private RequestCreateDto CreateValidRequestDto()
    {
        return new RequestCreateDto
        {
            RequestTypeId = _validRequestTypeId.ToString(),
            RequestStatusId = _validRequestStatusId.ToString(),
            DynamicData = new Dictionary<string, object>
            {
                { "employeeId", "EMP001" },
                { "startDate", "2024-01-15" },
                { "endDate", "2024-01-20" },
                { "reason", "Personal vacation" }
            }
        };
    }
}