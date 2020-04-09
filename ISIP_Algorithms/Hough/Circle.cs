using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Hough
{
    public class Circle
    {

        public static System.Windows.Point CircleCenter { get; set; }

        public static Image<Gray, Byte> Apply(Image<Gray, Byte> InputImage, int r)
        {

            Image<Gray, Byte> Hough = new Image<Gray, Byte>(InputImage.Size);
            Image<Gray, Byte> SobelImage = Filters.Sobel.Apply(InputImage, 100);      

            int max = 0, max_x = 0, max_y = 0;
            for (int x = 0; x < SobelImage.Width; x++)
            {
                for (int y = 0; y < SobelImage.Height; y++)
                {

                    if (SobelImage.Data[y, x, 0] >= 255)
                    {
                        for (int unghi = 0; unghi < 360; unghi++)
                        {
                            int xx = (int)(r * Math.Cos(unghi) + x);
                            int yy = (int)(r * Math.Sin(unghi) + y);

                            if (xx > 0 && yy > 0 && xx < SobelImage.Width && yy < SobelImage.Height)
                            {
                                Hough.Data[yy, xx, 0] = (byte)((int)Hough.Data[yy, xx, 0] + 1);

                                if (max < (int)Hough.Data[yy, xx, 0])
                                {
                                    max = (int)Hough.Data[yy, xx, 0];
                                    max_x = xx;
                                    max_y = yy;
                         
                                }
                            }
                        }

                        }

                    }
                }
                    
            int q = 0;
            int g = 0;
            int ok = 0;
            int medie_x = 0, medie_y=0, nr_x = 0, nr_y =0;

            while(ok==0)
                {
                    ok=1;
                    q++;
                    g++;

                    if( (int)Hough.Data[max_y-q, max_x-g, 0] > ((int)Hough.Data[max_y, max_x, 0] -30 ) )
                    {
                        medie_x += max_x-q;
                        medie_y += max_y-g;
                        nr_x++;
                        nr_y++;
                        ok=1;
                    }
                    if( (int)Hough.Data[max_y+q, max_x+g, 0] > ((int)Hough.Data[max_y, max_x, 0] -30 ) )
                    {
                         medie_x += max_x+q;
                         medie_y += max_y+g;
                        nr_x++;
                        nr_y++;
                         ok=1;
                    }
                if( (int)Hough.Data[max_y-q, max_x+g, 0] > ((int)Hough.Data[max_y, max_x, 0] -30 ) )
                    {
                        medie_x += max_x-q;
                        nr_x++;
                        nr_y++;
                         medie_y += max_y+g;
                            ok=1;
                    }
                if( (int)Hough.Data[max_y+q, max_x-g, 0] > ((int)Hough.Data[max_y, max_x, 0] -30 ) )
                    {
                        medie_x += max_x+q;
                         medie_y += max_y-g;
                        nr_x++;
                        nr_y++;
                        ok=1;
                    }
               }



       //     Image<Gray, Byte> ResultImage = new Image<Gray, Byte>(InputImage.Size);

            InputImage.Data[medie_y / nr_y, medie_x / nr_x, 0] = 255;

            CircleCenter = new System.Windows.Point(medie_x / nr_x, medie_y / nr_y);

            return Hough;
        }

    }
}
