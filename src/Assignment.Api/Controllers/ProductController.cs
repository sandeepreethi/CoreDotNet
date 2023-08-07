using System.Diagnostics.CodeAnalysis;
using Assignment.Api.Extensions;
using Assignment.Application.Endpoints.Product.Commands;
using Assignment.Application.Endpoints.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Authorization;

namespace Assignment.Api.Controllers;

[ExcludeFromCodeCoverage]
[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/product")]
[ApiVersion("1.0")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public ProductController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
    {
        _mediator = mediator;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    [Route("GetAllProducts")]
    public async Task<JsonResult> GetProduct([FromQuery] ProductQuery request) => new JsonResult((await _mediator.Send(request)).ToActionResult());

    [HttpPost]
    [Route("GetProductById")]
    public async Task<JsonResult> GetProductById([FromBody] ProductQuery request) =>
        new JsonResult((await _mediator.Send(request)).ToActionResult());


    [HttpPost]
    [Route("UploadFile")]
    public async Task<JsonResult> UploadFile([FromForm] IFormFile loadFile)
    {

        string uploads = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads");
        Directory.CreateDirectory(uploads);
        //foreach (IFormFile file in loadFile)
        //if (loadFile.Length > 0)
        string filePath = Path.Combine(uploads, loadFile.FileName);
        using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            loadFile.CopyTo(fileStream);
        }
        string connectionstring = "DefaultEndpointsProtocol=https;AccountName=cs410032000b0dc9176;AccountKey=6qMnyMkYEBkD2Kp0oig2mtHxB2jzTOecnT+Yv9shJhRT92J2TFgDEYAC7aZEEPkz9sRT5GxpAwEc+AStN1JGEw==;EndpointSuffix=core.windows.net";
        string containerName = "product";
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionstring);
        BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var files = Directory.GetFiles(uploads);

        try
        { 
        foreach (var file in files)
        {
            using (MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(file)))
            {
                blobContainerClient.UploadBlob(Path.GetFileName(file), stream);
                    
                }
        }
        return null;
        }
        catch (Exception ex)
        {

        }


        return null; // new JsonResult((await _mediator.Send(command)).ToActionResult());
    }




    [HttpPost]
    [Route("AddProduct")]
    // public async Task<JsonResult> AddProductAsync([FromBody] AddProductCommand command)
    public async Task<JsonResult> AddProductAsync([FromBody] AddProductCommand command)
    {
        return new JsonResult((await _mediator.Send(command)).ToActionResult());
    }


    [HttpPut]
    [Route("UpdateProduct")]
    public async Task<JsonResult> UpdateProduct([FromBody] UpdateProductCommand command) =>
        new JsonResult((await _mediator.Send(command)).ToActionResult());
    [HttpPost]
    [Route("DeleteProduct")]
    public async Task<JsonResult> DeleteProduct([FromBody] DeleteProductCommand command) =>
        new JsonResult((await _mediator.Send(command)).ToActionResult());
}
