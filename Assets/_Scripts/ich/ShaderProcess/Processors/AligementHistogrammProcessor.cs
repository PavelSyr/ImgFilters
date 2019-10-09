using ich.ShaderProcess.Processors.Common;
using ich.Utils.UnityThread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    public class AligementHistogrammProcessor : BaseProcessor
    {
        public AligementHistogrammProcessor()
        {
        }

        public AligementHistogrammProcessor(Material material) : base(material)
        {
        }

        public override void SetupMaterial()
        {
            if (SrcTexture == null) return;

            Texture2D tex = (Texture2D)SrcTexture;

            if (tex != null)
            {
                var pixels = tex.GetPixels32();

                ThreadWrapper.Create(
                    () =>
                    {
                        var histogramm = new RGBHistogramm();
                        histogramm.AddColors(pixels);
                        histogramm.CalculateCDFValues();
                        var count = pixels.Length;

                        for (int i = 0; i < count; i++)
                        {
                            pixels[i] = histogramm.AligementedColor2RGB(pixels[i], count);
                        }

                        return pixels;
                    },
                    (processedPixeles) =>
                    {
                        tex.SetPixels32((Color32[])processedPixeles);
                        tex.Apply();
                    },
                    (error) =>
                    {
                        Debug.Log("Error : " + error);
                    }
                );
            }
        }
    }
}
