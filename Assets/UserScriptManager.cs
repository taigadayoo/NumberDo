using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace NovelGame
{
    // テキストファイルから文章を読み込み、ステートメントの実行を管理するクラス
    public class UserScriptManager : MonoBehaviour
    {
        [SerializeField] TextAsset _textFile; // テキストファイルを格納するための変数

        List<string> _sentences = new List<string>(); // 読み込んだ文章を格納するリスト
        public bool isWaiting = false; // ウェイト中かどうかのフラグ
                                       // スクリプトの初期化時に実行されるメソッド
        void Awake()
        {
            // テキストファイルから文章を一行ずつ読み込んでリストに格納する
            StringReader reader = new StringReader(_textFile.text);
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                _sentences.Add(line);
            }
        }

        // 現在の文章を取得するメソッド
        public string GetCurrentSentence()
        {
            return _sentences[GameManager.Instance.lineNumber];
        }

        // 命令文であるかどうかを判定するメソッド
        public bool IsStatement(string sentence)
        {
            string[] words = sentence.Split(','); // 文章を単語に分割する

            if (words[0] == "com") // A列目がcomの場合、命令とみなす
            {
                return true;
            }
            return false;
        }

        //文字列が数値なら数値に、そうでないならデフォルト値に設定するメソッド
        private int ConvertToInt(string str, int defaultNum = 0)
        {
            return int.TryParse(str, out int result) ? result : defaultNum;
        }


        // 命令を実行するメソッド
        public void ExecuteStatement(string sentence)
        {
            string[] words = sentence.Split(','); // 文章を単語に分割する

            // 単語によって処理を分岐する
            switch (words[1])
            {
                case "putImage": // putImageステートメントの場合
                    int img_x = ConvertToInt(words[5]);
                    int img_y = ConvertToInt(words[6]);
                    int scale_percent = ConvertToInt(words[7], 100);
                    GameManager.Instance.imageManager.PutImage(words[2], words[3], img_x, img_y, scale_percent); // 画像を表示する
                    GameManager.Instance.mainTextController.GoToTheNextLine(); //次の行に進む
                    break;
                default: //そうでない場合
                    break;
            }
        }
    }
}
