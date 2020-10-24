using System.Collections.Generic;
using System.Threading.Tasks;

namespace YS.Meta
{
    public interface IMetaService
    {
        Task<MetaInfo> GetMeta(string name);

        Task<List<string>> GetAllKeys();
    }
}
