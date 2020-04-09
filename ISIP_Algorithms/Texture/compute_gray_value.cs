using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Texture
{
    public class compute_gray_value
    {
        public static double [] Apply(Image<Gray, Byte> InputImage, int xx, int yy)
        {   //xx, yy coordonate
            double[] f = new double[256];
            double[] p = new double[256];
            for (int t = 0; t < 256; t++)
            {
                f[t] = 0;
                p[t] = 0;
            }

            for (int i = xx-64; i <= xx+64; i++)
            {
                for (int j = yy-64; j <= yy+64; j++)
                {
                    f[(int)InputImage.Data[j, i, 0]]++;

                }
            }
            for (int i = 0; i < 256; i++)
            {
                p[i] = f[i] / (128 * 128);
            }
            double M1 = 0;
            for (int i = 0; i < 256; i++)
            {
                M1 += i * p[i];
            }
            double M2= 0;
            for (int i = 0; i < 256; i++)
			{
			  M2 += (M1-i)*(M1-i)*p[i];
			}
            double M3 = 0;
            for (int i = 0; i < 256; i++)
            {
                M3 += (M1 - i) * (M1 - i) *(M1 - i)* p[i];
            } double M4= 0;
            for (int i = 0; i < 256; i++)
            {
                M4 += (M1 - i) * (M1 - i) * (M1 - i) * (M1 - i) * p[i];
            } double M5 = 0;
            for (int i = 0; i < 256; i++)
            {
                M5 += p[i] * p[i];
            }
            return new double[4] { M2, M3, M4, M5 };
        }
       
    }
}
