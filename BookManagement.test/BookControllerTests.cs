using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using BookManagement.test.Model;
using BookManagement.test.Service;
using Newtonsoft.Json;
using Moq.Protected;


[TestClass]
public class BookControllerTests
{
    private BookService _bookService;
    private List<Owner> _owners;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;

    [TestInitialize]
    public async Task Setup()
    {
        _bookService = new BookService(new HttpClient());
        _owners = await _bookService.GetOwnersAsync();
        if (_owners == null)
        {
            _owners = new List<Owner>();
        }
    }

    [TestMethod]
    public async Task GetOwnersAsync_ShouldReturnEmptyList_OnBadRequest()
    {
        // Mock a 400 Bad Request response
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            });

        var result = await _bookService.GetOwnersAsync();

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void GetBooksByCategory_HardcoverOnly_ShouldFilterAndSortBooks()
    {
        var result = _bookService.GetBooksByCategory(_owners, true);

        // Debugging: Print the result
        foreach (var category in result.Keys)
        {
            Console.WriteLine($"{category}: {string.Join(", ", result[category])}");
        }

        Assert.AreEqual(4, result["Books owned by Adults"].Count);
        Assert.AreEqual(2, result["Books owned by Children"].Count);
    }

    [TestMethod]
    public void GetBooksByCategory_ShouldGroupAndSortBooks()
    {
        var result = _bookService.GetBooksByCategory(_owners);

        // Debugging: Print the result
        foreach (var category in result.Keys)
        {
            Console.WriteLine($"{category}: {string.Join(", ", result[category])}");
        }

        Assert.AreEqual(6, result["Books owned by Adults"].Count);
        Assert.AreEqual(4, result["Books owned by Children"].Count);
    }
}
