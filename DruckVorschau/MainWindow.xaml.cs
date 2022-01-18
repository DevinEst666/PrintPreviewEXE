using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Printing;
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
using Image = System.Windows.Controls.Image;

namespace DruckVorschau
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = (DurckView)this.DataContext;

          //  vm.code128(BarCode.Text);
           // BarCode.Text = vm.code128(BarCode.Text);
           // BarCode.Text = vm.BarcodeEncode(BarCode.Text);
        }

        //public string code(string chaine)
        //{

        //    int i, j, intWeight, intlenght, intWtProd, fs;
        //    int[] arrayData = new int[0];

        //    var arraySubst = new object[] { "Ã", "Ä", "Å", "Æ", "Ç", "È", "É", "Ê" };
            

        //    intlenght = chaine.Length;
        //    arrayData[0] = 104;
        //    intWtProd = 104;

        //    for (j = 0; j < intlenght; j+= 1)
        //    {
        //        arrayData[j + 1] = x.charCodeAt(j) - 32; // Have to convert to Code 128 encoding
        //        intWeight = j + 1; // to generate the checksum
        //        intWtProd += intWeight * arrayData[j + 1];
        //    }
        //    arrayData[j + 1] = intWtProd % 103;
        //    arrayData[j + 2] = 106;
        //    int chr = (arrayData[j + 1]);

        //    chr = int.Parse(arrayData[j + 1], 10); // Gotta convert from character to a number
        //    if (chr > 94)
        //    {
        //        chrString = arraySubst[chr - 95];
        //    }
        //    else
        //    {
        //        chrString = String.fromCharCode(chr + 32);
        //    }
        //    if (chr > 94)
        //    {

        //    }

        //    return code(chaine);
        //}

        private void btnToolbarClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed && e.ClickCount == 1)
            {
                this.DragMove();
            }
            else
            {
                if (e.ClickCount == 2 && this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                }

            }
        }

        int temp = 0;
        private void btnToolbarMax_Click(object sender, RoutedEventArgs e)
        {
            if (temp == 1)
            {
                this.WindowState = WindowState.Normal;
                btnToolbarMax.Content = "£";
                temp = 0;
            }

            else
            {
                this.WindowState = WindowState.Maximized;
                btnToolbarMax.Content = "";
                temp = 1;
            }
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
           
            Image textBox = (Image)sender;

            var vm = (DurckView)this.DataContext;
            BarCodeChangeWindow window2 = new BarCodeChangeWindow();

            if (textBox.Name == "BarCode")
            {
                vm.enablesecondBarcode = false;
                vm.enablefirstBarcode = true;
            }

            if (textBox.Name == "secondBarcode")
            {
                vm.enablefirstBarcode = false;
                vm.enablesecondBarcode = true;
            }

            window2.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrinterSettings settings = new PrinterSettings();
            string Printer = settings.PrinterName;

            PrintWindow print = new PrintWindow();
            print.ShowDialog();

        }

        public void print()
        {
            var vm = (DurckView)this.DataContext; 
            PrintDialog printDlg = new PrintDialog();
            vm.printcontrol = true;

            MessageBox.Show(vm.selected_printer);
            printDlg.PrintQueue = new PrintQueue(new PrintServer(), vm.selected_printer);
            printDlg.PrintTicket.CopyCount = 1;


            printDlg.PrintVisual(previewPrint, "Border Printing.");
            vm.printcontrol = false;
        }

        private void addSize(object sender, RoutedEventArgs e)
        {
            var vm = (DurckView)this.DataContext;

            Button button = sender as Button;

            if(button.Name == "add1")
                vm.size = vm.size + 5;
            if (button.Name == "add2")
                vm.sizeCodeXY = vm.sizeCodeXY + 5;
            if (button.Name == "add3")
                vm.sizeBarcode1 = vm.sizeBarcode1 + 5; 
            if (button.Name == "add4")
                vm.sizeCode2 = vm.sizeCode2 + 5;
            if (button.Name == "add5")
                vm.sizeBarCode2 = vm.sizeBarCode2 + 5;
        }

        private void decreaseSize(object sender, RoutedEventArgs e)
        {
            var vm = (DurckView)this.DataContext;

            Button button = sender as Button; 
            if (button.Name == "minus1")
                vm.size = vm.size - 5;
            if (button.Name == "minus2")
                vm.sizeCodeXY = vm.sizeCodeXY - 5;
            if (button.Name == "minus3") 
                if (vm.sizeBarcode1 > 85)
                    vm.sizeBarcode1 = vm.sizeBarcode1 - 5;
            if (button.Name == "minus4")
                vm.sizeCode2 = vm.sizeCode2 - 5;
            if (button.Name == "minus5")
                if(vm.sizeBarCode2 > 85)
                    vm.sizeBarCode2 = vm.sizeBarCode2 - 5;

        }

        private void Italic(object sender, RoutedEventArgs e)
        {
            var vm = (DurckView)this.DataContext;

            if (italic1.IsChecked == true)
                vm.italicCode2 = "Italic";
            else
                vm.italicCode2 = "normal";

            if (italic2.IsChecked == true)
                vm.italicCode3 = "Italic";
            else
                vm.italicCode3 = "normal";


            if (italic3.IsChecked == true)
                vm.italicCode4 = "Italic";
            else
                vm.italicCode4 = "normal";
        }

        private void Bold(object sender, RoutedEventArgs e)
        {
            var vm = (DurckView)this.DataContext;

            if (bold1.IsChecked == true)
                vm.bold1 = "ExtraBold";
            else
                vm.bold1 = "DemiBold";

            if (bold2.IsChecked == true)
                vm.bold2 = "ExtraBold";
            else
                vm.bold2 = "normal";

            if (bold3.IsChecked == true)
                vm.bold3 = "ExtraBold";
            else
                vm.bold3 = "normal";
        }

        private void visible(object sender, RoutedEventArgs e)
        {
            var vm = (DurckView)this.DataContext;
            if (visi.IsChecked == true)
                vm.visibile1 = "Hidden";

            else 
                vm.visibile1 = "Visible";

            if (visi2.IsChecked == true)
                vm.visibile2 = "Hidden";

            else
                vm.visibile2 = "Visible";
        }

        private void btnToolbarMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
