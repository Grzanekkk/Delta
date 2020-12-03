using System;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Forms;
using System.Windows.Media;
using LinqToDB;
using System.Windows.Shapes;
using LiveCharts.Defaults;

namespace Delta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WyświetlWykres();
        }

        double delta;
        double a, b, c;
        double p, q;
        double x1, x2;

        string postacKanoniczna, postacOgolna, postacIloczynowa, wierzcholek, miejscaZerowe;

        private void ObliczButton_Click(object sender, EventArgs e)
        {
            PrzypiszABC();

            if (a == 0)
            {
                MessageBox.Show("To nie jest funkcja kwadratowa!");
                return;
            }

            ObliczWKolejnosci();
            WyświetlDane();

        }

        void WyświetlDane()
        {
            ogolnaTxt.Text = postacOgolna;
            kanonicznaTxt.Text = postacKanoniczna;
            iloczynowaTxt.Text = postacIloczynowa;
            wierzcholekTxt.Text = wierzcholek;
            miejsceZeroweTxt.Text = miejscaZerowe;
            deltaTxt.Text = delta.ToString();
        }

        void WyświetlWykres()
        {
            //wykresFunkcij.Series = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Values = new ChartValues<ObservablePoint>
            //        {
            //            new ObservablePoint(p,q),
            //            new ObservablePoint(12,3),
            //            new ObservablePoint(q,p)
            //        },
            //        PointGeometrySize = 15
            //    }
            //};
        }

        void ObliczWKolejnosci()
        {
            ObliczDelte();

            ObliczWierzchołek();
            ObliczMiejscaZerowe();

            ObliczPostacOgolna();
            ObliczPostacKanoniczna();
            ObliczPostacIloczynowa();
        }


        void PrzypiszABC()
        {
            a = Convert.ToDouble(aInput.Text);
            b = Convert.ToDouble(bInput.Text);
            c = Convert.ToDouble(cInput.Text);
        }


        #region Liczenie


        void ObliczDelte()
        {
            delta = (b * b) - (4 * a * c);
        }

        void ObliczWierzchołek()
        {
            p = Math.Round (- b / (2 * a), 2);
            q = Math.Round (- delta / (4 * a), 2);

            wierzcholek = $"({p}; {q})";
        }

        void ObliczMiejscaZerowe()
        {
            if(delta > 0)
            {
                x1 = Math.Round((-b + Math.Sqrt(delta)) / (2 * a), 2);
                x2 = Math.Round((-b - Math.Sqrt(delta)) / (2 * a), 2);

                miejscaZerowe = $"x1 = {x1}, x2 = {x2}";
            }
            else if(delta == 0)
            {
                x1 = Math.Round((-b - Math.Sqrt(delta)) / 2 * a, 2);
                miejscaZerowe = $"{x1}";
            }
            else if(delta < 0)
            {
                miejscaZerowe = "Brak";
            }        
        }

        void ObliczPostacOgolna()
        {
            if (b < 0)
            {
                if(c < 0)
                {
                    postacOgolna = $"{a}x² - {Math.Abs(b)}x - {Math.Abs(c)}";
                    return;
                }

                postacOgolna = $"{a}x² - {Math.Abs(b)}x + {c}";
                return;
            }

            if (c < 0)
            {
                postacOgolna = $"{a}x² + {b}x - {Math.Abs(c)}";
                return;
            }

            postacOgolna = $"{a}x² + {b}x + {c}";
        }

        void ObliczPostacKanoniczna()
        {
            if (p < 0)
            {
                if (q < 0)
                {
                    postacKanoniczna = $"{a}(x + {Math.Abs(p)})² - {Math.Abs(q)}";
                    return;
                }

                postacKanoniczna = $"{a}(x + {Math.Abs(p)})² + {q}";
                return;
            }

            if (q < 0)
            {
                postacKanoniczna = $"{a}(x - {p})² - {Math.Abs(q)}";
                return;
            }

            postacKanoniczna = $"{a}(x - {p})² + {q}";
        }

        void ObliczPostacIloczynowa()
        {
            if (delta < 0)
            {
                postacIloczynowa = "Brak";
                return;
            }

            if(delta == 0)
            {
                x2 = x1;
            }

            if (x1 < 0)
            {
                if (x2 < 0)
                {
                    postacIloczynowa = $"{a}(x + {Math.Abs(x1)})(x + {Math.Abs(x2)})";
                    return;
                }

                postacIloczynowa = $"{a}(x + {Math.Abs(x1)})(x - {x2})";
                return;
            }

            if (x2 < 0)
            {
                postacIloczynowa = $"{a}(x - {x1})(x + {Math.Abs(x2)})";
                return;
            }

            postacIloczynowa = $"{a}(x - {x1})(x - {x2})";
        }


        #endregion Liczenie
    }
}
