using Dapper;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Response;
using InventorySystem.SharedLayer.Response;
using System.Data;

namespace InventorySystem.Infrastructure.Repositories
{
    public class DropdownRepository : BaseRepository, IDropdownRepository
    {
        private readonly DbContext dbContext;
        public DropdownRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}

