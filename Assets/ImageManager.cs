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

        // �e�L�X�g�t�@�C������A�������Sprite��GameObject��������悤�ɂ��邽�߂̎���
        Dictionary<string, Sprite> _textToSprite;
        Dictionary<string, GameObject> _textToParentObject;

        // �e�摜��GameObject���Ǘ����邽�߂̃��X�g
        List<(string, GameObject, int)> _textToSpriteObject;

        void Awake()
        {
            //�e�L�X�g���X�v���C�g�̏������Ɖ摜�̒ǉ�
            _textToSprite = new Dictionary<string, Sprite>();
            _textToSprite.Add("background1", _background1);

            //�e�L�X�g���e�I�u�W�F�N�g(background��event��)�̒ǉ�
            _textToParentObject = new Dictionary<string, GameObject>();
            _textToParentObject.Add("backgroundObject", _backgroundObject);
            _textToParentObject.Add("eventObject", _eventObject);

            _textToSpriteObject = new List<(string, GameObject, int)>();
        }


        // �摜��z�u����
        public void PutImage(string imageName, string parentObjectName, int img_x = 0, int img_y = 0, int scale_percent = 100)
        {
            //�摜���擾
            Sprite image = _textToSprite[imageName];
            GameObject parentObject = _textToParentObject[parentObjectName];


            // �V�����摜�𐶐�����
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;

            // �摜��GameObject�𐶐�
            GameObject item = Instantiate(_imagePrefab, parent);
            item.GetComponent<Image>().sprite = image;
            RectTransform rectTransform = item.GetComponent<RectTransform>();

            // RectTransform��ݒ�
            rectTransform.anchoredPosition = new Vector2(img_x, img_y); //�A���J�[�|�C���g����̑��Έʒu��ݒ�
            rectTransform.localRotation = rotation; // ��]��ݒ�

            float scale = (float)scale_percent / 100; // �g�嗦������
            rectTransform.localScale = new Vector3(scale, scale, 1.0f);

            //�摜�̒ǉ�(��3�����͌�Ɏg�p)
            _textToSpriteObject.Add((imageName, item, 0));
        }

    }
}
