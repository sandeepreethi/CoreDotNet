using System.Data.SqlClient;
using System.Data;
using Assignment.Application.Interfaces.Persistence.DataServices.Product.Commands;
using Assignment.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Assignment.Infrastructure.Persistence.DataServices.Product.Commands;

public class UpdateProductDataService : IUpdateProductDataService
{
    private readonly IConfiguration _configuration;
    public UpdateProductDataService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Domain.Entities.Product> UpdateProduct(Domain.Entities.Product product, CancellationToken cancellationToken = default)
    {

        try
        {
            var connectionString = _configuration.GetSection("DBConnection:DefaultConnection").Value;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "usp_updateProduct",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", product.Id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                SqlParameter outParameter = new SqlParameter
                {
                    ParameterName = "@Outputid", 
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output 
                };
                cmd.Parameters.Add(outParameter);
                connection.Open();
                cmd.ExecuteNonQuery();
                return await Task.Run(() =>
                {
                    return product;
                });
            }

        }
        catch (Exception ex)
        {
        }
        return null;
        
    }



}
