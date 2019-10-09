using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ich.ShaderProcess.Processors.Common
{
    public static class ImageProcessor
    {
        public static RGBHistogramm Histogramm(Color32[] colors)
        {
            RGBHistogramm histogramm = new RGBHistogramm();

            foreach (var c in colors)
            {
                histogramm.AddColor(c);
            }

            return histogramm;
        }

        public static int Color32ToUint(this Color32 color32)
        {
            return (color32.r << 16) + (color32.g << 8) + color32.b;
        }
    }
}
