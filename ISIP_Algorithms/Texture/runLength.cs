using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Texture
{
    public class runLength
    {
        public static double[] Apply(Image<Gray, Byte> InputImage, int xx, int yy)
        {
            double RL1 = 0, RL2 = 0, RL3 = 0, RL4 = 0, RL5 = 0, P = 0 ;

            int[,] matrix = new int[33, 33];
            for (int i = 1; i <=32; i++)
            {
                for (int j = 1; j <=32; j++)
                {
                    matrix[i, j] = 0;
                }
                
            }
            int k=1, q=1;
            for (int i = xx - 64; i <= xx + 64; i++)
            {
                for (int j = yy - 64; j <= yy + 64;j= j+q)
                {
                    q = 1;
                    if ((int)((InputImage.Data[j, i, 0]/8)+1) == (int)((InputImage.Data[j + 1, i, 0]/8)+1) && k!=32)
                        k++;
                    else
                    {
                        q = k;
                        matrix[(int)(InputImage.Data[j,i,0]/8)+1, k]++;
                        k = 1;
                        P++;
                    }
                }
            }
            int suma_matrice = 0;
            for (int i = 1; i <= 32; i++)
            {
                for (int j = 1; j <=32 ; j++)
                {
                    suma_matrice += matrix[i, j]/(j*j);
                }
                
            }
            RL1 = suma_matrice / P;
                int suma_matrice1 = 0;
            for (int i = 1; i <= 32; i++)
            {
                for (int j = 1; j <=32 ; j++)
                {
                    suma_matrice1 += matrix[i, j]*(j*j);
                }
                
            }
            RL2 = suma_matrice1 / P;


            int suma_matrice2 = 0;
            for (int i = 1; i <=32; i++)
            {
                int suma_linie = 0;
                for (int j = 1; j <= 32; j++)
                {
                    suma_linie += matrix[i, j];

                }
                suma_matrice2 += suma_linie * suma_linie;
            }
            RL3 = suma_matrice2 / P;


            int suma_matrice3 = 0;
            for (int j = 1; j <= 32; j++)
            {
                int suma_linie = 0;
                for (int i = 1; i <= 32; i++)
                {
                    suma_linie += matrix[i, j];

                }
                suma_matrice3 += suma_linie * suma_linie;
            }
            RL4 = suma_matrice3 / P;
            RL5 = P/ (128 * 128);




            return new double [5]{RL1, RL2, RL3, RL4, RL5};
        }
    }
}
