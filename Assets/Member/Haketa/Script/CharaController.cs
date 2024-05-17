using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaController : MonoBehaviour
{
    //X�̏��
    float xLimit = 7f;

    public bool isDead = false;
   [SerializeField]
    SceneManagement sceneManagement;
    public void RbuttonClick()
    {
        transform.Translate(1, 0, 0);
    }

    public void LButtonClick()
    {
        transform.Translate(-1, 0, 0);
    }

    void Update()
    {
        //if (sceneManagement == null)
        //{
        //    sceneManagement = FindObjectOfType<SceneManagement>();
        //}
        //���݂̃|�W�V������ێ�����
        Vector3 currentPos = this.transform.position;

        //Mathf.Clamp��X,Y�̒l���ꂼ�ꂪ�ŏ��`�ő�͈͓̔��Ɏ��߂�B
        //�͈͂𒴂��Ă�����͈͓��̒l��������
        currentPos.x = Mathf.Clamp(currentPos.x, -xLimit, xLimit);

        //position��currentPos�ɂ���
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
            isDead = true;

            Debug.Log("�Q�[���I�[�o�[");
        }
    }
}
