using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace SimpleSFSample
{
    /// <summary>
    /// Loads and unloads assets.
    /// </summary>
    public interface IAssetLoader
    {
        UniTask<T> LoadAssetAsync<T>(string assetPath, CancellationToken cancellationToken) where T : UnityEngine.Object;
        UniTask<IEnumerable<T>> LoadAssetsAsync<T>(string assetPath, CancellationToken cancellationToken) where T : UnityEngine.Object;
        UniTask UnloadAssetAsync<T>(string assetPath, CancellationToken cancellationToken) where T : UnityEngine.Object;
        UniTask UnloadAssetsAsync<T>(string assetPath, CancellationToken cancellationToken) where T : UnityEngine.Object;
    }
}