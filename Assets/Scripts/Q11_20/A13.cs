﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class A13 : MonoBehaviour
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
    /// MAX-MINフィルタ
    /// </summary>
    public void MAX_MINFilter()
    {
        if (transform.root.GetComponent<FileManager>().m_Texture == null)
            return;

        // Pixel情報取得
        m_Texture = transform.root.GetComponent<FileManager>().m_Texture;
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

                List<float> listColors = new List<float>();

                // 上端
                if (h == 0)
                {
                    listColors.Add(0.0f);
                    // 左端
                    if (w == 0)
                    {

                        listColors.Add(0.2126f * pixels[pxIndex            ].r + 0.7152f * pixels[pxIndex            ].g + 0.0722f * pixels[pxIndex            ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + 1        ].r + 0.7152f * pixels[pxIndex + 1        ].g + 0.0722f * pixels[pxIndex + 1        ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width    ].r + 0.7152f * pixels[pxIndex + width    ].g + 0.0722f * pixels[pxIndex + width    ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width + 1].r + 0.7152f * pixels[pxIndex + width + 1].g + 0.0722f * pixels[pxIndex + width + 1].b);
                    }
                    // 右端
                    else if (w == width - 1)
                    {
                        listColors.Add(0.2126f * pixels[pxIndex - 1        ].r + 0.7152f * pixels[pxIndex - 1        ].g + 0.0722f * pixels[pxIndex - 1        ].b);
                        listColors.Add(0.2126f * pixels[pxIndex            ].r + 0.7152f * pixels[pxIndex            ].g + 0.0722f * pixels[pxIndex            ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width - 1].r + 0.7152f * pixels[pxIndex + width - 1].g + 0.0722f * pixels[pxIndex + width - 1].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width    ].r + 0.7152f * pixels[pxIndex + width    ].g + 0.0722f * pixels[pxIndex + width    ].b);
                    }
                    else
                    {
                        listColors.Add(0.2126f * pixels[pxIndex - 1        ].r + 0.7152f * pixels[pxIndex - 1        ].g + 0.0722f * pixels[pxIndex - 1        ].b);
                        listColors.Add(0.2126f * pixels[pxIndex            ].r + 0.7152f * pixels[pxIndex            ].g + 0.0722f * pixels[pxIndex            ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + 1        ].r + 0.7152f * pixels[pxIndex + 1        ].g + 0.0722f * pixels[pxIndex + 1        ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width - 1].r + 0.7152f * pixels[pxIndex + width - 1].g + 0.0722f * pixels[pxIndex + width - 1].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width    ].r + 0.7152f * pixels[pxIndex + width    ].g + 0.0722f * pixels[pxIndex + width    ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width + 1].r + 0.7152f * pixels[pxIndex + width + 1].g + 0.0722f * pixels[pxIndex + width + 1].b);
                    }
                }
                // 下端
                else if (h == height - 1)
                {
                    listColors.Add(0.0f);
                    // 左端
                    if (w == 0)
                    {
                        listColors.Add(0.2126f * pixels[pxIndex            ].r + 0.7152f * pixels[pxIndex            ].g + 0.0722f * pixels[pxIndex            ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + 1        ].r + 0.7152f * pixels[pxIndex + 1        ].g + 0.0722f * pixels[pxIndex + 1        ].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width    ].r + 0.7152f * pixels[pxIndex - width    ].g + 0.0722f * pixels[pxIndex - width    ].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width + 1].r + 0.7152f * pixels[pxIndex - width + 1].g + 0.0722f * pixels[pxIndex - width + 1].b);
                    }
                    // 右端
                    else if (w == width - 1)
                    {
                        listColors.Add(0.2126f * pixels[pxIndex            ].r + 0.7152f * pixels[pxIndex            ].g + 0.0722f * pixels[pxIndex            ].b);
                        listColors.Add(0.2126f * pixels[pxIndex - 1        ].r + 0.7152f * pixels[pxIndex - 1        ].g + 0.0722f * pixels[pxIndex - 1        ].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width - 1].r + 0.7152f * pixels[pxIndex - width - 1].g + 0.0722f * pixels[pxIndex - width - 1].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width    ].r + 0.7152f * pixels[pxIndex - width    ].g + 0.0722f * pixels[pxIndex - width    ].b);
                    }
                    else
                    {
                        listColors.Add(0.2126f * pixels[pxIndex            ].r + 0.7152f * pixels[pxIndex            ].g + 0.0722f * pixels[pxIndex            ].b);
                        listColors.Add(0.2126f * pixels[pxIndex - 1        ].r + 0.7152f * pixels[pxIndex - 1        ].g + 0.0722f * pixels[pxIndex - 1        ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + 1        ].r + 0.7152f * pixels[pxIndex + 1        ].g + 0.0722f * pixels[pxIndex + 1        ].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width - 1].r + 0.7152f * pixels[pxIndex - width - 1].g + 0.0722f * pixels[pxIndex - width - 1].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width    ].r + 0.7152f * pixels[pxIndex - width    ].g + 0.0722f * pixels[pxIndex - width    ].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width + 1].r + 0.7152f * pixels[pxIndex - width + 1].g + 0.0722f * pixels[pxIndex - width + 1].b);
                    }
                }
                else
                {
                    // 左端
                    if (w == 0)
                    {
                        listColors.Add(0.0f);
                        listColors.Add(0.2126f * pixels[pxIndex            ].r + 0.7152f * pixels[pxIndex            ].g + 0.0722f * pixels[pxIndex            ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + 1        ].r + 0.7152f * pixels[pxIndex + 1        ].g + 0.0722f * pixels[pxIndex + 1        ].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width    ].r + 0.7152f * pixels[pxIndex - width    ].g + 0.0722f * pixels[pxIndex - width    ].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width + 1].r + 0.7152f * pixels[pxIndex - width + 1].g + 0.0722f * pixels[pxIndex - width + 1].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width    ].r + 0.7152f * pixels[pxIndex + width    ].g + 0.0722f * pixels[pxIndex + width    ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width + 1].r + 0.7152f * pixels[pxIndex + width + 1].g + 0.0722f * pixels[pxIndex + width + 1].b);
                    }
                    // 右端
                    else if (w == width - 1)
                    {
                        listColors.Add(0.0f);
                        listColors.Add(0.2126f * pixels[pxIndex - 1        ].r + 0.7152f * pixels[pxIndex - 1        ].g + 0.0722f * pixels[pxIndex - 1        ].b);
                        listColors.Add(0.2126f * pixels[pxIndex            ].r + 0.7152f * pixels[pxIndex            ].g + 0.0722f * pixels[pxIndex            ].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width - 1].r + 0.7152f * pixels[pxIndex - width - 1].g + 0.0722f * pixels[pxIndex - width - 1].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width    ].r + 0.7152f * pixels[pxIndex - width    ].g + 0.0722f * pixels[pxIndex - width    ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width - 1].r + 0.7152f * pixels[pxIndex + width - 1].g + 0.0722f * pixels[pxIndex + width - 1].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width    ].r + 0.7152f * pixels[pxIndex + width    ].g + 0.0722f * pixels[pxIndex + width    ].b);
                    }
                    else
                    {
                        listColors.Add(0.2126f * pixels[pxIndex - 1        ].r + 0.7152f * pixels[pxIndex - 1        ].g + 0.0722f * pixels[pxIndex - 1        ].b);
                        listColors.Add(0.2126f * pixels[pxIndex            ].r + 0.7152f * pixels[pxIndex            ].g + 0.0722f * pixels[pxIndex            ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + 1        ].r + 0.7152f * pixels[pxIndex + 1        ].g + 0.0722f * pixels[pxIndex + 1        ].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width - 1].r + 0.7152f * pixels[pxIndex - width - 1].g + 0.0722f * pixels[pxIndex - width - 1].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width    ].r + 0.7152f * pixels[pxIndex - width    ].g + 0.0722f * pixels[pxIndex - width    ].b);
                        listColors.Add(0.2126f * pixels[pxIndex - width + 1].r + 0.7152f * pixels[pxIndex - width + 1].g + 0.0722f * pixels[pxIndex - width + 1].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width - 1].r + 0.7152f * pixels[pxIndex + width - 1].g + 0.0722f * pixels[pxIndex + width - 1].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width    ].r + 0.7152f * pixels[pxIndex + width    ].g + 0.0722f * pixels[pxIndex + width    ].b);
                        listColors.Add(0.2126f * pixels[pxIndex + width + 1].r + 0.7152f * pixels[pxIndex + width + 1].g + 0.0722f * pixels[pxIndex + width + 1].b);
                    }
                }

                float y = listColors.Max() - listColors.Min();

                change_pixels[pxIndex] = new Color(y, y, y);
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