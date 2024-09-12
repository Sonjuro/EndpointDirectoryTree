namespace EnpointDirectoryTreeTest;

using EndpointDirectoryTree;

[TestFixture]
public class DirectoryTreeTests
{
    private DirectoryTree directoryTree;

    [SetUp]
    public void Setup() {
        directoryTree = new DirectoryTree();
    }

    [Test]
    public void CreateFolder_NewFolder_ShouldPass() {
        string newFolderName = "Test";
        string[] input = new string[] { "CREATE", newFolderName };
        directoryTree.CreateFolder(input);
        Assert.Pass();
    }

    [Test]
    public void CreateFolder_ExistingFolder_ShouldThrowError() {
        string newFolderName = "Test";
        string[] input = new string[] { "CREATE", newFolderName };
        directoryTree.CreateFolder(input);
        Assert.That(() => {directoryTree.CreateFolder(input);}, Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void CreateFolder_NonExistantRoot_ShouldPass() {
        string newFolderName = "Test/NewFolder";
        string[] input = new string[] { "CREATE", newFolderName };
        Assert.Pass();
    }
}