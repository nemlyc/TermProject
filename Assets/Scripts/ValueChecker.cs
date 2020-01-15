using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ValueChecker : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField ip_inputField;
    [SerializeField]
    private InputField port_inputField;

    [SerializeField]
    private TMP_Text ipAddress;
    [SerializeField]
    private Text portNumber;
    [SerializeField]
    private TMP_Text error;

    private string ControllerScene = "Controller";
    private string ErrorMessage = "IPAddress or PortNumber is empty !!";

    // Start is called before the first frame update
    void Start()
    {
        ip_inputField = ip_inputField.GetComponent<TMP_InputField>();
        port_inputField = port_inputField.GetComponent<InputField>();

        ipAddress = ipAddress.GetComponent<TMP_Text>();
        portNumber = portNumber.GetComponent<Text>();
        error = error.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InputIPAddress() {
        ipAddress.text = ip_inputField.text;
        MetaSettings.AccessIPAddress = ipAddress.text;
        PlayerPrefs.SetString("IP", ipAddress.text);
    }
    public void InputPortNumber() {
        portNumber.text = port_inputField.text;
        MetaSettings.AccessPort = int.Parse(portNumber.text);
        var intNum = int.Parse(portNumber.text);
        Debug.Log(intNum);
        PlayerPrefs.SetInt("Port", int.Parse(portNumber.text));
    }
    public void OnClick() {
        if (MetaSettings.AccessIPAddress != null && MetaSettings.AccessPort != null) {
            MetaSettings.AccessIPAddress = ipAddress.text;
            MetaSettings.AccessPort = int.Parse(portNumber.text);
            SceneManager.LoadScene(ControllerScene);
        } else {
            error.text = ErrorMessage;
        }
    }
}
