using UnityEngine;
using UnityEngine.UI;
public class ItemCountView : MonoBehaviour
{
    [SerializeField] private Text text;

    public void RefreshText(int collectedItemCount, int totalItemCount)
    {
        text.text = $"已拾取 {collectedItemCount}/{totalItemCount}";
    }
}
