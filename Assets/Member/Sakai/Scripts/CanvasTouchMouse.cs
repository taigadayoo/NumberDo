using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasTouchMouse : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    public bool isCheck = false;

    private GameObject clickBomb;
    private bool isKeySelected = false;
    public bool isLightSelected = false;
    public bool isCandleSelected = false;
    public bool isKnifeSelected = false;
    public bool isKeyDoorSelected = false;

    // ����̃I�u�W�F�N�g�����ʂ��邽�߂̃^�O
    public string keyTag = "Key";
    public string bombTag = "Bomb";
    public string rockerTag = "Rocker";
    public string LightTag = "BlackLight";
    public string CandleFireTag = "candleFire";
    public string KnifeTag = "candleKnife";
    public string PictureTag = "Picture";
    public string keyDoorTag = "KeyDoor";
    public CheckBool lastClickedObject;
    public CheckBool previousClickedObject; // �ЂƂO�ɃN���b�N���ꂽ�I�u�W�F�N�g


    SceneManagement sceneManagement;
    ItemLight itemLight;
    ItemLight itemNextLight;
    [SerializeField]
    RockerScripts rockerScripts;
    // �N���b�N���ꂽ�Ō��2�̃I�u�W�F�N�g���i�[���郊�X�g
    private List<CheckBool> clickedObjects = new List<CheckBool>();

    void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
        if (raycaster == null)
        {
            raycaster = GetComponent<GraphicRaycaster>();
        }
        if (eventSystem == null)
        {
            eventSystem = FindObjectOfType<EventSystem>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Input.mousePosition;

            // �}�E�X�̃|�C���^�ʒu����Ray���΂�
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            // ���ׂĂ�UI�v�f���i�[���郊�X�g
            List<RaycastResult> results = new List<RaycastResult>();

            // Raycast�����s
            raycaster.Raycast(pointerEventData, results);

            // �q�b�g����UI�v�f���`�F�b�N
            foreach (RaycastResult result in results)
            {
                GameObject hitObject = result.gameObject;
                CheckBool clickableObject = hitObject.GetComponent<CheckBool>();

                if (clickableObject != null)
                {
                    itemNextLight = clickableObject.GetComponent<ItemLight>();

                    // �����I�u�W�F�N�g���ēx�N���b�N���ꂽ�ꍇ
                    if (clickableObject == lastClickedObject)
                    {
                        // �I��������
                        clickableObject.isCheck = false;
                        itemNextLight.ChangeNomal();
                        Debug.Log("Deselected: " + hitObject.name);

                        // lastClickedObject��previousClickedObject�����Z�b�g
                        lastClickedObject = null;
                        if (previousClickedObject == clickableObject)
                        {
                            previousClickedObject = null;
                        }
                        else if (previousClickedObject != null)
                        {
                            // ��O�̃I�u�W�F�N�g�͑I�����ꂽ�܂�
                            lastClickedObject = previousClickedObject;
                            itemLight = previousClickedObject.GetComponent<ItemLight>();
                            itemLight.ChangeLight();
                        }
                    }
                    else
                    {
                        // �O�ɃN���b�N���ꂽ�I�u�W�F�N�g�̏���
                        if (previousClickedObject != null)
                        {
                            itemLight = previousClickedObject.GetComponent<ItemLight>();
                            previousClickedObject.isCheck = false;
                            itemLight.ChangeNomal();
                        }

                        // ���݂̃I�u�W�F�N�g��isCheck��true�ɐݒ肵�AChangeLight���Ăяo���ăX�v���C�g�����ւ���
                        clickableObject.isCheck = true;
                        itemNextLight.ChangeLight();
                        //SampleSoundManager.Instance.PlaySe(SeType.SE1);

                        // lastClickedObject��previousClickedObject�Ɉڂ�
                        previousClickedObject = lastClickedObject;

                        // ���݂̃I�u�W�F�N�g��lastClickedObject�Ƃ��ċL��
                        lastClickedObject = clickableObject;

                        // �N���b�N���ꂽ�Ō��2�̃I�u�W�F�N�g���Ǘ�
                        clickedObjects.Add(clickableObject);
                        if (clickedObjects.Count > 2)
                        {
                            clickedObjects.RemoveAt(0);
                        }
                    }
                    isKeySelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(keyTag));
                    isLightSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(LightTag));
                    isCandleSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(CandleFireTag));
                    isKnifeSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(KnifeTag));
                    isKeyDoorSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(keyDoorTag));


                
                }

                break; // �ŏ��̃q�b�g����UI�v�f�̂ݏ���
            }

            // �V�[����̃I�u�W�F�N�g�����o
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag(rockerTag))
            {
                // �{�����}�E�X�ŃN���b�N���ꂽ�ꍇ�̏���
                if (isKeySelected)
                {
                    rockerScripts.OpenRocker();
                }
                isKeySelected = false;
            }
            //if (hit.collider != null && hit.collider.CompareTag(PictureTag))
            //{
            //    // �{�����}�E�X�ŃN���b�N���ꂽ�ꍇ�̏���
            //    if (isLightSelected)
            //    {
            //        Debug.Log("����̓��C�g�ł��B");
            //    }
            //    isLightSelected = false;
            //}
        }
    }
}