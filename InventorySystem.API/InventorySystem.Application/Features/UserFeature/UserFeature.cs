using FluentValidation;
using InventorySystem.Application.Features.UploadFeature.Interfaces;
using InventorySystem.Application.Features.UserFeature.Interfaces;
using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Common;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.UserFeature
{
	public class UserFeature : IUserFeature
	{
		public readonly IUserRepository userRepository;
		private readonly IValidator<SaveUserRequest> userValidator;
		public readonly IUploadDocumentFeature uploadDocument;
		public UserFeature(IUserRepository userRepository, IValidator<SaveUserRequest> userValidator, IUploadDocumentFeature uploadDocument)
		{
			this.userRepository = userRepository;
			this.userValidator = userValidator;
			this.uploadDocument = uploadDocument;
		}

		public async Task<Response> User(int id)
		{
			Response response = new Response();
			response.Result = await userRepository.GetSingleRecordById<UserByIdResponse>("GetUserById", id);
			response.IsSuccess = 1;
			response.Message = "Data Fetched Successfully.";
			response.ResponseCode = 200;
			return response;
		}

		public async Task<Response> User(int pageNum, int pageSize, string name, string mobile, int status, int warehouseId, int departmentId)
		{
			Response response = new Response();
			response.Result = await userRepository.User(pageNum, pageSize, name, mobile, status, warehouseId, departmentId);
			response.IsSuccess = 1;
			response.Message = "Data Fetched Successfully.";
			response.ResponseCode = 200;
			return response;
		}

		public async Task<Response> User(SaveUserRequest request, int userId)
		{
			userValidator.ValidateAndThrow(request);
			Response response = new Response();
			response.Result = await userRepository.User(request, userId);
			response.IsSuccess = 1;
			response.Message = "User Added Succesfully";
			response.ResponseCode = 201;
			return response;
		}

		public async Task<Response> User(EditUserRequest request, int id, int userid)
		{
			Response response = await userRepository.Put<EditUserRequest>("UpdateUser", request, id, userid);
			return response;
		}

		public async Task<Response> DeleteUser(int id, int userId)
		{
			Response response = await userRepository.Delete("DeleteUser", id, userId);
			return response;
		}

		public async Task<Response> UpdateLoginUser(UpdateUserLogin request, int id, int userid)
		{
			Response response = await userRepository.Put<UpdateUserLogin>("UpdateUsernamePassword", request, id, userid);
			return response;
		}

		public async Task<Response> UserDetailForMobile(int userid)
		{
			Response response = new Response();
			response.Result = await userRepository.UserDetailForMobile(userid);
			response.IsSuccess = 1;
			response.Message = "Data fetched successfully.";
			response.ResponseCode = 200;
			return response;
		}

		public async Task<Response> UserProfileDetail(int userid)
		{
			Response response = new Response();
			response.Result = await userRepository.GetSingleRecordById<UserProfileResponse>("UserProfileDetail", userid);
			response.IsSuccess = 1;
			response.Message = "Data fetched successfully.";
			response.ResponseCode = 200;
			return response;
		}

		public async Task<Response> EditUserProfile(EditUserProfileRequest request, int Id)
		{
			UploadDocumentRequest req = new UploadDocumentRequest();
			req.DocumentName = request.DocumentName;
			req.Base64 = request.Base64Image;
			Response result = await uploadDocument.UploadDoument(req);
			if (result != null && !string.IsNullOrEmpty(result.Result.Url))
			{
				request.Base64Image = result.Result.Url;
			}
			Response response = await userRepository.Put<EditUserProfileRequest>("EditUserProfile", request, Id);
			return response;
		}

		public async Task<Response> ResetPassword(ResetPasswordRequest request, int id)
		{
			Response response = await userRepository.Put<ResetPasswordRequest>("ResetPassword", request, id);
			return response;
		}
		public async Task<Response> Count()
		{
			Response response = new Response();
			response.Result = await userRepository.GetSingleRecord<ActiveCounterResponse>("UserCount");
			response.IsSuccess = 1;
			response.Message = "Data Fetched Successfully.";
			response.ResponseCode = 200;
			return response;
		}
	}
}
