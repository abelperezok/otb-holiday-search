using System.Text.Json;
using OTB.HolidaySearch.Data;

namespace OTB.HolidaySearch.Tests;

public class JsonHotelRepositoryTests
{
    [Fact]
    public void TestCreateWithValidFilePath()
    {
        // Arrange

        // Act
        var repo = new JsonHotelRepository(JsonFilePathConstants.HotelJsonPath);

        // Assert
        Assert.NotNull(repo);
    }
    
    [Fact]
    public void TestCreateWithInvalidFilePath()
    {
        // Arrange

        // Act
        var ex = Assert.Throws<InvalidOperationException>(() =>
        {
            var repo = new JsonHotelRepository("invalid-path");
        });

        // Assert
        Assert.IsType<FileNotFoundException>(ex.InnerException);
        Assert.Equal("Unable to load json file", ex.Message);
    }
    
    [Fact]
    public void TestCreateWithInvalidFileContent()
    {
        // Arrange

        // Act
        var ex = Assert.Throws<InvalidOperationException>(() =>
        {
            _ = new JsonHotelRepository(JsonFilePathConstants.EmptyJsonPath);
        });

        // Assert
        Assert.IsType<JsonException>(ex.InnerException);
        Assert.Equal("Invalid json content", ex.Message);
    }
    
    [Fact]
    public void TestCreateWithInvalidFileContentValidJson()
    {
        // Arrange

        // Act
        var ex = Assert.Throws<InvalidOperationException>(() =>
        {
            _ = new JsonHotelRepository(JsonFilePathConstants.NullJsonPath);
        });

        // Assert
        Assert.IsType<JsonException>(ex.InnerException);
        Assert.Equal("Invalid json content - null data", ex.InnerException.Message);
    }
        
    [Fact]
    public void TestCreateWithInvalidFileDirectory()
    {
        // Arrange

        // Act
        var ex = Assert.Throws<InvalidOperationException>(() =>
        {
            _ = new JsonHotelRepository(JsonFilePathConstants.InvalidJsonPath);
        });

        // Assert
        Assert.IsType<DirectoryNotFoundException>(ex.InnerException);
        Assert.Equal("Unknown error loading json file", ex.Message);
    }
}
