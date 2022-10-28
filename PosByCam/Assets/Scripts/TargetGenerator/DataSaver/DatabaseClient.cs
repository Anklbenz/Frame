#nullable enable
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;


public abstract class DatabaseClient {
    private const int RESPONSE_OK = 200;
    private const string REQUEST_CONTENT_TYPE = "application/json";
    private const string REQUEST_METHOD = "POST";

    public event Action? ServerResponseEvent;
    public long ResponseCode;
    private readonly string _authorizationHeader;

    protected DatabaseClient(string authorizationHeader){
        _authorizationHeader = authorizationHeader;
    }
    
    protected async UniTask<string?> GetResponse(string url, string paramJson = ""){
        ResponseCode = -1;
            
        var webRequest = UnityWebRequest.Get(url);
        webRequest.method = REQUEST_METHOD;
        webRequest.SetRequestHeader("Accept", REQUEST_CONTENT_TYPE);
        webRequest.SetRequestHeader("Content-Type", REQUEST_CONTENT_TYPE);
        webRequest.SetRequestHeader("Authorization", _authorizationHeader);
        //  webRequest.SetRequestHeader("appKey", THINGWORX_APP_KEY);
        if (paramJson != "")
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(paramJson));

        webRequest.certificateHandler = new ForceAcceptAllCertificates();
        var operation = webRequest.SendWebRequest();

        while (!operation.isDone)
            await UniTask.Yield();

        ResponseCode = webRequest.responseCode;

        webRequest.certificateHandler.Dispose();
        webRequest.uploadHandler?.Dispose();

        ServerResponseEvent?.Invoke();

        return ResponseCode == RESPONSE_OK ? webRequest.downloadHandler.text : null;
    }
}