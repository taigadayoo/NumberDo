using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorPassWord : MonoBehaviour
{
    [SerializeField]
    public InputField inputField;
    SampleSoundManager sampleSoundManager;
    ObjectManager objectManager;
    [SerializeField]
    ItemGetSet getSet;
    [SerializeField]
    SceneManagement sceneManagement;
    Interactable interactable;

    public Text digit1;
    public Text digit2;
    public Text digit3;


    // 各桁の現在の値（アルファベット）
    private char[] chars = new char[3] { 'A', 'A', 'A' };

    // 正しいパスワード（アルファベット）
    private char[] correctPassword = new char[3] { 'S', 'A', 'J'};

    private void Start()
    {
        UpdateDigitTexts();
        objectManager = FindObjectOfType<ObjectManager>();
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
    }

    private void Update()
    {
        //if (this.gameObject.activeSelf)
        //{
        //    objectManager.Ontext = true;
        //}

        if (IsPasswordCorrect())
        {
            //sampleSoundManager.PlaySe(SeType.SE9);
            objectManager.monitorPass.SetActive(false);
            objectManager.miniGameZoom.SetActive(true);
            //interactable.touchAction = Interactable.TouchAction.itemGeted;
        }
        CheckDigitClick(digit1, 0);
        CheckDigitClick(digit2, 1);
        CheckDigitClick(digit3, 2);
    }

    public void CheckPassword()
    {
        string inputPassword = inputField.text;
        // 現在は使用されていませんが、必要に応じて処理を追加できます
    }


    void CheckDigitClick(Text digitText, int digitIndex)
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverUIElement(digitText))
            {
                // 対応する桁のアルファベットを次に進める
                chars[digitIndex] = NextCharacter(chars[digitIndex]);

                // 更新されたアルファベットをテキストに表示
                UpdateDigitTexts();
            }

        }
    }

    void UpdateDigitTexts()
    {
        digit1.text = chars[0].ToString();
        digit2.text = chars[1].ToString();
        digit3.text = chars[2].ToString();
    }

    char NextCharacter(char current)
    {
        if (current == 'Z')
            return 'A';  // 'Z'の次は'A'に戻る
        else
            return (char)(current + 1);
    }

    bool IsMouseOverUIElement(Text textElement)
    {
        RectTransform rectTransform = textElement.GetComponent<RectTransform>();
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);
        return rectTransform.rect.Contains(localMousePosition);
    }

    bool IsPasswordCorrect()
    {
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] != correctPassword[i])
            {
                return false;
            }
        }
        return true;
    }
}
