using UnityEngine;
using UnityEngine.UI;
public class ItemCountView : MonoBehaviour
{
    [SerializeField] private Text text;
    private ItemCollectionModel model;
    public void Bind(ItemCollectionModel targetModel)
    {
        // 防止重复绑定时重复订阅
        Unbind();

        model = targetModel;

        if (model == null)
        {
            Debug.LogError("[ItemCountView] Model is null.");
            return;
        }

        model.CollectedItemCountChanged += OnCollectedItemCountChanged;

        // 绑定时立即显示当前状态
        RefreshText(
            model.CollectedItemCount,
            model.TotalItemCount
        );
    }
  
     private void OnCollectedItemCountChanged(int newCollectedItemCount)
    {
        RefreshText(
            newCollectedItemCount,
            model.TotalItemCount
        );
    }

    private void RefreshText(
        int collectedItemCount,
        int totalItemCount
    )
    {
        text.text = $"已拾取 {collectedItemCount}/{totalItemCount}";
    }

    public void Unbind()
    {
        if (model == null)
        {
            return;
        }

        model.CollectedItemCountChanged -= OnCollectedItemCountChanged;
        model = null;
    }

    private void OnDestroy()
    {
        Unbind();
    }
}
