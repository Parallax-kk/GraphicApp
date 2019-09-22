using UnityEngine;
using UnityEngine.UI;

public class A12 : MonoBehaviour
{
    /// <summary>
    /// 画像のテクスチャ
    /// </summary>
    private Texture2D m_Texture = null;

    /// <summary>
    /// モーションフィルタ
    /// </summary>
    public void MotionBlur()
    {
        if (transform.root.GetComponent<FileManager>().GetTexture() == null)
            return;
        // Pixel情報取得
        m_Texture = transform.root.GetComponent<FileManager>().GetTexture();
        ImageDate imageDate = new ImageDate(m_Texture.GetPixels(), m_Texture.width, m_Texture.height);

        // 書き換え用テクスチャの作成
        Color[] change_pixels = new Color[imageDate.m_Pixels.Length];

        for (int h = 0; h < imageDate.m_Height; h++)
        {
            for (int w = 0; w < imageDate.m_Width; w++)
            {
                int pxIndex = w + imageDate.m_Width * h;

                Color color = new Color();

                // 上端
                if (h == 0)
                {
                    // 左端
                    if (w == 0)
                    {
                        color = imageDate.m_Pixels[pxIndex] + imageDate.m_Pixels[pxIndex + imageDate.m_Width + 1];
                    }
                    // 右端
                    else if (w == imageDate.m_Width - 1)
                    {
                        color = imageDate.m_Pixels[pxIndex];
                    }
                    else
                    {
                        color = imageDate.m_Pixels[pxIndex] + imageDate.m_Pixels[pxIndex + imageDate.m_Width + 1];
                    }
                }
                // 下端
                else if (h == imageDate.m_Height - 1)
                {
                    // 左端
                    if (w == 0)
                    {
                        color = imageDate.m_Pixels[pxIndex];
                    }
                    // 右端
                    else if (w == imageDate.m_Width - 1)
                    {
                        color = imageDate.m_Pixels[pxIndex - imageDate.m_Width - 1] + imageDate.m_Pixels[pxIndex];
                    }
                    else
                    {
                        color = imageDate.m_Pixels[pxIndex - imageDate.m_Width - 1] + imageDate.m_Pixels[pxIndex];
                    }
                }
                else
                {
                    // 左端
                    if (w == 0)
                    {
                        color = imageDate.m_Pixels[pxIndex] + imageDate.m_Pixels[pxIndex + imageDate.m_Width + 1];
                    }
                    // 右端
                    else if (w == imageDate.m_Width - 1)
                    {
                        color = imageDate.m_Pixels[pxIndex - imageDate.m_Width - 1] + imageDate.m_Pixels[pxIndex];
                    }
                    else
                    {
                        color = imageDate.m_Pixels[pxIndex - imageDate.m_Width - 1] + imageDate.m_Pixels[pxIndex] + imageDate.m_Pixels[pxIndex + imageDate.m_Width + 1];
                    }
                }

                change_pixels[pxIndex] = color / 3.0f;
            }
        }

        // 画像変更
        ImageUtil.ChangeImage(change_pixels, imageDate.m_Width, imageDate.m_Height);
    }
}