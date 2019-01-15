using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class A06 : MonoBehaviour
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
    /// 減色処理
    /// </summary>
    public void SubtractiveColor()
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

        // 減色処理
        for (int i = 0; i < pixels.Count(); i++)
        {
            change_pixels[i].r =   0.0f          <= pixels[i].r && pixels[i].r <  63.0f / 256.0f ?  32.0f / 256.0f :
                                  63.0f / 256.0f <= pixels[i].r && pixels[i].r < 127.0f / 256.0f ?  96.0f / 256.0f :
                                 127.0f / 256.0f <= pixels[i].r && pixels[i].r < 191.0f / 256.0f ? 160.0f / 256.0f :
                                 191.0f / 256.0f <= pixels[i].r && pixels[i].r < 256.0f / 256.0f ? 224.0f / 256.0f : 0.0f;

            change_pixels[i].g =   0.0f          <= pixels[i].g && pixels[i].g <  63.0f / 256.0f ?  32.0f / 256.0f :
                                  63.0f / 256.0f <= pixels[i].g && pixels[i].g < 127.0f / 256.0f ?  96.0f / 256.0f :
                                 127.0f / 256.0f <= pixels[i].g && pixels[i].g < 191.0f / 256.0f ? 160.0f / 256.0f :
                                 191.0f / 256.0f <= pixels[i].g && pixels[i].g < 256.0f / 256.0f ? 224.0f / 256.0f : 0.0f;

            change_pixels[i].b =   0.0f          <= pixels[i].b && pixels[i].b <  63.0f / 256.0f ?  32.0f / 256.0f :
                                  63.0f / 256.0f <= pixels[i].b && pixels[i].b < 127.0f / 256.0f ?  96.0f / 256.0f :
                                 127.0f / 256.0f <= pixels[i].b && pixels[i].b < 191.0f / 256.0f ? 160.0f / 256.0f :
                                 191.0f / 256.0f <= pixels[i].b && pixels[i].b < 256.0f / 256.0f ? 224.0f / 256.0f : 0.0f;

            change_pixels[i].a = 1.0f;
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