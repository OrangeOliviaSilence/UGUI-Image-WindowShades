using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UniTaskTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoopPrint());
        LoopPrint1().Forget();
        HttpAsyncTest().Forget();
	}

    // Update is called once per frame
    void Update()
    {
		print($"{GetInstanceID()} Update: {Time.frameCount}");
	}
    
    IEnumerator LoopPrint()
    {
        while (true)
        {
			print($"Start IEnumerator LoopPrint: {Time.frameCount}");
			print($"{GetInstanceID()} IEnumerator LoopPrint: {Time.frameCount}");
            yield return null;
			print($"End IEnumerator LoopPrint: {Time.frameCount}");
		}
    }

    async UniTask LoopPrint1()
    {
        while (true)
        {
			print($"Start async UniTask LoopPrint1: {Time.frameCount}");
			print($"{GetInstanceID()} async UniTask LoopPrint1: {Time.frameCount}");
			await UniTask.NextFrame();  // 不要用Yield()，有坑！会导致第一帧的时候该循环内的逻辑被执行2次
			print($"End async UniTask LoopPrint1: {Time.frameCount}");
		}
    }

    async UniTask HttpAsyncTest()
    {
        print($"HttpAsyncTest 1: {Time.frameCount}");

        var webReq = UnityWebRequest.Get("https://www.baidu.com");
        var web_res = await webReq.SendWebRequest();  // 原本放在协程里的yield return的async操作，如今都可以在async函数里用await执行
		print(web_res.downloadHandler.text);

        print($"HttpAsyncTest 2: {Time.frameCount}");
	}
}
