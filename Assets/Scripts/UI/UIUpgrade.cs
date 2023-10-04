using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    public FlySwatter flySwatter; // FlySwatter 스크립트 연결할 변수

   [SerializeField] private Button upgradeButton;

    void Start()
    {
        upgradeButton = GetComponent<Button>(); // 버튼 컴포넌트 가져오기
        upgradeButton.onClick.AddListener(Upgrade); // 버튼 클릭 이벤트에 Upgrade 메서드 연결
    }

    void Upgrade()
    {
        flySwatter.Upgrade(); // FlySwatter 클래스의 Upgrade 메서드 호출
        upgradeButton.interactable = false; // 버튼 비활성화
    }
}
