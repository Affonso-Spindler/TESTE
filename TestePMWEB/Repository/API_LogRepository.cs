using TestePMWEB.Context;
using TestePMWEB.Models;

namespace TestePMWEB.Repository
{
    public class API_LogRepository : Repository<API_Log>, IAPI_LogRepository
    {
        public API_LogRepository(AppDbContext contexto) : base(contexto)
        {

        }
    }
}
