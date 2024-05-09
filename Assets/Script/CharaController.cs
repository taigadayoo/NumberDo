using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaController : MonoBehaviour
{
    //X�̏��
    float xLimit = 5.5f;

    public void RbuttonClick()
    {
        transform.Translate(2, 0, 0);
    }

    public void LButtonClick()
    {
        transform.Translate(-2, 0, 0);
    }

    void Update()
    {
        //�ǉ��@���݂̃|�W�V������ێ�����
        Vector3 currentPos = transform.position;

        //�ǉ��@Mathf.Clamp��X,Y�̒l���ꂼ�ꂪ�ŏ��`�ő�͈͓̔��Ɏ��߂�B
        //�͈͂𒴂��Ă�����͈͓��̒l��������
        currentPos.x = Mathf.Clamp(currentPos.x, -xLimit, xLimit);

        //�ǉ��@position��currentPos�ɂ���
        transform.position = currentPos;
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
