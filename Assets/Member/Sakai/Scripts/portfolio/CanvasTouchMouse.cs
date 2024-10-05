using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasTouchMouse : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    public bool isCheck = false;

    private GameObject clickBomb;
    public bool isKeySelected = false;
    public bool isLightSelected = false;
    public bool isCandleSelected = false;
    public bool isKnifeSelected = false;
    public bool isNomalKnifeSelected = false;
    public bool isKeyDoorSelected = false;
    public bool isCandle2Selected = false;

    // ����̃I�u�W�F�N�g�����ʂ��邽�߂̃^�O
    public string keyTag = "Key";
    public string bombTag = "Bomb";
    public string rockerTag = "Rocker";
    public string LightTag = "BlackLight";
    public string CandleFireTag = "candleFire";
    public string KnifeTag = "candleKnife";
    public string nomalKnifeTag = "knife";
    public string PictureTag = "Picture";
    public string keyDoorTag = "KeyDoor";
    public string CandleTag = "CandleFire2";
    public CheckBool lastClickedObject;
    public CheckBool previousClickedObject; // �ЂƂO�ɃN���b�N���ꂽ�I�u�W�F�N�g

    SceneManagement sceneManagement;
    ItemLight itemLight;
    ItemLight itemNextLight;
    [SerializeField]
    RockerScripts rockerScripts;

    // �N���b�N���ꂽ�Ō��2�̃I�u�W�F�N�g���i�[���郊�X�g
    private List<CheckBool> clickedObjects = new List<CheckBool>();
    MixImageScripts imageScripts;

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
        imageScripts = FindObjectOfType<MixImageScripts>();
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
                        imageScripts.foundMatch = false; // ����̑I�����O�ꂽ�ꍇ��false�ɐݒ�

                        imageScripts.mixImage.enabled = false;
                        // �I��������
                        clickableObject.isCheck = false;
                        itemNextLight.ChangeNomal();
                        Debug.Log("Deselected: " + hitObject.name);

                        // �����I�u�W�F�N�g���O��N���b�N���Ă�previousClickedObject��null�ɂȂ�Ȃ��悤�ɂ���
                        if (previousClickedObject != null)
                        {
                            // �O�̃I�u�W�F�N�g���ĂёI����Ԃɂ��Č��点��
                            lastClickedObject = previousClickedObject;
                            itemLight = previousClickedObject.GetComponent<ItemLight>();
                            itemLight.ChangeLight();
                            previousClickedObject = null; // ��x���Z�b�g
                        }
                        else
                        {
                            lastClickedObject = null; // �I�������S�ɉ���
                        }
                    }
                    else
                    {
                        // �O�̃I�u�W�F�N�g���I������Ă����ꍇ�͑I������
                        if (previousClickedObject != null && previousClickedObject != clickableObject)
                        {
                            imageScripts.foundMatch = false; // ����̑I�����O�ꂽ�ꍇ��false�ɐݒ�
                            imageScripts.mixImage.enabled = false;
                            itemLight = previousClickedObject.GetComponent<ItemLight>();
                            previousClickedObject.isCheck = false;
                            itemLight.ChangeNomal();
                        }

                        // ���݂̃I�u�W�F�N�g��isCheck��true�ɐݒ肵�AChangeLight���Ăяo��
                        clickableObject.isCheck = true;
                        itemNextLight.ChangeLight();

                        // lastClickedObject��previousClickedObject�Ɉڂ�
                        previousClickedObject = lastClickedObject;

                        // ���݂̃I�u�W�F�N�g��lastClickedObject�ɃZ�b�g
                        lastClickedObject = clickableObject;

                        // clickedObjects���X�g���X�V
                        clickedObjects.Add(clickableObject);
                        if (clickedObjects.Count > 2)
                        {
                            clickedObjects.RemoveAt(0); // 2�܂ŕێ�
                        }
                    }

                    // �^�O�Ɋ�Â��ăt���O���X�V
                    isKeySelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(keyTag));
                    isLightSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(LightTag));
                    isCandleSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(CandleFireTag));
                    isKnifeSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(KnifeTag));
                    isKeyDoorSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(keyDoorTag));
                    isNomalKnifeSelected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(nomalKnifeTag));
                    isCandle2Selected = clickedObjects.Exists(obj => obj != null && obj.CompareTag(CandleTag));
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
                    SceneManager.LoadScene("TutorialScenarioScene2");
                }
                isKeySelected = false;
            }
        }
    }
}