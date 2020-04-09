using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ISIP_FrameworkHelpers;

namespace ISIP_FrameworkGUI.Windows
{
    /// <summary>
    /// Interaction logic for mathematical_function.xaml
    /// </summary>
    public partial class mathematical_function : Window
    {
        public mathematical_function()
        {
            InitializeComponent();
        }

        private void do_it_Click(object sender, RoutedEventArgs e)
        {
            int k;
            double S;
       
            try
            {
                k = int.Parse(input_K.Text);
                S = double.Parse(input_S.Text);

                if (k > 0 && S > 0)
                {
                    mainCanvas.Children.Clear();

                    List<System.Windows.Point> function = new List<Point>();

                    for (int x = 0; x < mainCanvas.ActualWidth; x++)
                    {
                        double y = mainCanvas.ActualHeight / 2 - 100*Math.Exp(-((Math.Pow(x - k, 2) / (S * S))));

                        Point fPoint = new Point(x, y);
                     //   fPoint.Offset(100, 100);
                        function.Add(fPoint);
                    }
                DrawHelper.DrawAndGetPolyline(mainCanvas, function, Brushes.Red, 2);
                    }
                else
                {
                    MessageBox.Show(
                    "Both values must npe positive", "Error!", MessageBoxButton.OK , MessageBoxImage.Warning); 
                }
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Wrong Input Message ", "Error!", MessageBoxButton.OK , MessageBoxImage.Error); 
            }

        }
    }
}
