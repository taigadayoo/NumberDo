using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBer : MonoBehaviour
{
    public Slider slider;
    public List<GameObject> getItemList = new List<GameObject>();
    public List<GameObject> pageList = new List<GameObject>();
    public GameObject handle;
    public GameObject button;
    private int itemCount = 0;
    public bool OnBar = false;
    public bool ischak = false;
    [SerializeField]
   public Image image;
    [SerializeField]
    GameObject itemGetPanel;

    public GameObject Pages;
    public GameObject page;
    public Sprite OnBerImage;
    public Sprite OffBerImage;
    public Canvas canvas;
    public GameObject exitItember;
    SampleSoundManager sampleSoundManager;
    public RectTransform[] spawnPositions; // �X�|�[���ʒu�̔z��
    ObjectManager objectManager;

    public int spawnedItemCount = 0;

   
    void Start()
    {
        exitItember.SetActive(false);
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
        objectManager = FindObjectOfType<ObjectManager>();
    }

    // Update is called once per frame
    void Update()
    {
      
        button.transform.position = handle.transform.position;
        SetPagesActive();
        SetItemsActive();
    }
    public int GetItemCount()
    {
        return getItemList.Count;//list���̃A�C�e���̌��𐔂���
    }

    public void OnItemBer()
    {
       
        GetItemCount();

        if(getItemList.Count> 0 && !OnBar )
        {
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE2);
            }
            image.sprite = OnBerImage;
            exitItember.SetActive(true);
            slider.value = getItemList.Count * 0.078f;�@//�A�C�e���̌��ɉ����ăA�C�e���o�[��L�΂�
            OnBar = true;
        }
       else if(OnBar)
        {
            image.sprite = OffBerImage;
            slider.value = 0;
            exitItember.SetActive(false);
            OnBar = false;

        }
        if (getItemList.Count == 0)
        {
            Debug.Log("�A�C�e������������܂���");//�A�C�e�����Ȃ��ꍇ�͉������Ȃ�
        }
    }
    public void AddItem(GameObject item)
    {
        getItemList.Add(item);
        itemGetPanel.SetActive(true);
       
        
        //if(spawnedItemCount == 0)
        //{
        //    item.SetActive(false);
        //}
        sampleSoundManager.PlaySe(SeType.SE1);
        if (spawnedItemCount < getItemList.Count && spawnedItemCount < spawnPositions.Length)
        {

            // �X�|�[���ʒu�̔z�񂩂�Ή�����ʒu���擾
            RectTransform targetPosition = spawnPositions[spawnedItemCount];


            // �A�C�e����Ή�����ʒu�ɃX�|�[��
            GameObject spawnedObject =  Instantiate(getItemList[spawnedItemCount], targetPosition.position, targetPosition.rotation);

            if (spawnedItemCount > 0)
            {

                Transform pageTargetPosition = spawnPositions[spawnedItemCount - 1];
                GameObject newPages = Instantiate(page, pageTargetPosition.position, pageTargetPosition.rotation);
                newPages.transform.SetParent(Pages.transform, false);
                pageList.Add(newPages);
                newPages.transform.SetParent(spawnPositions[spawnedItemCount -1 ], false);
                newPages.transform.SetAsFirstSibling();
                
               
            }
            getItemList.Remove(item);
            getItemList.Add(spawnedObject);

            spawnedObject.transform.SetParent(spawnPositions[spawnedItemCount], false);

            // �X�|�[���ς݃A�C�e���̐����C���N�������g
            spawnedItemCount++;

            if(OnBar == true)
            {
               slider.value = getItemList.Count * 0.075f;//��ڂ̃I�u�W�F�N�g�͕K����\��
           }
        }

    }
    void SetItemsActive()
    {
      
        foreach (GameObject item in getItemList)
        {
            item.SetActive(OnBar);
        }
    }
    void SetPagesActive()
    {

        foreach (GameObject page in pageList)
        {
            page.SetActive(OnBar);
        }
    }
    public void RemoveItem(GameObject itemToRemove)
    {
        if (getItemList.Contains(itemToRemove))
        {
            int indexToRemove = getItemList.IndexOf(itemToRemove);
         
            getItemList.Remove(itemToRemove); // �A�C�e�����X�g����폜
            Destroy(itemToRemove); // �A�C�e����GameObject��j��

            // �w�i���X�g�̊Ǘ�
            if (indexToRemove < pageList.Count)
            {
              
                Destroy(pageList[indexToRemove]);
                pageList.RemoveAt(indexToRemove);
            }

            // �A�C�e�����X�g���Ĕz�u
            UpdateItemBar();

            // �X�|�[���ς݃A�C�e���̐����f�N�������g
            spawnedItemCount = Mathf.Max(0, getItemList.Count);
        }
        else
        {
            Debug.Log("�A�C�e�����X�g����A�C�e���������܂���");
        }
    }

    private void UpdateItemBar()
    {
        // �A�C�e���̍Ĕz�u
        for (int i = 0; i < getItemList.Count; i++)
        {
            RectTransform targetPosition = spawnPositions[i];
            getItemList[i].transform.position = targetPosition.position;
            getItemList[i].transform.rotation = targetPosition.rotation;
        }

        // �w�i�̍Ĕz�u
        for (int i = 0; i < pageList.Count; i++)
        {
            // �w�i��2�ڂ̃A�C�e������\�����邽�߁Ai��1����n�߂�
            if (i < getItemList.Count - 1)
            {
                RectTransform pageTargetPosition = spawnPositions[i + 1];
                pageList[i].transform.position = pageTargetPosition.position;
                pageList[i].transform.rotation = pageTargetPosition.rotation;
                pageList[i].SetActive(true);
            }
            else
            {
                // �]���Ȕw�i�͔�\���ɂ���
                pageList[i].SetActive(false);
            }
        }

        // �A�C�e���o�[�̒������X�V
        slider.value = getItemList.Count * 0.055f;
    }
}
