using iMobile.Object.Entity;

namespace iMobile.DAL.Interfaces
{
    public interface IPhoneRepository:IBaseRepository<Phone>
    {
        Task<bool> Create(Phone entity);
        Task<bool> Delete(Phone entity);
        Task<Phone> GetByName(string name);
    }
}
