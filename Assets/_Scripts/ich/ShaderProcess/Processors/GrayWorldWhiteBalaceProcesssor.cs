using ich.Utils.UnityThread;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    [System.Serializable]
    public class GrayWorldWhiteBalaceProcesssor : BaseProcessor
    {
        [SerializeField]
        public Color AvgColor = new Color(0.5f, 0.5f, 0.5f, 1);
        [SerializeField]
        public bool ManualMode = false;

        public event Action<Color> OnChanged;

        public GrayWorldWhiteBalaceProcesssor()
        {
        }

        public GrayWorldWhiteBalaceProcesssor(Material material) : base(material)
        {
        }

        public override void SetupMaterial()
        {
            if (ManualMode)
            {
                SetMaterailVars();
            }
            else
            {
                Calculation();
            }
        }

        private void Calculation()
        {
            CalcAverageColor(
                (result) => {
                    AvgColor = (Color)result;

                    SetMaterailVars();

                    if (OnChanged != null) OnChanged(AvgColor);
                }
            );
        }

        private void SetMaterailVars()
        {
            m_Material.SetFloat("_Rw", AvgColor.r);
            m_Material.SetFloat("_Gw", AvgColor.g);
            m_Material.SetFloat("_Bw", AvgColor.b);
        }

        private void CalcAverageColor(Action<object> OnComplete)
        {
            if (SrcTexture == null) return;

            Texture2D tex = (Texture2D)SrcTexture;

            if (tex != null)
            {
                var pixels = tex.GetPixels();

                ThreadWrapper.Create(
                    () =>
                    {
                        float r_avg = 0;
                        float g_avg = 0;
                        float b_avg = 0;
                        foreach (var c in pixels)
                        {
                            r_avg += c.r;
                            g_avg += c.g;
                            b_avg += c.b;
                        }

                        r_avg /= pixels.Length;
                        g_avg /= pixels.Length;
                        b_avg /= pixels.Length;

                        float rgb_avg = (r_avg + g_avg + b_avg) / 3f;

                        return new Color( r: r_avg / rgb_avg,
                                          g: g_avg / rgb_avg,
                                          b: b_avg / rgb_avg );
                    },
                    OnComplete,
                    (error) =>
                    {
                        Debug.Log("Error : " + error);
                    }
                );

            }
        }
    }
}