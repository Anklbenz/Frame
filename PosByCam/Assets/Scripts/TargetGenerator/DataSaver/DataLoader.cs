using System;
using ClassesForJson;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DataLoader {
    
    private readonly string _requestName;
    private readonly JsonRequestSender _jsonRequestSender;
    
    public DataLoader(JsonRequestSender jsonRequestSender, string requestName){
        _jsonRequestSender = jsonRequestSender;
        _requestName = requestName;
    }

    
    public async UniTask<Data> TryLoad(){
        var result = await _jsonRequestSender.ExecuteService<ServerData, Id>(_requestName, new Id() { id = 1 });
        return  ServerDataToData(result);
    }
    
    private Data ServerDataToData(ServerData serverData){
        return new Data
        {
            Texture = Base64ToTexture2D(serverData.frame),
            Position = serverData.origin,
            Rotation = serverData.rotation,
        };
    }
    
    private Texture2D Base64ToTexture2D(string base64String){
        var texture = new Texture2D(2, 2);
        
        var bytes = Convert.FromBase64String(base64String);
        texture.LoadImage(bytes);
 
        return texture;
    }
}
