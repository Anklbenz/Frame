using System;
using ClassesForJson;
using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class DataSender {
    [SerializeField] private string authorizationKey;
    [SerializeField] private string url;
    [SerializeField] private string servicesUrl;
    [SerializeField] private string saveRequestName;
    [SerializeField] private string loadRequestName;

    public DataLoader loader ;
    public DataSaver saver;

    private JsonRequestSender _jsonRequestSender;

    public void Init(){
        _jsonRequestSender = new JsonRequestSender(url, servicesUrl, authorizationKey);
        loader = new DataLoader(_jsonRequestSender, loadRequestName);
        saver = new DataSaver(_jsonRequestSender, saveRequestName);
    }
}
