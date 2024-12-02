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
    /// Controls the action of the auto button.
    /// </summary>
    public class AutoButtonSupervisor : ICancellationInitializable
    {
        private readonly IAutoSwitch autoSwitch;
        private readonly Button autoButton;
        private readonly Image buttonImage;
        private readonly Sprite spriteOnEnable;
        private readonly Sprite spriteOnDisable;

        public AutoButtonSupervisor(IAutoSwitch autoSwitch, Settings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            this.autoSwitch = autoSwitch ?? throw new ArgumentNullException(nameof(autoSwitch));
            autoButton = settings.AutoButton ?? throw new ArgumentNullException(nameof(settings));
            buttonImage = autoButton.GetComponent<Image>();
            spriteOnEnable = settings.SpriteOnEnable ?? throw new ArgumentNullException(nameof(settings));
            spriteOnDisable = settings.SpriteOnDisable ?? throw new ArgumentNullException(nameof(settings));
        }

        public void InitializeWithCancellation(CancellationToken cancellationToken)
        {
            //When the auto button is cliced, turn on/off the auto mode
            autoButton.OnClickAsAsyncEnumerable(cancellationToken: cancellationToken)
                .ForEachAsync(_ => autoSwitch.SwitchAuto(!autoSwitch.IsAutoActive), cancellationToken: cancellationToken);
            //When the auto mode is switched, notify it
            UniTaskAsyncEnumerable.EveryValueChanged(autoSwitch, x => x.IsAutoActive)
                .ForEachAsync(isActive =>
                {
                    buttonImage.sprite = isActive ? spriteOnEnable : spriteOnDisable;
                }, cancellationToken: cancellationToken);
            //On pointer enter
            autoButton.GetAsyncPointerEnterTrigger()
                .AsUniTaskAsyncEnumerable()
                .ForEachAsync(_ =>
                {
                    if (autoButton.interactable && !autoSwitch.IsAutoActive)
                    {
                        buttonImage.sprite = spriteOnEnable;
                    }
                }, cancellationToken: cancellationToken);
            //On pointer exit
            autoButton.GetAsyncPointerExitTrigger()
                .AsUniTaskAsyncEnumerable()
                .ForEachAsync(_ =>
                {
                    if (!autoSwitch.IsAutoActive)
                    {
                        buttonImage.sprite = spriteOnDisable;
                    }
                }, cancellationToken: cancellationToken);

        }

        [Serializable]
        public class Settings
        {
            public Button AutoButton;
            public Sprite SpriteOnEnable;
            public Sprite SpriteOnDisable;
        }
    }
}