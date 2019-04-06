using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf.parser;
using CsvHelper;
using System.IO;


namespace homework
{
 
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        public void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dlg = new OpenFileDialog();
            string path;
            dlg.Filter = "PDF Files(*.pdf)|*.pdf|All files(*.*)|*.*";
            int numer = 0;
            numer = Convert.ToInt32(textBox2.Text);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                path = dlg.FileName.ToString();


                string strText = string.Empty;

                string f = "";
                int contract=0, rema=0, remb=0, remc=0, remd=0, shorty=0, longi=0, close=0;
                try
                {
                    PdfReader reader = new PdfReader(path);
                    for (int page = numer; page <= numer; page++)
                    {
                        ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                        string s = PdfTextExtractor.GetTextFromPage(reader, page, its);
                        s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
                        strText = strText + s;



                        string[] lines = s.Split('\n');
                        int j=0;
                        int temp = -2;
                        int suma = 0;
                        string a = "";
                        string b = "";
                        string c = "";
                        string d = "";
                        for (j=0; j<=lines.Length-3;j++)
                        {
                            
                            

                            if (lines[j].Contains("CONTRACT DESCRIPTION") == true)
                            {
                                //pobieranie nazwy instrumentu a
                                
                                contract = lines[j].IndexOf("CONTRACT");
                                shorty = lines[j].IndexOf("SHORT");
                                longi = lines[j].IndexOf("LONG");
                                close = lines[j].IndexOf("PRICE");

                                

                                temp = j;
                            }

                            // if (j == temp + 1)
                            // {
                            //  while (suma != 2) 
                            // {
                            //   for (int k=5; k<=lines.Length-3; k++) {

                            a = lines[j].Substring(contract);
                            rema = a.IndexOf("   ");
                            a = a.Remove(rema);

                            if (lines[j].Contains(a) == true && lines[j + 1].Contains("SPREAD") == true)
                                        {
                                        do
                                        {
                                            b = lines[j + 2].Substring(shorty);
                                            remb = b.IndexOf("*");
                                            b = b.Remove(contract);
                                            if (b.Contains("*") == true)
                                            { b = b.Remove(remb); }
                                            else { b = ""; }

                                            c = lines[j + 2].Substring(longi);
                                            remc = c.IndexOf("*");
                                            c = c.Remove(shorty-16);
                                            if (c.Contains("*") == true)
                                            { c = c.Remove(remc); }
                                            else { c = ""; }

                                            d = lines[j + 2].Substring(close);
                                            remd = d.IndexOf("   ");
                                            d = d.Remove(remd);
                                            suma = 2;

                                           // MessageBox.Show(a);
                                            
                                        } while (suma != 2);
                                        
                                            
    
                                        }
                                        else
                                        {
                                            //MessageBox.Show("tooo");
                                        }

                            // }

                            // } 
                            //}
                            if(a!="" && d != "" && (b !="" || c!="") ) { 
                            f = f + a + ";" + c + ";" + b + ";" + d + ";" + '\n';
                                a = "";
                                b = "";
                                c = "";
                                d = "";
                            }
                        }
                        string g;
                        g = "Contract Description;LONG;SHORT;CLOSE;";
                        listBox1.Items.Add(g);
                        listBox1.Items.Add(f);
                       
                      //  System.IO.File.WriteAllLines(@"C:\Users\michal\desktop\WriteLines.txt", lines);


                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string eksport;
            eksport = textBox1.Text;

            StreamWriter MyoutputWriter = new StreamWriter(@eksport);
            foreach (var item in listBox1.Items)
            {
                MyoutputWriter.WriteLine(item.ToString());
            }
            MyoutputWriter.Close();
            listBox1.Items.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
