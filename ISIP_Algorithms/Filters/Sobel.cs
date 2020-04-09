using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Filters
{
    public class Sobel
    {
        public static Image<Gray, Byte> Apply(Image<Gray, Byte> InputImage, int T)
        {

            Image<Gray, Byte> ResultImage = new Image<Gray, Byte>(InputImage.Size);

            for (int x = 0; x < InputImage.Width; x++)
            {
                for (int y = 0; y < InputImage.Height; y++)
                {
                    if (x < 1 || y < 1 || x > InputImage.Width - 2 || y > InputImage.Height - 2)
                        ResultImage.Data[y, x, 0] = 0;
                    else
                    {
                        int g_x = 0;
                        int g_y = 0;

                        g_y = InputImage.Data[y - 1, x - 1, 0] * (-1) + InputImage.Data[y, x - 1, 0] * (-2) + InputImage.Data[y + 1, x - 1, 0] * (-1) + InputImage.Data[y - 1, x + 1, 0] * 1 + InputImage.Data[y, x + 1, 0] * 2 + InputImage.Data[y + 1, x + 1, 0] * 1;
                        g_x = InputImage.Data[y - 1, x - 1, 0] * (-1) + InputImage.Data[y - 1, x, 0] * (-2) + InputImage.Data[y - 1, x + 1, 0] * (-1) + InputImage.Data[y + 1, x - 1, 0] * 1 + InputImage.Data[y + 1, x, 0] * 2 + InputImage.Data[y + 1, x + 1, 0] * 1;

                        if ((int)Math.Sqrt(g_x * g_x + g_y * g_y) < T)
                            ResultImage.Data[y, x, 0] = 0;
                        else
                            ResultImage.Data[y, x, 0] = 255;

                    }

                }
                
            }

            return ResultImage;
        }

    }
}
