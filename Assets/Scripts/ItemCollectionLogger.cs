using UnityEngine;

public class ItemCollectionLogger : MonoBehaviour
{
    private ItemCollectionModel model;
    public void Bind(ItemCollectionModel targetModel)
    {
        Unbind();

        model = targetModel;

        if (model == null)
        {
            Debug.LogError("[ItemCollectionLogger] Model is null.");
            return;
        }

        model.CollectedItemCountChanged += OnCollectedItemCountChanged;
        model.CollectionCompleted += OnCollectionCompleted;
    }
    private void OnCollectedItemCountChanged(int newCollectedItemCount)
    {
        Debug.Log(
          $"拾取第{newCollectedItemCount}个 Item");
    }
    private void OnCollectionCompleted()
    {
        Debug.Log("已拾取完所有物品");
    }
    public void Unbind()
    {
        if (model == null)
        {
            return;
        }

        model.CollectedItemCountChanged -= OnCollectedItemCountChanged;
        model.CollectionCompleted -= OnCollectionCompleted;
        model = null;
    }
    private void OnDestroy()
    {
        Unbind();
    }
}
