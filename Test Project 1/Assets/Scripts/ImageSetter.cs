using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSetter : MonoBehaviour
{
    public Image targetImage;
    public Color color;

    public void SetImageColor()
    {
        if (this.targetImage == null)
            return;
        this.targetImage.color = this.color;
    }

    int cnt = 0;
    void Update()
    {
        //print($"Update Start: frameCount: {Time.frameCount}");
        if (cnt <= 0)
        {
            //StartCoroutine(TestCoroutine());
            cnt++;
        }
        //print($"Update End: frameCount: {Time.frameCount}");
    }

    IEnumerator TestCoroutine()
    {
        print($"TestCoroutine Start: frameCount: {Time.frameCount}");
        yield return null;
        print($"TestCoroutine End: frameCount: {Time.frameCount}");
    }
}
