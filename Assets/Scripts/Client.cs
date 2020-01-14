using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TMPro;

using UnityEngine;

namespace Assets.Scripts
{
    public class Client:MonoBehaviour
    {
        public string host = "133.14.195.193";
        public int port = 3333;
        private const int TELLO_WAITING_TIME = 5;
        public TMP_Text waitTimeText;
        private UdpClient _client;
        private bool _canControl;
        
        // 定数の事前定義
        const string UP = "up";
        const string DOWN = "down";
        const string FRONT = "front";
        const string BACK = "back";
        const string RIGHT = "right";
        const string LEFT = "left";
        const string TURN_RIGHT = "turn_right";
        const string TURN_LEFT = "turn_left";

        const int MOVE_VALUE = 10;//ここがよくわからないので数値にしてあるけど、telloの規格に合わせて決め打ちしていい。


        private void Start() {
            _client = new UdpClient();
            _client.Connect(host, port);
            _canControl = true;
            if (waitTimeText == null)
            {
                waitTimeText = GameObject.Find("WaitTime").GetComponent<TMP_Text>();
            }
        }
        private void Update() {
            if (_canControl)
            {
                IdentifyInputkey();
            }
        }

        IEnumerator WaitTello()
        {
            _canControl = false;
            for (int i = 0; i < TELLO_WAITING_TIME; i++)
            {
                int waitSeconds = TELLO_WAITING_TIME - 1;
                waitTimeText.text = "Wait for : " + (waitSeconds - i) + " seconds";
                yield return new WaitForSeconds(1);
            }
            waitTimeText.text = "";
            _canControl = true;
        }

        private void InputOperation(string commandName)
        {
            string json = EncodeToJson(commandName, MOVE_VALUE);
            byte[] operation = Encoding.UTF8.GetBytes(json);
            _client.Send(operation, operation.Length);

            StartCoroutine(WaitTello());
        }
        
        private void IdentifyInputkey() {
            if (Input.anyKeyDown) {
                // 入力があったキーをcodeに代入
                foreach(KeyCode code in Input.inputString) {
                    //codeがcaseの値に一致する場合
                    switch (code) {
                        case KeyCode.W:
                            InputOperation(FRONT);
                            Debug.Log("前進！" + EncodeToJson(FRONT, MOVE_VALUE));
                            break;
                        case KeyCode.S:
                            InputOperation(BACK);
                            Debug.Log("後退！" + EncodeToJson(BACK, MOVE_VALUE));
                            break;
                        case KeyCode.D:
                            InputOperation(RIGHT);
                            Debug.Log("右へ！" + EncodeToJson(RIGHT, MOVE_VALUE));
                            break;
                        case KeyCode.A:
                            InputOperation(LEFT);
                            Debug.Log("左へ！" + EncodeToJson(LEFT, MOVE_VALUE));
                            break;
                        case KeyCode.Alpha8:
                            InputOperation(UP);
                            Debug.Log("上昇！" + EncodeToJson(UP, MOVE_VALUE));
                            break;
                        case KeyCode.Alpha2:
                            InputOperation(DOWN);
                            Debug.Log("下降！" + EncodeToJson(DOWN, MOVE_VALUE));
                            break;
                        case KeyCode.Alpha4:
                            InputOperation(TURN_RIGHT);
                            Debug.Log("右回転！" + EncodeToJson(TURN_RIGHT, MOVE_VALUE));
                            break;
                        case KeyCode.Alpha6:
                            InputOperation(TURN_LEFT);
                            Debug.Log("左回転！" + EncodeToJson(TURN_LEFT, MOVE_VALUE));
                            break;
                    }
                }
            }
        }

        string EncodeToJson(string commandName, int value) {
            var command = new ControlCommand { commandName = commandName, value = value };
            var json = JsonUtility.ToJson(command);
            return json;
        }
        private void OnApplicationQuit() {
            _client.Close();
        }
    }
}
