using WebMotors.Anuncio.Model.DAO.BaseRepository;

namespace WebMotors.Anuncio.Model.Repository.BaseDao
{
    public class BaseTesteWebMotorsRepository<T> : BaseDaoRepository<T>
    {
        public BaseTesteWebMotorsRepository() : base("data source=DESKTOP-M9UJP5S; initial catalog=teste_webmotors;integrated security=true")
        {
        }
    }
}
