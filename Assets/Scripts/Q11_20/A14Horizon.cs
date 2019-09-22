using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class A14Horizon : MonoBehaviour
{
    /// <summary>
    /// 画像のテクスチャ
    /// </summary>
    private Texture2D m_Texture = null;

    /// <summary>
    /// 微分フィルタ
    /// </summary>
    public void DifferentialFilterHorizon()
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

                float y = 0.0f;

                // 横方向
                //      0  0 0
                // K = [-1  1 0]
                //      0  0 0

                // 左端
                if (w == 0)
                {
                    y = listGrayPixels[pxIndex];
                }
                else
                {
                    y = (-1.0f * listGrayPixels[pxIndex - 1] + listGrayPixels[pxIndex]);
                }

                change_pixels[pxIndex] = new Color(y, y, y);
            }
        }

        // 画像変更
        ImageUtil.ChangeImage(change_pixels, imageDate.m_Width, imageDate.m_Height);
    }
}