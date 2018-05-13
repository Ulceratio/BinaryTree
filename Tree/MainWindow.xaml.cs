using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

namespace Tree
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random = new Random((int)DateTime.Now.ToBinary());

        BinaryTree<int> tree;

        public MainWindow()
        {
            InitializeComponent();

            inputValues.Text = "5, 9 , 8 , 3, 4, 7, 12, 11, 13, 1, 2 ";
            //var res = (from element in inputValues.Text.Split(',') select int.Parse(element)).ToList();
            //BinaryTree<int> tempTree = new BinaryTree<int>();
            //BinaryTree<int>.Element rootElement = null;
            //foreach (var item in res)
            //{
            //    rootElement = tempTree.BalancedSearchWithInsertRecursion(rootElement, item);
            //}
            //AVL aVL = new AVL();
            //foreach (var item in res)
            //{
            //    aVL.Add(item);
            //}
            //inputValues.Text = "1,2,3,4,5,6,7,8,9,10";
            //inputValues.Text = "10,9,8,7,6,5,4,3,2,1";
            //inputValues.Text = "77,15,57,47,36,55,98,28,7,45,33,5,25,100,2,4,138,6,11,8";
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            var res = (from element in inputValues.Text.Split(',') select int.Parse(element)).ToList();
            //tree = new BinaryTree<int>(res,"AVL");
            tree = new BinaryTree<int>(res, "Simple");
            binaryTree.Text = tree.ToString();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            resultOfSearch.Text = tree.SearchWithInsert(int.Parse(searchTB.Text)).ToString();
            binaryTree.Text = tree.ToString();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            tree.Delete(int.Parse(deleteTB.Text));
            binaryTree.Text = tree.ToString();
        }

        private void startFromFile_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.ShowDialog();
            //string path = fileDialog.FileName;
            //string path = "testFile.txt";
            //string[] lines = File.ReadAllLines(path);
            //int[] masToTree = (from element in lines.AsParallel() select int.Parse(element)).ToArray();
            //MessageBox.Show("reading is done");
            //inputValues.Text = String.Join(",", masToTree);
            //MessageBox.Show("text is done");
            //Thread.Sleep(1000);
            Task.Run(() =>
            {
                //tree = new BinaryTree<int>(masToTree, "AVL");
                tree = new BinaryTree<int>(new int[] { 1,2,3,4,5,6,7,8,9,10}, "AVL");
                //tree = new BinaryTree<int>(new int[] { 10,9,8,7,6,5,4,3,2,1}, "AVL");
                binaryTree.Dispatcher.Invoke(()=> binaryTree.Text = tree.ToString());
            });     
        }


        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 50000; i++)
            {
                using (StreamWriter writer = File.AppendText("testFile.txt"))
                {
                    writer.WriteLine(i.ToString());
                }
            }
        }
    }
}
