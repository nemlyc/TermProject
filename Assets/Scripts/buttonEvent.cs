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
        public string AccessIP;
        public int AccessPort;
        //クラインアントクラス
        Client client;
        //コントロール管理クラス
        CommandManager commandManager;
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
        const string LAND = "land";

        // Start is called before the first frame update
        void Start()
        {
            //Clientクラスの生成
            client = gameObject.AddComponent<Client>();
            client.host = AccessIP;
            client.port = AccessPort;
            //コントロール管理クラスの生成（シングルトン）
            commandManager = CommandManager.getInstance();
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
                waitTimeText.text = "Wait (" + (waitSeconds - i) + ")";
                yield return new WaitForSeconds(1);
            }
            waitTimeText.text = "Active";
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
        public void upButtonEvent()
        {
            Debug.Log("上昇ボタンクリック検知");
            Command(UP, 20);
        }
        public void downButtonEvent()
        {
            Debug.Log("下降ボタンクリック検知");
            Command(DOWN, 20);
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
        public void RandButtonEvent()
        {
            Debug.Log("着陸ボタンクリック検知");
            Command(LAND,1);
        }
    }

}