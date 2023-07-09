using MemberEvaluationService.Helpers;
using MemberEvaluationService.Services;

namespace MemberEvaluationService.Repository
{
    public interface IRepositoryWrapper
    {
        IUserService user { get; }
        void Save();
    }
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DataContext _dataContext;
        private IUserService _user;

        public IUserService user { get
            {
                if (_user == null)
                {
                    _user = new UserService(_dataContext);
                }
                return _user;
            }
        }

        public RepositoryWrapper(DataContext context)
        {
            _dataContext = context;
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }
    }
}
