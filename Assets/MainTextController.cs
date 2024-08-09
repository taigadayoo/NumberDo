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
            string statement = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if (GameManager.Instance.userScriptManager.IsStatement(statement))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(statement);
            }
            // �ŏ��̍s�̃e�L�X�g��\��
            DisplayText();
        }

        // Update is called once per frame
        void Update()
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
            if (Input.GetMouseButtonUp(0))
            {
                GoToTheNextLine();
                DisplayText();

            }
            else
            {
                _displayedSentenceLength = _sentenceLength;
            }
        }

        // ���̍s�́A���ׂĂ̕������\������Ă��Ȃ���΁A�܂����̍s�֐i�ނ��Ƃ͂ł��Ȃ�
        public bool CanGoToTheNextLine()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
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
            GameManager.Instance.lineNumber++;
            //���݂̕����`���A���ꂪ���ߕ��ł���΂�������s����
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if (GameManager.Instance.userScriptManager.IsStatement(sentence))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(sentence);
            }
        }


        // �e�L�X�g��\��
        public void DisplayText()
        {
            //���݂̕��͂�userScriptManager��GetCurrentSentence�ň��������Ă���
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            //�J���}��؂�Ŗ��O�Ɩ{�����킯��
            string[] words = sentence.Split(',');

            //1�Ԃ����O�A2�Ԃ��{��
            string namesentence = words[1];
            string textsentence = words[2];
            //���ꂼ��e�L�X�g�I�u�W�F�N�g�ɑ������
            _mainTextObject.text = textsentence;
            _nameTextObject.text = namesentence;
        }
    }
}