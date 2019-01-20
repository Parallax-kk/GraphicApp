using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A10 : MonoBehaviour
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
    /// メディアンフィルタ
    /// </summary>
    public void MedianFilter()
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

                SortedDictionary<float, Color> sortedDicClors = new SortedDictionary<float, Color>();

                // 上端
                if (h == 0)
                {
                    sortedDicClors[0.0f] = Color.black;

                    // 左端
                    if (w == 0)
                    {
                        sortedDicClors[pixels[pxIndex            ].r + pixels[pxIndex            ].g + pixels[pxIndex            ].b] = pixels[pxIndex            ];
                        sortedDicClors[pixels[pxIndex + 1        ].r + pixels[pxIndex + 1        ].g + pixels[pxIndex + 1        ].b] = pixels[pxIndex + 1        ];
                        sortedDicClors[pixels[pxIndex + width    ].r + pixels[pxIndex + width    ].g + pixels[pxIndex + width    ].b] = pixels[pxIndex + width    ];
                        sortedDicClors[pixels[pxIndex + width + 1].r + pixels[pxIndex + width + 1].g + pixels[pxIndex + width + 1].b] = pixels[pxIndex + width + 1];
                    }
                    // 右端
                    else if (w == width - 1)
                    {
                        sortedDicClors[pixels[pxIndex - 1        ].r + pixels[pxIndex - 1        ].g + pixels[pxIndex - 1        ].b] = pixels[pxIndex - 1        ];
                        sortedDicClors[pixels[pxIndex            ].r + pixels[pxIndex            ].g + pixels[pxIndex            ].b] = pixels[pxIndex            ];
                        sortedDicClors[pixels[pxIndex + width - 1].r + pixels[pxIndex + width - 1].g + pixels[pxIndex + width - 1].b] = pixels[pxIndex + width - 1];
                        sortedDicClors[pixels[pxIndex + width    ].r + pixels[pxIndex + width    ].g + pixels[pxIndex + width    ].b] = pixels[pxIndex + width    ];
                    }
                    else
                    {
                        sortedDicClors[pixels[pxIndex - 1        ].r + pixels[pxIndex - 1        ].g + pixels[pxIndex - 1        ].b] = pixels[pxIndex - 1        ];
                        sortedDicClors[pixels[pxIndex            ].r + pixels[pxIndex            ].g + pixels[pxIndex            ].b] = pixels[pxIndex            ];
                        sortedDicClors[pixels[pxIndex + 1        ].r + pixels[pxIndex + 1        ].g + pixels[pxIndex + 1        ].b] = pixels[pxIndex + 1        ];
                        sortedDicClors[pixels[pxIndex + width - 1].r + pixels[pxIndex + width - 1].g + pixels[pxIndex + width - 1].b] = pixels[pxIndex + width - 1];
                        sortedDicClors[pixels[pxIndex + width    ].r + pixels[pxIndex + width    ].g + pixels[pxIndex + width    ].b] = pixels[pxIndex + width    ];
                        sortedDicClors[pixels[pxIndex + width + 1].r + pixels[pxIndex + width + 1].g + pixels[pxIndex + width + 1].b] = pixels[pxIndex + width + 1];
                    }
                }
                // 下端
                else if (h == height - 1)
                {
                    sortedDicClors[0.0f] = Color.black;

                    // 左端
                    if (w == 0)
                    {
                        sortedDicClors[pixels[pxIndex            ].r + pixels[pxIndex            ].g + pixels[pxIndex            ].b] = pixels[pxIndex            ];
                        sortedDicClors[pixels[pxIndex + 1        ].r + pixels[pxIndex + 1        ].g + pixels[pxIndex + 1        ].b] = pixels[pxIndex + 1        ];
                        sortedDicClors[pixels[pxIndex - width    ].r + pixels[pxIndex - width    ].g + pixels[pxIndex - width    ].b] = pixels[pxIndex - width    ];
                        sortedDicClors[pixels[pxIndex - width + 1].r + pixels[pxIndex - width + 1].g + pixels[pxIndex - width + 1].b] = pixels[pxIndex - width + 1];
                    }
                    // 右端
                    else if (w == width - 1)
                    {
                        sortedDicClors[pixels[pxIndex            ].r + pixels[pxIndex            ].g + pixels[pxIndex            ].b] = pixels[pxIndex            ];
                        sortedDicClors[pixels[pxIndex - 1        ].r + pixels[pxIndex - 1        ].g + pixels[pxIndex - 1        ].b] = pixels[pxIndex - 1        ];
                        sortedDicClors[pixels[pxIndex - width - 1].r + pixels[pxIndex - width - 1].g + pixels[pxIndex - width - 1].b] = pixels[pxIndex - width - 1];
                        sortedDicClors[pixels[pxIndex - width    ].r + pixels[pxIndex - width    ].g + pixels[pxIndex - width    ].b] = pixels[pxIndex - width    ];
                    }
                    else
                    {
                        sortedDicClors[pixels[pxIndex            ].r + pixels[pxIndex            ].g + pixels[pxIndex            ].b] = pixels[pxIndex            ];
                        sortedDicClors[pixels[pxIndex - 1        ].r + pixels[pxIndex - 1        ].g + pixels[pxIndex - 1        ].b] = pixels[pxIndex - 1        ];
                        sortedDicClors[pixels[pxIndex + 1        ].r + pixels[pxIndex + 1        ].g + pixels[pxIndex + 1        ].b] = pixels[pxIndex + 1        ];
                        sortedDicClors[pixels[pxIndex - width - 1].r + pixels[pxIndex - width - 1].g + pixels[pxIndex - width - 1].b] = pixels[pxIndex - width - 1];
                        sortedDicClors[pixels[pxIndex - width    ].r + pixels[pxIndex - width    ].g + pixels[pxIndex - width    ].b] = pixels[pxIndex - width    ];
                        sortedDicClors[pixels[pxIndex - width + 1].r + pixels[pxIndex - width + 1].g + pixels[pxIndex - width + 1].b] = pixels[pxIndex - width + 1];
                    }
                }
                else
                {
                    // 左端
                    if (w == 0)
                    {
                        sortedDicClors[0.0f] = Color.black;

                        sortedDicClors[pixels[pxIndex            ].r + pixels[pxIndex            ].g + pixels[pxIndex            ].b] = pixels[pxIndex            ];
                        sortedDicClors[pixels[pxIndex + 1        ].r + pixels[pxIndex + 1        ].g + pixels[pxIndex + 1        ].b] = pixels[pxIndex + 1        ];
                        sortedDicClors[pixels[pxIndex - width    ].r + pixels[pxIndex - width    ].g + pixels[pxIndex - width    ].b] = pixels[pxIndex - width    ];
                        sortedDicClors[pixels[pxIndex - width + 1].r + pixels[pxIndex - width + 1].g + pixels[pxIndex - width + 1].b] = pixels[pxIndex - width + 1];
                        sortedDicClors[pixels[pxIndex + width    ].r + pixels[pxIndex + width    ].g + pixels[pxIndex + width    ].b] = pixels[pxIndex + width    ];
                        sortedDicClors[pixels[pxIndex + width + 1].r + pixels[pxIndex + width + 1].g + pixels[pxIndex + width + 1].b] = pixels[pxIndex + width + 1];
                    }
                    // 右端
                    else if (w == width - 1)
                    {
                        sortedDicClors[0.0f] = Color.black;

                        sortedDicClors[pixels[pxIndex - 1        ].r + pixels[pxIndex - 1        ].g + pixels[pxIndex - 1        ].b] = pixels[pxIndex - 1        ];
                        sortedDicClors[pixels[pxIndex            ].r + pixels[pxIndex            ].g + pixels[pxIndex            ].b] = pixels[pxIndex            ];
                        sortedDicClors[pixels[pxIndex - width - 1].r + pixels[pxIndex - width - 1].g + pixels[pxIndex - width - 1].b] = pixels[pxIndex - width - 1];
                        sortedDicClors[pixels[pxIndex - width    ].r + pixels[pxIndex - width    ].g + pixels[pxIndex - width    ].b] = pixels[pxIndex - width    ];
                        sortedDicClors[pixels[pxIndex + width - 1].r + pixels[pxIndex + width - 1].g + pixels[pxIndex + width - 1].b] = pixels[pxIndex + width - 1];
                        sortedDicClors[pixels[pxIndex + width    ].r + pixels[pxIndex + width    ].g + pixels[pxIndex + width    ].b] = pixels[pxIndex + width    ];
                    }
                    else
                    {
                        sortedDicClors[pixels[pxIndex - 1        ].r + pixels[pxIndex - 1        ].g + pixels[pxIndex - 1        ].b] = pixels[pxIndex - 1        ];
                        sortedDicClors[pixels[pxIndex            ].r + pixels[pxIndex            ].g + pixels[pxIndex            ].b] = pixels[pxIndex            ];
                        sortedDicClors[pixels[pxIndex + 1        ].r + pixels[pxIndex + 1        ].g + pixels[pxIndex + 1        ].b] = pixels[pxIndex + 1        ];
                        sortedDicClors[pixels[pxIndex - width - 1].r + pixels[pxIndex - width - 1].g + pixels[pxIndex - width - 1].b] = pixels[pxIndex - width - 1];
                        sortedDicClors[pixels[pxIndex - width    ].r + pixels[pxIndex - width    ].g + pixels[pxIndex - width    ].b] = pixels[pxIndex - width    ];
                        sortedDicClors[pixels[pxIndex - width + 1].r + pixels[pxIndex - width + 1].g + pixels[pxIndex - width + 1].b] = pixels[pxIndex - width + 1];
                        sortedDicClors[pixels[pxIndex + width - 1].r + pixels[pxIndex + width - 1].g + pixels[pxIndex + width - 1].b] = pixels[pxIndex + width - 1];
                        sortedDicClors[pixels[pxIndex + width    ].r + pixels[pxIndex + width    ].g + pixels[pxIndex + width    ].b] = pixels[pxIndex + width    ];
                        sortedDicClors[pixels[pxIndex + width + 1].r + pixels[pxIndex + width + 1].g + pixels[pxIndex + width + 1].b] = pixels[pxIndex + width + 1];
                    }
                }

                change_pixels[pxIndex] = sortedDicClors.ElementAt((int)(sortedDicClors.Count * 0.5f + 0.5f)).Value;
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