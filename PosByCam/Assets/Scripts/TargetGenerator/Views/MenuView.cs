using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : View {
    public RawImage previewImage;
    public Button prepareButton, saveButton, loadButton;
    public TMP_InputField assetIdInput;


    public void SetImage(Texture2D texture){
        previewImage.color = Color.white;
        previewImage.texture = texture;
    }
}
    
