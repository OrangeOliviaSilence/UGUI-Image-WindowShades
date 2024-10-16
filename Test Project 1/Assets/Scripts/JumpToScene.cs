using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JumpToScene : MonoBehaviour
{
    public Button jumpButton;

    public string jumpToSceneName;

    // Start is called before the first frame update
    void Start()
    {
        if (jumpButton != null)
            jumpButton.onClick.AddListener(jump2SpecificScene);
            jumpButton.GetComponentInChildren<Text>().text = SceneManager.GetActiveScene().name;
    }

    void jump2SpecificScene()
    {
        SceneManager.LoadScene(jumpToSceneName);
    }
}
