using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class A14Vertical : MonoBehaviour
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
    /// 微分フィルタ
    /// </summary>
    public void DifferentialFilterVertical()
    {
        if (transform.root.GetComponent<FileManager>().m_Texture == null)
            return;

        // Pixel情報取得
        m_Texture = transform.root.GetComponent<FileManager>().m_Texture;
        int width = m_Texture.width;
        int height = m_Texture.height;
        Color[] pixels = m_Texture.GetPixels();

        // グレースケール化
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

        // 書き換え用テクスチャの作成
        Color[] change_pixels = new Color[pixels.Length];

        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                int pxIndex = w + width * h;

                float y = 0.0f;

                // 縦方向
                //      0 -1 0
                // K = [0  1 0]
                //      0  0 0

                // 上端
                if (h == 0)
                {
                    y = listGrayPixels[pxIndex];
                }
                else
                {
                    y = (-1.0f * listGrayPixels[pxIndex - width] + listGrayPixels[pxIndex]);
                }

                change_pixels[pxIndex] = new Color(y,y,y);
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
        transform.root.GetComponent<FileManager>().m_Texture = m_Texture;
    }
}