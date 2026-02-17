using NUnit.Framework;
using ShootCommon.InteractiveObjectsSpawnerService;
using ShootCommon.InteractiveObjectsSpawnerService.Tests.tests.Mocks;

public class InteractiveObjectsManagerTest
{

    [Test]
    public void CheckAddItem()
    {
        InteractiveObjectsManager manager = InitManager();
        Assert.AreEqual(false, manager.ContainerIsExists("testContainer"));
        manager.AddContainer("testContainer",  new InteractiveObjectContainerMock());
        Assert.AreEqual(true, manager.ContainerIsExists("testContainer"));
    }
    
    [Test]
    public void CheckRemoveItem()
    {
        InteractiveObjectsManager manager = InitManager();
        Assert.AreEqual(false, manager.ContainerIsExists("testContainer"));
        manager.AddContainer("testContainer",  new InteractiveObjectContainerMock());
        Assert.AreEqual(true, manager.ContainerIsExists("testContainer"));
        manager.RemoveContainer("testContainer");
        Assert.AreEqual(false, manager.ContainerIsExists("testContainer"));
    }
    
    [Test]
    public void CheckAddTheSameItem()
    {
        InteractiveObjectsManager manager = InitManager();
        Assert.AreEqual(false, manager.ContainerIsExists("testContainer"));
        InteractiveObjectContainerMock container1 = new InteractiveObjectContainerMock();
        InteractiveObjectContainerMock container2 = new InteractiveObjectContainerMock();
        manager.AddContainer("testContainer",  container1);
        Assert.AreEqual(true, manager.ContainerIsExists("testContainer"));
        manager.AddContainer("testContainer",  container2);
        Assert.AreEqual(true, manager.ContainerIsExists("testContainer"));
    }

    private InteractiveObjectsManager InitManager()
    {
        InteractiveObjectsManager manager = new InteractiveObjectsManager();
        return manager;
    }
}
