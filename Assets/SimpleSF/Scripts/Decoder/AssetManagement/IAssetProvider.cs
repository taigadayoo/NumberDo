namespace SimpleSFSample
{
    /// <summary>
    /// Provides assets loaded
    /// </summary>
    public interface IAssetProvider
    {
        T GetAsset<T>(string assetName) where T : UnityEngine.Object;
    }
}