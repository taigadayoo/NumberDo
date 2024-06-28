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

        SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
    }

    // Update is called once per frame
  public  void OnStart()
    {
            SampleSoundManager.Instance.StopBgm();
            sceneManagement.OnStart();
    }
    public void OnScenario()
    {
        SampleSoundManager.Instance.StopBgm();
        Debug.Log("�V�i���I�ł��B");
    }
    public void Exit()
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
    }
    void Update()
    {
        // �}�E�X�̈ʒu����Ray���΂��ďՓ˔���
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // �Փ˂����I�u�W�F�N�g�ɉ����ď������s��
            if (hitObject == start)
            {
                ChangeStart();
            }
            else if (hitObject == scenario)
            {
                ChangeScenario();
            }
            else if (hitObject == exit)
            {
                ChangeExit();
            }
        }
        else
        {
            // Ray������������Ȃ������ꍇ�̏���
            DefaultStart();
            DefaultScenario();
            DefaultExit();
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

