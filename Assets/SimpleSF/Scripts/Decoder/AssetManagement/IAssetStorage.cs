using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace SimpleSFSample
{
    /// <summary>
    /// Mediates in loading and unloading assets.
    /// </summary>
    public interface IAssetStorage
    {
        UniTask LoadAssetsAsync<T>(string assetPath, CancellationToken cancellationToken) where T : UnityEngine.Object;
        UniTask UnloadAssetsAsync<T>(string assetPath, CancellationToken cancellationToken) where T : UnityEngine.Object;
        IEnumerable<string> GetPathAll<T>() where T : UnityEngine.Object;
    }
}