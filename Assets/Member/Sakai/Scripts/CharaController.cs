using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharaController : MonoBehaviour
{
    //X�̏��
    float xLimit = 320f;
    public bool isDead = false;

    SampleSoundManager soundManager;
    [SerializeField]
    SceneManagement sceneManagement;

    [SerializeField]
    Ballgenerator ballgenerator;
    // RectTransform�̎Q��
    private RectTransform rectTransform;
    public Image rightImage;
    public Image leftImage;
    public Sprite pushRight;
    public Sprite pushLeft;
    public Sprite nomalRight;
    public Sprite nomalLeft;
    ObjectManager objectManager;
    void Start()
    {
        // RectTransform�R���|�[�l���g���擾
        rectTransform = GetComponent<RectTransform>();
        soundManager = FindObjectOfType<SampleSoundManager>();
        objectManager = FindObjectOfType<ObjectManager>();
    }
    private void OnEnable()
    {
        leftImage.sprite = nomalLeft;
        rightImage.sprite = nomalRight;
    }
    public void RbuttonClick()
    {
        SampleSoundManager.Instance.PlaySe(SeType.SE11);
        // �ʒu��ύX
        rectTransform.anchoredPosition += new Vector2(160, 0);
        if (soundManager != null)
        {
            SampleSoundManager.Instance.PlaySe(SeType.SE3);
        }
        rightImage.sprite = pushRight;

        StartCoroutine(RevertSpriteRight());
    }

    public void LButtonClick()
    {
        // �ʒu��ύX
        SampleSoundManager.Instance.PlaySe(SeType.SE11);
        rectTransform.anchoredPosition += new Vector2(-160, 0);
        if (soundManager != null)
        {
            SampleSoundManager.Instance.PlaySe(SeType.SE3);
        }
        leftImage.sprite = pushLeft;

        StartCoroutine(RevertSpriteLeft());
    }
    private IEnumerator RevertSpriteRight()
    {
        // �w�肵���b�������҂�
        yield return new WaitForSeconds(0.2f);

        // �X�v���C�g��nomalImage�ɖ߂�
        rightImage.sprite = nomalRight;
    }
    private IEnumerator RevertSpriteLeft()
    {
        // �w�肵���b�������҂�
        yield return new WaitForSeconds(0.2f);

        // �X�v���C�g��nomalImage�ɖ߂�
        leftImage.sprite = nomalLeft;
    }

    void Update()
    {
        // ���݂̈ʒu���擾
        Vector2 currentPos = rectTransform.anchoredPosition;

        // Mathf.Clamp��X�̒l��͈͓��Ɏ��߂�
        currentPos.x = Mathf.Clamp(currentPos.x, -xLimit, xLimit);

        // �ʒu���X�V
        rectTransform.anchoredPosition = currentPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ballprefab")) // �e�ƏՓ˂�����v���C���[�����ł�����
        {
            //GameOverScene���Ăяo��
            isDead = true;
            ballgenerator.ClearAllBalls();
            objectManager.OnMiniGame = false;
            objectManager.miniGameDead.SetActive(true);
            objectManager.miniGame.SetActive(false);
            SampleSoundManager.Instance.PlaySe(SeType.SE12);
         
            ////�Q�[�����̎��Ԃ��~�߂�
            //Time.timeScale = 0f;          
        }
    }
}
