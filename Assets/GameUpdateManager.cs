using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NovelGame
{
    public class GameUpdateManager : MonoBehaviour
    {
        //ゲームがどのような状態なのかを表現する変数stateMode
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
                //novelモードのとき、mainTextControllerのnovelUpdateを呼び出す
                GameManager.Instance.mainTextController.novelUpdate();
            }
            else
            { //waitモードなど他のモードのときは何もしない
                return;
            }
        }
        //ノベルモードにするメソッド
        public void ToggleToNovelMode()
        {
            stateMode = "novel";
        }

        //ウェイトモードにするメソッド
        public void ToggleToWaitMode()
        {
            stateMode = "wait";
        }

    }
}