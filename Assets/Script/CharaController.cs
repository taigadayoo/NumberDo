using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaController : MonoBehaviour
{
    public void RbuttonClick()
    {
        transform.Translate(2, 0, 0);
    }

    public void LButtonClick()
    {
        transform.Translate(-2, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ballprefab")) // �e�ƏՓ˂�����v���C���[�����ł�����
        {
            gameObject.SetActive(false);

            //�Q�[�����̎��Ԃ��~�߂�
            Time.timeScale = 0f;

            //GameOverScene���Ăяo��
            SceneManager.LoadScene("GameOverScene");

            Debug.Log("�Q�[���I�[�o�[");
        }
    }
}
