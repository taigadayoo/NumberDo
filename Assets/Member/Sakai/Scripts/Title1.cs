
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Title1 : MonoBehaviour
{
    [SerializeField]
    SceneManagement sceneManagement;

    public GameObject end1;
    public GameObject end2;
    public GameObject main;
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject back;
    public GameObject trueEnd;
    public GameObject panel;

    public List<Sprite> touchImage = new List<Sprite>();

    public List<Sprite> defaultImage = new List<Sprite>();

    private Image end1Image;
    private Image end2Image;
    private Image mainImage;
    private Image tutoroal1Image;
    private Image tutoroal2Image;
    private Image backImage;
    private Image trueImage;

    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    private GameObject previousHoverEnd1 = null;
    private GameObject previousHoverEnd2 = null;
    private GameObject previousHoverMain = null;
    private GameObject previousHoverTutorial1 = null;
    private GameObject previousHoverTutorial2 = null;
    private GameObject previousHoverBack = null;
    private GameObject previousHovertrue = null;
    [SerializeField]
    TitleAnim anim;
    SampleSoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        end1Image = end1.GetComponent<Image>();
        end2Image = end2.GetComponent<Image>();
        mainImage = main.GetComponent<Image>();
        tutoroal1Image = tutorial1.GetComponent<Image>();
        tutoroal2Image = tutorial2.GetComponent<Image>();
        backImage = back.GetComponent<Image>();
        trueImage = trueEnd.GetComponent<Image>();
        soundManager = FindObjectOfType<SampleSoundManager>();
        // �����摜��ݒ肷��
        DefaultEnd1();
        DefaultEnd2();
        DefaultMain();
        DefaultTutorial1();
        DefaultTutorial2();
        DefaultBack();
        DefaultTrue();
        panel.SetActive(false);
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();

        SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
    }

   public void OnEnd1()
    {

        soundManager.PlaySe(SeType.SE6);
        SampleSoundManager.Instance.StopBgm();
        sceneManagement.OnGameOver();
        panel.SetActive(true);
    }
     public void OnEnd2()
    {
        soundManager.PlaySe(SeType.SE6);
        SampleSoundManager.Instance.StopBgm();
        sceneManagement.OnGameOver2();
        panel.SetActive(true);
    }
     public void OnMainGame()
    {
        soundManager.PlaySe(SeType.SE6);
        SampleSoundManager.Instance.StopBgm();
        SampleSoundManager.Instance.PlayBgm(BgmType.BGM3); 
        sceneManagement.OnMainGame();
        panel.SetActive(true);
    }
     public void OnTutorial1()
    {
        soundManager.PlaySe(SeType.SE6);

        SampleSoundManager.Instance.StopBgm();
        sceneManagement.OnAttention();
        panel.SetActive(true);
    }
    public void OnTutorial2()
    {
        soundManager.PlaySe(SeType.SE6);
        SampleSoundManager.Instance.StopBgm();
        sceneManagement.OnMainGameMove();
        panel.SetActive(true);
    }
     public void OnTitle()
    {
        SampleSoundManager.Instance.StopBgm();
        sceneManagement.OnTitle();
        soundManager.PlaySe(SeType.SE6);
        panel.SetActive(true);
    }
    public void OnTrueEnd()
    {
        SampleSoundManager.Instance.StopBgm();
        sceneManagement.OnClear();
        soundManager.PlaySe(SeType.SE6);
        panel.SetActive(true);
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

        bool isHoveringEnd1 = false;
        bool isHoveringEnd2 = false;
        bool isHoveringMain = false;
        bool isHoveringTutorial1 = false;
        bool isHoveringTutorial2 = false;
        bool isHoveringBack = false;
        bool isHoveringTrue = false;

        // Raycast���ʂ��������ꍇ
        if (results.Count > 0)
        {
            foreach (var result in results)
            {
                GameObject hitObject = result.gameObject;

                // �e�I�u�W�F�N�g�ɑ΂��鏈��
                if (hitObject == end1)
                {
                    isHoveringEnd1 = true;
                    if (hitObject != previousHoverEnd1)
                    {
                        ChangeEnd1();
                        previousHoverEnd1 = hitObject;
                        soundManager.PlaySe(SeType.SE7);
                    }
                }
                else if (hitObject == end2)
                {
                    isHoveringEnd2 = true;
                    if (hitObject != previousHoverEnd2)
                    {
                        soundManager.PlaySe(SeType.SE7);
                        ChangeEnd2();
                        previousHoverEnd2 = hitObject;

                    }
                }
                else if (hitObject == main)
                {
                    isHoveringMain = true;
                    if (hitObject != previousHoverMain)
                    {
                        ChangeMain();
                        previousHoverMain = hitObject;
                        soundManager.PlaySe(SeType.SE7);
                    }
                }
                else if (hitObject == tutorial1)
                {
                    isHoveringTutorial1 = true;
                    if (hitObject != previousHoverTutorial1)
                    {
                        ChangeTutorial1();
                        previousHoverTutorial1 = hitObject;
                        soundManager.PlaySe(SeType.SE7);
                    }
                }
                else if (hitObject == tutorial2)
                {
                    isHoveringTutorial2 = true;
                    if (hitObject != previousHoverTutorial2)
                    {
                        ChangeTutorial2();
                        previousHoverTutorial2 = hitObject;
                        soundManager.PlaySe(SeType.SE7);
                    }
                }
                else if (hitObject == back)
                {
                    isHoveringBack = true;
                    if (hitObject != previousHoverBack)
                    {
                        ChangeBack();
                        previousHoverBack = hitObject;
                        soundManager.PlaySe(SeType.SE7);
                    }
                }
                else if (hitObject == trueEnd)
                {
                    isHoveringTrue = true;
                    if (hitObject != previousHovertrue)
                    {
                        ChangeTrue();
                        previousHovertrue = hitObject;
                        soundManager.PlaySe(SeType.SE7);
                    }
                }
            }
        }
        if (!isHoveringEnd1 && previousHoverEnd1 != null)
        {
            DefaultEnd1();
            previousHoverEnd1 = null;
        }
        if (!isHoveringEnd2 && previousHoverEnd2 != null)
        {
            DefaultEnd2();
            previousHoverEnd2 = null;
        }
        if (!isHoveringMain && previousHoverMain != null)
        {
            DefaultMain();
            previousHoverMain = null;
        }
        if (!isHoveringTutorial1 && previousHoverTutorial1 != null)
        {
            DefaultTutorial1();
            previousHoverTutorial1 = null;
        }
        if (!isHoveringTutorial2 && previousHoverTutorial2 != null)
        {
            DefaultTutorial2();
            previousHoverTutorial2 = null;
        }
        if (!isHoveringBack && previousHoverBack != null)
        {
            DefaultBack();
            previousHoverBack = null;
        }
        if (!isHoveringTrue && previousHovertrue != null)
        {
            DefaultTrue();
            previousHovertrue = null;
        }
    }

    // �Ώۂ�GameObject�ɑ΂��鏈��
    private void ChangeEnd1()
    {
        if (end1Image != null && touchImage.Count > 0)
        {
            end1Image.sprite = touchImage[0]; // touchImage���X�g�̍ŏ��̗v�f��ݒ�
        }
    }

    private void ChangeEnd2()
    {
        if (end2Image != null && touchImage.Count > 1)
        {
            end2Image.sprite = touchImage[1]; // touchImage���X�g��2�Ԗڂ̗v�f��ݒ�
        }
    }

    private void ChangeMain()
    {
        if (main != null && touchImage.Count > 2)
        {
            mainImage.sprite = touchImage[2]; // touchImage���X�g��3�Ԗڂ̗v�f��ݒ�
        }
    }
    private void ChangeTutorial1()
    {
        if (tutorial1 != null && touchImage.Count > 3)
        {
            tutoroal1Image.sprite = touchImage[3]; // touchImage���X�g��3�Ԗڂ̗v�f��ݒ�
        }
    }
    private void ChangeTutorial2()
    {
        if (tutorial2 != null && touchImage.Count > 4)
        {
            tutoroal2Image.sprite = touchImage[4]; // touchImage���X�g��3�Ԗڂ̗v�f��ݒ�
        }
    }
    private void ChangeBack()
    {
        if (back != null && touchImage.Count > 5)
        {
            backImage.sprite = touchImage[5]; // touchImage���X�g��3�Ԗڂ̗v�f��ݒ�
        }
    }
    private void ChangeTrue()
    {
        if (trueEnd != null && touchImage.Count > 6)
        {
            trueImage.sprite = touchImage[6]; // touchImage���X�g��3�Ԗڂ̗v�f��ݒ�
        }
    }
    private void DefaultEnd1()
    {
        if (end1Image != null && defaultImage.Count > 0)
        {
            end1Image.sprite = defaultImage[0]; //defaultImage���X�g�̍ŏ��̗v�f��ݒ�
        }
    }

    private void DefaultEnd2()
    {
        if (end2Image != null && defaultImage.Count > 1)
        {
            end2Image.sprite = defaultImage[1]; // defaultImage���X�g��2�Ԗڂ̗v�f��ݒ�
        }
    }

    private void DefaultMain()
    {
        if (mainImage != null && defaultImage.Count > 2)
        {
            mainImage.sprite = defaultImage[2]; // defaultImage���X�g��3�Ԗڂ̗v�f��ݒ�
        }
    }
    private void DefaultTutorial1()
    {
        if (tutoroal1Image != null && defaultImage.Count > 3)
        {
            tutoroal1Image.sprite = defaultImage[3]; // defaultImage���X�g��3�Ԗڂ̗v�f��ݒ�
        }
    }
    private void DefaultTutorial2()
    {
        if (tutoroal2Image != null && defaultImage.Count > 4)
        {
            tutoroal2Image.sprite = defaultImage[4]; // defaultImage���X�g��3�Ԗڂ̗v�f��ݒ�
        }
    }
        private void DefaultBack()
        {
            if (backImage != null && defaultImage.Count > 5)
            {
                backImage.sprite = defaultImage[5]; // defaultImage���X�g��3�Ԗڂ̗v�f��ݒ�
            }
        }
    private void DefaultTrue()
    {
        if (trueImage != null && defaultImage.Count > 6)
        {
            trueImage.sprite = defaultImage[6]; // defaultImage���X�g��3�Ԗڂ̗v�f��ݒ�
        }
    }
}


