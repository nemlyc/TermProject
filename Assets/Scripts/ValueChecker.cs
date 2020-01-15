using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ValueChecker : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField ip_inputField;
    [SerializeField]
    private TMP_InputField port_inputField;

    [SerializeField]
    private TMP_Text ipAddress;
    [SerializeField]
    private TMP_Text portNumber;
    [SerializeField]
    private TMP_Text error;

    private string ControllerScene = "Controller";
    private string ErrorMessage = "IPAddress or PortNumber is empty !!";

    // Start is called before the first frame update
    void Start()
    {
        ip_inputField = ip_inputField.GetComponent<TMP_InputField>();
        port_inputField = port_inputField.GetComponent<TMP_InputField>();

        ipAddress = ipAddress.GetComponent<TMP_Text>();
        portNumber = portNumber.GetComponent<TMP_Text>();
        error = error.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InputIPAddress() {
        ipAddress.text = ip_inputField.text;
        MetaSettings.AccessIPAddress = ipAddress.text;
    }
    public void InputPortNumber() {
        portNumber.text = port_inputField.text;
        MetaSettings.AccessPort = portNumber.text;
    }
    public void OnClick() {
        if (MetaSettings.AccessIPAddress != null && MetaSettings.AccessPort != null) {
            SceneManager.LoadScene(ControllerScene);
        } else {
            error.text = ErrorMessage;
        }
    }
}
