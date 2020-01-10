using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    // 定数の事前定義
    string UP = "up";
    string DOWN = "down";
    string FRONT = "front";
    string BACK = "back";
    string RIGHT = "right";
    string LEFT = "left";
    string TURN_RIGHT = "turn_right";
    string TURN_LEFT = "turn_left";

    int MOVE_VALUE = 10;//ここがよくわからないので数値にしてあるけど、telloの規格に合わせて決め打ちしていい。

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        identifyInputkey();
        //この後に5秒間の遅延処理を入れなければならない。
        //UIにプログレスバーの搭載など。
    }

    /// <summary>
    /// 押されたキーを判別し、それに応じた操作を行う。
    /// </summary>
    private void identifyInputkey() {
        if (Input.anyKeyDown) {
            // 入力があったキーをcodeに代入
            foreach(KeyCode code in Input.inputString) {
                //codeがcaseの値に一致する場合
                switch (code) {
                    case KeyCode.W:
                        //Debug.Log("前進！" + encodeToJson(FRONT, MOVE_VALUE));
                        break;
                    case KeyCode.S:
                        //Debug.Log("後退！" + encodeToJson(BACK, MOVE_VALUE));
                        break;
                    case KeyCode.D:
                        //Debug.Log("右へ！" + encodeToJson(RIGHT, MOVE_VALUE));
                        break;
                    case KeyCode.A:
                        //Debug.Log("左へ！" + encodeToJson(LEFT, MOVE_VALUE));
                        break;
                    case KeyCode.Alpha8:
                        //Debug.Log("上昇！" + encodeToJson(UP, MOVE_VALUE));
                        break;
                    case KeyCode.Alpha2:
                        //Debug.Log("下降！" + encodeToJson(DOWN, MOVE_VALUE));
                        break;
                    case KeyCode.Alpha4:
                        //Debug.Log("右回転！" + encodeToJson(TURN_RIGHT, MOVE_VALUE));
                        break;
                    case KeyCode.Alpha6:
                        //Debug.Log("左回転！" + encodeToJson(TURN_LEFT, MOVE_VALUE));
                        break;
                }
            }
        }
    }

    string encodeToJson(string commandName, int value) {
        var command = new ControlCommand { commandName = commandName, value = value };
        var json = JsonUtility.ToJson(command);
        return json;
    }
}
