using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NovelGame
{
    public class MainTextController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _mainTextObject;
        [SerializeField] TextMeshProUGUI _nameTextObject;
        int _displayedSentenceLength;
        int _sentenceLength;
        float _time;
        float _feedTime;

        // Start is called before the first frame update
        void Start()
        {
            _time = 0f;
            _feedTime = 0.05f;

            // �ŏ��̍s�̃e�L�X�g��\���A�܂��͖��߂����s
            string sentence = ScenarioGameManager.Instance.userScriptManager.GetCurrentSentence();
            bool isStatement = ScenarioGameManager.Instance.userScriptManager.IsStatement(sentence);
            DisplayText(sentence, isStatement);
            if (isStatement)
            {
                ScenarioGameManager.Instance.userScriptManager.ExecuteStatement(sentence);
            }
            //���݂̕��͂����͂Ȃ̂Ƀm�x�����[�h����Ȃ����
            else if (ScenarioGameManager.Instance.gameUpdateManager.getStateMode() != "novel")
            {
                ScenarioGameManager.Instance.gameUpdateManager.ToggleToNovelMode();
            }
        }

        // �m�x�����[�h����update
        public void novelUpdate()
        {

            // ���͂��P�������\������
            _time += Time.deltaTime;
            if (_time >= _feedTime)
            {
                _time -= _feedTime;
                if (!CanGoToTheNextLine())
                {
                    _displayedSentenceLength++;
                    _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;
                }
            }

            // �N���b�N���ꂽ�Ƃ��A���̍s�ֈړ�
            if (Input.GetMouseButtonUp(0))
            {
                if (CanGoToTheNextLine())
                {
                    GoToTheNextLine();
                }
                else
                {
                    _displayedSentenceLength = _sentenceLength;
                }
            }
        }

        // ���̍s�́A���ׂĂ̕������\������Ă��Ȃ���΁A�܂����̍s�֐i�ނ��Ƃ͂ł��Ȃ�
        public bool CanGoToTheNextLine()
        {
            string sentence = ScenarioGameManager.Instance.userScriptManager.GetCurrentSentence();
            string[] words = sentence.Split(',');
            string textsentence = words[2];
            _sentenceLength = textsentence.Length;
            return (_displayedSentenceLength > textsentence.Length);
        }

        // ���̍s�ֈړ�
        public void GoToTheNextLine()
        {

            _displayedSentenceLength = 0;
            _time = 0f;
            _mainTextObject.maxVisibleCharacters = 0;
            ScenarioGameManager.Instance.lineNumber++;
            string sentence = ScenarioGameManager.Instance.userScriptManager.GetCurrentSentence();
            bool isStatement = ScenarioGameManager.Instance.userScriptManager.IsStatement(sentence);

            DisplayText(sentence, isStatement);
            if (isStatement)
            {
                ScenarioGameManager.Instance.userScriptManager.ExecuteStatement(sentence);
            }
            //���݂̕��͂����͂Ȃ̂Ƀm�x�����[�h����Ȃ����
            else if (ScenarioGameManager.Instance.gameUpdateManager.getStateMode() != "novel")
            {
                ScenarioGameManager.Instance.gameUpdateManager.ToggleToNovelMode();
            }
        }


        // �e�L�X�g��\��
        public void DisplayText(string sentence, bool isStatement)
        {

            string[] words = sentence.Split(',');
            string namesentence = words[1];
            string textsentence = words[2];

            textsentence = textsentence.Replace("/", "\n");

           
            if (isStatement)
            {
                _mainTextObject.text = null;
                _nameTextObject.text = null;

            }
            else
            {
                _mainTextObject.text = textsentence;
                _nameTextObject.text = namesentence;
            }

        }
    }
}