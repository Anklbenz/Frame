using Cysharp.Threading.Tasks;
using UnityEngine;

public class JsonRequestSender : DatabaseClient {
    private readonly string _servicesUrl;

    public JsonRequestSender(string hostUrl, string servicesUrl, string authorizationHeader) : base(authorizationHeader){
        _servicesUrl = hostUrl + servicesUrl;
    }
    
    //T request result, TU param class
    public async UniTask<T> ExecuteService<T, TU>(string serviceName, TU param = null) where T : class where TU : class{
        var requestUrl = _servicesUrl + serviceName;
        var operationIdParamInJson = JsonUtility.ToJson(param);
        return await Get<T>(requestUrl, operationIdParamInJson);
    }
    
    public async UniTask<bool> ExecuteServiceWithoutResult<TU>(string serviceName, TU param = null)  where TU : class{
        var requestUrl = _servicesUrl + serviceName;
        var operationIdParamInJson = JsonUtility.ToJson(param);
        return await GetWithoutResult(requestUrl, operationIdParamInJson);
    }
    
    private async UniTask<T> Get<T>(string url, string param = ""){
        var json = await GetResponse(url, param);
        return json != null ? JsonUtility.FromJson<T>(json) : default;
    }
    private async UniTask<bool> GetWithoutResult(string url, string param = ""){
        var json = await GetResponse(url, param);
        return json != null;
    }
}
