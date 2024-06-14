using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    [SerializeField]
    public InputField inputField;
    private string correctPassword = "1996"; // 正しいパスワード

    public bool OnePassWord;
    ObjectManager objectManager;

    ItemBer itemBer;
    [SerializeField]
    SceneManagement sceneManagement;

    SampleSoundManager sampleSoundManager;
    private void Start()
    {
       objectManager =  FindObjectOfType<ObjectManager>();
        itemBer = FindObjectOfType<ItemBer>();
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
        OnePassWord = false;
    }
    private void Update()
    {
      
    }
    public void CheckPassword()
    {
        string inputPassword = inputField.text;

        if (inputPassword == correctPassword )
        {
            //objectManager.key
            //objectManager.key.SetActive(true);
            
            this.gameObject.SetActive(false);
            OnePassWord = true;
          
            if (sampleSoundManager != null)
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE4);
            }
        }
       
        else
        {
            Debug.Log("パスワードが一致しません。もう一度お試しください。");
        }
    }
}