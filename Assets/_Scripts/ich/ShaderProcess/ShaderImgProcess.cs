using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShaderImgProcess {

    public static RenderTexture Canny(Texture src, Material canny, float alphaBlend = 0.5f)
    {
        canny.SetFloat("_Alpha", alphaBlend);
        return Blit(src, canny);
    }

    public static RenderTexture Contrast(Texture src, Material contrast, float value, float level)
    {
        contrast.SetFloat("_Contrast", value);
        contrast.SetFloat("_Level", level);
        return Blit(src, contrast);
    }

    public static RenderTexture Grayscale(Texture src, Material grayscale)
    {
        return Blit(src, grayscale);
    }

    public static RenderTexture Blur(Texture src, Material blurX, Material blurY)
    {
        Texture rTex = Blit(src, blurX);
        return Blit(rTex, blurY);
    }

    public static RenderTexture Blit(Texture src, Material mat)
    {
        RenderTexture rTex = new RenderTexture(src.width, src.height, 0);
        Graphics.Blit(src, rTex, mat);
        return rTex;

    }
}
