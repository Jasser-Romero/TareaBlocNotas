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
        INotasServices notasServices;
        string rutaTreeView;
        public FrmMenu(INotasServices notasServices)
        {
            this.notasServices = notasServices;
            InitializeComponent();
        }

        private void NuevoArchivos() {

            try{

                if (string.IsNullOrEmpty(this.richTextBox1.Text))
                {
                    MessageBox.Show("Necesitas guardar primero","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    this.richTextBox1.Text = string.Empty;
                    this.Text = "Sin titulo";
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
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
            treeView1.ImageList = imageList1;
            treeView1.ImageIndex = 1;
            treeView1.Nodes[0].ImageIndex = 0;
            treeView1.SelectedImageIndex = 1;
            
            
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMenu_Load(sender, e);

            string rutaArchivo = string.Empty;
            string filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo=openFileDialog.FileName;
                rutaTreeView = Path.GetDirectoryName(@rutaArchivo);
                filePath = Path.GetDirectoryName(rutaArchivo);
                
                LoadFolder(treeView1.Nodes, new DirectoryInfo(@filePath));
                richTextBox1.Text = notasServices.Read(@rutaArchivo);

            }
            

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMenu_Load(sender, e);

            string rutaArchivo = string.Empty;
            string filePath = string.Empty;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = saveFileDialog.FileName;
                rutaTreeView = Path.GetDirectoryName(@rutaArchivo);
                filePath = Path.GetDirectoryName(rutaArchivo);
                notasServices.Create(richTextBox1.Text, rutaArchivo);
                LoadFolder(treeView1.Nodes, new DirectoryInfo(@filePath));
                
               
            }
            

        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Seguro que quiere salir?", "Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            string rutaAbsoluta = string.Empty;
            rutaAbsoluta = rutaTreeView + "\\"+treeView1.SelectedNode.Text;
            richTextBox1.Text = notasServices.Read(@rutaAbsoluta);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        
    }
}
