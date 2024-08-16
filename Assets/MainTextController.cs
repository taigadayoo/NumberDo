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

            // 最初の行のテキストを表示、または命令を実行
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            bool isStatement = GameManager.Instance.userScriptManager.IsStatement(sentence);
            DisplayText(sentence, isStatement);
            if (isStatement)
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(sentence);
            }
            //現在の文章が文章なのにノベルモードじゃなければ
            else if (GameManager.Instance.gameUpdateManager.getStateMode() != "novel")
            {
                GameManager.Instance.gameUpdateManager.ToggleToNovelMode();
            }
        }

        // ノベルモード時のupdate
        public void novelUpdate()
        {

            // 文章を１文字ずつ表示する
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

            // クリックされたとき、次の行へ移動
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

        // その行の、すべての文字が表示されていなければ、まだ次の行へ進むことはできない
        public bool CanGoToTheNextLine()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            string[] words = sentence.Split(',');
            string textsentence = words[2];
            _sentenceLength = textsentence.Length;
            return (_displayedSentenceLength > textsentence.Length);
        }

        // 次の行へ移動
        public void GoToTheNextLine()
        {

            _displayedSentenceLength = 0;
            _time = 0f;
            _mainTextObject.maxVisibleCharacters = 0;
            GameManager.Instance.lineNumber++;
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            bool isStatement = GameManager.Instance.userScriptManager.IsStatement(sentence);

            DisplayText(sentence, isStatement);
            if (isStatement)
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(sentence);
            }
            //現在の文章が文章なのにノベルモードじゃなければ
            else if (GameManager.Instance.gameUpdateManager.getStateMode() != "novel")
            {
                GameManager.Instance.gameUpdateManager.ToggleToNovelMode();
            }
        }


        // テキストを表示
        public void DisplayText(string sentence, bool isStatement)
        {

            string[] words = sentence.Split(',');

            string namesentence = words[1];
            string textsentence = words[2];
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