using Cysharp.Threading.Tasks;
using ScenarioFlow;
using ScenarioFlow.Scripts;
using ScenarioFlow.Tasks;
using System;
using UnityEngine;

namespace SimpleSFSample
{
	public class ObjectBuilder : MonoBehaviour
    {
        [SerializeField]
        AutoButtonSupervisor.Settings autoButtonSupervisorSettings;
        [SerializeField]
        BackgroundAnimator.Settings backgroundAnimatorSettings;
        [SerializeField]
        ButtonNotifier.Settings buttonNotifierSettings;
        [SerializeField]
        CharacterProvider.Settings characterProviderSettings;
        [SerializeField]
        DialogueWriter.Settings dialogueWriterSettings;
        [SerializeField]
        KeyNotifier.Settings keyNotifierSettings;
        [SerializeField]
        LogSupervisor.Settings logSupervisorSettings;
        [SerializeField]
        PlayerSelectionPresenter.Settings playerSelectionPresenterSettings;
        [SerializeField]
        SceneTransitionAnimator.Settings sceneTransitionAnimatorSettings;
        [SerializeField]
        SkipButtonSupervisor.Settings skipButtonSupervisorSettings;

        [SerializeField]
        ScenarioScript scenarioScript;
        [SerializeField]
        private float autoDuration = 2.0f;
        [SerializeField]
        private float skipDuration = 0.01f;

        private async UniTaskVoid Start()
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();

            var buttonNotifier = new ButtonNotifier(buttonNotifierSettings);
            var keyNotifier = new KeyNotifier(keyNotifierSettings);
            var autoNextNotifier = new AutoNextNotifier();
            var compositeAnyNextNotifier = new CompositeAnyNextNotifier(new INextNotifier[]
            {
                autoNextNotifier,
                buttonNotifier,
                keyNotifier,
            });
            var compositeAnyCancellationNotifier = new CompositeAnyCancellationNotifier(new ICancellationNotifier[]
            {
                buttonNotifier,
                keyNotifier,
            });
			var scenarioPauseStatus = new ScenarioPauseStatus();
			var notifierScenarioPauseDecorator = new NotifierScenarioPauseDecorator(compositeAnyNextNotifier, compositeAnyCancellationNotifier, scenarioPauseStatus);

            var scenarioTaskExecutor = new ScenarioTaskExecutor(notifierScenarioPauseDecorator, notifierScenarioPauseDecorator);
            var scenarioTaskExecutorScenarioPauseDecorator = new ScenarioTaskExecutorScenarioPauseDecorator(scenarioTaskExecutor, scenarioTaskExecutor, scenarioPauseStatus);
            var scenarioBookReader = new ScenarioBookReader(scenarioTaskExecutorScenarioPauseDecorator);


            autoNextNotifier.Duration = autoDuration;
            autoNextNotifier.IsActive = false;
            scenarioTaskExecutor.Duration = skipDuration;

			var autoSwitch = new AutoSwitch(autoNextNotifier);
            var skipSwitch = new SkipSwitch(scenarioTaskExecutor);
            var autoSkipSwitchExclusiveDecorator = new AutoSkipSwitchExclusiveDecorator(autoSwitch, skipSwitch);
            var autoSkipSwitchScenarioPauseDecorator = new AutoSkipSwitchScenarioPauseDecorator(autoSkipSwitchExclusiveDecorator, autoSkipSwitchExclusiveDecorator, scenarioPauseStatus);

            var scenarioPauserAutoSkipBreakDecorator = new ScenarioPauserAutoSkipBreakDecorator(scenarioPauseStatus, autoSkipSwitchScenarioPauseDecorator, autoSkipSwitchScenarioPauseDecorator);
			var logSupervisor = new LogSupervisor(scenarioPauserAutoSkipBreakDecorator, scenarioPauseStatus, logSupervisorSettings);

			var resourcesAssetLoader = new ResourcesAssetLoader();
			var assetStorage = new AssetStorage(resourcesAssetLoader);

            var characterAnimator = new CharacterAnimator();

			var scenarioBookPublisher = new ScenarioBookPublisher(new IReflectable[]
            {
				new BackgroundAnimator(backgroundAnimatorSettings),
				new BranchMaker(scenarioBookReader),
				new CancellationTokenDecoder(scenarioTaskExecutor),
                characterAnimator,
				new CharacterProvider(characterProviderSettings),
				new DelayMaker(),
				new DialogueWriter(characterAnimator, logSupervisor, dialogueWriterSettings),
				new PrimitiveDecoder(),
				new PlayerSelectionPresenter(scenarioBookReader, logSupervisor, playerSelectionPresenterSettings),
				new SceneTransitionAnimator(sceneTransitionAnimatorSettings),
				new SpriteProvider(assetStorage, assetStorage),
				new VectorDecoder(),
            });

			var cancellationInitializables = new ICancellationInitializable[]
            {
                new AutoButtonSupervisor(autoSkipSwitchScenarioPauseDecorator, autoButtonSupervisorSettings),
                logSupervisor,
                new SkipButtonSupervisor(autoSkipSwitchScenarioPauseDecorator, skipButtonSupervisorSettings),
            };

            var disposables = new IDisposable[]
            {
                scenarioTaskExecutor,
            };

            foreach (var disposable in disposables)
            {
                disposable.AddTo(cancellationToken);
            }

            foreach (var cancellationInitializable in cancellationInitializables)
            {
                cancellationInitializable.InitializeWithCancellation(cancellationToken);
            }

            var scenarioBook = scenarioBookPublisher.Publish(scenarioScript);
            try
            {
                await scenarioBookReader.ReadAsync(scenarioBook, cancellationToken);
            }
            finally
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }
    }
}