using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    /// <summary>
    /// RawImage
    /// </summary>
    [SerializeField]
    private RawImage m_RawImage = null;

    /// <summary>
    /// RawImage表示パネル
    /// </summary>
    [SerializeField]
    private GameObject m_RawImagePanel = null;

    /// <summary>
    /// メインパネル
    /// </summary>
    [SerializeField]
    private GameObject m_MainPanel = null;

    /// <summary>
    /// 画像テクスチャ
    /// </summary>
    private Texture2D m_Texture = null;
    public void      SetTexture(Texture2D tex) { m_Texture = tex;  }
    public Texture2D GetTexture(             ) { return m_Texture; }

    /// <summary>
    /// 画像ファイル読み込み
    /// </summary>
    public void LoadFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.FileName = "";
        openFileDialog.Filter = "PNG Files(.png) |*.png";
        openFileDialog.CheckFileExists = true;

        openFileDialog.ShowDialog();

        Debug.Log("Open File : " + openFileDialog.FileName);

        if (openFileDialog.FileName != "")
        {
            string filePath = openFileDialog.FileName;

            // 画像をバイナリで開く
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fileStream);
            byte[] binaryData = br.ReadBytes((int)br.BaseStream.Length);
            br.Close();

            int pos = 16; // 16バイトから開始

            // 横幅取得
            int width = 0;
            for (int i = 0; i < 4; i++)
            {
                width = width * 256 + binaryData[pos++];
            }

            // 縦幅取得
            int height = 0;
            for (int i = 0; i < 4; i++)
            {
                height = height * 256 + binaryData[pos++];
            }
            var mainPanelSize = m_MainPanel.GetComponent<RectTransform>().sizeDelta;

            Vector2 imageSize = new Vector2(width, height);

            // アスペクト比修正
            if (width > mainPanelSize.x - 10)
            {
                float ratio = (mainPanelSize.x - 10) / width;
                imageSize = new Vector2(width, height) * ratio;
            }
            if (imageSize.y > mainPanelSize.y - 10)
            {
                float ratio = (mainPanelSize.y - 10) / imageSize.y;
                imageSize = imageSize * ratio;
            }

            m_RawImagePanel.GetComponent<RectTransform>().sizeDelta = imageSize;

            m_Texture = new Texture2D(width, height);
            m_Texture.LoadImage(binaryData);

            Rect    rect   = new Rect(0, 0, width, height);
            Vector2 pivot  = new Vector2(0.5f, 0.5f);
            Sprite  sprite = Sprite.Create(m_Texture, rect, pivot);

            m_RawImage.texture = sprite.texture;
        }
    }

    /// <summary>
    /// 画像保存
    /// </summary>
    public void SaveFile()
    {
        Texture2D outputTexture = ImageUtil.m_Texture;

        if (outputTexture != null)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "";
            saveFileDialog.InitialDirectory = "";
            saveFileDialog.Filter = "PNG Files(.png) |*.png";
            saveFileDialog.Title = "保存先のファイルを選択してください";
            saveFileDialog.RestoreDirectory = true;

            //ダイアログを表示する
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                Debug.Log("Save File :" + saveFileDialog.FileName);

                var png = outputTexture.EncodeToPNG();
                File.WriteAllBytes(saveFileDialog.FileName, png);
            }
        }
    }
}
