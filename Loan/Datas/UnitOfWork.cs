using Loan.Data;
using Loan.Models.Domain;

namespace Loan.Datas
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(UserDbContext userDbContext, IHttpContextAccessor httpContextAccessor)
        {
            Context = userDbContext;
            ContextAccessor = httpContextAccessor;

            if (httpContextAccessor.HttpContext != null)
            {
                var id = httpContextAccessor.HttpContext.Session.GetString("UserId");
                if (id != null)
                {
                    var users = userDbContext.User.FirstOrDefault(x => x.UserId == Convert.ToInt64(id));
                    User = users;
                }
            }
        }
        public UserDbContext Context { get; private set; }

        public User User { get; private set; }

        public IHttpContextAccessor ContextAccessor { get; private set; }


        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
