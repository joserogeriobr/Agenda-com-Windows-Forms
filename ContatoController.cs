using System.Data.SQLite;
using AgendaSQLITE.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSQLITE.Controller
{
    internal class ContatoController
    {
        private static SQLiteConnection sqliteConnection;

        public ContatoController()
        { }

        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection(@"Data Source=c:\dados\Contato.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }

        public static void CriarBancoSQLite()
        {
            try
            {
                SQLiteConnection.CreateFile(@"c:\dados\Contato.sqlite");
            }
            catch
            {
                throw;
            }
        }

        public static void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Contatos(Id int, Nome Varchar(50), Celular Varchar(20), Email Varchar(80))";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public static DataTable GetContatos()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Contatos";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetContato(int Id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Contatos Where Id=" + Id;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void AddContato(Contato contato)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Contatos(Id, Nome, Celular, Email ) values (@Id, @Nome, @Celular, @Email)";
                    cmd.Parameters.AddWithValue("@Id", contato.Id);
                    cmd.Parameters.AddWithValue("@Nome", contato.Nome);
                    cmd.Parameters.AddWithValue("@Celular", contato.Celular);
                    cmd.Parameters.AddWithValue("@Email", contato.Email);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateContato(Contato contato)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (contato.Id != null)
                    {
                        cmd.CommandText = "UPDATE Contatos SET Nome=@Nome, Email=@Email WHERE Id=@Id";
                        cmd.Parameters.AddWithValue("@Id", contato.Id);
                        cmd.Parameters.AddWithValue("@Nome", contato.Nome);
                        cmd.Parameters.AddWithValue("@Email", contato.Email);
                        cmd.ExecuteNonQuery();
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteContato(int Id)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "DELETE FROM Contatos Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
