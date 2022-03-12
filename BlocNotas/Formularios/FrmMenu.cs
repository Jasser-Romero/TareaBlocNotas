using AppCore.Interfaces;
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

namespace BlocNotas.Formularios
{
    public partial class FrmMenu : Form
    {
        string winDir = System.Environment.GetEnvironmentVariable("windir");
        INotasServices notasServices;
        public FrmMenu(INotasServices notasServices)
        {
            this.notasServices = notasServices;
            InitializeComponent();
        }

        private void LoadFolder(TreeNodeCollection nodes, DirectoryInfo folder)
        {
            var newNode = nodes.Add(folder.Name);
            foreach (var childFolder in folder.EnumerateDirectories())
            {
                LoadFolder(newNode.Nodes, childFolder);
            }
            foreach (FileInfo file in folder.EnumerateFiles())
            {
                newNode.Nodes.Add(file.Name);
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Agregar FileSystemWatcher
            FrmMenu_Load(sender, e);
            string rutaArchivo = string.Empty;
            string filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo=openFileDialog.FileName;
                filePath = Path.GetDirectoryName(rutaArchivo);
                LoadFolder(treeView1.Nodes, new DirectoryInfo(@filePath));
            }

            
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Agregar FileSystemWatcher
            FrmMenu_Load(sender, e);
            string rutaArchivo = string.Empty;
            string filePath = string.Empty;

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = saveFileDialog.FileName;
                filePath = Path.GetDirectoryName(rutaArchivo);
                LoadFolder(treeView1.Nodes, new DirectoryInfo(@filePath));
            }
            
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Quiere salir?", "Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Application.Exit();
            }

        }
    }
}
