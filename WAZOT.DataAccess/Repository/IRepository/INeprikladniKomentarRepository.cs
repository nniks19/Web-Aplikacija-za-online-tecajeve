using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface INeprikladniKomentarRepository: IRepository<Neprikladni_komentar>
    {
        void Update(Neprikladni_komentar obj);
    }
}
