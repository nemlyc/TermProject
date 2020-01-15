using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts
{
    class Server:MonoBehaviour
    {
        public int LOCAL_PORT = 55555;
        private static UdpClient udp;
        private Thread thread;

        public Image image;

        byte[] data;
        bool isRecieved = false;

        private void Start()
        {
            if (image == null)
            {
                image = GameObject.Find("Image").GetComponent<Image>();
            }
            
            udp = new UdpClient(LOCAL_PORT);
            thread = new Thread(new ThreadStart(ThreadMethod));
            thread.Start();
        }

        private void Update()
        {
            //受信を監視し、受信したときに画像を表示するようにする。
            if (isRecieved) {
                DisplayOperation(data);
                isRecieved = false;
            }
        }

        private void ThreadMethod()
        {
            while (true)
            {
                IPEndPoint remoteEP = null;
                data = udp.Receive(ref remoteEP);
                isRecieved = true;
            }
        }

        /// <summary>
        /// バイナリ からテクスチャ を生成し、スプライトに変換を行い、imageオブジェクトに貼り付けを行う。
        /// </summary>
        /// <param name="binaryData"></param>
        public void DisplayOperation(byte[] binaryData)
        {
            var texture = ReadImageBinary(binaryData);
            LoadCapturedImage(texture);
            
        }
        

        /// <summary>
        /// バイナリデータからテクスチャを生成する。
        /// </summary>
        /// <param name="binaryData">受け取ったバイナリ データ</param>
        /// <returns></returns>
        private Texture2D ReadImageBinary(byte[] binaryData)
        {
            Texture2D texture = new Texture2D(1,1);
            texture.LoadImage(binaryData);
            
            return texture;
        }

        /// <summary>
        /// 撮影した画像をテクスチャとしてUIに表示する。
        /// </summary>
        /// <param name="texture">生成したテクスチャデータ</param>
        private void LoadCapturedImage(Texture2D texture)
        {
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            image.sprite = sprite;
        }
        private void OnApplicationQuit() {
            udp.Close();
        }
    }
}
