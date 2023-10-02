using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonStartScene : MonoBehaviour
{
    // 이 메서드는 버튼이 클릭되었을 때 호출됩니다.
    public void GoToStartScene()
    {
        // "StartScene"은 이동할 씬의 이름입니다.
        SceneManager.LoadScene("StartScene");
    }
}
