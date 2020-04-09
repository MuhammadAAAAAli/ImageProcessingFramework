using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Interpolation
{
    public class linear
    {
        public static Image<Gray, Byte> Apply(Image<Gray, Byte> InputImage, double a)
        {
            Image<Gray, Byte> ResultImage = new Image<Gray, Byte>(InputImage.Width, InputImage.Height);

            double centerX = (InputImage.Width - 1) / 2.0;
            double centerY = (InputImage.Height - 1) / 2.0;

            for (int x = 0; x < ResultImage.Width; x++)
            {
                for (int y = 0; y < ResultImage.Height; y++)
                {
                    double xx, yy;

                    xx = (x - centerX) / a + centerX + 0.5 ;
                    yy = (y - centerY) / a + centerY + 0.5 ;

                    double xd = xx - (int)xx;
                    double yd = yy - (int)yy;
                    if (xx > 0 && (xx) < InputImage.Width-1 && yy > 0 && (yy) < InputImage.Height - 1)
                    {
                        ResultImage.Data[y, x, 0] = (byte)((1 - xd) * ( 1 - yd )* (int)InputImage.Data[(int)(yy), (int)(xx), 0] + xd * (1 - yd) * (int)InputImage.Data[(int)(yy), (int)(xx+1), 0] + ( 1- xd) * yd * (int)InputImage.Data[(int)(yy+1), (int)(xx), 0] + xd * yd * (int)InputImage.Data[(int)(yy+1), (int)(xx+1), 0]);
                    }
                    
                }

            }

            return ResultImage;
        }
    }
}
