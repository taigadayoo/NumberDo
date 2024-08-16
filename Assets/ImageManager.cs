using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NovelGame
{
    public class ImageManager : MonoBehaviour
    {
        [SerializeField] Sprite _background1;
        [SerializeField] GameObject _backgroundObject;
        [SerializeField] GameObject _eventObject;
        [SerializeField] GameObject _imagePrefab;

        // テキストファイルから、文字列でSpriteやGameObjectを扱えるようにするための辞書
        Dictionary<string, Sprite> _textToSprite;
        Dictionary<string, GameObject> _textToParentObject;

        // 各画像のGameObjectを管理するためのリスト
        List<(string, GameObject, int)> _textToSpriteObject;

        void Awake()
        {
            //テキスト→スプライトの初期化と画像の追加
            _textToSprite = new Dictionary<string, Sprite>();
            _textToSprite.Add("background1", _background1);

            //テキスト→親オブジェクト(backgroundかeventか)の追加
            _textToParentObject = new Dictionary<string, GameObject>();
            _textToParentObject.Add("backgroundObject", _backgroundObject);
            _textToParentObject.Add("eventObject", _eventObject);

            _textToSpriteObject = new List<(string, GameObject, int)>();
        }


        // 画像を配置する
        public void PutImage(string imageName, string parentObjectName, int img_x = 0, int img_y = 0, int scale_percent = 100)
        {
            //画像を取得
            Sprite image = _textToSprite[imageName];
            GameObject parentObject = _textToParentObject[parentObjectName];


            // 新しい画像を生成する
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;

            // 画像のGameObjectを生成
            GameObject item = Instantiate(_imagePrefab, parent);
            item.GetComponent<Image>().sprite = image;
            RectTransform rectTransform = item.GetComponent<RectTransform>();

            // RectTransformを設定
            rectTransform.anchoredPosition = new Vector2(img_x, img_y); //アンカーポイントからの相対位置を設定
            rectTransform.localRotation = rotation; // 回転を設定

            float scale = (float)scale_percent / 100; // 拡大率を決定
            rectTransform.localScale = new Vector3(scale, scale, 1.0f);

            //画像の追加(第3引数は後に使用)
            _textToSpriteObject.Add((imageName, item, 0));
        }

    }
}
