using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Networking;

public class UniTaskTest2 : MonoBehaviour
{
#if false
	Coroutine _coroutineLoopPrint;

	// Start is called before the first frame update
	void Start()
	{
		_coroutineLoopPrint = StartCoroutine(LoopPrint());
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			if (_coroutineLoopPrint != null)
			{
				print($"StopCoroutine: {_coroutineLoopPrint}");
				StopCoroutine(_coroutineLoopPrint);
				_coroutineLoopPrint = null;
			}
		}
		if (Input.GetMouseButtonUp(1))
		{
			if (_coroutineLoopPrint == null)
			{
				_coroutineLoopPrint = StartCoroutine(LoopPrint());
			}
		}
	}

	IEnumerator LoopPrint()
	{
		print($"Start IEnumerator LoopPrint: {Time.frameCount}");
		while (true)
		{
			yield return null;
		}
	}

#else
	CancellationTokenSource _cancelTokenSrcLoopPrint;

	void Start()
	{
		_cancelTokenSrcLoopPrint = new CancellationTokenSource();
		LoopPrintUniTask(_cancelTokenSrcLoopPrint.Token).Forget();
	}

	void OnDestroy()
	{
		if (_cancelTokenSrcLoopPrint != null)
		{
			TriggerCancelLoopPrintUniTask();
		}
	}

	void OnDisable()
	{
		if (_cancelTokenSrcLoopPrint != null)
		{
			TriggerCancelLoopPrintUniTask();
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			if (_cancelTokenSrcLoopPrint != null)
			{
				TriggerCancelLoopPrintUniTask();
			}
		}
		if (Input.GetMouseButtonUp(1))
		{
			if (_cancelTokenSrcLoopPrint == null)
			{
				_cancelTokenSrcLoopPrint = new CancellationTokenSource();
				LoopPrintUniTask(_cancelTokenSrcLoopPrint.Token).Forget();
			}
		}
	}

	async UniTask LoopPrintUniTask(CancellationToken cancelToken)
	{
		print($"Start UniTask LoopPrintUniTask: {Time.frameCount}");
		while (!cancelToken.IsCancellationRequested)
		{
			await UniTask.NextFrame(cancellationToken:cancelToken, cancelImmediately:true);
		}
	}

	void TriggerCancelLoopPrintUniTask()
	{
		if (_cancelTokenSrcLoopPrint != null)
		{
			print($"Stop UniTask: {_cancelTokenSrcLoopPrint}");
			_cancelTokenSrcLoopPrint.Cancel();
			_cancelTokenSrcLoopPrint.Dispose();
			_cancelTokenSrcLoopPrint = null;
		}
	}
#endif
}
