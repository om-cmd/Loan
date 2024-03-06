using Loan.Data;
using Loan.Models.Domain;

namespace Loan.Datas
{
    public interface IUnitOfWork
    {
        public UserDbContext Context { get; }
       public User User { get; }
        public IHttpContextAccessor ContextAccessor { get; }

        public void Save();
    }
}
