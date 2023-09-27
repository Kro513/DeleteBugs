using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStore : MonoBehaviour
{
<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
=======
    [SerializeField] private Button[] button;
    [SerializeField] private TowerSpawner towerSpawner;

    //[SerializeField] TMP_Text[] coinTxt;

    private void Start()
    {
        for(int i = 0; i < button.Length; i++)
        {
            int buttonIndex = i;
            button[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        //towerSpawner.SpawnTower(buttonIndex);
>>>>>>> Stashed changes
    }
}
