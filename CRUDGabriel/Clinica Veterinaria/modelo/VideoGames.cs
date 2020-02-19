using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Cadastro_VideoGame.util;
using Cadastro_VideoGame.modelo;
using System.Windows.Forms;


namespace Cadastro_VideoGame.modelo
{
    public class VideoGames
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string marca { get; set; }
        

        public VideoGames()
        {

        }

        public void Cadastrar()
        {
            NpgsqlConnection conexao = null;
            try
            {

                conexao = ConectaDB.getConexao();
                string sql = "INSERT INTO registros (nome,marca) VALUES" +
                    "('" + this.nome + "','" + this.marca + "')";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                cmd.Parameters.Add(new NpgsqlParameter("@nome", this.nome));
                cmd.Parameters.Add(new NpgsqlParameter("@marca", this.marca));
                
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                if (conexao != null)
                {
                    conexao.Close();
                }
            }
        }

        public List<VideoGames> Visualizar()
        {
            NpgsqlConnection conexao = null;
            try
            {

                conexao = ConectaDB.getConexao();
                string sql = "SELECT * FROM registros";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                NpgsqlDataReader dr = cmd.ExecuteReader();

                List<VideoGames> listadeVideoGames = new List<VideoGames>();
                while (dr.Read())
                {
                    VideoGames videoGames = new VideoGames();
                    videoGames.Id = Convert.ToInt16(dr["id"]);
                    videoGames.nome = dr["nome"].ToString();
                    videoGames.marca = dr["marca"].ToString();
                    

                    listadeVideoGames.Add(videoGames);

                }


                return listadeVideoGames;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conexao != null)
                {
                    conexao.Close();
                }
            }


        }

        public void Update()
        {
            NpgsqlConnection conexao = null;
            try
            {
                conexao = ConectaDB.getConexao();
                String sql = "UPDATE  public.registros SET nome = @nome, marca = @marca WHERE id = @id";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);


                cmd.Parameters.Add("@id", Id);
                cmd.Parameters.Add("@nome",nome);
                cmd.Parameters.Add("@marca",marca);
                
                
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conexao != null)
                {
                    conexao.Close();
                }
            }
        }


        public void Deletar()
        {

            NpgsqlConnection conexao = null;
            try
            {
                conexao = ConectaDB.getConexao();
                String sql = "DELETE FROM public.registros WHERE id = @id;";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                cmd.Parameters.Add("@id", Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conexao != null)
                {
                    conexao.Close();
                }
            }
        }
    }
}
