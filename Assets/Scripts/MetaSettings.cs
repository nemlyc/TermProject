using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaSettings : MonoBehaviour
{
    public static string AccessIPAddress;
    public static string AccessPort;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static string getIPAddress() {
        return AccessIPAddress;
    }
    public static string getPortNumber() {
        return AccessPort;
    }

}
