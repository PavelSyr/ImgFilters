using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ich.ShaderProcess.Processors.Common
{
    public static class ColorUtils
    {
        public static byte RGB2GrayByte(byte r, byte g, byte b)
        {
            return (byte)(0.2126f * r + 0.7152f * g + 0.0722f * b);
        }

        public static float RGB2GrayFloat(float r, float g, float b)
        {
            return (0.2126f * r + 0.7152f * g + 0.0722f * b);
        }
    }
}
