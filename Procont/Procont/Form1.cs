using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics; //process takibi
using System.Threading; //ufak bir bekleme gerekiyor thread.sleep kullan�ld�

namespace Procont
{
    public partial class Form1 : Form
    {
        public int selectedProcess = -1;
        public Process[] pList; // process listesini tutar
        public Form1()
        {
            InitializeComponent();
            pListYenile(); // process listesini yeniler
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                pList[treeView1.SelectedNode.Index].Kill(); // butona t�kland���nda ilgili process kill edilsin
                pListYenile(); // kill i�lemi sonras� liste yenile
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // e�er process sonland�r�lam�yorsan kullan�c�ya bildir
            }
        }
        public void pListYenile()
        {
            Thread.Sleep(100); // yenileme i�lemi kill den sonra oldu�unda bazen kill edilen fonksiyon hala listede g�r�n�yordu,bu y�zden ufak bir gecikme kullan�ld�..
            treeView1.Nodes.Clear(); // treeview yap�s�n�n nodelar� temizleniyor. 
            pList = Process.GetProcesses(); // process listemizi al�yoruz
            int i = 0; // i bizim treeview yap�m�zda indeksimiz olacak
            foreach (Process proc in pList) // listemizdeki herbir process i�in
            {
                treeView1.Nodes.Add(proc.ProcessName); // ad�n� k�k olarak ekliyoruz
                treeView1.SelectedNode = treeView1.Nodes[i]; // bu nodu se�ili hale getiriyoruz
                treeView1.SelectedNode.Nodes.Add("�ncelik : " + proc.BasePriority); // �nceli�ini yazd�r�yoruz alt kademe olarak
                treeView1.SelectedNode.Nodes.Add("ID : " + proc.Id); // process id
                treeView1.SelectedNode.Nodes.Add("Sanal Bellek : " + proc.VirtualMemorySize64); // ne kadar sanal bellek kullnm��
                i++; // indeksimizi art�r�yoruz
            }
            treeView1.SelectedNode = treeView1.Nodes[0];
        }
    }
}
