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
			await UniTask.NextFrame(cancelToken, true);  // ��Ҫ��Yield()���пӣ��ᵼ�µ�һ֡��ʱ���ѭ���ڵ��߼���ִ��2��
			print($"End async UniTask LoopPrint1: {Time.frameCount}");
		}
    }

    async UniTask HttpAsyncTest(CancellationToken cancelToken)
    {
        print($"HttpAsyncTest 1: {Time.frameCount}");

        var webReq = UnityWebRequest.Get("https://www.baidu.com");
        var web_res = await webReq.SendWebRequest().WithCancellation(cancelToken, true);  // ԭ������Э�����yield return��async��������񶼿�����async��������awaitִ��
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
	/// ��UniTaskִ��Э��
	/// </summary>
	/// <param name="cancelToken">����UniTask�ٷ����飬����UniTask��ö�����cancelToken��For propagate Cancellation, all async method recommend to accept CancellationToken cancellationToken at last argument, and pass CancellationToken from root to end.</param>
	/// <returns></returns>
	async UniTask StartTestUniTaskCallDelayCoroutine(CancellationToken cancelToken)
    {
		// ��Э��ת��ΪUniTask��async����
        //      ���ǵ������ ToUniTask() �� IEnumerator ת��Ϊ UniTask ʱ��ʵ�����㲢����ֱ�ӻ��һ���򵥵�ֵ���ͽṹ�壬���ǻᴴ��һ�����ڿ���Э��״̬�͵��ȵ�״̬����
        //      ���״̬���Ĵ������漰��һ�����ڴ����ͼ��㣬��Ȼ UniTask �ṹ�屾���漰�ѷ��䣬����Щ������Ȼ�����һЩ������ڴ濪��
		await TestDelayCoroutine().WithCancellation(cancelToken);
	}
}
