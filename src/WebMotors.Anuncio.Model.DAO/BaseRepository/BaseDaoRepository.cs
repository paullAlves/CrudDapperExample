using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using WebMotors.Anuncio.Model.DAO.BaseInterface;

namespace WebMotors.Anuncio.Model.DAO.BaseRepository
{
    public class BaseDaoRepository<T> : IBaseDaoInterface<T>
    {
        private string strConnection;
        protected string StrConnection => strConnection;

        public BaseDaoRepository(string strConnection)
        {
            this.strConnection = strConnection;
        }

        protected DbConnection ObterConexao(string strConnection)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);
            return new SqlConnection(strConnection);
        }

        public virtual void Alterar(T model, out string mensagem, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    conn.Open();
                    int id = conn.Update(model);
                    if (id == 0) throw new Exception("Erro ao atualizar os dados. Entre em contato com o administrador.");
                    mensagem = null;
                }
                catch (Exception ex)
                {
                    mensagem = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual void Excluir(object obj, out string mensagem, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    conn.Open();
                    int id = conn.Delete<T>(obj);
                    if (id == 0) throw new Exception("Erro ao excluir os dados. Entre em contato com o administrador.");
                    mensagem = null;
                }
                catch (Exception ex)
                {
                    mensagem = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public virtual void ExcluirLista(object obj, out string mensagem, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    conn.Open();
                    int id = conn.DeleteList<T>(obj);
                    if (id == 0) throw new Exception("Erro ao excluir os dados. Entre em contato com o administrador.");
                    mensagem = null;
                }
                catch (Exception ex)
                {
                    mensagem = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual int Inserir(T model, out string mensagem, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    conn.Open();
                    mensagem = null;
                    int id = conn.Insert(model) ?? 0;
                    if (id == 0) throw new Exception("Erro ao inserir os dados. Entre em contato com o administrador.");
                    return id;
                }
                catch (Exception ex)
                {
                    mensagem = ex.Message;
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public virtual IEnumerable<T> Obter(object parametros, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    conn.Open();
                    return conn.GetList<T>(parametros);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public virtual T ObterPorChave(object parametros, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    conn.Open();
                    return conn.Get<T>(parametros);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public virtual IEnumerable<T> ObterTodos(string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    conn.Open();
                    return conn.GetList<T>();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual void ExecuteCommand(string sqlString, object objectParams, out string mensagem, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                //IDbTransaction transaction = null;
                try
                {
                    conn.Open();
                    //transaction = conn.BeginTransaction();

                    conn.Execute(sqlString, objectParams, commandType: CommandType.Text);
                    //transaction.Commit();

                    mensagem = null;
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    mensagem = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TCampo> ExecuteQuery<TCampo>(string sqlString, object objectParams, CommandType type, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    return conn.Query<TCampo>(sqlString, objectParams, commandType: type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<T> ExecuteQuery(string sqlString, object objectParams, CommandType type, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    return conn.Query<T>(sqlString, objectParams, commandType: type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<T> ExecuteQuery(object objectParams, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    return conn.GetList<T>(objectParams);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, TReturn>(string sqlString, object objectParams, Func<T1, T2, TReturn> map, string[] splitOn, CommandType type = CommandType.Text, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    return conn.Query<T1, T2, TReturn>(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, TReturn>(string sqlString, object objectParams, Func<T1, T2, T3, TReturn> map, string[] splitOn, CommandType type = CommandType.Text, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, T4, TReturn>(string sqlString, object objectParams, Func<T1, T2, T3, T4, TReturn> map, string[] splitOn, CommandType type = CommandType.Text, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, T4, T5, TReturn>(string sqlString, object objectParams, Func<T1, T2, T3, T4, T5, TReturn> map, string[] splitOn, CommandType type = CommandType.Text, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, T4, T5, T6, TReturn>(string sqlString, object objectParams, Func<T1, T2, T3, T4, T5, T6, TReturn> map, string[] splitOn, CommandType type = CommandType.Text, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, T4, T5, T6, T7, TReturn>(string sqlString, object objectParams, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, string[] splitOn, CommandType type = CommandType.Text, string strConnection = null)
        {
            using (var conn = ObterConexao(strConnection ?? this.strConnection))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
