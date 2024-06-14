using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // ����̃I�u�W�F�N�g���Q�Ƃ��邽�߂̕ϐ�
    public GameObject targetObjectBox;
    public GameObject targetObjectBox2;
    public GameObject targetObjectBox3;
    public GameObject targetObjectPass;
    public GameObject key;
    public List<GameObject> items = new List<GameObject>();
    public GameObject password;
    public GameObject judgeButton;
    public GameObject inputField;
    public GameObject gameCanvas;

    public List<GameObject> touchObject = new List<GameObject>();
    private bool OnBox = false;
    private bool OnPass = false;
    private bool OnBox2 = false;
    private bool OnBox3 = false;
    ItemBer itemBer;
    SampleSoundManager sampleSoundManager;
    ButtonCotroller buttonCotroller;
    Timer timer;
    Password passwordScripts;
   
    private void Start()
    {
        password.SetActive(false);
        judgeButton.SetActive(false);
        inputField.SetActive(false);
        passwordScripts = FindObjectOfType<Password>();
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
        itemBer = FindObjectOfType<ItemBer>();
        buttonCotroller = FindObjectOfType<ButtonCotroller>();
       timer =  FindFirstObjectByType<Timer>();
}
    void Update()
    {
        ObjectTouch();
     

    }
   public void DeactivateAllObjects()
    {
        foreach (GameObject obj in touchObject)
        {
            if (obj != null) // null�`�F�b�N���s��
            {
                obj.SetActive(false);
            }
        }
    }
   public void ActivateAllObjects()
    {
        foreach (GameObject obj in touchObject)
        {
            if (obj != null) // null�`�F�b�N���s��
            {
                obj.SetActive(true);
            }
        }
    }
    private void ObjectTouch()
    {
        
            if (Input.GetMouseButtonDown(0))
        {
            // �}�E�X�̈ʒu����Ray���΂�
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            // Ray��Collider2D�ɓ��������������o����
            if (hit.collider != null)
            {
                // ��������Collider2D��GameObject������̃I�u�W�F�N�g�ł��邩���m�F����
                if (hit.collider.gameObject == targetObjectBox)
                {
                    if (OnBox == false)
                    {
                        buttonCotroller.OnButtonClick();
                        OnBox = true;
                        timer.Stop();
                        if (sampleSoundManager != null)
                        {
                            SampleSoundManager.Instance.PlaySe(SeType.SE2);
                        }
                    }
                   
                }
                if(hit.collider.gameObject == targetObjectPass && !passwordScripts.OnePassWord)
                {
                    if (OnPass == false)
                    {
                        if (sampleSoundManager != null)
                        {
                            SampleSoundManager.Instance.PlaySe(SeType.SE3);
                        }
                        judgeButton.SetActive(true);
                        inputField.SetActive(true);
                        OnPass = true;
                    }
                    else
                    {
                        judgeButton.SetActive(false);
                        inputField.SetActive(false);
                        OnPass = false;
                    }
                }
                if (hit.collider.gameObject == targetObjectPass && passwordScripts.OnePassWord)
                {
                    itemBer.AddItem(key);
                }
                    if (hit.collider.gameObject == targetObjectBox2 && !OnBox2)
                {
                    itemBer.AddItem(items[0]);

                    OnBox2 = true;
                }
                if (hit.collider.gameObject == targetObjectBox3 && !OnBox3)
                {
                    itemBer.AddItem(items[1]);

                    OnBox3 = true;
                }
            }
        }
        
    }
}
