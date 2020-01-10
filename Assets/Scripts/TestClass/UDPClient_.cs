using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Scripts.TestClass
{
    class UDPClient_:MonoBehaviour
    {
        public string host = "192.168.2.172";
        public int port = 3333;
        private UdpClient client;

        private void Start() {
            client = new UdpClient();
            client.Connect(host, port);
        }
        private void Update() {
            
        }

        private void OnGUI() {
            if(GUI.Button(new Rect(10,10,100,40), "send")) {
                byte[] dgram = Encoding.UTF8.GetBytes("hello");
                client.Send(dgram, dgram.Length);
            }
        }

        private void OnApplicationQuit() {
            client.Close();
        }
    }
}
