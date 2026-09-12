using System;

public class ItemCollectionModel
{
    public int TotalItemCount { get; } //需要收集的物体总数

    public int CollectedItemCount { get; private set; } //已收集的物体数量

    //是否已收集完所有物体
    public bool IsCompleted
    {
        get
        {
            return CollectedItemCount >= TotalItemCount;
        }
    }

    public event Action<int> CollectedItemCountChanged; //已收集物体数量变化事件
    public event Action CollectionCompleted;//收集完成事件
    public ItemCollectionModel(int totalItemCount)
    {
        if (totalItemCount < 0)
        {
            totalItemCount = 0;
        }

        TotalItemCount = totalItemCount;
        CollectedItemCount = 0;
    }
    /// <summary>
    /// 尝试收集一个物品。
    /// </summary>
    /// <returns>
    /// 成功收集时返回 true；已经达到总数量时返回 false。
    /// </returns>
    /// <remarks>
    /// 成功时触发数量变化事件；本次收集刚好达到总数量时，
    /// 随后触发完成事件。
    /// </remarks>
    public bool TryCollect()
    {
        if (IsCompleted)
        {
            return false;
        }

        CollectedItemCount++;
        CollectedItemCountChanged?.Invoke(CollectedItemCount);
        if (IsCompleted)
        {
            CollectionCompleted?.Invoke();
        }
        return true;
    }
}