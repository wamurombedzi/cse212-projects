using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with distinct priorities and remove them one by one.
    // Expected Result: Items are dequeued in order of their priorities (highest to lowest).
    // Defect(s) Found: None initially 
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item1", 10);
        priorityQueue.Enqueue("Item2", 20);
        priorityQueue.Enqueue("Item3", 15);

        Assert.AreEqual("Item2", priorityQueue.Dequeue()); // Highest priority
        Assert.AreEqual("Item2", priorityQueue.Dequeue()); // Next highest priority
        Assert.AreEqual("Item2", priorityQueue.Dequeue()); // Lowest priority

    }

    [TestMethod]
    // Scenario: Add items with the same priority and ensure FIFO behavior.
    // Expected Result: Items are dequeued in the same order they were enqueued.
    // Defect(s) Found:  None initially.
    public void TestPriorityQueue_2()
    {
        var priorityQueue2 = new PriorityQueue();
        priorityQueue2.Enqueue("Item1", 10);
        priorityQueue2.Enqueue("Item2", 10);
        priorityQueue2.Enqueue("Item3", 10);

        Assert.AreEqual("Item2", priorityQueue2.Dequeue()); // FIFO for same priority
        Assert.AreEqual("Item2", priorityQueue2.Dequeue());
        Assert.AreEqual("Item2", priorityQueue2.Dequeue());

    }

    // Add more test cases as needed below.
}