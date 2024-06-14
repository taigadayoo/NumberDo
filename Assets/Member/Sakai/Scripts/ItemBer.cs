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

    public GameObject Pages;
    public GameObject page;
    public Sprite OnBerImage;
    public Sprite OffBerImage;
    public Canvas canvas;
    public GameObject exitItember;

    public Transform[] spawnPositions; // �X�|�[���ʒu�̔z��

    public int spawnedItemCount = 0;

   
    void Start()
    {
        exitItember.SetActive(false);
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
        //if(spawnedItemCount == 0)
        //{
        //    item.SetActive(false);
        //}

        if (spawnedItemCount < getItemList.Count && spawnedItemCount < spawnPositions.Length)
        {

            // �X�|�[���ʒu�̔z�񂩂�Ή�����ʒu���擾
            Transform targetPosition = spawnPositions[spawnedItemCount];


            // �A�C�e����Ή�����ʒu�ɃX�|�[��
            GameObject spawnedObject =  Instantiate(getItemList[spawnedItemCount], targetPosition.position, targetPosition.rotation);

            if (spawnedItemCount > 0)
            {

                Transform pageTargetPosition = spawnPositions[spawnedItemCount - 1];
                GameObject newPages = Instantiate(page, pageTargetPosition.position, pageTargetPosition.rotation);
                newPages.transform.SetParent(Pages.transform, false);
                pageList.Add(newPages);
                
                newPages.transform.SetAsFirstSibling();
                
               
            }
            getItemList.Remove(item);
            getItemList.Add(spawnedObject);
          
            spawnedObject.transform.SetParent(canvas.transform, false);
        
        // �X�|�[���ς݃A�C�e���̐����C���N�������g
        spawnedItemCount++;

            if(OnBar == true)
            {
               slider.value = getItemList.Count * 0.078f;//��ڂ̃I�u�W�F�N�g�͕K����\��
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


}
