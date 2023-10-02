using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public UIMenu uIMenu { get; private set; }
    [field: SerializeField] public UIStore uIStore { get; private set; }
    [field: SerializeField] public UIUpgrade uIUpgrade { get; private set; }

    [field: SerializeField] public UIWaves uiWaves { get; private set; }
    [field: SerializeField] public UIDayNight uiDayNight { get; private set; }

    public static UIManager Instance;

    void Start()
    {
        Instance = this;
    }

}
