using System;
using AgendaSQLITE.Controller;
using AgendaSQLITE.Model;
using System.Windows.Forms;
using System.Data;

namespace AgendaSQLITE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void btnCriarBanco_Click(object sender, EventArgs e)
        {
            try
            {
                ContatoController.CriarBancoSQLite();
                btnCriarBanco.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void btnCriarTabela_Click(object sender, EventArgs e)
        {
            try
            {
                ContatoController.CriarTabelaSQlite();
                btnCriarTabela.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void btnExibirDados_Click(object sender, EventArgs e)
        {
            ExibirDados();
        }

        private void ExibirDados()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ContatoController.GetContatos();
                dgvTabela.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRegistro.Text))
            {
                MessageBox.Show("Informe o Registro do contato a ser localizado");
                return;
            }
            try
            {
                DataTable dt = new DataTable();
                int codigo = Convert.ToInt32(txtRegistro.Text);
                dt = ContatoController.GetContato(codigo);
                dgvTabela.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (!Valida())
            {
                MessageBox.Show("Informe os dados contato para incluir");
                return;
            }

            try
            {
                Contato cont = new Contato();
                cont.Id = Convert.ToInt32(txtRegistro.Text);
                cont.Nome = txtNome.Text;
                cont.Celular = txtCelular.Text;
                cont.Email = txtEmail.Text;
                ContatoController.AddContato(cont);
                ExibirDados();
                LimpaDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private bool Valida()
        {
            if (string.IsNullOrEmpty(txtRegistro.Text) && string.IsNullOrEmpty(txtNome.Text) && string.IsNullOrEmpty(txtCelular.Text) && string.IsNullOrEmpty(txtEmail.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void LimpaDados()
        {
            txtRegistro.Text = "";
            txtNome.Text = "";
            txtCelular.Text = "";
            txtEmail.Text = "";
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (!Valida())
            {
                MessageBox.Show("Informe os dados cliente a atualizar");
                return;
            }

            try
            {
                Contato cont = new Contato();
                cont.Id = Convert.ToInt32(txtRegistro.Text);
                cont.Nome = txtNome.Text;
                cont.Celular = txtCelular.Text;
                cont.Email = txtEmail.Text;
                ContatoController.UpdateContato(cont);
                ExibirDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRegistro.Text))
            {
                MessageBox.Show("Informe o Registro do contato a ser Excluído");
                return;
            }

            try
            {
                int codigo = Convert.ToInt32(txtRegistro.Text);
                ContatoController.DeleteContato(codigo);
                ExibirDados();
                LimpaDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            const string mensagem = "Deseja encerrar a aplicação?";
            const string titulo = "Encerrar aplicação";
            var resultado = MessageBox.Show(mensagem, titulo,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void dgvTabela_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvTabela.Rows[e.RowIndex];
                txtRegistro.Text = row.Cells["Id"].Value.ToString();
                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtCelular.Text = row.Cells["Celular"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
            }
        }
    }
}
