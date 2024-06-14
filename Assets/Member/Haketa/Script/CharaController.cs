using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaController : MonoBehaviour
{
    //X�̏��
    float xLimit = 4f;
    public bool isDead = false;

 
    public void RbuttonClick()
    {
        transform.Translate(2, 0, 0);
        SampleSoundManager.Instance.PlaySe(SeType.SE3);
    }

    public void LButtonClick()
    {
        transform.Translate(-2, 0, 0);
        SampleSoundManager.Instance.PlaySe(SeType.SE3);
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
            //GameOverScene���Ăяo��
            isDead = true;
            SceneManagement.Instance.OnGameOver();
            gameObject.SetActive(false);

            ////�Q�[�����̎��Ԃ��~�߂�
            //Time.timeScale = 0f;          
        }
    }
}
