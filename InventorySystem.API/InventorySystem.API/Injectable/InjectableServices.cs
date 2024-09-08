using InventorySystem.Infrastructure.Repositories;
using InventorySystem.Application.Features.UserFeature.Interfaces;
using InventorySystem.Application.Features.UserFeature;
using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.Application.Features.WareFeature;
using InventorySystem.Application.Features.WarehouseFeature.Interfaces;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.Application.Features.ConfigurationFeature.Interfaces;
using InventorySystem.Application.Features.ConfigurationFeature;
using InventorySystem.Application.Features.VendorFeature.Interfaces;
using InventorySystem.Application.Features.ProductFeature;
using InventorySystem.Application.Features.LoginFeature.Interfaces;
using InventorySystem.Application.Features.LoginFeature;
using InventorySystem.Application.Features.SalesOrdersFeature;
using InventorySystem.Application.Features.StockInwardFeature.Interfaces;
using InventorySystem.Application.Features.StockInwardFeature;
using InventorySystem.Application.Features.Dropdown_Feature;
using InventorySystem.Application.Features.InventoryFeatures.interfaces;
using InventorySystem.Application.Features.InventoryFeatures;
using InventorySystem.Application.Features.Admin_Feature.Interfaces;
using InventorySystem.Application.Features.Admin_Feature;
using InventorySystem.Application.Features.StockAuditFeature;
using InventorySystem.Application.Features.StockAuditFeature.interfaces;
using InventorySystem.Application.Features.ManagerApprovalFeature.Interfaces;
using InventorySystem.Application.Features.ManagerApprovalFeature;
using InventorySystem.Application.Features.UploadFeature.Interfaces;
using InventorySystem.Application.Features.UploadFeature;

namespace InventorySystem.API.Injectable
{
    public class InjectableServices
    {
        public static void Services(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserFeature, UserFeature>();
            builder.Services.AddScoped<IBaseRepository, BaseRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IWarehouseFeature, WarehouseFeature>();
            builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            builder.Services.AddScoped<IConfigurationFeature, ConfigurationFeature>();
            builder.Services.AddScoped<IDropdownFeature, DropdownFeature>();
            builder.Services.AddScoped<IDropdownRepository, DropdownRepository>();
            builder.Services.AddScoped<IVendorRepository, VendorRepository>();
            builder.Services.AddScoped<IVendorFeature, VendorFeature>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();      
            builder.Services.AddScoped<IProductFeature, ProductFeature>();
            builder.Services.AddScoped<ILoginFeature, LoginFeature>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();    
            builder.Services.AddScoped<ISalesOrdersFeature, SalesOrdersFeature>();    
            builder.Services.AddScoped<ISalesOrdersRepository, SalesOrdersRepository>();    
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<IStockInwardFeature, StockInwardFeature>();
            builder.Services.AddScoped<IStockInwardRepository, StockInwardRepository>();
            builder.Services.AddScoped<IInventoryFeature, InventoryFeature>();
            builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAdminFeature, AdminFeature>();
            builder.Services.AddScoped<IStockAuditRepository, StockAuditRepository>();
            builder.Services.AddScoped<IStockAuditFeature, StockAuditFeature>();
            builder.Services.AddScoped<IManagerApprovalRepository, ManagerApprovalRepository>();
            builder.Services.AddScoped<IManagerApprovalFeatures,ManagerApprovalFeatures>();
            builder.Services.AddScoped<IUploadDocumentFeature, UploadDocumentFeature>();
            builder.Services.AddScoped<DbContext>();
            builder.Services.AddHttpContextAccessor();
        }
    }
}
