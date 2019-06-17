using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public string savePath = "d:";
    public string feilName = "File";
    public Rect rect = new Rect(0, 0, 1920, 1080);
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CaptureScreen(Camera.main, rect);
        }
    }
    public Texture2D CaptureScreen(Camera came, Rect r)
    {
        RenderTexture rt = new RenderTexture((int)r.width, (int)r.height, 0);

        came.targetTexture = rt;
        came.Render();

        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)r.width, (int)r.height, TextureFormat.RGB24, false);

        screenShot.ReadPixels(r, 0, 0);
        screenShot.Apply();

        came.targetTexture = null;
        RenderTexture.active = null;
        GameObject.Destroy(rt);

        byte[] bytes = screenShot.EncodeToPNG();
        // string filename = "d:" + "/ScreenShot.png";
        string filename = Application.streamingAssetsPath + "/ScreenShot.png";
        if (!System.IO.Directory.Exists(Application.streamingAssetsPath))
        {
            System.IO.Directory.CreateDirectory(Application.streamingAssetsPath);
            Debug.Log("创建：" + Application.streamingAssetsPath);
        }
        System.IO.File.WriteAllBytes(filename, bytes);

        return screenShot;
    }
}
