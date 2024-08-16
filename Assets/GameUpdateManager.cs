using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NovelGame
{
    public class GameUpdateManager : MonoBehaviour
    {
        //�Q�[�����ǂ̂悤�ȏ�ԂȂ̂���\������ϐ�stateMode
        string stateMode;

        // Start is called before the first frame update
        void Start()
        {
            stateMode = "novel";
        }

        public string getStateMode()
        {
            return stateMode;
        }
        // Update is called once per frame
        void Update()
        {

            if (stateMode == "novel")
            {
                //novel���[�h�̂Ƃ��AmainTextController��novelUpdate���Ăяo��
                GameManager.Instance.mainTextController.novelUpdate();
            }
            else
            { //wait���[�h�ȂǑ��̃��[�h�̂Ƃ��͉������Ȃ�
                return;
            }
        }
        //�m�x�����[�h�ɂ��郁�\�b�h
        public void ToggleToNovelMode()
        {
            stateMode = "novel";
        }

        //�E�F�C�g���[�h�ɂ��郁�\�b�h
        public void ToggleToWaitMode()
        {
            stateMode = "wait";
        }

    }
}