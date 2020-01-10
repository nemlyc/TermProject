using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class advanceButtonAction : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
    }
    //ボタンクリックイベント
    private void OnClickButton()
    {
        Debug.Log("AdvanceButton Active!");
    }
}
