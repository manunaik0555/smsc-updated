using Grpc.Core;

using Microsoft.AspNetCore.Identity;

using MySql.Data.MySqlClient;
using NuGet.Protocol.Plugins;

using server.api.DataAccess;
using server.api.DataAccess.SqlQueryExtensions;
using server.api.gRPC.Customer;
using server.api.Identity.Services;

using System.Configuration;

namespace server.api.gRPC.Services.Customer;

public class ProductService : Product.ProductBase
{
    private readonly IDatabase database;

    public ProductService(IDatabase database)
    {
        this.database = database;
    }

    public async override Task<GetProductsReply> GetProducts(GetProductsRequest request, ServerCallContext context)
    {

        var reply = new GetProductsReply();
        var sql = "SELECT * FROM listed_products";
        var countSql = "SELECT COUNT(*) FROM listed_products";

        if (request.Id != 0)
        {
            sql += $" WHERE Id = {request.Id.ToSqlString()}";
            countSql += $" WHERE Id = {request.Id.ToSqlString()}";
        }

        if (request.P is not null)
        {
            if (request.P.Limit < 1)
            {
                request.P.Limit = 20;
            }
            sql += $" LIMIT {request.P.Limit.ToSqlString()} OFFSET {request.P.Offset.ToSqlString()}";
        }

        else
        {
            sql += $" LIMIT {20.ToSqlString()} OFFSET {0.ToSqlString()}";
        }

        var products = await database.QueryAllAsync<ListedProductMessage>(sql);

        reply.Products.AddRange(products);

        reply.Count = await database.ExecuteScalarAsync<long>(countSql);

        return reply;
    }
}
