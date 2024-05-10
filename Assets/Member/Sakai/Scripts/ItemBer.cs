using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBer : MonoBehaviour
{
    public Slider slider;
    public List<GameObject> getItemList = new List<GameObject>();
    public GameObject handle;
    public GameObject button;
    private int itemCount = 0;
    public bool OnBar = false;
    public bool ischak = false;
    public Canvas canvas;


    public Transform[] spawnPositions; // �X�|�[���ʒu�̔z��

    private int spawnedItemCount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        button.transform.position = handle.transform.position;
       
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
           
            slider.value = getItemList.Count * 0.1f;�@//�A�C�e���̌��ɉ����ăA�C�e���o�[��L�΂�
            OnBar = true;
        }
       else if(OnBar)
        {
            slider.value = 0;
           
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
        if(spawnedItemCount == 0)
        {
            item.SetActive(false);
        }
       
        if (spawnedItemCount < getItemList.Count && spawnedItemCount < spawnPositions.Length)
        {

            // �X�|�[���ʒu�̔z�񂩂�Ή�����ʒu���擾
            Transform targetPosition = spawnPositions[spawnedItemCount];

            // �A�C�e����Ή�����ʒu�ɃX�|�[��
            GameObject spawnedObject =  Instantiate(getItemList[spawnedItemCount], targetPosition.position, targetPosition.rotation);

            spawnedObject.transform.SetParent(canvas.transform, false);
        
        // �X�|�[���ς݃A�C�e���̐����C���N�������g
        spawnedItemCount++;

            if(OnBar == true)
            {
               slider.value = getItemList.Count * 0.1f;//��ڂ̃I�u�W�F�N�g�͕K����\��
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

   
}
