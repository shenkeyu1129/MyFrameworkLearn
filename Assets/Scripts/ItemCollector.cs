using UnityEngine;

public class ItemCollector : MonoBehaviour
{

    [SerializeField] private GameObject items;
    [SerializeField] private ItemCountView itemCountView;
    private ItemCollectionModel itemCountModel;

    private void Start()
    {
        int totalItemCount = items.transform.childCount;

        itemCountModel = new ItemCollectionModel(totalItemCount);

        itemCountView.RefreshText(
            itemCountModel.CollectedItemCount,
            itemCountModel.TotalItemCount
        );
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Item"))
        {
            return;
        }

        bool collected = itemCountModel.TryCollect();

        if (!collected)
        {
            return;
        }

        Debug.Log(
            $"拾取第{itemCountModel.CollectedItemCount}个 Item"
        );

        itemCountView.RefreshText(
            itemCountModel.CollectedItemCount,
            itemCountModel.TotalItemCount
        );

        Destroy(other.gameObject);

        if (itemCountModel.IsCompleted)
        {
            Debug.Log("已拾取完所有物品");
        }
    }

}
