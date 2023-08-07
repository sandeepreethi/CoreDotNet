using System.Data.SqlClient;
using System.Data;
using Assignment.Application.Interfaces.Persistence.DataServices.Product.Commands;
using Assignment.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Assignment.Infrastructure.Persistence.DataServices.Product.Commands;

public class DeleteProductDataService : IDeleteProductDataService
{
    private readonly IConfiguration _configuration;
    public DeleteProductDataService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<Domain.Entities.Product> DeleteProduct(Domain.Entities.Product product, CancellationToken cancellationToken = default)
    {

        try
        {
            var connectionString = _configuration.GetSection("DBConnection:DefaultConnection").Value;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "usp_deleteProduct",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", product.Id);
                SqlParameter outParameter = new SqlParameter
                {
                    ParameterName = "@OutputId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                connection.Open();
                int returnvalue = cmd.ExecuteNonQuery();

                return await Task.Run(() =>
                {
                    product.IsRecordDelted = Convert.ToBoolean(returnvalue);

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
