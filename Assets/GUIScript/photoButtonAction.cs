using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class photoButtonAction : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.OnClick.AddListener(onClickButton);
    }

    //クリックイベント
    private void onClickButton()
    {
        Debug.log("PhotoButtonAction Active!");
    }
}
