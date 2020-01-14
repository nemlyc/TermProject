using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts
{

    public class buttonEvent : MonoBehaviour
    {
        //クラインアントクラス
        Client client;
        //コントロール管理クラス
        CommandManager commandManager;
        Slider upDownSlider;
        Slider waitSlider;
        public TMP_Text waitTimeText;
        private int CurrentAltitude;
        private const int TELLO_WAITING_TIME = 5;
        // 定数の事前定義
        const string UP = "up";
        const string DOWN = "down";
        const string FRONT = "front";
        const string BACK = "back";
        const string RIGHT = "right";
        const string LEFT = "left";
        const string TURN_RIGHT = "turn_right";
        const string TURN_LEFT = "turn_left";
        const string PHOTO = "photo";

        // Start is called before the first frame update
        void Start()
        {
            //Clientクラスの生成
            client = new Client();
            //コントロール管理クラスの生成（シングルトン）
            commandManager = CommandManager.getInstance();
            // スライダーを取得する
            upDownSlider = GameObject.Find("UpDownSlider").GetComponent<Slider>();
            waitSlider = GameObject.Find("WatingSlider").GetComponent<Slider>();
            //待機時間テキストの取得
            if (waitTimeText == null)
            {
                waitTimeText = GameObject.Find("WaitTime").GetComponent<TMP_Text>();
            }
        }

        private void Command(string commandName, int commandValue)
        {
            if (CommandManager.getCanControl())
            {
                client.InputOperation(commandName, commandValue);
                StartCoroutine(WaitTello());
                Debug.Log(commandName + "を送信しました");
            }
        }

        IEnumerator WaitTello()
        {
            CommandManager.ChangeControl(false);
            for (int i = 0; i < TELLO_WAITING_TIME; i++)
            {
                int waitSeconds = TELLO_WAITING_TIME - 1;
                waitTimeText.text = "Wait for : " + (waitSeconds - i) + " seconds";
                yield return new WaitForSeconds(1);
            }
            waitTimeText.text = "";
            CommandManager.ChangeControl(true);
        }

        // ボタン操作系統
        public void advanceButtonEvent()
        {
            Debug.Log("前進ボタンクリック検知");
            Command(FRONT, 1);
        }
        public void rightButtonEvent()
        {
            Debug.Log("右旋回ボタンクリック検知");
            Command(RIGHT, 1);
        }
        public void recessionButtonEvent()
        {
            Debug.Log("後退ボタンクリック検知");
            Command(BACK, 1);
        }
        public void leftButtonEvent()
        {
            Debug.Log("左旋回ボタンクリック検知");
            Command(LEFT, 1);
        }
        public void upDownSliderEvent()
        {
            Debug.Log("上下スライダーの検知");
            Debug.Log("現在の高度：" + CurrentAltitude + "  スライダー数値：" + upDownSlider.value);
            int altitude = CurrentAltitude - (int)upDownSlider.value;
            if (altitude < 0)
            {
                // 上昇
                Command(UP, altitude);
            }
            else if (altitude > 0)
            {
                // 下降
                Command(DOWN, altitude);
            }
            CurrentAltitude = (int)upDownSlider.value;
        }
        public void leftRollButtonEvent()
        {
            Debug.Log("左回転ボタンクリック検知");
            Command(TURN_LEFT, 1);
        }
        public void rightRollButtonEvent()
        {
            Debug.Log("右回転ボタンクリック検知");
            Command(TURN_RIGHT, 1);
        }
        public void photoButtonEvent()
        {
            Debug.Log("撮影ボタンクリック検知");
            Command(PHOTO, 1);
        }
    }

}