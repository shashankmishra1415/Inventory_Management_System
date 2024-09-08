using FluentValidation;
using InventorySystem.Application.Features.ManagerApprovalFeature.Interfaces;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.ManagerApprovalFeature
{
    public class ManagerApprovalFeatures:IManagerApprovalFeatures
    {
        public IManagerApprovalRepository managerApprovalRepository;
        public IValidator<ManagerApprovalRequest> managerApprovalvalidator;
        public IValidator<ManagerApprovalUpdateRequest> managerreturnUpdateValidator;
        public ManagerApprovalFeatures(
            IManagerApprovalRepository managerApprovalRepository,
            IValidator<ManagerApprovalRequest> Managerapprovalvalidator,
            IValidator<ManagerApprovalUpdateRequest> validator) 
        {
            this.managerApprovalRepository = managerApprovalRepository;
            this.managerApprovalvalidator = Managerapprovalvalidator;
            this.managerreturnUpdateValidator=validator;
        }

        public async Task<Response> ManagerApproval(ManagerApprovalRequest request,int userId)
        {
            managerApprovalvalidator.ValidateAndThrow(request);
            Response response = await managerApprovalRepository.Post<ManagerApprovalRequest>("AddManagerApprovalForDamageAndReturnProduct", request,userId);
            return response;
        }

        public async Task<Response>ManagerApproval(ManagerApprovalUpdateRequest request,int id,int userId)
        {
            managerreturnUpdateValidator.ValidateAndThrow(request);
            Response response = await managerApprovalRepository.Put<ManagerApprovalUpdateRequest>("UpdateReturnDamageProductApproval", request, id, userId);
            return response;
        }

        public async Task<Response> ManagerApproval(int pageNum, int pageSize, int recordType, DateTime? fromDate, DateTime? toDate)
        {
            Response response = new Response();
            response.Result = await managerApprovalRepository.ManagerApproval(pageNum,pageSize,recordType,fromDate,toDate);

            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";

            return response;
        }

        public async Task<Response>ManagerApproval(int id)
        {
            Response response = new Response();
            response.Result = await managerApprovalRepository.GetSingleRecordById<SerialNumberByIdResponse>("GetSerialNumberById", id);
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            return response;
        }
    }
}
