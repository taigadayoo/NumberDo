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


    // �e���̌��݂̒l�i�A���t�@�x�b�g�j
    private char[] chars = new char[3] { 'A', 'A', 'A' };

    // �������p�X���[�h�i�A���t�@�x�b�g�j
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
        // ���݂͎g�p����Ă��܂��񂪁A�K�v�ɉ����ď�����ǉ��ł��܂�
    }


    void CheckDigitClick(Text digitText, int digitIndex)
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverUIElement(digitText))
            {
                // �Ή����錅�̃A���t�@�x�b�g�����ɐi�߂�
                chars[digitIndex] = NextCharacter(chars[digitIndex]);

                // �X�V���ꂽ�A���t�@�x�b�g���e�L�X�g�ɕ\��
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
            return 'A';  // 'Z'�̎���'A'�ɖ߂�
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
