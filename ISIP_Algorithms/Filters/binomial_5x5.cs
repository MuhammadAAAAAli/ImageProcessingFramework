using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Filters
{
    public class binomial_5x5
    {
        public static Image<Gray, Byte> Apply(Image<Gray, Byte> InputImage)
        {
            Image<Gray, Byte> ResultImage = new Image<Gray, Byte>(InputImage.Size);

            int[] coef = { 1, 4, 6, 4, 1 };
            int[,] matrice = new int[10,10];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrice[j,i] = coef[i] * coef[j];
                }
            }

            for (int x = 0; x < InputImage.Width; x++)
            {
                for (int y = 0; y < InputImage.Height; y++)
                {
                    if (x < 2 || y < 2 || x > InputImage.Width - 3 || y > InputImage.Height - 3)
                        ResultImage.Data[y, x, 0] = InputImage.Data[y, x, 0];
                    else
                    {
                        int suma = 0;
                        int k = -2;
                        int h = -2;

                        while (h != 3)
                        {
                            suma += (int)(InputImage.Data[y + k, x + h, 0] * matrice[k + 2,h + 2]);
                            k++;
                            if (k == 3)
                            {
                                k = -2;
                                h++;
                            }
                        }
                        ResultImage.Data[y, x, 0] = (byte)(suma / 256);
                    }
                }
            }

            return ResultImage;
        }

    }

    public class binomial_7x7
    {
        public static Image<Gray, Byte> Apply(Image<Gray, Byte> InputImage)
        {
            Image<Gray, Byte> ResultImage = new Image<Gray, Byte>(InputImage.Size);

            int[] coef = { 1, 5, 10, 10, 5, 1 };
            int[,] matrice = new int[10, 10];

    //        int suma_mat = 0;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    matrice[j, i] = coef[i] * coef[j];
     //               suma_mat += matrice[j, i];
                }
            }

     //       int bla = suma_mat;



            for (int x = 0; x < InputImage.Width; x++)
            {
                for (int y = 0; y < InputImage.Height; y++)
                {
                    if (x < 3 || y < 3 || x > InputImage.Width - 4 || y > InputImage.Height - 4 )
                        ResultImage.Data[y, x, 0] = InputImage.Data[y, x, 0];
                    else
                    {
                        int suma = 0;
                        int k = -3;
                        int h = -3;

                        while (h != 4)
                        {
                            suma += (int)(InputImage.Data[y + k, x + h, 0] * matrice[k + 3, h + 3]);
                            k++;
                            if (k == 4)
                            {
                                k = -3;
                                h++;
                            }
                        }
                        ResultImage.Data[y, x, 0] = (byte)(suma / 1024);
                    }
                }
            }

            return ResultImage;
        }

    }
}
