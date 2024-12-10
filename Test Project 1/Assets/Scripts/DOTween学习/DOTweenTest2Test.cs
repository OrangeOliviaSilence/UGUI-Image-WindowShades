using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DOTweenTest2Test : MonoBehaviour
{
    public Text text;

    [TextArea] public string stringToShow;

    string scrambleTexts = "¨€";

	void Awake()
	{
        
	}

	void OnEnable()
    {
        var duration = 5f;
        var loopTimes = -1;

        text.DOKill();
        text.DOText(endValue: stringToShow, duration: duration, scrambleMode: ScrambleMode.Custom, scrambleChars: scrambleTexts).
            From(null).
            SetEase(Ease.Linear).
            SetLoops(loopTimes, LoopType.Yoyo);

        transform.DOKill();
        transform.DOShakePosition(duration: duration, strength: new Vector3(3, 3, 0), fadeOut: false).
            SetEase(Ease.Linear).
            SetLoops(loopTimes, LoopType.Yoyo);
    }
}
