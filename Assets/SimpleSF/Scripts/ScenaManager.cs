using System.Threading;
using Cysharp.Threading.Tasks;
using ScenarioFlow;
using SimpleSFSample;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleSFSample
{
    public class SceneManager : IReflectable
    {
        public SceneTransitionAnimator sceneTransitionAnimator;
        public Image curtainImage;
        public string nextSceneName;

        private void Start()
        {
            // �����������i��F�ŏ��̃V�[����ݒ�j
            if (sceneTransitionAnimator != null)
            {
                sceneTransitionAnimator.nextSceneName = nextSceneName;
            }
        }

        public async void StartSceneTransition(float duration)
        {
            // �L�����Z���g�[�N�����쐬
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            // �V�[���J�ڂ��J�n
            await sceneTransitionAnimator.ExitSceneTransitionAsync(duration, cancellationTokenSource.Token);
        }
    }
}


