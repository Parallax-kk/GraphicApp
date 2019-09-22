using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class A16Vertical : MonoBehaviour
{
    /// <summary>
    /// 画像のテクスチャ
    /// </summary>
    private Texture2D m_Texture = null;

    /// <summary>
    /// Prewittフィルタ Vertical
    /// </summary>
    public void PrewittFilterVertical()
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
                // 縦方向
                //       -1  -1 -1
                // K = [  0   0  0]
                //        1   1  1
               
                float y = 0.0f;

                // 上端
                if (h == 0)
                {
                    // 左端
                    if (w == 0)
                    {
                        y = (1.0f * listGrayPixels[pxIndex + imageDate.m_Width    ])
                          + (1.0f * listGrayPixels[pxIndex + imageDate.m_Width + 1]);
                    }
                    // 右端
                    else if (w == imageDate.m_Width - 1)
                    {
                        y = (1.0f * listGrayPixels[pxIndex + imageDate.m_Width - 1])
                          + (1.0f * listGrayPixels[pxIndex + imageDate.m_Width    ]);
                    }
                    else
                    {
                        y = (1.0f * listGrayPixels[pxIndex + imageDate.m_Width - 1])
                          + (1.0f * listGrayPixels[pxIndex + imageDate.m_Width    ]) 
                          + (1.0f * listGrayPixels[pxIndex + imageDate.m_Width + 1]);
                    }
                }
                // 下端
                else if (h == imageDate.m_Height - 1)
                {
                    // 左端
                    if (w == 0)
                    {
                        y = (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width    ])
                          + (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width + 1]);
                    }
                    // 右端
                    else if (w == imageDate.m_Width - 1)
                    {
                        y = (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width - 1])
                          + (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width    ]);
                    }
                    else
                    {
                        y = (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width - 1])
                          + (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width    ])
                          + (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width + 1]);
                    }
                }
                else
                {
                    // 左端
                    if (w == 0)
                    {
                        y = (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width    ])
                          + (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width + 1])
                          + ( 1.0f * listGrayPixels[pxIndex + imageDate.m_Width    ])
                          + ( 1.0f * listGrayPixels[pxIndex + imageDate.m_Width + 1]);
                    }
                    // 右端
                    else if (w == imageDate.m_Width - 1)
                    {
                        y = (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width - 1])
                          + (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width    ])
                          + ( 1.0f * listGrayPixels[pxIndex + imageDate.m_Width    ])
                          + ( 1.0f * listGrayPixels[pxIndex + imageDate.m_Width - 1]);
                    }
                    else
                    {
                        y = (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width - 1])
                          + (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width    ])
                          + (-1.0f * listGrayPixels[pxIndex - imageDate.m_Width + 1])
                          + ( 1.0f * listGrayPixels[pxIndex + imageDate.m_Width - 1])
                          + ( 1.0f * listGrayPixels[pxIndex + imageDate.m_Width    ])
                          + ( 1.0f * listGrayPixels[pxIndex + imageDate.m_Width + 1]);
                    }
                }

                if (y < 0)
                    y = 0.0f;

                change_pixels[pxIndex] = new Color(y,y,y);
            }
        }

        // 画像変更
        ImageUtil.ChangeImage(change_pixels, imageDate.m_Width, imageDate.m_Height);
    }
}