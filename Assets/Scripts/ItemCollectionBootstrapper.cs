using UnityEngine;

public class ItemCollectionBootstrapper : MonoBehaviour
{
    [SerializeField] private ItemCountView itemCountView;
    [SerializeField] private ItemCollectionLogger itemCollectionLogger;
    [SerializeField] private ItemCollector itemCollector;


    [SerializeField] private GameObject items;
    private ItemCollectionModel itemCountModel;
    private void Awake()
    {
        int totalItemCount = items.transform.childCount;
        itemCountModel = new ItemCollectionModel(totalItemCount);
    }
    private void Start()
    {
        itemCountView.Bind(itemCountModel);
        itemCollectionLogger.Bind(itemCountModel);
        itemCollector.Bind(itemCountModel);

    }
    private void OnDestroy()
    {
        if (itemCountView != null)
        {
            itemCountView.Unbind();
        }

        if (itemCollectionLogger != null)
        {
            itemCollectionLogger.Unbind();
        }
        if (itemCollector != null)
        {
            itemCollector.Unbind();
        }
    }
}
