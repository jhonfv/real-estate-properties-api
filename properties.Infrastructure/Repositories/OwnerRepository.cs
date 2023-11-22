using Dapper;
using properties.Domain.Entities;
using properties.Domain.Interfaces;
using System.Data;

namespace properties.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IDbConnection _db;

        public OwnerRepository(IDbConnection db)
        {
            _db = db;
        }




        public async Task<Owner> CreateAsync(Owner owner)
        {
            var queryString = @"
                                INSERT INTO [Owner]
                                           ([Name]
                                           ,[Address]
                                           ,[PhotoPath]
                                           ,[Birthday])
		                                   OUTPUT INSERTED.*
                                     VALUES
                                           (@Name
                                           ,@Address
                                           ,@PhotoPath
                                           ,@Birthday)";
            var response = await _db.QueryFirstOrDefaultAsync<Owner>(queryString, param:owner);
            return response;
        }


        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            var queryString = @"
                                SELECT IdOwner 
                                      ,Name 
                                      ,Address 
                                      ,PhotoPath 
                                      ,Birthday 
                                  FROM Owner";
            
            var response = await _db.QueryAsync<Owner>(queryString);
            
            return response;
        }

        public async Task<Owner> GetByIdAsync(int OwnerId)
        {
            var queryString = @"
                                SELECT IdOwner 
                                      ,Name 
                                      ,Address 
                                      ,PhotoPath 
                                      ,Birthday 
                                  FROM Owner
                                  WHERE IdOwner=@OwnerId";  
            
            var response = await _db.QueryFirstOrDefault(queryString, param: new {OwnerId});
            return response;
        }
    }
}
