using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AppConverter
{
    public partial class Form1 : Form
    {
        Bmp bm;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void bttnCargar_Click(object sender, EventArgs e)
        {
            abrirArchivo();
        }

        public void abrirArchivo()
        {
            ///<summary>
            ///Instanciando el dialogo de archivos
            /// </summary>
            OpenFileDialog openFileDialog1
                 = new OpenFileDialog();
            ///<summary>
            ///Filtro y opciones
            /// </summary>
            openFileDialog1.Filter = "All Files(*.*) | *.*| Text Files (.txt)|*.txt ";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = false;

            DialogResult userClickedOk = openFileDialog1.ShowDialog();

            if (userClickedOk == DialogResult.OK)
            {
                FileStream file = new FileStream(openFileDialog1.FileName, FileMode.Open);
                leerArchivo(file);
            }
        } 
        /// <summary>
        /// Lee el archivo
        /// </summary>
        /// <param name="s"></param>
        public void leerArchivo(FileStream s)
        {
            BinaryReader br = new BinaryReader(s);
            int tipo = br.ReadInt16();
            if (whatType(tipo))
            {
                textBox1.Text = "Es un archivo bmp";
                bm = new Bmp(howBig(br),howHigh(br),howWidth(br),howPix(br));
                textBox1.Text+=Environment.NewLine+bm.ToString();
                br.Close();
                s.Close();
            }
            else
            {
                textBox1.Text = "No es un archivo bmp";
            }
            
        }
        /// <summary>
        /// Especifica si es un bmp o no
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public bool whatType(int tipo)
        {
            if(tipo== 19778)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        /// <summary>
        /// Tamaño del archivo
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public int howBig(BinaryReader br)
        {
            Int32 tama = br.ReadInt32();
            //Espacio para la siguiente lectura
            for (int i = 0; i < 12; i++)
            {
                br.Read();
            }
            return tama;
        } 
        /// <summary>
        /// Altura de pixeles
        /// </summary>
        /// <returns></returns>
        public int howHigh(BinaryReader br)
        {
            int alto = br.ReadInt32();
            return alto;
        }
        /// <summary>
        /// Ancho de pixeles
        /// </summary>
        /// <returns></returns>
        public int howWidth(BinaryReader br)
        {
            int ancho = br.ReadInt32();
            return ancho;
        }
        public int howPix(BinaryReader br)
        {
            ///Espacio reservado
            for (int i = 0; i < 2; i++)
            {
                br.Read();
            }
            int bitPix = br.ReadInt16();
            return bitPix;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            convertirXML(textBox2.Text,textBox3.Text,textBox4.Text,textBox5.Text,textBox6.Text);
        }
        public void convertirXML(string nombre,string autor, string editorial, string year, string tema)
        {
            saveFileDialog1.ShowDialog();
            FileStream file = new FileStream(saveFileDialog1.FileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(file);
            sw.WriteLine("<?xml version=\"1.0\" ?>");
            sw.WriteLine("<LIBRERIA>");
            sw.WriteLine("  <LIBRO>");
            sw.WriteLine("      <NOMBRE>" + nombre + "</NOMBRE>");
            sw.WriteLine("      <AUTOR>" + autor + "</AUTOR>");
            sw.WriteLine("      <EDITORIAL>" + editorial + "</EDITORIAL>");
            sw.WriteLine("      <AÑO>" + year + "</AÑO>");
            sw.WriteLine("      <TEMA>" + tema + "</TEMA>");
            sw.WriteLine("  </LIBRO>");
            sw.WriteLine("</LIBRERIA>");
            sw.Close();
            file.Close();
        }
    }
}
