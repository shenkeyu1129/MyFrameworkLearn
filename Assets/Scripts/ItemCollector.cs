using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private ItemCollectionModel model;
    public void Bind(ItemCollectionModel targetModel)
    {
        Unbind();

        model = targetModel;

    }

    public void Unbind()
    {
        if (model == null)
        {
            return;
        }
        model = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Item"))
        {
            return;
        }

        bool collected = model.TryCollect();

        if (!collected)
        {
            return;
        }
        Destroy(other.gameObject);
    }
    private void OnDestroy()
    {
        Unbind();
    }
}
