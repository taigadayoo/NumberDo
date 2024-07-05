
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField]
    SceneManagement sceneManagement;

    public GameObject start;
    public GameObject scenario;
    public GameObject exit;

    public List<Sprite> touchImage = new List<Sprite>();

    public List<Sprite> defaultImage = new List<Sprite>();

    private Image startImage;
    private Image scenarioImage;
    private Image exitImage;

    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    private GameObject previousHoverStart = null;
    private GameObject previousHoverScenario = null;
    private GameObject previousHoverExit = null;
    [SerializeField]
    TitleAnim anim;

    // Start is called before the first frame update
    void Start()
    {
        startImage = start.GetComponent<Image>();
        scenarioImage = scenario.GetComponent<Image>();
        exitImage = exit.GetComponent<Image>();

        // �����摜��ݒ肷��
        DefaultStart();
        DefaultScenario();
        DefaultExit();

        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();

        SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
    }

  async  public void OnStart()
    {
        await anim.TitleAnimation();
        SampleSoundManager.Instance.StopBgm();
        sceneManagement.OnStart();
        
    }
   async public void OnScenario()
    {
        SampleSoundManager.Instance.StopBgm();
        Debug.Log("�V�i���I�ł��B");
        await anim.TitleAnimation();
    }
   async public void Exit()
    {
        {
            SampleSoundManager.Instance.StopBgm();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        // �r���h���ꂽ�Q�[���Ŏ��s�����R�[�h
        Application.Quit();
        #endif
        }
        await anim.TitleAnimation();
    }

   async void Update()
    {
        // Raycast�̌��ʂ�ۑ����郊�X�g
        List<RaycastResult> results = new List<RaycastResult>();

        // �}�E�X�̈ʒu����Raycast���s��
        pointerEventData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        raycaster.Raycast(pointerEventData, results);

        bool isHoveringStart = false;
        bool isHoveringScenario = false;
        bool isHoveringExit = false;

        // Raycast���ʂ��������ꍇ
        if (results.Count > 0)
        {
            foreach (var result in results)
            {
                GameObject hitObject = result.gameObject;

                // �e�I�u�W�F�N�g�ɑ΂��鏈��
                if (hitObject == start)
                {
                    isHoveringStart = true;
                    if (hitObject != previousHoverStart)
                    {
                        ChangeStart();
                        previousHoverStart = hitObject;
                       
                    }
                }
                else if (hitObject == scenario)
                {
                    isHoveringScenario = true;
                    if (hitObject != previousHoverScenario)
                    {
                        ChangeScenario();
                        previousHoverScenario = hitObject;
                       
                    }
                }
                else if (hitObject == exit)
                {
                    isHoveringExit = true;
                    if (hitObject != previousHoverExit)
                    {
                        ChangeExit();
                        previousHoverExit = hitObject;
                       
                    }
                }
            }
        }
        if (!isHoveringStart && previousHoverStart != null)
        {
            DefaultStart();
            previousHoverStart = null;
        }
        if (!isHoveringScenario && previousHoverScenario != null)
        {
            DefaultScenario();
            previousHoverScenario = null;
        }
        if (!isHoveringExit && previousHoverExit != null)
        {
            DefaultExit();
            previousHoverExit = null;
        }
    }

    // �Ώۂ�GameObject�ɑ΂��鏈��
    private void ChangeStart()
    {
        if (startImage != null && touchImage.Count > 0)
        {
            startImage.sprite = touchImage[0]; // touchImage���X�g�̍ŏ��̗v�f��ݒ�
        }
    }

    private void ChangeScenario()
    {
        if (scenarioImage != null && touchImage.Count > 1)
        {
            scenarioImage.sprite = touchImage[1]; // touchImage���X�g��2�Ԗڂ̗v�f��ݒ�
        }
    }

    private void ChangeExit()
    {
        if (exitImage != null && touchImage.Count > 2)
        {
            exitImage.sprite = touchImage[2]; // touchImage���X�g��3�Ԗڂ̗v�f��ݒ�
        }
    }

    private void DefaultStart()
    {
        if (startImage != null && defaultImage.Count > 0)
        {
            startImage.sprite = defaultImage[0]; //defaultImage���X�g�̍ŏ��̗v�f��ݒ�
        }
    }

    private void DefaultScenario()
    {
        if (scenarioImage != null && defaultImage.Count > 1)
        {
            scenarioImage.sprite = defaultImage[1]; // defaultImage���X�g��2�Ԗڂ̗v�f��ݒ�
        }
    }

    private void DefaultExit()
    {
        if (exitImage != null && defaultImage.Count > 2)
        {
            exitImage.sprite = defaultImage[2]; // defaultImage���X�g��3�Ԗڂ̗v�f��ݒ�
        }
    }
}

