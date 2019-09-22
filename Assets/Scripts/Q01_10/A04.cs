using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class A04 : MonoBehaviour
{
    /// <summary>
    /// 画像のテクスチャ
    /// </summary>
    private Texture2D m_Texture = null;

    /// <summary>
    /// 出力先RawImage
    /// </summary>
    [SerializeField]
    private RawImage m_RawImage = null;

    private void Awake()
    {
        if (m_RawImage == null)
        {
            m_RawImage = GameObject.Find("Canvas/MainPanel/RawImage").GetComponent<RawImage>();
        }
    }

    /// <summary>
    /// 大津の二値化
    /// </summary>
    public void OotsuBinarization()
    {
        if (transform.root.GetComponent<FileManager>().GetTexture() == null)
            return;

        // Pixel情報取得
        m_Texture = transform.root.GetComponent<FileManager>().GetTexture();
        int width = m_Texture.width;
        int height = m_Texture.height;
        Color[] pixels = m_Texture.GetPixels();

        // 書き換え用テクスチャの作成
        Color[] change_pixels = new Color[pixels.Length];

        List<float> listY = new List<float>();
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                int pxIndex = w + width * h;
                listY.Add(0.2126f * pixels[pxIndex].r + 0.7152f * pixels[pxIndex].g + 0.0722f * pixels[pxIndex].b);
            }
        }

        float maxThreshold = 0.0f;
        float max = 0.0f;

        for (float threshold = 0.0f; threshold < 1.0f; threshold += 0.01f)
        {
            List<float> listUpperY = new List<float>();
            List<float> listDownerY = new List<float>();

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    int pxIndex = w + width * h;

                    if (listY[pxIndex] > threshold)
                    {
                        listUpperY.Add(listY[pxIndex]);
                    }
                    else
                    {
                        listDownerY.Add(listY[pxIndex]);
                    }
                }
            }

            if (listUpperY.Count() == 0 || listDownerY.Count() == 0)
                continue;

            //ω1 * ω2 (m1 - m2)^2
            float candidateThreshold = listUpperY.Count() * listDownerY.Count() * Mathf.Pow((listUpperY.Sum() / listUpperY.Count - listDownerY.Sum() / listDownerY.Count), 2);

            if (candidateThreshold > max)
            {
                max = candidateThreshold;
                maxThreshold = threshold;
            }
        }

        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                int pxIndex = w + width * h;

                // 2値化
                float y = 0.2126f * pixels[pxIndex].r + 0.7152f * pixels[pxIndex].g + 0.0722f * pixels[pxIndex].b;

                Color change_pixel = y < maxThreshold ?
                    new Color(0.0f, 0.0f, 0.0f, pixels[pxIndex].a) :
                    new Color(1.0f, 1.0f, 1.0f, pixels[pxIndex].a);

                change_pixels.SetValue(change_pixel, pxIndex);
            }
        }

        // 書き換え用テクスチャの生成
        Texture2D change_texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        change_texture.filterMode = FilterMode.Point;
        change_texture.SetPixels(change_pixels);
        change_texture.Apply();

        // テクスチャを貼り替える
        m_Texture = change_texture;
        var rect = new Rect(0.0f, 0.0f, m_Texture.width, m_Texture.height);
        var pivot = new Vector2(0.5f, 0.5f);
        var sprite = Sprite.Create(m_Texture, rect, pivot);
        m_RawImage.texture = sprite.texture;
        transform.root.GetComponent<FileManager>().SetTexture(m_Texture);
    }
}