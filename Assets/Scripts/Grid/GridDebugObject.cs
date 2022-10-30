using TMPro;
using UnityEngine;
public class GridDebugObject : MonoBehaviour
{
    private GridObject gridObject;
    [SerializeField] private TextMeshPro textMeshPro;
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    private void Update()
    {
        textMeshPro.text = gridObject.ToString();
    }
}
