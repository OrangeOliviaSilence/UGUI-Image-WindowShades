using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


/// <summary>
/// ≤‚ ‘Process
/// </summary>
public class UniTaskTest3 : MonoBehaviour, IProgress<float>
{
    // Start is called before the first frame update
    void Start()
    {
        AsyncGetWebInfo(destroyCancellationToken).Forget();
		PlayerLoopSystem currentLoop = PlayerLoop.GetCurrentPlayerLoop();
        for (int i = 0; i < currentLoop.subSystemList.Length; i++)
        {
            var subsystem = currentLoop.subSystemList[i];
			Debug.Log($"PlayerLoop Phase[{i}]: {subsystem}");

            // ¥Ú”°≥ˆSubSubSystem
            if (subsystem.subSystemList != null)
            {
				for (int j = 0; j < subsystem.subSystemList.Length; j++)
				{
                    var subSubSystem = subsystem.subSystemList[j];
					Debug.Log($"PlayerLoop Phase[{i}]: {subsystem}: [{j}]: {subSubSystem}");
				}
			}
		}
	}

    // Update is called once per frame
    void Update()
    {

	}

    async UniTask AsyncGetWebInfo(CancellationToken cancelToken)
    {
        var webReq = UnityWebRequest.Get("https://www.bilibili.com").SendWebRequest();
        var result = await webReq.WithCancellation(cancelToken);
        print(result.downloadHandler.text);
    }

	public void Report(float value)
    {
        Debug.Log(value);
    }
}
