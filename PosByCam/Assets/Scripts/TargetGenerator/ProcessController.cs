using UnityEngine;

[System.Serializable]
public class ProcessController {
    [SerializeField] private MenuView menuView;
    [SerializeField] private GameObject ground;
    private DataGetter _dataGetter;
    private DataSender _dataSender;
    
    public void Init(DataGetter dataGetter, DataSender dataSender){
        _dataGetter = dataGetter;
        _dataSender = dataSender;
        menuView.prepareButton.onClick.AddListener(PrepareDataProcess);
        menuView.saveButton.onClick.AddListener(SaveDataProcess);
        menuView.loadButton.onClick.AddListener(LoadDataProcess);
    }

    private async void SaveDataProcess(){
        if (_dataGetter.data == null) return;
        var result = await _dataSender.saver.TrySave(_dataGetter.data);
        Debug.Log(result);
    }

    private async void LoadDataProcess(){
        var result = await _dataSender.loader.TryLoad();
        menuView.SetImage( result.Texture);
    }

    private async void PrepareDataProcess(){
        ground.SetActive(false);
        menuView.Hide();
        
        await _dataGetter.Processing();
        
        menuView.SetImage(_dataGetter.data.Texture);
        
        menuView.Show();
        ground.SetActive(true);
    }
}
