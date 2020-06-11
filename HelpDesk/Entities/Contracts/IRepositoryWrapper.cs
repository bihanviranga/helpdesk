using System.Threading.Tasks;

namespace HelpDesk.Entities.Contracts
{
    public interface IRepositoryWrapper
    {
        ICompanyRepository Company { get; }
        Task Save();
    }
}
