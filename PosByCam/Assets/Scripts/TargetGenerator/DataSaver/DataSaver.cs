using System;
using ClassesForJson;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DataSaver  {
    private readonly string _requestName;
    private readonly JsonRequestSender _jsonRequestSender;
    
    public DataSaver(JsonRequestSender jsonRequestSender, string requestName){
        _jsonRequestSender = jsonRequestSender;
        _requestName = requestName;
    }
    
    public async UniTask<bool> TrySave(Data data){
        var sendData = DataToServerData(data);
        return await _jsonRequestSender.ExecuteServiceWithoutResult(_requestName, sendData);
    }

    private ServerData DataToServerData(Data data){
        return new ServerData
        {
            id = 1,
            asset_id = 1,
            frame = Texture2DToBase64(data.Texture),
            origin = data.Position,
            rotation = data.Rotation
        };
    }

    private string Texture2DToBase64(Texture2D texture){
        byte[] imageData = texture.EncodeToPNG();
        return Convert.ToBase64String(imageData);
    }
}
