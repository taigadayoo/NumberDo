using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ballgenerator : MonoBehaviour
{
    public RectTransform[] positions; // �����_���ɑI�������ʒu�̔z��
    public GameObject BallPrefab; // ��������{�[���̃v���n�u
    private float delta = 0;
    public GameObject Balls;
    public float time = 1.3f; // �C���^�[�o������
    public TimeCounter timeCounter; // �J�E���g�_�E�����Ǘ�����X�N���v�g
    public Text countdownText; // UI��ŃJ�E���g�_�E����\������Text
    public int countdownTime = 3; // �J�E���g�_�E���̊J�n�l

    private List<GameObject> ballList = new List<GameObject>();
    private void Start()
    {
        StartCoroutine(StartCountdown());
      
    }
    private void OnEnable()
    {
        StartCoroutine(StartCountdown());
    }
    void Update()
    {
        StartCoroutine(MiniGameStart());
       
    }
    private IEnumerator StartCountdown()
    {
        int currentTime = countdownTime;

        while (currentTime > 0)
        {
            // ���݂̃J�E���g��\��
            countdownText.text = currentTime.ToString();

            // 1�b�҂�
            yield return new WaitForSeconds(1f);

            // �J�E���g�����炷
            currentTime--;
        }


        countdownText.text = "";
    }
    private IEnumerator MiniGameStart()
    {
      
        yield return new WaitForSeconds(3);

        this.delta += Time.deltaTime;

        // �C���^�[�o�����Ԃ𒴂����ꍇ
        if (this.delta > this.time)
        {
            // �J�E���g�_�E����1�ȏ�̏ꍇ
            if (1 <= timeCounter.countdown)
            {
                // ���Ԃ����Z�b�g
                this.delta = 0;

                // �����_���Ɉʒu��I��
                int randomIndex = Random.Range(0, positions.Length);
                Vector2 randomPosition = positions[randomIndex].anchoredPosition;

                // �{�[���̃C���X�^���X�𐶐�
                GameObject ball = Instantiate(BallPrefab, Balls.transform);
                RectTransform ballRectTransform = ball.GetComponent<RectTransform>();

                // ���������{�[���̈ʒu��ݒ�
                ballRectTransform.anchoredPosition = randomPosition;
                ballList.Add(ball);
            }
        }
    }
    public void ClearAllBalls()
    {
        foreach (GameObject ball in ballList)
        {
            Destroy(ball);
        }

        // ���X�g���N���A
        ballList.Clear();
    }
}

