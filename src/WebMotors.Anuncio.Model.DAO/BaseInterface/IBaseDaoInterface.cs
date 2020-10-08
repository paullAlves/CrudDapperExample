using System.Collections.Generic;

namespace WebMotors.Anuncio.Model.DAO.BaseInterface
{
    public interface IBaseDaoInterface<T>
    {
        int Inserir(T model, out string mensagem, string strConnection = null);
        void Alterar(T model, out string mensagem, string strConnection = null);
        void Excluir(object obj, out string mensagem, string strConnection = null);
        void ExcluirLista(object obj, out string mensagem, string strConnection = null);
        T ObterPorChave(object parametros, string strConnection = null);
        IEnumerable<T> Obter(object parametros, string strConnection = null);
        IEnumerable<T> ObterTodos(string strConnection = null);
    }
}
