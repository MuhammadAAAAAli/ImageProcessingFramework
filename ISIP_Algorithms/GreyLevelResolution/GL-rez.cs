using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

using ISIP_UserControlLibrary;

namespace ISIP_Algorithms.GreyLevelResolution
{
    public class GL_rez
    {
        public static Image<Gray, byte> Apply(Image<Gray, byte> inputImage, int grey_lvl)
        {
            Image<Gray,byte> resultImage = new Image<Gray, byte>(inputImage.Size);

            int nr_gray = (int)Math.Pow(2 , grey_lvl) ;

    //        resultImage.Data[y, x, 0] = inputImage.Data[y, x, 0];
     //       resultImage.Data.grey

            int[] gray_shade = new int[256];
   
            for(int i=1;i<=nr_gray;i++)
		         gray_shade[nr_gray-i] = 256/nr_gray *(nr_gray-i) + 256/nr_gray/2;
	  


            for (int x = 0; x < inputImage.Width; x++)
            {
                for (int y = 0; y < inputImage.Height; y++)
                {
                    resultImage.Data[y, x, 0] = (byte)gray_shade[(int) ( inputImage.Data[y, x, 0] / ( 256 / nr_gray ) )];
                }
            }


            return resultImage;
        }
    }
}
