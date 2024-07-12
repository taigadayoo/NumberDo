using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    [SerializeField]
    public InputField inputField;
   

   
    ObjectManager objectManager;
    [SerializeField]
    ItemGetSet getSet;
    ItemBer itemBer;
    [SerializeField]
    SceneManagement sceneManagement;
    private bool okPass = false;
    SampleSoundManager sampleSoundManager;

    public Text digit1;
    public Text digit2;
    public Text digit3;
    public Text digit4;

    // �e���̌��݂̒l
    private int[] digits = new int[4];

    private int[] correctPassword = new int[4] { 1, 9, 9, 6 };

    private void Start()
    {
        UpdateDigitTexts();
        objectManager =  FindObjectOfType<ObjectManager>();
        itemBer = FindObjectOfType<ItemBer>();
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
       objectManager.OnePassWord = false;

    }
    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            objectManager.Ontext = true;
        }
        else
        {
            objectManager.Ontext = false;
        }

        if (IsPasswordCorrect())
        {
            objectManager.unrocking = true;
            this.gameObject.SetActive(false);
        }
        CheckDigitClick(digit1, 0);
        CheckDigitClick(digit2, 1);
        CheckDigitClick(digit3, 2);
        CheckDigitClick(digit4, 3);
    }
    public void CheckPassword()
    {
        string inputPassword = inputField.text;

      
    }
 
    private void OkPass()
    {
        itemBer.AddItem(objectManager.items[3]);
        objectManager.imageNum = 3;
        getSet.ImageChange(objectManager.imageNum);
        
        objectManager.OnePassWord = true;

        if (sampleSoundManager != null)
        {
            SampleSoundManager.Instance.PlaySe(SeType.SE4);
        }
    }
    void CheckDigitClick(Text digitText, int digitIndex)
    {
        // �}�E�X�{�^����������A�N���b�N��UI�v�f��ōs��ꂽ�ꍇ
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverUIElement(digitText))
            {
                // �Ή����錅�̃J�E���g�𑝂₷
                digits[digitIndex]++;

                // �J�E���g��10�ɂȂ�����0�ɖ߂�
                if (digits[digitIndex] > 9)
                {
                    digits[digitIndex] = 0;
                }

                // �X�V���ꂽ�J�E���g���e�L�X�g�ɕ\��
                UpdateDigitTexts();
            }
            else if(!IsMouseOverUIElement(digit1) &&
             !IsMouseOverUIElement(digit2) &&
             !IsMouseOverUIElement(digit3) &&
             !IsMouseOverUIElement(digit4))
            {
                objectManager.Ontext = false;
                this.gameObject.SetActive(false);
                objectManager.OnPass = false;
            }
        }
    }

    void UpdateDigitTexts()
    {
        // �e���̃J�E���g���e�L�X�g�ɔ��f
        digit1.text = digits[0].ToString();
        digit2.text = digits[1].ToString();
        digit3.text = digits[2].ToString();
        digit4.text = digits[3].ToString();
    }

    bool IsMouseOverUIElement(Text textElement)
    {
        // UI�v�f�̋��E��`���擾
        RectTransform rectTransform = textElement.GetComponent<RectTransform>();
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);

        // �}�E�X�ʒu��UI�v�f�͈͓̔��ɂ��邩���`�F�b�N
        return rectTransform.rect.Contains(localMousePosition);
    }
    bool IsPasswordCorrect()
    {
        // ���͂��ꂽ�J�E���^�[�̒l���p�X���[�h�ƈ�v���邩���`�F�b�N
        for (int i = 0; i < digits.Length; i++)
        {
            if (digits[i] != correctPassword[i])
            {
                return false;
            }
        }
        return true;
    }
  
}