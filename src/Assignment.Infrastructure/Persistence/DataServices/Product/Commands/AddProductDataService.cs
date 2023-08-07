using System.Data.SqlClient;
using System.Data;
using Assignment.Application.Interfaces.Persistence.DataServices.Product.Commands;
using Assignment.Domain.Entities;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Assignment.Infrastructure.Persistence.DataServices.Product.Commands;

public class AddProductDataService : IAddProductDataService
{
    private readonly IConfiguration _configuration;
    public AddProductDataService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Domain.Entities.Product> AddProduct(Domain.Entities.Product product, CancellationToken cancellationToken = default)
    {
        try
        {
            var connectionString = _configuration.GetSection("DBConnection:DefaultConnection").Value;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "usp_addProduct",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                SqlParameter outParameter = new SqlParameter
                {
                    ParameterName = "@Id", 
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output 
                };
                cmd.Parameters.Add(outParameter);
                connection.Open();
                cmd.ExecuteNonQuery();
                FileUpload("123");
                return await Task.Run(() =>
                {
                    product.Id = Convert.ToInt32(outParameter.Value);

                    return product;
                });
                
            }

        }
        catch (Exception ex)
        {
        }
        return null;
        
    }


    private string FileUpload(string fileName)
    {
        try
        {
            var connectionString = _configuration.GetSection("DBConnectionBlob:DefaultConnectionBlob").Value;
            var containerName = _configuration.GetSection("DBConnectionBlob:ContainerName").Value;
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var files = Directory.GetFiles(@"C:\June30\Assessment\BackEnd\src\Assignment.Api\UploadFiles");

            foreach (var file in files)
            {
                using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(file)))
                {
                    blobContainerClient.UploadBlob(Path.GetFileName(file), stream);
                }
            }
            return "fileuploaded";
        }
        catch(Exception ex)
        {

        }
        return "";
    }

}
