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

    // ����̃I�u�W�F�N�g�����ʂ��邽�߂̃^�O
    public string keyTag = "Key";
    public string bombTag = "Bomb";
    private CheckBool lastClickedObject;
    SceneManagement sceneManagement;
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
                    // �O��N���b�N���ꂽ�I�u�W�F�N�g��isCheck��false�ɐݒ肵�A�A���t�@�l�����ɖ߂�
                    if (lastClickedObject != null && lastClickedObject != clickableObject)
                    {
                        lastClickedObject.isCheck = false;
                        SetAlpha(lastClickedObject.gameObject, 0.5f); // �A���t�@�l�����ɖ߂�
                    }

                    // ���݂̃I�u�W�F�N�g��isCheck��true�ɐݒ肵�A�A���t�@�l�𔼓����ɂ���
                    clickableObject.isCheck = true;
                    SetAlpha(hitObject, 1f); // �A���t�@�l�𔼓����ɐݒ�
                    SampleSoundManager.Instance.PlaySe(SeType.SE1);
                    // ���݂̃I�u�W�F�N�g��lastClickedObject�Ƃ��ċL��
                    lastClickedObject = clickableObject;
                    if (hitObject.CompareTag(keyTag))
                    {
                        isKeySelected = true;
                    }
                }


                break;
            }
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag(bombTag))
            {
                // �{�����}�E�X�ŃN���b�N���ꂽ�ꍇ�̏���
                if (isKeySelected)
                {
                    sceneManagement.OnClear();
                }
                isKeySelected = false;
            }
        }
    }
    private void SetAlpha(GameObject obj, float alpha)
    {
        Image image = obj.GetComponent<Image>();
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �h�A���N���b�N����A�J�M���I������Ă��āA�h�A�ɐG�ꂽ�ꍇ
        if (clickBomb != null && lastClickedObject != null && lastClickedObject.gameObject.CompareTag(keyTag) && other.gameObject == clickBomb)
        {
            // �h�A�ɐG�ꂽ�ۂɃV�[����ύX���鏈��
            Debug.Log("Door unlocked with key! Changing scene...");
          
        }
    }

}
