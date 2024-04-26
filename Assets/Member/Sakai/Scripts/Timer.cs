using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timeRemaining = 180f; // �^�C�}�[�̏����l��3���ɐݒ�

    private bool OneGameOver = false;
    [SerializeField]
    SceneManagement sceneManagement;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // �o�ߎ��Ԃ����Z
            DisplayTime(timeRemaining); // �c�莞�Ԃ�\��
        }
        else
        {
            timeRemaining = 0; // �^�C�}�[��0�ɂȂ�����A���Ԃ�0�ɐݒ�
            DisplayTime(timeRemaining); // �c�莞�Ԃ�\��
            if (OneGameOver == false)
            {
                sceneManagement.OnGameOver();
                OneGameOver = true;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // ���ƕb�ɕϊ�
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // �^�C�}�[�̕\���`����ݒ�i00:00�j
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}
