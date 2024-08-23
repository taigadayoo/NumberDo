using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace NovelGame
{
    // �e�L�X�g�t�@�C�����當�͂�ǂݍ��݁A�X�e�[�g�����g�̎��s���Ǘ�����N���X
    public class UserScriptManager : MonoBehaviour
    {
        [SerializeField] TextAsset _textFile; // �e�L�X�g�t�@�C�����i�[���邽�߂̕ϐ�
        [SerializeField] Animator animator; // �A�j���[�^�[�R���|�[�l���g���i�[����ϐ�

        List<string> _sentences = new List<string>(); // �ǂݍ��񂾕��͂��i�[���郊�X�g
        public bool isWaiting = false; // �E�F�C�g�����ǂ����̃t���O

        // �X�N���v�g�̏��������Ɏ��s����郁�\�b�h
        void Awake()
        {
            // �e�L�X�g�t�@�C�����當�͂���s���ǂݍ���Ń��X�g�Ɋi�[����
            StringReader reader = new StringReader(_textFile.text);
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                _sentences.Add(line);
            }
        }

        // ���݂̕��͂��擾���郁�\�b�h
        public string GetCurrentSentence()
        {
            return _sentences[ScenarioGameManager.Instance.lineNumber];
        }

        // ���ߕ��ł��邩�ǂ����𔻒肷�郁�\�b�h
        public bool IsStatement(string sentence)
        {
            string[] words = sentence.Split(',');// ���͂�P��ɕ�������

            if (words[0] == "com") // A��ڂ�com�̏ꍇ�A���߂Ƃ݂Ȃ�
            {
                return true;
            }
            return false;
        }

        //�����񂪐��l�Ȃ琔�l�ɁA�����łȂ��Ȃ�f�t�H���g�l�ɐݒ肷�郁�\�b�h
        private int ConvertToInt(string str, int defaultNum = 0)
        {
            return int.TryParse(str, out int result) ? result : defaultNum;
        }


        // ���߂����s���郁�\�b�h
        public void ExecuteStatement(string sentence)
        {
            string[] words = sentence.Split(','); // ���͂�P��ɕ�������

            //END���o���ꍇ�A�V�[���J�ڂ��s��
            if (sentence == "END")
            {
                ChangeScene();
                return;
            }

            // �P��ɂ���ď����𕪊򂷂�
            switch (words[1])
            {
                case "putImage": // putImage�X�e�[�g�����g�̏ꍇ
                    int img_x = ConvertToInt(words[5]);
                    int img_y = ConvertToInt(words[6]);
                    int scale_percent = ConvertToInt(words[7], 100);
                    ScenarioGameManager.Instance.imageManager.PutImage(words[2], words[3], img_x, img_y, scale_percent); // �摜��\������
                    ScenarioGameManager.Instance.mainTextController.GoToTheNextLine(); //���̍s�ɐi��
                    break;

                case "playAnimation": //playAnimation�X�e�[�g�����g�̏ꍇ
                    PlayAnimation(words[2]); // �w�肳�ꂽ�A�j���[�V�������Đ�
                    break;

                default: //�����łȂ��ꍇ
                    break;
            }
        }

        //
        void PlayAnimation(string animationName)
        {
            if (animator != null) 
            {
                animator.Play(animationName);
                StartCoroutine(WaitForAnimation(animator.GetCurrentAnimatorStateInfo(0).length));
            }
            else
            {
                Debug.LogError("Animator not assigned.");
            }
        }

        // �A�j���[�V��������������̂�ҋ@���Ă��玟�̍s�ɐi�ރ��\�b�h
        IEnumerator WaitForAnimation(float duration)
        {
            isWaiting = true;
            yield return new WaitForSeconds(duration);
            isWaiting = false;
            ScenarioGameManager.Instance.mainTextController.GoToTheNextLine();
        }

        //�V�[����ύX���郁�\�b�h
        void ChangeScene()
        {
            //�V�[����ύX����R�[�h
            UnityEngine.SceneManagement.SceneManager.LoadScene("");
        }
    }
}
