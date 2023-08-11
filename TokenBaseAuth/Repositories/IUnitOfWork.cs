using Microsoft.AspNetCore.Components.Web;

namespace TokenBaseAuth.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit(bool state = true);
    }
}
