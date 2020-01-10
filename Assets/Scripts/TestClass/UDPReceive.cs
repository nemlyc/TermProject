using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Scripts.TestClass
{
    class UDPReceive:MonoBehaviour
    {
        int LOCA_LPORT = 3333;
        static UdpClient udp;
        Thread thread;

        private void Start() {
            udp = new UdpClient(LOCA_LPORT);
            udp.Client.ReceiveTimeout = 100000;
            thread = new Thread(new ThreadStart(ThreadMethod));
            thread.Start();
        }
        private void Update() {
            
        }

        private void OnApplicationQuit() {
            thread.Abort();
        }

        private void ThreadMethod() {
            while (true) {
                IPEndPoint remoteEP = null;
                byte[] data = udp.Receive(ref remoteEP);
                string text = Encoding.UTF8.GetString(data);//ここutf8でいけるのかしら。
                Debug.Log(text);
            }
        }
    }
}
