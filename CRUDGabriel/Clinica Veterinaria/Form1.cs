using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cadastro_VideoGame.modelo;
using Cadastro_VideoGame.util;
using Npgsql;



namespace Cadastro_VideoGame
{
    public partial class FormCadastro : Form
    {
      
        public FormCadastro()
        {
            InitializeComponent();
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            try
            {


                VideoGames videoGames = new VideoGames();

                if (txtnome.Text.Trim() == string.Empty || txtMarca.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Existem campos vazios", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtnome.BackColor = Color.Red;
                    txtMarca.BackColor = Color.Red;

                }
                else
                {
                    string Nome = txtnome.Text;
                    string Marca = txtMarca.Text;



                    txtnome.Text = " ";
                    txtMarca.Text = " ";


                   
                    videoGames.nome = Nome;
                    videoGames.marca = Marca;


                    videoGames.Cadastrar();

                    MessageBox.Show("Cadastrado com sucesso! ");
                    txtnome.BackColor = Color.White;
                    txtMarca.BackColor = Color.White;

                }

            }
            catch (Exception ex)
                              
            {
                MessageBox.Show("Erro!", "Erro ao Cadastrar!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            

                txtnome.Text = " ";
                txtMarca.Text = " ";
                
                txtnome.Focus();




            }
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            VideoGames videoGames = new VideoGames();

            dgvRegistro.DataSource = videoGames.Visualizar();
            dgvRegistro.Show();
            btnVoltar.Show();
            


        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            dgvRegistro.Hide();

            btnVoltar.Hide();
            btnDelete.Hide();
            txtId.Hide();
            lblInsiraId.Hide();
            btnOk.Hide();

            txtnome.Text = " ";
            txtMarca.Text = " ";
            

        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            VideoGames videoGames = new VideoGames();

            

            dgvRegistro.DataSource = videoGames.Visualizar();
            dgvRegistro.Show();
            btnDelete.Show();
            btnVoltar.Show();
            txtId.Show();
            lblInsiraId.Show();
            




        }

        private void dgvRegistro_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbAnimal_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            VideoGames videoGames = new VideoGames();
            dgvRegistro.DataSource = videoGames.Visualizar();
            dgvRegistro.Show();          
            btnVoltar.Show();
            btnOk.Show();
            txtId.Show();
            lblInsiraId.Show();



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            

            
            {
                try
                {
                    VideoGames videoGames = new VideoGames();
                    videoGames.Id = Convert.ToInt16(txtId.Text);
                    videoGames.Deletar();
                    if (txtId.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Preencha o ID !!!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Operação realizada com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        dgvRegistro.Hide();

                        btnVoltar.Hide();
                        btnDelete.Hide();
                        txtId.Hide();
                        lblInsiraId.Hide();

                        txtnome.Text = " ";
                        txtMarca.Text = " ";

                        txtId.Text = " ";
                        txtId.BackColor = Color.White;
                    }

                }
                catch (Exception ex)
                {
                    
                        MessageBox.Show("Preencha o ID !!!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtId.BackColor = Color.Red;
                    
                }

                
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtId.Text;
                string nome = txtnome.Text;
                string marca = txtMarca.Text;
                

                VideoGames videoGames = new VideoGames();
                videoGames.Id = Convert.ToInt32(id);
                videoGames.nome = nome;
                videoGames.marca = marca;
               

                videoGames.Update();

                MessageBox.Show("Operação Realizada com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar alterar. " + ex.Message,
                 "Falha na operação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            btnUpdate.Hide();
            btnCancelar.Hide();
            btnCadastro.Enabled = true;
            btnVisualizar.Enabled = true;
            btnAtualizar.Enabled = true;
            btnDeletar.Enabled = true;
                              
            txtnome.Text = " ";
            txtMarca.Text = " ";
            

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            

            dgvRegistro.Hide();
            btnOk.Hide();
            btnVoltar.Hide();
            btnDelete.Hide();
            btnUpdate.Show();
            txtId.Hide();
            lblInsiraId.Hide();
            btnUpdate.Show();
            btnCancelar.Show();
            btnCadastro.Enabled = false;
            btnVisualizar.Enabled = false;
            btnAtualizar.Enabled = false;
            btnDeletar.Enabled = false;
            
        

            VideoGames videoGames = new VideoGames();

            NpgsqlConnection conexao = null;
            try
            {

                conexao = ConectaDB.getConexao();
                string sql = "SELECT nome,marca FROM registros WHERE id=@id ";



                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id",txtId.Text);

                
                NpgsqlDataReader leitor = cmd.ExecuteReader();

                while (leitor.Read())
                {
                    
                                         
                    txtnome.Text = leitor["nome"].ToString();
                    txtMarca.Text = leitor["marca"].ToString();
                    


                }

                txtnome.Focus();

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnVoltar.Hide();
            btnDelete.Hide();
            txtId.Hide();
            lblInsiraId.Hide();
            btnUpdate.Hide();
            btnCancelar.Hide();
            btnCadastro.Enabled = true;
            btnVisualizar.Enabled = true;
            btnAtualizar.Enabled = true;
            btnDeletar.Enabled = true;

            txtnome.Text = " ";
            txtMarca.Text = " ";
            
            txtId.Text = " ";
        }

        private void FormCadastro_Load(object sender, EventArgs e)
        {

        }

        private void btnAtualiza_Click(object sender, EventArgs e)
        {

        }

        private void txtnome_TextChanged(object sender, EventArgs e)
        {

            txtnome.BackColor = Color.White;
            
        }

        private void txtnome_Click(object sender, EventArgs e)
        {
            txtnome.BackColor = Color.White;
        }

        private void txtMarca_Click(object sender, EventArgs e)
        {
            txtMarca.BackColor = Color.White;
        }

        private void txtId_Click(object sender, EventArgs e)
        {
            txtId.BackColor = Color.White;
        }
    }
}
