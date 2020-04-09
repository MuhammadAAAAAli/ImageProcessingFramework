using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows;



namespace ISIP_Algorithms.Hough
{
    public class Rectang
    {
        public static Point startp = new Point(), endp = new Point();

        public static Image<Gray, Byte> Apply(Image<Gray, Byte> InputImage)
        {
            InputImage = Filters.Sobel.Apply(InputImage, 150);

            int r_max = (int)(Math.Sqrt(InputImage.Height * InputImage.Height + InputImage.Width * InputImage.Width));

            Image<Gray, int> Hough = new Image<Gray, int>(360,2*r_max);

            int max1 = 0;
                
             double r_m = 0, alpha_m = 0;
                 
            for (int x = 0; x < InputImage.Width; x++)
            {
                for (int y = 0; y < InputImage.Height; y++)
                {
                    if ( InputImage.Data[y,x,0] == 255 )
                        for (int alpha = 0; alpha < 360; alpha++)
                        {
                            int r = (int)(Math.Cos(alpha * Math.PI / 180.0) * x + Math.Sin(alpha * Math.PI / 180.0) * y + 0.5);
                            r += r_max;
                            Hough.Data[r , alpha, 0] ++;
                        }                   
                }
            }

            

            for (int r = 0; r < 2*r_max; r++)
            {
                for (int alpha = 0; alpha < 360; alpha++)
                {
                    if ((int)Hough.Data[r, alpha, 0] > max1)
                    {
                        max1 = (int)Hough.Data[r, alpha, 0];
                        r_m = r;
                        alpha_m = alpha;
                    }
                }
            }

            r_m -= r_max;
            bool vertical = false;
            if (alpha_m == 90.0 || alpha_m == 270.0) vertical = true;
            alpha_m *= Math.PI / 180.0;

           

            if (vertical)
            {
                startp.X = endp.X = (int)r_m;
                startp.Y = 50;
                endp.Y = InputImage.Height - 50;
            }

            else
            {
                startp.X = 50;
                endp.X = InputImage.Width - 50;
                double mmax = Math.Tan(alpha_m);
                if (mmax == 0.0) startp.Y = endp.Y = (int)r_m / Math.Cos(alpha_m);
                else
                {
                    startp.Y = (int)(-1.0/mmax * startp.X + r_m / Math.Sin(alpha_m));
                    endp.Y = (int)(-1.0/mmax * endp.X + r_m / Math.Sin(alpha_m));
                }
            }

            return Hough.Convert<Gray, Byte>();
        }
    }
}
