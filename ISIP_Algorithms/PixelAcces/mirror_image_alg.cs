using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.PixelAcces
{
    public class mirror_image_alg
    {
        public static Image<Gray, byte> InverByYAxes(Image<Gray, byte> InputImage)
        {
            Image<Gray, byte> resultImage = new Image<Gray, byte>(InputImage.Size);

            for (int x = 0; x < InputImage.Width; x++)
            {
                for (int y = 0; y < InputImage.Height; y++)
                {
                    resultImage.Data[y, InputImage.Width - x - 1, 0] = InputImage.Data[y, x, 0];
                }
            }
            return resultImage;
        }

        public static Image<Gray, byte> InvertByXAxes(Image<Gray, byte> InputImage)
        {
            Image<Gray, byte> resultImage2 = new Image<Gray, byte>(InputImage.Size);

            for (int x = 0; x < InputImage.Width; x++)
            {
                for (int y = 0; y < InputImage.Height; y++)
                {
                    resultImage2.Data[InputImage.Height - y - 1, x, 0] = InputImage.Data[y, x, 0];
                }
            }
            return resultImage2;
        }
    }
}
