using UnityEngine;
using UnityEngine.UI;

public class A08 : MonoBehaviour
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

    /// <summary>
    /// グリッドサイズ
    /// </summary>
    [SerializeField]
    private const int GRID_SIZE = 8;

    private void Awake()
    {
        if (m_RawImage == null)
        {
            m_RawImage = GameObject.Find("Canvas/MainPanel/RawImage").GetComponent<RawImage>();
        }
    }

    /// <summary>
    /// 最大プーリング
    /// </summary>
    public void MaxPooling()
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

        for (int h = 0; h < height; h += GRID_SIZE)
        {
            for (int w = 0; w < width; w += GRID_SIZE) 
            {
                int pxIndex = w + width * h;

                float r = 0.0f, g = 0.0f, b = 0.0f;
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    for (int j = 0; j < GRID_SIZE; j++)
                    {
                        r = Mathf.Max(r, pixels[pxIndex + j + width * i].r);
                        g = Mathf.Max(g, pixels[pxIndex + j + width * i].g);
                        b = Mathf.Max(b, pixels[pxIndex + j + width * i].b);
                    }
                }

                Color change_pixel = new Color(r,g,b, pixels[pxIndex].a);
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    for (int j = 0; j < GRID_SIZE; j++)
                    {
                        change_pixels[pxIndex + j + width * i] = change_pixel;
                    }
                }
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