using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    [SerializeField]
    public InputField inputField;
    private string correctPassword = "1234"; // �������p�X���[�h

    ObjectManager objectManager;

    ItemBer itemBer;
    [SerializeField]
    SceneManagement sceneManagement;
    private void Start()
    {
       objectManager =  FindObjectOfType<ObjectManager>();
        itemBer = FindObjectOfType<ItemBer>();
    }

    public void CheckPassword()
    {
        string inputPassword = inputField.text;

        if (inputPassword == correctPassword)
        {
            //objectManager.key
            //objectManager.key.SetActive(true);
            itemBer.AddItem(objectManager.key);
            this.gameObject.SetActive(false);
            SampleSoundManager.Instance.PlaySe(SeType.SE4);
        }
        else
        {
            Debug.Log("�p�X���[�h����v���܂���B������x���������������B");
        }
    }
}