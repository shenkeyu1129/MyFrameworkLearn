public class ItemCollectionModel
{
    public int TotalItemCount { get; }

    public int CollectedItemCount { get; private set; }

    public bool IsCompleted
    {
        get
        {
            return CollectedItemCount >= TotalItemCount;
        }
    }

    public ItemCollectionModel(int totalItemCount)
    {
        if (totalItemCount < 0)
        {
            totalItemCount = 0;
        }

        TotalItemCount = totalItemCount;
        CollectedItemCount = 0;
    }

    public bool TryCollect()
    {
        if (IsCompleted)
        {
            return false;
        }

        CollectedItemCount++;
        return true;
    }
}