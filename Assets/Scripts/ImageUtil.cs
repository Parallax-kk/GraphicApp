using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUtil : MonoBehaviour
{
    /// <summary>
    /// 画像テクスチャ
    /// </summary>
    public static Texture2D m_Texture = null;

    /// <summary>
    /// 画像をグレースケール化
    /// </summary>
    /// <param name="pixels"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static List<float> GetGrayPixels(Color[] pixels, int width, int height)
    {
        List<float> listGrayPixels = new List<float>();
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                int pxIndex = w + width * h;

                float y = 0.299f * pixels[pxIndex].r + 0.587f * pixels[pxIndex].g + 0.114f * pixels[pxIndex].b;
                listGrayPixels.Add(y);
            }
        }

        return listGrayPixels;
    }

    /// <summary>
    /// 画像変更
    /// </summary>
    /// <param name="pixels"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static void ChangeImage(Color[] pixels, int width, int height)
    {
        // 書き換え用テクスチャの生成
        Texture2D change_texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        change_texture.filterMode = FilterMode.Point;
        change_texture.SetPixels(pixels);
        change_texture.Apply();

        // テクスチャを貼り替える
        m_Texture = change_texture;
        var rect = new Rect(0.0f, 0.0f, m_Texture.width, m_Texture.height);
        var pivot = new Vector2(0.5f, 0.5f);
        var sprite = Sprite.Create(m_Texture, rect, pivot);
        GameObject.Find("Canvas/MainPanel/RawImage").GetComponent<RawImage>().texture = sprite.texture;
    }
}

public class ImageDate
{
    /// <summary>
    /// ピクセルデータ
    /// </summary>
    public Color[] m_Pixels = null;

    /// <summary>
    /// 横
    /// </summary>
    public int m_Width = 0;

    /// <summary>
    /// 縦
    /// </summary>
    public int m_Height = 0;

    public ImageDate(Color[] pixels, int width, int height)
    {
        m_Pixels = pixels;
        m_Width  = width;
        m_Height = height;
    }
}
