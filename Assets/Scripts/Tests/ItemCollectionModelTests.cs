using NUnit.Framework;
using System;
public class ItemCollectionModelTests
{
    [Test]
    public void NewModel_StartsWithZeroCollectedItems()
    {
        // Arrange：准备测试对象
        var model = new ItemCollectionModel(3);

        // Assert：验证初始状态
        Assert.AreEqual(3, model.TotalItemCount);
        Assert.AreEqual(0, model.CollectedItemCount);
        Assert.IsFalse(model.IsCompleted);
    }
    [Test]
    public void TryCollect_IncreasesCollectedCount()
    {
        // Arrange：准备测试对象
        var model = new ItemCollectionModel(3);

        // Act：执行操作
        bool result = model.TryCollect();

        // Assert：验证结果
        Assert.IsTrue(result);
        Assert.AreEqual(1, model.CollectedItemCount);
        Assert.IsFalse(model.IsCompleted);

    }
    [Test]
    public void TryCollect_AfterReachingTotalCount_ReturnsFalse()
    {
        // Arrange：准备测试对象
        var model = new ItemCollectionModel(2);

        // Act：执行操作
        bool result1 = model.TryCollect();
        bool result2 = model.TryCollect();
        bool result3 = model.TryCollect();

        // Assert：验证结果
        Assert.IsTrue(result1);
        Assert.IsTrue(result2);
        Assert.IsFalse(result3);
        Assert.AreEqual(2, model.CollectedItemCount);
        Assert.IsTrue(model.IsCompleted);
    }
    [Test]
    public void TryCollect_SuccessfulCollection_RaisesCountChanged()
    {
        // Arrange：准备测试对象和事件记录变量
        var model = new ItemCollectionModel(3);

        int eventCount = 0;
        int receivedCount = -1;

        Action<int> handler = newCount =>
        {
            eventCount++;
            receivedCount = newCount;
        };

        model.CollectedItemCountChanged += handler;

        // Act：执行一次成功的收集
        bool result = model.TryCollect();

        // 取消订阅，保持测试对象清理完整
        model.CollectedItemCountChanged -= handler;

        // Assert：验证行为结果
        Assert.IsTrue(result);
        Assert.AreEqual(1, eventCount);
        Assert.AreEqual(1, receivedCount);
        Assert.AreEqual(1, model.CollectedItemCount);
    }

    [Test]
    public void TryCollect_AfterCompletion_DoesNotRaiseCountChanged()
    {
        // Arrange
        var model = new ItemCollectionModel(1);

        int eventCount = 0;

        Action<int> handler = newCount =>
        {
            eventCount++;
        };

        model.CollectedItemCountChanged += handler;

        // Act
        bool firstResult = model.TryCollect();
        bool secondResult = model.TryCollect();

        model.CollectedItemCountChanged -= handler;

        // Assert
        Assert.IsTrue(firstResult);
        Assert.IsFalse(secondResult);
        Assert.AreEqual(1, model.CollectedItemCount);
        Assert.AreEqual(1, eventCount);
    }
    [Test]
    public void TryCollect_WhenReachingTotalCount_RaisesCompletionOnce()
    {
        // Arrange：准备测试对象
        var model = new ItemCollectionModel(2);
        int eventCount = 0;
        Action handler = () =>
        {
            eventCount++;
        };

        model.CollectionCompleted += handler;

        // Act：执行操作
        bool result1 = model.TryCollect();
        bool result2 = model.TryCollect();
        bool result3 = model.TryCollect();
        model.CollectionCompleted -= handler;
        // Assert：验证结果
        Assert.IsTrue(result1);
        Assert.IsTrue(result2);
        Assert.IsFalse(result3);
        Assert.AreEqual(1, eventCount);
    }
}
