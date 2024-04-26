using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    [SerializeField]
    public InputField inputField;
    private string correctPassword = "1234"; // 正しいパスワード

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
            Debug.Log("パスワードが一致しません。もう一度お試しください。");
        }
    }
}