using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;


namespace ISIP_Algorithms.Filters
{
    public class color_Sobel
    {
        public static Image<Gray, byte> Apply(Image<Bgr, byte> InputImage, int T)
        {
            Image<Gray, Byte> ResultImage = new Image<Gray, Byte>(InputImage.Size);

            for (int x = 0; x < InputImage.Width; x++)
            {
                for (int y = 0; y < InputImage.Height; y++)
                {
                    
                        int g_x_0 = 0;
                        int g_y_0 = 0;

                        int g_x_1 = 0;
                        int g_y_1 = 0;

                        int g_x_2= 0;
                        int g_y_2= 0;

                        if (x < 1 || y < 1 || x > InputImage.Width - 2 || y > InputImage.Height - 2)
                            ResultImage.Data[y, x, 0] = 0;
                        else
                        {
                            g_y_0 = InputImage.Data[y - 1, x - 1, 0] * (-1) + InputImage.Data[y, x - 1, 0] * (-2) + InputImage.Data[y + 1, x - 1, 0] * (-1) + InputImage.Data[y - 1, x + 1, 0] * 1 + InputImage.Data[y, x + 1, 0] * 2 + InputImage.Data[y + 1, x + 1, 0] * 1;
                            g_x_0 = InputImage.Data[y - 1, x - 1, 0] * (-1) + InputImage.Data[y - 1, x, 0] * (-2) + InputImage.Data[y - 1, x + 1, 0] * (-1) + InputImage.Data[y + 1, x - 1, 0] * 1 + InputImage.Data[y + 1, x, 0] * 2 + InputImage.Data[y + 1, x + 1, 0] * 1;

                            g_y_1 = InputImage.Data[y - 1, x - 1, 1] * (-1) + InputImage.Data[y, x - 1, 1] * (-2) + InputImage.Data[y + 1, x - 1, 1] * (-1) + InputImage.Data[y - 1, x + 1, 1] * 1 + InputImage.Data[y, x + 1, 1] * 2 + InputImage.Data[y + 1, x + 1, 1] * 1;
                            g_x_1 = InputImage.Data[y - 1, x - 1, 1] * (-1) + InputImage.Data[y - 1, x, 1] * (-2) + InputImage.Data[y - 1, x + 1, 1] * (-1) + InputImage.Data[y + 1, x - 1, 1] * 1 + InputImage.Data[y + 1, x, 1] * 2 + InputImage.Data[y + 1, x + 1, 1] * 1;

                            g_y_2 = InputImage.Data[y - 1, x - 1, 2] * (-1) + InputImage.Data[y, x - 1, 2] * (-2) + InputImage.Data[y + 1, x - 1, 2] * (-1) + InputImage.Data[y - 1, x + 1, 2] * 1 + InputImage.Data[y, x + 1, 2] * 2 + InputImage.Data[y + 1, x + 1, 2] * 1;
                            g_x_2 = InputImage.Data[y - 1, x - 1, 2] * (-1) + InputImage.Data[y - 1, x, 2] * (-2) + InputImage.Data[y - 1, x + 1, 2] * (-1) + InputImage.Data[y + 1, x - 1, 2] * 1 + InputImage.Data[y + 1, x, 2] * 2 + InputImage.Data[y + 1, x + 1, 2] * 1;
                            if ((((int)Math.Sqrt(g_x_0 * g_x_0 + g_y_0 * g_y_0) >= T || (int)Math.Sqrt(g_x_1 * g_x_1 + g_y_1 * g_y_1) >= T || (int)Math.Sqrt(g_x_2 * g_x_2 + g_y_2 * g_y_2) >= T)))
                                ResultImage.Data[y, x, 0] = 255;
                            else
                                ResultImage.Data[y, x, 0] = 0;

                        }

                }

            }

            return ResultImage;
        }



    }
}
