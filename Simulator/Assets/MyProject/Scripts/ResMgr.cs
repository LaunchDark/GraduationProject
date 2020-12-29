using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//资源加载器
public class ResMgr : MonoBehaviour
{
    static ResMgr instance;
    public static ResMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ResMgr").AddComponent<ResMgr>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    Dictionary<string, AssetBundle> resPackagesDic = new Dictionary<string, AssetBundle>();
    Dictionary<string, Object> objDic = new Dictionary<string, Object>();

    //预加载资源预览
    public List<Object> preLoadObjPreview = new List<Object>();

    public Object LoadByCore(string path)
    {
        //return Load("Core/" + path);
        return Load(path);
    }

    public Object Load(string path)
    {
        if (objDic.ContainsKey(path))
            return objDic[path];
        Object o = Resources.Load(path);
        objDic.Add(path, o);
        return o;
    }

    public void LoadAsync(string path, System.Action complete)
    {
        StartCoroutine(StartCoroutineLoadResAssetAsync(path, complete));
    }

    private IEnumerator StartCoroutineLoadResAssetAsync(string path, System.Action complete)
    {
        ResourceRequest request = Resources.LoadAsync(path);
        yield return request;
        if (!objDic.ContainsKey(path))
        {
            objDic.Add(path, request.asset);
            if (Application.isEditor)
                preLoadObjPreview.Add(request.asset);
        }
        complete.Invoke();
    }

    private AssetBundle GetResPackageAssetBundle(string abName)
    {
        AssetBundle ab = null;
        if (resPackagesDic.ContainsKey(abName))
            ab = resPackagesDic[abName];
        if (ab == null)
        {
            ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + abName.ToLower());
            resPackagesDic[abName] = ab;
        }
        return ab;
    }

    public Object LoadByResPackage(string path)
    {
        string abName = path.Split('/')[0];
        string pfName = path.Split('/')[1];
        if (objDic.ContainsKey(path))
            return objDic[path];
        AssetBundle ab = GetResPackageAssetBundle(abName);
        Object o = ab.LoadAsset(pfName);
        objDic.Add(path, o);
        return o;
    }

    //预加载资源包专用,异步加载AB包里的所有对象
    public void LoadByResPackageAllAssetsAsync(string abName, System.Action complete)
    {
        StartCoroutine(StartCoroutineLoadAbAssetAsync(abName, complete));
    }

    private IEnumerator StartCoroutineLoadAbAssetAsync(string abName, System.Action complete)
    {
        AssetBundle ab = GetResPackageAssetBundle(abName);
        AssetBundleRequest request = ab.LoadAllAssetsAsync();
        yield return request;
        for (int i = 0; i < request.allAssets.Length; i++)
        {
            var o = request.allAssets[i];
            string key = abName + "/" + o.name;
            if (!objDic.ContainsKey(key))
            {
                objDic.Add(key, o);
                if (Application.isEditor)
                    preLoadObjPreview.Add(o);
            }
        }
        complete.Invoke();
    }

    /// <summary>
    /// 卸载资源包AB的所有对象
    /// </summary>
    /// <param name="abName"></param>
    public void UnLoadResPackageAssetBundleAsset(string abName)
    {
        if (resPackagesDic.ContainsKey(abName))
        {
            resPackagesDic[abName].Unload(true);
            resPackagesDic.Remove(abName);
        }
        foreach (var item in objDic)
        {
            if (item.Key.Contains(abName + "/"))
                objDic.Remove(item.Key);
            if (Application.isEditor)
                preLoadObjPreview.Remove(item.Value);
        }
    }

    public void UnLoadAsset(string abName)
    {
        if (objDic.ContainsKey(abName))
        {
            Object obj = objDic[abName];
            //Resources.UnloadAsset(obj);
            objDic.Remove(abName);
            if (Application.isEditor)
                preLoadObjPreview.Remove(obj);
        }
    }
}
