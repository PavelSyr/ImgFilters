using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ich.ShaderProcess.Processors.Common
{
    public class RGBHistogramm
    {
        public Histogramm R;
        public Histogramm G;
        public Histogramm B;
        public Histogramm Gray;

        public RGBHistogramm()
        {
            R = new Histogramm();
            G = new Histogramm();
            B = new Histogramm();
            Gray = new Histogramm();
        }

        public void CalculateCDFValues()
        {
            R.CalculateCDFvalues();
            G.CalculateCDFvalues();
            B.CalculateCDFvalues();
            Gray.CalculateCDFvalues();
        }

        public Color32 AligementedColor2RGB(Color32 color, int count)
        {
            var red =   R.AligementedValue(color.r, count);
            var green = G.AligementedValue(color.g, count);
            var blue =  B.AligementedValue(color.b, count);

            return new Color32(
                r: red,
                g: green,
                b: blue,
                a: 1);
        }

        public Color AligementedColor2Gray(Color32 color, int count)
        {
            byte gray = ColorUtils.RGB2GrayByte(color.r, color.g, color.b);

            gray = (byte)(Gray.AligementedValue2(gray, count) * 255);

            return new Color32(
                r: gray,
                g: gray,
                b: gray,
                a: 1);
        }

        public void AddColors(Color32[] colors)
        {
            for(int i =0; i < colors.Length; i++)
            {
                AddColor(colors[i]);
            }
        }

        public void AddColor(Color32 c)
        {
            R[c.r]++;
            G[c.g]++;
            B[c.b]++;

            var gray = ColorUtils.RGB2GrayByte(c.r, c.g, c.b);
            Gray[gray]++;
        }
    }

    public class Histogramm
    {
        private Dictionary<int, int> m_ColorsCount;
        private Dictionary<int, int> m_cdf;
        private int m_MinCdf;

        public Histogramm()
        {
            m_ColorsCount = new Dictionary<int, int>();
            m_cdf = new Dictionary<int, int>();
            Empty();
        }

        public byte AligementedValue(int index, int count)
        {
            return (byte)Mathf.RoundToInt(((float)(m_cdf[index] - m_MinCdf) / (count - 1)) * 255);
        }

        public float AligementedValue2(int index, int count)
        {
            return (float)(m_cdf[index] - m_MinCdf) / (count - 1);
        }

        public void Empty()
        {
            for (int i = 0; i < 256; i++)
            {
                m_ColorsCount[i] = 0;
                m_cdf[i] = 0;
            }
            m_MinCdf = 1;
        }

        public int MinCdf
        {
            get { return m_MinCdf; }
        }

        /// <summary>
        /// CDF is Cumulative Distribution Function
        /// </summary>
        public void CalculateCDFvalues()
        {
            m_cdf[0] = m_ColorsCount[0];

            int min_cdf = m_cdf[0] == 0? int.MaxValue : m_cdf[0];

            for (int i = 1; i < 256; i++)
            {
                var cdf = m_cdf[i - 1] + m_ColorsCount[i];
                m_cdf[i] = cdf;
                if (cdf != 0 && cdf < min_cdf)
                {
                    min_cdf = cdf;
                }
            }
        }

        public int this[int key]
        {
            get
            {
                return m_ColorsCount[key];
            }
            set
            {
                m_ColorsCount[key] = value;
            }
        }
    }
}
