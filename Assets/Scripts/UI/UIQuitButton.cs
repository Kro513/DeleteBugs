using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuitButton : MonoBehaviour
{
    // Quit 버튼을 클릭할 때 호출할 메서드
    public void QuitGame()
    {
        // 게임 종료
        // Unity에서는 데스크톱 플랫폼에서 실행 중인 게임을 종료하는 데 사용되는 Application.Quit() 메서드를 제공합니다.
        // 단, 이 메서드는 빌드된 데스크톱 애플리케이션에서만 작동하며 에디터 모드에서는 동작하지 않습니다. 
        //아직 확인은 못해봤어요.
        Application.Quit();
    }
}
