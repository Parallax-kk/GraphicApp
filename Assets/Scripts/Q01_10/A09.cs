﻿using UnityEngine;
using UnityEngine.UI;

public class A09 : MonoBehaviour
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
    /// `ガウシアンフィルタ
    /// </summary>
    public void GaussianBlur()
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

        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++) 
            {
                int pxIndex = w + width * h;

                Color color = new Color();

                // 上端
                if (h == 0)
                {
                    // 左端
                    if (w == 0)
                    {
                        color  = pixels[pxIndex        ] * 4 + pixels[pxIndex         + 1] * 2
                               + pixels[pxIndex + width] * 2 + pixels[pxIndex + width + 1] * 1;
                    }
                    // 右端
                    else if (w == width - 1)
                    {
                        color  = pixels[pxIndex - 1        ] * 2 + pixels[pxIndex        ] * 4
                               + pixels[pxIndex + width - 1] * 1 + pixels[pxIndex + width] * 2;
                    }
                    else
                    {
                        color  = pixels[pxIndex - 1        ] * 2 + pixels[pxIndex        ] * 4 + pixels[pxIndex         + 1] * 2
                               + pixels[pxIndex + width - 1] * 1 + pixels[pxIndex + width] * 2 + pixels[pxIndex + width + 1] * 1;
                    }
                }
                // 下端
                else if (h == height - 1)
                {
                    // 左端
                    if (w == 0)
                    {
                         color  = pixels[pxIndex - width] * 2 + pixels[pxIndex - width + 1] * 1
                                + pixels[pxIndex        ] * 4 + pixels[pxIndex         + 1] * 2;
                    }
                    // 右端
                    else if (w == width - 1)
                    {
                        color  = pixels[pxIndex - width - 1] * 1 + pixels[pxIndex - width] * 2
                               + pixels[pxIndex - 1        ] * 2 + pixels[pxIndex        ] * 4;
                    }
                    else
                    {
                        color  = pixels[pxIndex - width - 1] * 1 + pixels[pxIndex - width] * 2 + pixels[pxIndex - width + 1] * 1
                               + pixels[pxIndex - 1        ] * 2 + pixels[pxIndex        ] * 4 + pixels[pxIndex         + 1] * 2;
                    }
                }
                else
                {
                    // 左端
                    if (w == 0)
                    {
                        color  = pixels[pxIndex - width] * 2 + pixels[pxIndex - width + 1] * 1
                               + pixels[pxIndex        ] * 4 + pixels[pxIndex         + 1] * 2
                               + pixels[pxIndex + width] * 2 + pixels[pxIndex + width + 1] * 1;
                    }
                    // 右端
                    else if (w == width - 1)
                    {
                        color  = pixels[pxIndex - width - 1] * 1 + pixels[pxIndex - width] * 2
                               + pixels[pxIndex - 1        ] * 2 + pixels[pxIndex        ] * 4
                               + pixels[pxIndex + width - 1] * 1 + pixels[pxIndex + width] * 2;
                    }
                    else
                    {
                        color  = pixels[pxIndex - width - 1] * 1 + pixels[pxIndex - width] * 2 + pixels[pxIndex - width + 1] * 1
                               + pixels[pxIndex - 1        ] * 2 + pixels[pxIndex        ] * 4 + pixels[pxIndex         + 1] * 2
                               + pixels[pxIndex + width - 1] * 1 + pixels[pxIndex + width] * 2 + pixels[pxIndex + width + 1] * 1;
                    }
                }

                change_pixels[pxIndex] = color / 16.0f;
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