using Cysharp.Threading.Tasks;
using ScenarioFlow;
using ScenarioFlow.Scripts.SFText;
using System;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace SimpleSFSample
{
    /// <summary>
    /// Provides a decoder for the 'Sprite' type.
    /// </summary>
    public class SpriteProvider : IReflectable
    {
        private static readonly string NullSymbol = "None";

        private readonly IAssetStorage assetStorage;
        private readonly IAssetProvider assetProvider;

        public SpriteProvider(IAssetStorage assetStorage, IAssetProvider assetProvider)
        {
            this.assetStorage = assetStorage ?? throw new ArgumentNullException(nameof(assetStorage));
            this.assetProvider = assetProvider ?? throw new ArgumentNullException(nameof(assetProvider));
        }

        [CommandMethod("load sprites async")]
        [Category("Resources")]
        [Description("Load sprites at the specified path.")]
        [Snippet("Load sprites at {${1:path}}.")]
        public async UniTask LoadSpritesAsync(string assetPath, CancellationToken cancellationToken)
        {
            await assetStorage.LoadAssetsAsync<Sprite>(assetPath, cancellationToken);
        }

        [CommandMethod("unload sprites async")]
        [Category("Resources")]
        [Description("Unload sprites at the specified path.")]
        [Snippet("Unload sprites at {${1:path}}.")]
        public async UniTask UnloadSpritesAsync(string assetPath, CancellationToken cancellationToken)
        {
            await assetStorage.UnloadAssetsAsync<Sprite>(assetPath, cancellationToken);
        }

        [CommandMethod("unload all sprites async")]
        [Category("Resources")]
        [Description("Unload all sprites.")]
        [Snippet("Unload all sprites.")]
        public async UniTask UnloadSpritesAllAsync(CancellationToken cancellationToken)
        {
            await UniTask.WhenAll(assetStorage.GetPathAll<Sprite>().Select(path => UnloadSpritesAsync(path, cancellationToken)));
        }

        [DecoderMethod]
        [Description("A decoder for the 'Sprite' type.")]
        [Description("Returns a sprite with the asset name.")]
        public Sprite GetSprite(string assetName)
        {
            if (assetName == NullSymbol)
            {
                return null;
            }
            return assetProvider.GetAsset<Sprite>(assetName);
        }
    }
}