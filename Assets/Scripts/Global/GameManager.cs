using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // ���� �޴����� �ν��Ͻ�

    
    private void Awake()
    {
        // ���� �޴��� �ν��Ͻ��� �����մϴ�.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
