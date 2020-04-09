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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Emgu.CV;
using Emgu.CV.Structure;
using ISIP_UserControlLibrary;

using ISIP_Algorithms.PixelAcces;
using ISIP_Algorithms.GreyLevelResolution;
using ISIP_Algorithms.Hough;
using ISIP_Algorithms.Filters;
using ISIP_Algorithms.Interpolation;
using ISIP_FrameworkHelpers;
using ISIP_Algorithms.COLOR;
using ISIP_Algorithms.Texture;


namespace ISIP_FrameworkGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int tresh = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void openGrayscaleImageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainControl.LoadImageDialog(ImageType.Grayscale);
        }

        private void openColorImageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainControl.LoadImageDialog(ImageType.Color);
        }

        private void saveProcessedImageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!mainControl.SaveProcessedImageToDisk())
            {
                MessageBox.Show("Processed image not available!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void saveAsOriginalMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.ProcessedGrayscaleImage != null)
            {
                mainControl.OriginalGrayscaleImage = mainControl.ProcessedGrayscaleImage;
            }
            else if (mainControl.ProcessedColorImage != null)
            {
                mainControl.OriginalColorImage = mainControl.ProcessedColorImage;
            }
        }

        private void mirrorByY_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                mainControl.ProcessedGrayscaleImage = mirror_image_alg.InverByYAxes(mainControl.OriginalGrayscaleImage);
            }
        }
        private void mirrorByX_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                mainControl.ProcessedGrayscaleImage = mirror_image_alg.InvertByXAxes(mainControl.OriginalGrayscaleImage);
            }
        }

        private void Gaus_menu_item_Click(object sender, RoutedEventArgs e)
        {
            Windows.mathematical_function GausWindow = new Windows.mathematical_function();

            GausWindow.Show();
        }

        private void GL_rez_do_it(object sender, RoutedEventArgs e)
        {
            UserInputDialog userInput = new UserInputDialog("Grey Level", new string[] { "Enter the nr of bits for the grey lvl :" });

            if (userInput.ShowDialog().Value == true)
            {
                int nr = (int)userInput.Values[0];

                mainControl.ProcessedGrayscaleImage = GL_rez.Apply(mainControl.OriginalGrayscaleImage, nr);

            }
        }

        private void do_it_5x5(object sender, RoutedEventArgs e)
        {
            mainControl.ProcessedGrayscaleImage = binomial_5x5.Apply(mainControl.OriginalGrayscaleImage);
        }

        private void do_it_7x7(object sender, RoutedEventArgs e)
        {
            mainControl.ProcessedGrayscaleImage = binomial_7x7.Apply(mainControl.OriginalGrayscaleImage);
        }

        private void do_it_Sobel(object sender, RoutedEventArgs e)
        {

            UserInputDialog userInput = new UserInputDialog("Threshold ? ", new string[] { "Threshold ?  :" });

            if (userInput.ShowDialog().Value == true)
            {
                int nr = (int)userInput.Values[0];
                mainControl.ProcessedGrayscaleImage = Sobel.Apply(mainControl.OriginalGrayscaleImage, nr);
            }
        }

        private void do_it_Hough(object sender, RoutedEventArgs e)
        {
            UserInputDialog userInput = new UserInputDialog("Raza ? ", new string[] { "Raza ?  :" });
            if (userInput.ShowDialog().Value == true)
            {
                int r = (int)userInput.Values[0];
                mainControl.ProcessedGrayscaleImage = Circle.Apply(mainControl.OriginalGrayscaleImage, r);

                EllipseGeometry circle = new EllipseGeometry(Circle.CircleCenter, r, r);
                Path circlePath = new Path
                {
                    Data = circle,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2
                };

                mainControl.OriginalImageCanvas.Children.Clear();
                mainControl.OriginalImageCanvas.Children.Add(circlePath);

            }


        }

        private void Do_it_rectangle(object sender, RoutedEventArgs e)
        {
            mainControl.ProcessedGrayscaleImage = Rectang.Apply(mainControl.OriginalGrayscaleImage);

            DrawHelper.DrawAndGetLine(mainControl.OriginalImageCanvas, Rectang.startp, Rectang.endp, Brushes.Green, 4);
        }

        private void do_it_linear(object sender, RoutedEventArgs e)
        {
            UserInputDialog userInput = new UserInputDialog("Redimensionare x2 , x3 , x4 ?  ", new string[] { "Redimensionare x2 , x3 , x4 ?  :" });
            if (userInput.ShowDialog().Value == true)
            {
                double a = userInput.Values[0];
                mainControl.ProcessedGrayscaleImage = linear.Apply(mainControl.OriginalGrayscaleImage,a);
            }

        }

        private void do_it_Sobel_color(object sender, RoutedEventArgs e)
        {
            UserInputDialog userInput = new UserInputDialog("Threshold ? ", new string[] { "Threshold ?  :" });

            if (userInput.ShowDialog().Value == true)
            {
                int nr = (int)userInput.Values[0];
                mainControl.ProcessedGrayscaleImage = color_Sobel.Apply(mainControl.OriginalColorImage, nr);
            }
        }

        private void do_it_Edges2(object sender, RoutedEventArgs e)
        {
            UserInputDialog userInput = new UserInputDialog("Threshold ? ", new string[] { "Threshold ?  :" });

            if (userInput.ShowDialog().Value == true)
            {
                int nr = (int)userInput.Values[0];
                mainControl.ProcessedGrayscaleImage = color_Sobel_2.Apply(mainControl.OriginalColorImage, nr);
            }
        }

        private void do_it_similar_color(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalColorImage != null)
            {
                UserInputDialog userInput = new UserInputDialog("Sigma? ", new string[] { "Sigma ?  :" });

                if (userInput.ShowDialog().Value == true)
                {
                    tresh = (int)userInput.Values[0];
                    mainControl.OriginalImageCanvas.MouseUp += new MouseButtonEventHandler(OriginalImageCanvas_MouseUp_Similar_Color);
                }
            }
        }

        public void  OriginalImageCanvas_MouseUp_Similar_Color(Object sender, MouseButtonEventArgs e)
        { 
            Point click_location = Mouse.GetPosition(mainControl.OriginalImageCanvas);
        mainControl.ProcessedColorImage = similar_color.Apply(mainControl.OriginalColorImage, tresh, mainControl.OriginalColorImage[(int)click_location.Y,(int) click_location.X]);
       
            mainControl.OriginalImageCanvas.MouseUp -= OriginalImageCanvas_MouseUp_Similar_Color;

        }

        private void compute_gray_values(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                mainControl.OriginalImageCanvas.MouseDown += new MouseButtonEventHandler(OriginalImageCanvas_MouseDown_Texture);
            }
            
        }

        public void OriginalImageCanvas_MouseDown_Texture(Object sender, MouseButtonEventArgs e)
        {
            Point click_location = Mouse.GetPosition(mainControl.OriginalImageCanvas);
            //mainControl.ProcessedGrayscaleImage = compute_gray_value.Apply(mainControl.OriginalGrayscaleImage,  mainControl.OriginalGrayscaleImage[(int)click_location.Y], mainControl.OriginalGrayscaleImage[(int)click_location.X]);
            if (click_location.X > 64 && click_location.Y > 64 && click_location.Y < mainControl.OriginalGrayscaleImage.Height - 64 && click_location.X < mainControl.OriginalGrayscaleImage.Width - 64)
            {
                DrawHelper.DrawAndGetRectangle(mainControl.OriginalImageCanvas, new Point(click_location.X - 64, click_location.Y - 64), new Point(click_location.X + 64, click_location.Y + 64), Brushes.Chocolate, Colors.Aqua,100);


                double[] rez = compute_gray_value.Apply(mainControl.OriginalGrayscaleImage, (int)click_location.X, (int)click_location.Y);
                string aux = "";
                for (int i = 0; i < rez.Length; i++)
                {
                    aux += "M" + (i + 2).ToString() + ":" + rez[i].ToString() + Environment.NewLine;
                }
                MessageBox.Show(aux);
                mainControl.OriginalImageCanvas.Children.Clear();
                mainControl.OriginalImageCanvas.MouseDown -= OriginalImageCanvas_MouseDown_Texture;
            }
            else
            
                MessageBox.Show(" ROI nu incape in imagine. Alegeti alta locatie!");
            
        }

        private void compute_gray_values_1(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                mainControl.OriginalImageCanvas.MouseDown += new MouseButtonEventHandler(OriginalImageCanvas_MouseDown_Texture_1);
            }
        }
        public void OriginalImageCanvas_MouseDown_Texture_1(Object sender, MouseButtonEventArgs e)
        {
            Point click_location = Mouse.GetPosition(mainControl.OriginalImageCanvas);
            //mainControl.ProcessedGrayscaleImage = compute_gray_value.Apply(mainControl.OriginalGrayscaleImage,  mainControl.OriginalGrayscaleImage[(int)click_location.Y], mainControl.OriginalGrayscaleImage[(int)click_location.X]);
            if (click_location.X > 64 && click_location.Y > 64 && click_location.Y < mainControl.OriginalGrayscaleImage.Height - 64 && click_location.X < mainControl.OriginalGrayscaleImage.Width - 64)
            {
                DrawHelper.DrawAndGetRectangle(mainControl.OriginalImageCanvas, new Point(click_location.X - 64, click_location.Y - 64), new Point(click_location.X + 64, click_location.Y + 64), Brushes.Chocolate, Colors.Aqua, 100);


                double[] rez = runLength.Apply(mainControl.OriginalGrayscaleImage, (int)click_location.X, (int)click_location.Y);
                string aux = "";
                for (int i = 0; i < rez.Length; i++)
                {
                    aux += "RL" + (i + 1).ToString() + ": " + rez[i].ToString() + Environment.NewLine;
                }
                MessageBox.Show(aux);
                mainControl.OriginalImageCanvas.Children.Clear();
                mainControl.OriginalImageCanvas.MouseDown -= OriginalImageCanvas_MouseDown_Texture_1;
            }
            else
                if(click_location.X < 64 || click_location.Y <64 || click_location.Y > mainControl.OriginalGrayscaleImage.Height - 64 || click_location.X > mainControl.OriginalGrayscaleImage.Width - 64)

                MessageBox.Show(" ROI nu incape in imagine. Alegeti alta locatie!");
                        
        }
    }
}
