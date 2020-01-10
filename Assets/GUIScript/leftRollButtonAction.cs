using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class leftRollButtonAction : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.OnClick.AddListener(onClickButton);
    }
    //ボタンクリックイベント
    private void onClickButton()
    {
        Debug.Log("LeftRollAction Active!");
    }
}
