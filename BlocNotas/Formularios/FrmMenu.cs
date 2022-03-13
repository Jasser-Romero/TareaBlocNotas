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
        int x = 217, cont = 0;
        List<string> list = new List<string>();
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
                richTextBox1.Text = notasServices.Read(@rutaArchivo);

            }

            
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Agregar FileSystemWatcher
            FrmMenu_Load(sender, e);
            string rutaArchivo = string.Empty;
            string filePath = string.Empty;

            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("Agrega algo","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = saveFileDialog.FileName;
                filePath = Path.GetDirectoryName(rutaArchivo);
                LoadFolder(treeView1.Nodes, new DirectoryInfo(@filePath));
                notasServices.Create(richTextBox1.Text, rutaArchivo);
            }
           
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            if(cont != 0)
            {
                list.Add(richTextBox1.Text);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Quiere salir?", "Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //Crear instancia
            Button temp = new Button();

            //Propiedades
            temp.Height = 23;
            temp.Width = 75;
            temp.Location = new Point(x, 5);
            x += 80;
            temp.Name = "btnBoton" + cont.ToString();
            temp.Text = "Ventana # "+ cont.ToString();
            
            cont++;

            //Adicionamos el boton al form
            Controls.Add(temp);
            
            //NuevoArchivos();
        }

        
    }
}
