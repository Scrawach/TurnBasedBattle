using UnityEngine;

namespace CodeBase.View.AssetManagement
{
    public interface IAssets
    {
        TObject Instantiate<TObject>(string path) where TObject : Object;
        TObject Instantiate<TObject>(string path, Vector3 at) where TObject : Object;
    }
}