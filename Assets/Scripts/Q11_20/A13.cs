using System.Collections.Generic;
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
    /// MAX-MINフィルタ
    /// </summary>
    public void MAX_MINFilter()
    {
        if (transform.root.GetComponent<FileManager>().GetTexture() == null)
            return;

        // Pixel情報取得
        m_Texture = transform.root.GetComponent<FileManager>().GetTexture();
        ImageDate imageDate = new ImageDate(m_Texture.GetPixels(), m_Texture.width, m_Texture.height);

        // グレースケール化
        List<float> listGrayPixels = ImageUtil.GetGrayPixels(imageDate.m_Pixels, imageDate.m_Width, imageDate.m_Height);

        // 書き換え用テクスチャの作成
        Color[] change_pixels = new Color[imageDate.m_Pixels.Length];

        for (int h = 0; h < imageDate.m_Height; h++)
        {
            for (int w = 0; w < imageDate.m_Width; w++)
            {
                int pxIndex = w + imageDate.m_Width * h;

                List<float> listColors = new List<float>();

                // 上端
                if (h == 0)
                {
                    listColors.Add(0.0f);
                    // 左端
                    if (w == 0)
                    {
                        listColors.Add(listGrayPixels[pxIndex            ]);
                        listColors.Add(listGrayPixels[pxIndex + 1        ]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width    ]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width + 1]);
                    }
                    // 右端
                    else if (w == imageDate.m_Width - 1)
                    {
                        listColors.Add(listGrayPixels[pxIndex - 1        ]);
                        listColors.Add(listGrayPixels[pxIndex            ]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width - 1]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width    ]);
                    }
                    else
                    {
                        listColors.Add(listGrayPixels[pxIndex - 1        ]);
                        listColors.Add(listGrayPixels[pxIndex            ]);
                        listColors.Add(listGrayPixels[pxIndex + 1        ]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width - 1]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width    ]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width + 1]);
                    }
                }
                // 下端
                else if (h == imageDate.m_Height - 1)
                {
                    listColors.Add(0.0f);
                    // 左端
                    if (w == 0)
                    {
                        listColors.Add(listGrayPixels[pxIndex            ]);
                        listColors.Add(listGrayPixels[pxIndex + 1        ]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width    ]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width + 1]);
                    }
                    // 右端
                    else if (w == imageDate.m_Width - 1)
                    {
                        listColors.Add(listGrayPixels[pxIndex            ]);
                        listColors.Add(listGrayPixels[pxIndex - 1        ]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width - 1]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width    ]);
                    }
                    else
                    {
                        listColors.Add(listGrayPixels[pxIndex            ]);
                        listColors.Add(listGrayPixels[pxIndex - 1        ]);
                        listColors.Add(listGrayPixels[pxIndex + 1        ]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width - 1]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width    ]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width + 1]);
                    }
                }
                else
                {
                    // 左端
                    if (w == 0)
                    {
                        listColors.Add(0.0f);
                        listColors.Add(listGrayPixels[pxIndex            ]);
                        listColors.Add(listGrayPixels[pxIndex + 1        ]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width    ]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width + 1]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width    ]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width + 1]);
                    }
                    // 右端
                    else if (w == imageDate.m_Width - 1)
                    {
                        listColors.Add(0.0f);
                        listColors.Add(listGrayPixels[pxIndex - 1        ]);
                        listColors.Add(listGrayPixels[pxIndex            ]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width - 1]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width    ]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width - 1]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width    ]);
                    }
                    else
                    {
                        listColors.Add(listGrayPixels[pxIndex - 1        ]);
                        listColors.Add(listGrayPixels[pxIndex            ]);
                        listColors.Add(listGrayPixels[pxIndex + 1        ]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width - 1]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width    ]);
                        listColors.Add(listGrayPixels[pxIndex - imageDate.m_Width + 1]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width - 1]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width    ]);
                        listColors.Add(listGrayPixels[pxIndex + imageDate.m_Width + 1]);
                    }
                }

                float y = listColors.Max() - listColors.Min();

                change_pixels[pxIndex] = new Color(y, y, y);
            }
        }

        // 画像変更
        ImageUtil.ChangeImage(change_pixels, imageDate.m_Width, imageDate.m_Height);
    }
}