using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    [SerializeField]
    public InputField inputField;
    private string correctPassword = "1234"; // �������p�X���[�h

    ObjectManager objectManager;
    [SerializeField]
    SceneManagement sceneManagement;
    private void Start()
    {
       objectManager =  FindObjectOfType<ObjectManager>();
    }

    public void CheckPassword()
    {
        string inputPassword = inputField.text;

        if (inputPassword == correctPassword)
        {
            objectManager.key.SetActive(true);
        }
        else
        {
            Debug.Log("�p�X���[�h����v���܂���B������x���������������B");
        }
    }
}