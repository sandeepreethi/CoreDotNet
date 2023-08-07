using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using Assignment.Application.Interfaces.Persistence.DataServices.Product.Queries;
using Assignment.Application.Interfaces.Services;
using Assignment.Domain.Entities;
using Assignment.Infrastructure.Services;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Assignment.Infrastructure.Persistence.DataServices.Product.Queries;

public class GetProductDataService : IGetProductDataService
{

    private readonly IConfiguration _configuration;
    private readonly ILoggerService<GetProductDataService> _loggerService;
    public GetProductDataService(IConfiguration configuration, ILoggerService<GetProductDataService> loggerService)
    {
        _configuration = configuration;
        _loggerService = loggerService;
    }
    public async Task<IEnumerable<Domain.Entities.Product>> GetAllProducts(bool includeInactive, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => GetProductList());
    }
    public async Task<IEnumerable<Domain.Entities.Product>> GetProductsById(int? id, bool includeInactive, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => GetProductListById(Convert.ToInt32(id)));

    }
    private List<Domain.Entities.Product> GetProductList()
    {
        FileDownload("1001.png");
        List<Domain.Entities.Product> oProductList = new List<Domain.Entities.Product>();
        try
        {
            var connectionString = _configuration.GetSection("DBConnection:DefaultConnection").Value;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_getAllProducts", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Domain.Entities.Product oProduct = new Domain.Entities.Product();
                    oProduct.Id = Convert.ToInt32(sdr["Id"]);
                    oProduct.Name = sdr["Name"].ToString();
                    oProduct.Description = sdr["Description"].ToString();
                    oProduct.Price = !sdr.IsDBNull(sdr.GetOrdinal("Price")) ? Convert.ToInt32(sdr["Price"]) : null;
                    oProductList.Add(oProduct);
                }
            }
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, "-GetProductDataService");
        }
        return oProductList;
    }
    private List<Domain.Entities.Product> GetProductListById(int id)
    {
        List<Domain.Entities.Product> oProductList = new List<Domain.Entities.Product>();
        try
        {
            var connectionString = _configuration.GetSection("DBConnection:DefaultConnection").Value;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "usp_getProductById",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter param1 = new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int,
                    Value = id,
                    Direction = ParameterDirection.Input
                };
                cmd.Parameters.Add(param1);
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Domain.Entities.Product oProduct = new Domain.Entities.Product();
                    oProduct.Id = Convert.ToInt32(sdr["Id"]);
                    oProduct.Name = sdr["Name"].ToString();
                    oProduct.Description = sdr["Description"].ToString();
                    oProduct.Price = !sdr.IsDBNull(sdr.GetOrdinal("Price")) ? Convert.ToInt32(sdr["Price"]) : null;
                    oProductList.Add(oProduct);
                }
            }
        }
        catch (Exception ex)
        {

        }
        return oProductList;
    }


    private void FileDownload(string fileName)
    {
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=cs410032000b0dc9176;AccountKey=6qMnyMkYEBkD2Kp0oig2mtHxB2jzTOecnT+Yv9shJhRT92J2TFgDEYAC7aZEEPkz9sRT5GxpAwEc+AStN1JGEw==;EndpointSuffix=core.windows.net";
        string containerName = "product";
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
        MemoryStream memoryStream = new MemoryStream();
        blobClient.DownloadTo(memoryStream);
        memoryStream.Position = 0;
        string content = new StreamReader(memoryStream).ReadToEnd();
        blobClient.DownloadTo(@"C:\Sandeep\" + fileName);
    }

}
