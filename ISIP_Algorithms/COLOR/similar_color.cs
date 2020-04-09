using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.COLOR
{
    public class similar_color
    {
        public static Image<Bgr, byte> Apply(Image<Bgr, byte> InputImage, int delta, Bgr pixel)
        {
            Image<Bgr, byte> ResultImage = InputImage;
            for (int i = 0; i < InputImage.Width; i++)
            {
                for (int j = 0; j < InputImage.Height; j++)
                {
                    int B = (int)InputImage.Data[j, i, 0] - (int)pixel.Blue;
                    int G = (int)InputImage.Data[j, i, 1] - (int)pixel.Green;
                    int R = (int)InputImage.Data[j, i, 2] - (int)pixel.Red;
                    int grad = 0;
                    grad =(int) Math.Sqrt(Math.Pow(B, 2) + Math.Pow(G, 2) + Math.Pow(R, 2));
                    if (grad < delta)
                    {
                        ResultImage.Data[j, i, 0] = 255;
                        ResultImage.Data[j, i, 1] = 255;
                        ResultImage.Data[j, i, 2] = 255;
                    }
                }
                
            }
            return ResultImage;
        } 
        
    }
}
