using UnityEngine;
using UnityEngine.UI;

public class FinderView : View
{
    public Button shotButton;

    public void Init(Vector2Int size){
        ((RectTransform)transform).sizeDelta = size;
    }
}
