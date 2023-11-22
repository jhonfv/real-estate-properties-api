using Dapper;
using properties.Domain.Entities;
using properties.Domain.Filters;
using properties.Domain.Interfaces;
using System.Data;
using System.Text;

namespace properties.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IDbConnection _db;

        public PropertyRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Property> CreateAsync(Property property)
        {
            var queryString = @"
                                INSERT INTO [dbo].[Property]
                                    ([Name]
                                    ,[Address]
                                    ,[Price]
                                    ,[CodeInternal]
                                    ,[Year]
                                    ,[IdOwner])
                                    OUTPUT INSERTED.*
                                VALUES
                                    (@Name
                                    ,@Address
                                    ,@Price
                                    ,@Year
                                    ,@CodeInternal
                                    ,@IdOwner)";
            
            var response = await _db.QueryFirstOrDefaultAsync<Property>(queryString, param:property);
            return response;
        }

        public async Task<PropertyImage> AddImageAsync(PropertyImage property)
        {
            var queryString = @"
                                INSERT INTO [dbo].[PropertyImage]
                                       ([IdProperty]
                                       ,[FilePath]
                                       ,[Enabled])
                                 OUTPUT INSERTED.*
                                 VALUES
                                       (@IdProperty
                                       ,@FilePath
                                       ,@Enabled)";

            var response = await _db.QueryFirstOrDefaultAsync<PropertyImage>(queryString, param: property);
            return response;
        }

        public Task<PropertyTrace> AddTrace(PropertyTrace property)
        {
            throw new NotImplementedException();
        }

        public async Task<Property> ChangePriceAsync(Property property)
        {
            var queryString = @"
                                UPDATE Property
                                   SET Price = @Price
                                 OUTPUT INSERTED.*
                                 WHERE idProperty = @idProperty";

            var response = await _db.QueryFirstOrDefaultAsync<Property>(queryString, param: property);
            return response;
        }


        public async Task<IEnumerable<Property>> getAllAsync()
        {
            var queryString = @"
                SELECT IdProperty
                      ,Name
                      ,Address
                      ,Price
                      ,CodeInternal
                      ,Year
                      ,IdOwner
                  FROM Property";
            var response = await _db.QueryAsync<Property>(queryString);

            return response;
        }

        public async Task<IEnumerable<Property>> getByFiltersAsync(FilterProperty filter)
        {
            
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine("SELECT IdProperty, Name, Address, Price, CodeInternal, Year, IdOwner FROM Property WHERE 1 = 1");

            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                queryBuilder.AppendLine("AND Name = @Name");
                parameters.Add("Name", filter.Name);
            }

            if (!string.IsNullOrWhiteSpace(filter.CodeInternal))
            {
                queryBuilder.AppendLine("AND CodeInternal = @CodeInternal");
                parameters.Add("CodeInternal", filter.CodeInternal);
            }

            if (filter.Year.HasValue)
            {
                queryBuilder.AppendLine("AND Year = @Year");
                parameters.Add("Year", filter.Year);
            }


            if (filter.MinPrice.HasValue && filter.MaxPrice.HasValue)
            {
                queryBuilder.AppendLine("AND Price BETWEEN @MinPrice AND @MaxPrice");
                parameters.Add("MinPrice", filter.MinPrice);
                parameters.Add("MaxPrice", filter.MaxPrice);
            }

            //Ordenamiento
            queryBuilder.AppendLine("ORDER BY IdProperty");

            // Pagination
            queryBuilder.AppendLine("OFFSET @PageOffset ROWS FETCH NEXT @PageSize ROWS ONLY");
            parameters.Add("PageOffset", (filter.PageNumber - 1) * filter.PageSize);
            parameters.Add("PageSize", filter.PageSize);

            var query = queryBuilder.ToString();
            var response = await _db.QueryAsync<Property>(query, parameters);

            return response;
        }

        public Task<Property> getByIdAsync(int idProperty)
        {
            throw new NotImplementedException();
        }

        public async Task<Property> UpdateAsync(Property property)
        {
            var queryString = @"
                                UPDATE Property
                                   SET Name = @Name
                                      ,Address = @Address
                                      ,Price = @Price
                                      ,CodeInternal = @CodeInternal
                                      ,Year = @Year
                                      ,IdOwner = @IdOwner
                                 OUTPUT INSERTED.*
                                 WHERE idProperty = @idProperty";

            var response = await _db.QueryFirstOrDefaultAsync<Property>(queryString, param: property);
            return response;
        }
    }
}
