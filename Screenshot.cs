using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public string savePath = "d:";
    //10101001-地形-高山
    public string id = "10101001";
    public string type = "地形";
    public string feilName = "高山";
    //展示图片：编号+类型+序号 例：101010011001-图片-001
    private string screenType = "1";
    public int screenID = 1;
    public Rect rect = new Rect(0, 0, 1920, 1080);
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
        string path = savePath + "/" + "Unity-" + type + "/" + id + "-" + type + "-" + feilName;
        string screenName = id + screenType + screenID + "-图片-00";//+ screenID + ".png";
        string screenFileName = path + "/" + screenName;
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
            Debug.Log("创建：" + path);
        }
        CheckNScreenName(path, screenID, bytes);
        return screenShot;
    }
    private void CheckNScreenName(string path, int _id, byte[] bytes)
    {
        string tempPath = path + "/" + id + screenType + "00" + _id + "-图片-00" + _id + ".png";
        if (!System.IO.File.Exists(tempPath))
        {
            System.IO.File.WriteAllBytes(tempPath, bytes);
            Debug.Log("保存图片：" + tempPath);
        }
        else
        {
            if (_id > 10)
            {
                Debug.LogError("图片序号还不能大于10");
            }
            _id++;
            CheckNScreenName(path, _id, bytes);
        }
    }
}