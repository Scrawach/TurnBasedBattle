using UnityEngine;

namespace CodeBase.View.AssetManagement
{
    public class Assets : IAssets
    {
        public TObject Instantiate<TObject>(string path) where TObject : Object =>
            Instantiate<TObject>(path, Vector3.zero);

        public TObject Instantiate<TObject>(string path, Vector3 at) where TObject : Object
        {
            var prefab = Resources.Load<TObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}