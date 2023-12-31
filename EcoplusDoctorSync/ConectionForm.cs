﻿using EcoplusDoctorSync.Helpers;
using EcoplusDoctorSync.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoplusDoctorSync
{
    public partial class ConectionForm : Form
    {
        public ConectionForm()
        {
            InitializeComponent();
        }

        public Root connectionsList;

        private void ConnectionForm_Load(object sender, EventArgs e) 
        {
            this.gbManual.Enabled = true;
            this.gbImportar.Enabled = false;
            this.ckbManual.Checked = true;
            this.ckbManual.Enabled = false;
            connectionsList = new Root();
            connectionsList.Conexoes = new List<Conexao>();
            LoadJson();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            if (lvConexoes.SelectedItems.Count != 0)
            {
                string apelido = lvConexoes.SelectedItems[0].SubItems[0].Text;
                int index = lvConexoes.SelectedItems[0].Index;
                connectionsList.Conexoes.RemoveAll(c => c.Apelido == apelido);
                lvConexoes.Items.RemoveAt(index);
            }

            Conexao conexao = new Conexao
            {
                Apelido = txtApelido.Text,
                Servidor = txtServidor.Text,
                Base = txtBancoDeDados.Text,
                Usuario = txtUsuario.Text,
                Senha = txtSenha.Text
            };

            connectionsList.Conexoes.Add(conexao);
            ListViewItem novoItem = new ListViewItem(new[] { conexao.Apelido, conexao.GetStringDeConexao() });

            lvConexoes.Items.Add(novoItem);

            lvConexoes.Refresh();

            ConfigurationHelper.WriteJsonFile(connectionsList);
            Limpar();
        }

        private void LoadJson() 
        {

            connectionsList = ConfigurationHelper.ReadValue();

            if(connectionsList.Conexoes != null)
            {
                foreach (var conn in connectionsList.Conexoes)
                {
                    ListViewItem novoItem = new ListViewItem(new[] { conn.Apelido, conn.GetStringDeConexao() });
                    lvConexoes.Items.Add(novoItem);

                }
            }

            
            lvConexoes.Refresh();

        }

        private void CloseForm()
        {
            ConfigurationHelper.WriteJsonFile(connectionsList);
            this.Close();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void lvConexoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvConexoes.SelectedItems.Count > 0)
            {
                // Obter o texto da primeira coluna (índice 0) do item selecionado
                string textoDaPrimeiraColuna = lvConexoes.SelectedItems[0].SubItems[0].Text;
                Popularform(textoDaPrimeiraColuna);
                btnExcluir.Enabled = true;
            }
        }

        private void Popularform(string conexaoSelecionada) 
        {

            Conexao conexao = connectionsList.Conexoes.FirstOrDefault(con => con.Apelido == conexaoSelecionada);

            if (conexao != null) 
            { 
            
                    txtApelido.Text = conexao.Apelido;
                    txtServidor.Text = conexao.Servidor;
                    txtUsuario.Text = conexao.Usuario;
                    txtSenha.Text = conexao.Senha;
                    txtBancoDeDados.Text = conexao.Base;
                    txtStringConexao.Text = conexao.GetStringDeConexao();
            }

        }

        private void Limpar() 
        {

            txtApelido.Text = string.Empty;
            txtServidor.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            txtSenha.Text = string.Empty;
            txtBancoDeDados.Text = string.Empty;
            txtStringConexao.Text = string.Empty;

            btnExcluir.Enabled = false;

            if (lvConexoes.SelectedItems.Count > 0)
            {
                // Desselecionar a linha selecionada
                lvConexoes.SelectedItems.Clear();
            }

        }

        private void Excluir() 
        {

            if (lvConexoes.SelectedItems.Count != 0)
            {
                string apelido = lvConexoes.SelectedItems[0].SubItems[0].Text;
                int index = lvConexoes.SelectedItems[0].Index;
                connectionsList.Conexoes.RemoveAll(c => c.Apelido == apelido);
                lvConexoes.Items.RemoveAt(index);
                ConfigurationHelper.WriteJsonFile(connectionsList);
            }
            else 
            {

                MessageBox.Show("Por favor selecione um conexão a ser excluída", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


            lvConexoes.Refresh();
            Limpar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Excluir();
        }
    }
}
