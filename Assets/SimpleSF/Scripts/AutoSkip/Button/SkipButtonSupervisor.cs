using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleSFSample
{
    /// <summary>
    /// Controls the action of the skip button.
    /// </summary>
    public class SkipButtonSupervisor : ICancellationInitializable
    {
        private readonly ISkipSwitch skipSwitch;
        private readonly Button skipButton;
        private readonly Image buttonImage;
        private readonly Sprite spriteOnEnable;
        private readonly Sprite spriteOnDisable;

        public SkipButtonSupervisor(ISkipSwitch skipSwitch, Settings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            this.skipSwitch = skipSwitch ?? throw new ArgumentNullException(nameof(skipSwitch));
            skipButton = settings.SkipButton ?? throw new ArgumentNullException(nameof(settings.SkipButton));
            buttonImage = skipButton.GetComponent<Image>();
            spriteOnEnable = settings.SpriteOnEnable ?? throw new ArgumentNullException(nameof(settings.SpriteOnEnable));
            spriteOnDisable = settings.SpriteOnDisable ?? throw new ArgumentNullException(nameof(settings.SpriteOnDisable));
        }

        public void InitializeWithCancellation(CancellationToken cancellationToken)
        {
            //When the skip button is clicked, turn on/off the skip mode
            skipButton.OnClickAsAsyncEnumerable(cancellationToken: cancellationToken)
                .ForEachAsync(_ => skipSwitch.SwitchSkip(!skipSwitch.IsSkipActive), cancellationToken: cancellationToken);
            //When the skip mode is switched, notify it
            UniTaskAsyncEnumerable.EveryValueChanged(skipSwitch, x => x.IsSkipActive)
                .ForEachAsync(isActive =>
                {
                    buttonImage.sprite = skipSwitch.IsSkipActive ? spriteOnEnable : spriteOnDisable;
                }, cancellationToken: cancellationToken);
            //On pointer enter
            skipButton.GetAsyncPointerEnterTrigger()
                .AsUniTaskAsyncEnumerable()
                .ForEachAsync(_ =>
                {
                    if (skipButton.interactable && !skipSwitch.IsSkipActive)
                    {
                        buttonImage.sprite = spriteOnEnable;
                    }
                }, cancellationToken: cancellationToken);
            //On pointer exit
            skipButton.GetAsyncPointerExitTrigger()
                .AsUniTaskAsyncEnumerable()
                .ForEachAsync(_ =>
                {
                    if (!skipSwitch.IsSkipActive)
                    {
                        buttonImage.sprite = spriteOnDisable;
                    }
                }, cancellationToken: cancellationToken);
        }

        [Serializable]
        public class Settings
        {
            public Button SkipButton;
            public Sprite SpriteOnEnable;
            public Sprite SpriteOnDisable;
        }
    }
}