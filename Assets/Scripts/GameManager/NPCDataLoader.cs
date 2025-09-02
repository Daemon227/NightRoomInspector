using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class NPCDataLoader : MonoBehaviour
{
    public void LoadDialog(string fileName, Action<NPCData> onLoadDialogCompleted)
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>(fileName);
        handle.Completed += (AsyncOperationHandle<TextAsset> task) =>
        {
            if(task.Status == AsyncOperationStatus.Succeeded)
            {
                string jsonText = task.Result.text;
                NPCData npcData = JsonUtility.FromJson<NPCData>(jsonText);
                onLoadDialogCompleted?.Invoke(npcData);
                Debug.Log("Finish");
            }
            else
            {
                Debug.Log("Load Json file error");
            }
        };
    }

    public void LoadImage(string fileName, Action<Sprite> onLoadImageCompleted)
    {
        var handle = Addressables.LoadAssetAsync<Sprite>(fileName);
        handle.Completed += handle =>
        {      
            onLoadImageCompleted?.Invoke(handle.Result);
        };
    }
}
