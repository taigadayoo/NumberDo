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
            string statement = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if (GameManager.Instance.userScriptManager.IsStatement(statement))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(statement);
            }
            // 最初の行のテキストを表示
            DisplayText();
        }

        // Update is called once per frame
        void Update()
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
            //現在の文を定義し、それが命令文であればそれを実行する
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if (GameManager.Instance.userScriptManager.IsStatement(sentence))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(sentence);
            }
        }


        // テキストを表示
        public void DisplayText()
        {
            //現在の文章をuserScriptManagerのGetCurrentSentenceで引っ張ってくる
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            //カンマ区切りで名前と本文をわける
            string[] words = sentence.Split(',');

            //1番が名前、2番が本文
            string namesentence = words[1];
            string textsentence = words[2];
            //それぞれテキストオブジェクトに代入する
            _mainTextObject.text = textsentence;
            _nameTextObject.text = namesentence;
        }
    }
}