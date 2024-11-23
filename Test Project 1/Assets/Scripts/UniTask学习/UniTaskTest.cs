using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class UniTaskTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoopPrint());
        //LoopPrint1(this.GetCancellationTokenOnDestroy()).Forget();
        //HttpAsyncTest(this.GetCancellationTokenOnDestroy()).Forget();
        //StartRequestForMicrophoneAuth(this.GetCancellationTokenOnDestroy()).Forget();
        StartTestUniTaskCallDelayCoroutine(this.GetCancellationTokenOnDestroy()).Forget();
	}

    // Update is called once per frame
    void Update()
    {
		//print($"{GetInstanceID()} Update: {Time.frameCount}");
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

    IEnumerator TestDelayCoroutine()
    {
        print($"TestDelayCoroutine: {Time.time}");
        yield return new WaitForSeconds(5);
		print($"TestDelayCoroutine: {Time.time}");
	}

    async UniTask LoopPrint1(CancellationToken cancelToken)
    {
        while (true)
        {
			print($"Start async UniTask LoopPrint1: {Time.frameCount}");
			print($"{GetInstanceID()} async UniTask LoopPrint1: {Time.frameCount}");
			await UniTask.NextFrame(cancelToken, true);  // 不要用Yield()，有坑！会导致第一帧的时候该循环内的逻辑被执行2次
			print($"End async UniTask LoopPrint1: {Time.frameCount}");
		}
    }

    async UniTask HttpAsyncTest(CancellationToken cancelToken)
    {
        print($"HttpAsyncTest 1: {Time.frameCount}");

        var webReq = UnityWebRequest.Get("https://www.baidu.com");
        var web_res = await webReq.SendWebRequest().WithCancellation(cancelToken, true);  // 原本放在协程里的yield return的async操作，如今都可以在async函数里用await执行
		print(web_res.downloadHandler.text);

        print($"HttpAsyncTest 2: {Time.frameCount}");
	}

    async UniTask StartRequestForMicrophoneAuth(CancellationToken cancelToken)
    {
        print(123);
        var asyncReq = Application.RequestUserAuthorization(UserAuthorization.Microphone);
        asyncReq.completed += (asyncOp) => print(asyncOp);
        await asyncReq.WithCancellation(cancellationToken: cancelToken, cancelImmediately: true);
	}

	/// <summary>
	/// 用UniTask执行协程
	/// </summary>
	/// <param name="cancelToken">根据UniTask官方建议，所有UniTask最好都传入cancelToken。For propagate Cancellation, all async method recommend to accept CancellationToken cancellationToken at last argument, and pass CancellationToken from root to end.</param>
	/// <returns></returns>
	async UniTask StartTestUniTaskCallDelayCoroutine(CancellationToken cancelToken)
    {
		// 将协程转换为UniTask的async程序。
        //      但是当你调用 ToUniTask() 将 IEnumerator 转换为 UniTask 时，实际上你并不是直接获得一个简单的值类型结构体，而是会创建一个用于控制协程状态和调度的状态机。
        //      这个状态机的创建会涉及到一定的内存分配和计算，虽然 UniTask 结构体本身不涉及堆分配，但这些操作依然会带来一些计算和内存开销
		await TestDelayCoroutine().WithCancellation(cancelToken);
	}
}
