using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using Microsoft.VisualBasic;


namespace DruckVorschau
{
    public class DurckView: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //[Reactive] public string Barcode { get; set; } = "123456789012";

        private bool _printcontrol = false;
        public bool printcontrol
        {
            get
            {
                return _printcontrol;
            }
            set
            {
                _printcontrol = value;
                NotifyPropertyChanged(nameof(printcontrol));
            }
        }

        private int _size = 70;
        public int size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                NotifyPropertyChanged(nameof(size));
            }
        }

        private int _sizeCodeXY = 35;
        public int sizeCodeXY
        {
            get
            {
                return _sizeCodeXY;
            }
            set
            {
                _sizeCodeXY = value;
                NotifyPropertyChanged(nameof(sizeCodeXY));
            }
        }

        private int _sizeBarcode1 = 120;
        public int sizeBarcode1
        {
            get
            {
                return _sizeBarcode1;
            }
            set
            {
                _sizeBarcode1 = value;
                NotifyPropertyChanged(nameof(sizeBarcode1));
            }
        }

        private int _sizeCode2 = 45;
        public int sizeCode2
        {
            get
            {
                return _sizeCode2;
            }
            set
            {
                _sizeCode2 = value;
                NotifyPropertyChanged(nameof(sizeCode2));
            }
        }

        private int _sizeBarCode2 = 120;
        public int sizeBarCode2
        {
            get
            {
                return _sizeBarCode2;
            }
            set
            {
                _sizeBarCode2 = value;
                NotifyPropertyChanged(nameof(sizeBarCode2));
            }
        }

        private string _italicCode2 ;
        public string italicCode2
        {
            get
            {
                return _italicCode2;
            }
            set
            {
                _italicCode2 = value;
                NotifyPropertyChanged(nameof(italicCode2));
            }
        }

        private string _italicCode3;
        public string italicCode3
        {
            get
            {
                return _italicCode3;
            }
            set
            {
                _italicCode3 = value;
                NotifyPropertyChanged(nameof(italicCode3));
            }
        }

        private string _italicCode4;
        public string italicCode4
        {
            get
            {
                return _italicCode4;
            }
            set
            {
                _italicCode4 = value;
                NotifyPropertyChanged(nameof(italicCode4));
            }
        }

        private string _bold1 = "DemiBold";
        public string bold1
        {
            get
            {
                return _bold1;
            }
            set
            {
                _bold1 = value;
                NotifyPropertyChanged(nameof(bold1));
            }
        }
        private string _bold2;
        public string bold2
        {
            get
            {
                return _bold2;
            }
            set
            {
                _bold2 = value;
                NotifyPropertyChanged(nameof(bold2));
            }
        }

        private string _bold3 ;
        public string bold3
        {
            get
            {
                return _bold3;
            }
            set
            {
                _bold3 = value;
                NotifyPropertyChanged(nameof(bold3));
            }
        }

        private string _visibile1;
        public string visibile1
        {
            get
            {
                return _visibile1;
            }
            set
            {
                _visibile1 = value;
                NotifyPropertyChanged(nameof(visibile1));
            }
        }

        private string _visibile2;
        public string visibile2
        {
            get
            {
                return _visibile2;
            }
            set
            {
                _visibile2 = value;
                NotifyPropertyChanged(nameof(visibile2));
            }
        }


        private string _BarCodeText = "*Placeholder*";
        public string BarCodeText
        {
            get
            {
                return _BarCodeText;
            }
            set
            {
                _BarCodeText = value;
                NotifyPropertyChanged(nameof(BarCodeText));
            }
        }

        private bool _enablefirstBarcode = false;
        public bool enablefirstBarcode
        {
            get
            {
                return _enablefirstBarcode;
            }
            set
            {
                _enablefirstBarcode = value;
                NotifyPropertyChanged(nameof(enablefirstBarcode));
            }
        }

        private bool _enablesecondBarcode;
        public bool enablesecondBarcode
        {
            get
            {
                return _enablesecondBarcode;
            }
            set
            {
                _enablesecondBarcode = value;
                NotifyPropertyChanged(nameof(enablesecondBarcode));
            }
        }

        private string _secondBarCodeText = "Placeholder";
        public string secondBarCodeText
        {
            get
            {
                return _secondBarCodeText;
            }
            set
            {
                _secondBarCodeText = value;
                NotifyPropertyChanged(nameof(secondBarCodeText));
            }
        }
        PrinterSettings settings = new PrinterSettings();



        private List<string> _printerlist;
        public List<string> printerList
        {
            get
            {
                return new List<string>() { settings.PrinterName};
            }
            set
            {
                _printerlist = value;
            }
        }

        private string _selected_printer;
        public string selected_printer
        {
            get
            {
                return _selected_printer;
            }
            set
            {
                _selected_printer = value;
                NotifyPropertyChanged(nameof(selected_printer));
            }
        }

        public string BarcodeEncode(string value)
        {
            int[] overChars = { 8216, 8217, 8220, 8221, 8226, 8211, 8212, 732, 8364 };
            char[] valueChars = value.ToCharArray();
            int[] checksumVals = new int[value.Length];

            for (int n = 0; n < valueChars.Length; n++)
            {
                checksumVals[n] = (((byte)valueChars[n]) - 32) * (n + 1);
            }

            int checksum = checksumVals.Sum() % 103;
            char check = (char)(checksum + 33);
            if (checksum > 93)
                check = (char)overChars[checksum - 94];

            string start = ((char)353).ToString();
            string stop = ((char)339).ToString();

            return start + value + check.ToString() + stop;
        }

        


        public string code128(string chaine)
        {
            string code128 = string.Empty;
            // V 2.0.0
            // Parametres : une chaine
            // Parameters : a string
            // Retour : * une chaine qui, affichee avec la police CODE128.TTF, donne le code barre
            // * une chaine vide si parametre fourni incorrect
            // Return : * a string which give the bar code when it is dispayed with CODE128.TTF font
            // * an empty string if the supplied parameter is no good


            //int %
            //long &
            //double #
            //short? single !
            //decimal @
            //string $
            int i = 0;
            long checksum;
            int mini;
            int dummy;
            bool tableB;

            if (chaine.Length > 0)
            {
                for (i = 1; i < chaine.Length; i++)
                {
                   // System.Windows.MessageBox.Show(chaine.Substring(i, 1));
                    switch ((int)chaine.Substring(i, 1).ToCharArray()[0])
                    {
                        case int n when (n >= 32 && n <= 126):
                        case 203:
                            break;

                        default:
                            i = 0;
                            break;
                    }
                }

                code128 = string.Empty;
                tableB = true;
                if(i > 0)
                {
                    i = 1;
                    while (i <= chaine.Length) 
                    { 
                        if(tableB)
                        {
                            mini = (i == 1 || i + 3 == chaine.Length) ? 4 : 6;
                            testnum(ref i, ref mini, ref code128);

                            if (mini < 0)
                            {
                                if (i == 1)
                                    code128 = ((char)210).ToString();
                                else 
                                    code128 = code128 + ((char)204).ToString();

                                tableB = false;
                            }
                        }
                        else
                        {
                            if(i == 1)
                                code128 = ((char)209).ToString();
                        }
                        if(tableB == false)
                        {
                            mini = 2;

                            testnum(ref i, ref mini, ref code128);

                            if(mini < 0)
                            {
                                dummy = ((int)chaine.Substring(i, 2).ToCharArray()[0]);
                                dummy = (dummy < 95 ? dummy + 32 : dummy + 105);
                                code128 = code128 + ((char)dummy).ToString();
                                i = i + 2;
                            }
                            else
                            {
                                code128 = code128 + ((char)205).ToString();
                                tableB = true;

                            }
                            if(tableB)
                            {
                                code128 = code128 + (int)chaine.Substring(i, 1).ToCharArray()[0];
                                i = i + 1;
                            }
                        }
                    }
                    for (i = 1; i <= code128.Length; i++)
                    {
                        dummy = (int)code128.Substring(i, 1).ToCharArray()[0];
                        dummy = (dummy < 127 ? dummy - 32 : dummy - 105);
                        if(i == 1)
                        {
                            checksum = dummy;
                            checksum = (checksum + (i - 1) * dummy);
                        }
                    }
                    //checksum = (checksum < 95 ? checksum + 32 : checksum + 105);

                    //code128 = code128 + checksum + 211;
                }


            }


            return "";
        }

        public static void testnum(ref int i, ref int mini, ref string code128)
        {
            mini = mini - 1;
            if(i + mini <= code128.Length)
            {
                while (mini >= 0)
                {
                    //If Asc(Mid$(chaine$, i% +mini %, 1)) < 48 Or Asc(Mid$(chaine$, i% +mini %, 1)) > 57 Then Exit Do;
                    if (code128.Substring(i + mini, 1).ToCharArray()[0] < 48 || code128.Substring(i + mini, 1).ToCharArray()[0] > 57)
                    {
                        mini = mini - 1;
                    }
                }
            }
        }

    }



}
