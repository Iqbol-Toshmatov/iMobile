using iMobile.Object.Entity;
using iMobile.Object.Response;
using iMobile.Service.Interfaces;
using iMobile.DAL.Interfaces;
using iMobile.Object.Enum;
using iMobile.Object.ViewModels.Phone;


namespace iMobile.Service.Implementations
{
    public class PhoneService : IPhoneService
    {
        private readonly IPhoneRepository _phoneRepository;

        public PhoneService(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }

        public async Task<IBaseResponse<Phone>> Get(int id)
        {
            var baseResponse=new BaseResponse<Phone>();

            try
            {
                var phone =_phoneRepository.Get(id);

                if (phone==null)
                {
                    baseResponse.Description = "Not Found";
                    baseResponse.StatusCode=StatusCode.NotFound;
                    return baseResponse;
                }

                baseResponse.Data= phone;
                return baseResponse;
            
            }
            catch (Exception ex)
            {
                return new BaseResponse<Phone>()
                {
                    Description = $"[GetPhones] : {ex.Message}",
                    StatusCode=StatusCode.NotFound, 
                };

            }

        }

        public async Task<IBaseResponse<Phone>> GetByName(string name)
        {
            var baseResponse = new BaseResponse<Phone>();

            try
            {
                var phone = _phoneRepository.GetByName(name);

                if (phone == null)
                {
                    baseResponse.Description = "Not Found";
                    baseResponse.StatusCode = StatusCode.NotFound;
                    return baseResponse;
                }

                baseResponse.Data = await phone;
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<Phone>()
                {
                    Description = $"[GetByName] : {ex.Message}",
                    StatusCode = StatusCode.NotFound,
                };

            }

        }

        public async Task<BaseResponse<PhoneViewModel>> CreatePhone(PhoneViewModel phoneViewModel)
        {
            var baseresponse=new BaseResponse<PhoneViewModel>();

            try
            {
                var phone = new Phone()
                {
                    Description= phoneViewModel.Description,
                    Onsale=phoneViewModel.Onsale,
                    Name=phoneViewModel.Name,
                    Model=phoneViewModel.Model,
                    Price=phoneViewModel.Price
                };

                _phoneRepository.Create(phone);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PhoneViewModel>()
                {
                    Description = $"[GetByName] : {ex.Message}",
                    StatusCode = StatusCode.NotFound,
                };
            }

            return baseresponse;
        }

        public async Task<IBaseResponse<bool>> DeletePhone(int id)
        {
            var baseresponse =new BaseResponse<bool>();

            try
            {
                var phone=_phoneRepository.Get(id);

                if (phone == null)
                {
                    baseresponse.Description = "Not Found";
                    baseresponse.StatusCode = StatusCode.NotFound;
                    return baseresponse;
                }

                _phoneRepository.Delete(phone);
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeletePhone] : {ex.Message}"
                };
            }

            return baseresponse;
        }

        public async Task<BaseResponse<IEnumerable<Phone>>> GetPhones()
        {
            var baseresponse = new BaseResponse<IEnumerable<Phone>>();
            try
            {
                var phones = _phoneRepository.Select();

                if (phones==null)
                {
                    baseresponse.Description = "Not Found";
                    baseresponse.StatusCode=StatusCode.InternalServerError;
                    return baseresponse;
                }

                baseresponse.Data= phones;
                baseresponse.StatusCode=StatusCode.Ok;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Phone>>()
                {
                    Description = $"[GetPhones] : {ex.Message}"
                };
            }

            return baseresponse;
        }

        public async Task<IBaseResponse<Phone>> Edit(int id, PhoneViewModel model)
        {
            var baseResponse=new BaseResponse<Phone>();

            try
            {
                var phone=_phoneRepository.Get(id);
                if (phone==null)
                {
                    baseResponse.StatusCode = StatusCode.NotFound;
                    baseResponse.Description = "Not Found";
                    return baseResponse;
                }

                phone.Description= model.Description;
                phone.Model = model.Model;
                phone.Price= model.Price;
                phone.Onsale= model.Onsale;
                phone.Name= model.Name;

                await _phoneRepository.Update(phone);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Phone>()
                {
                    Description = $"[Edit] : {ex.Message}"
                };
            }

            return new BaseResponse<Phone>();
        }
    }
}

