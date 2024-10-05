using UnityEngine;
using UnityEngine.UI;

public class BombPass : MonoBehaviour
{
    [SerializeField]
    public InputField inputField;



    ObjectManager objectManager;
    [SerializeField]
    ItemGetSet getSet;
    ItemBer itemBer;
    [SerializeField]
    SceneManagement sceneManagement;
    private bool okBombPass = false;
    SampleSoundManager sampleSoundManager;

    public GameObject doorKey;
    Interactable interactable;

    public Text digit1;
    public Text digit2;
    public Text digit3;
    public Text digit4;

    Timer timer;
    // �e���̌��݂̒l
    private int[] digits = new int[4];

    private int[] correctPassword = new int[4] { 1, 9, 0, 2 };

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        UpdateDigitTexts();
        objectManager = FindObjectOfType<ObjectManager>();
        itemBer = FindObjectOfType<ItemBer>();
        sampleSoundManager = FindObjectOfType<SampleSoundManager>();
        objectManager.OnePassWord = false;
        objectManager.Ontext = true;
    }
    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            objectManager.Ontext = true;
        }

        if (IsPasswordCorrect())
        {
            if (sampleSoundManager != null)
            {
                sampleSoundManager.PlaySe(SeType.SE9);
            }
            objectManager.textEnd = true;
            itemBer.AddItem(objectManager.items[16]);
            getSet.ImageChange(22);
            objectManager.allColliderSwicth(true);
            objectManager.unrock = true;
            objectManager.bombPass.SetActive(false);
            objectManager.zoomOffColMain.SetActive(false);
            timer.Stop();
            objectManager.bombRock.SetActive(false);
            objectManager.bombUnrock.SetActive(true);
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
            else if (!IsMouseOverUIElement(digit1) &&
             !IsMouseOverUIElement(digit2) &&
             !IsMouseOverUIElement(digit3) &&
             !IsMouseOverUIElement(digit4))
            {
                objectManager.allColliderSwicth(true);
                objectManager.Ontext = false;
                objectManager.bombPass.SetActive(false);
                objectManager.OnPass = false;
                objectManager.OnBox4 = false;
                objectManager.textEnd = true;

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
