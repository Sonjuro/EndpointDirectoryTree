namespace EnpointDirectoryTreeTest;

using EndpointDirectoryTree;

[TestFixture]
public class FolderTests
{
    private Folder root;

    [SetUp]
    public void Setup() {
        root = new Folder("");
    }

    [Test]
    public void ContainsFolder_NonExistantFolder_ShouldBeFalse() {
        string filePath = "Test";

        Assert.That(!root.ContainsFolder(new FilePath(filePath)));
    }

    [Test]
    public void AddFolder_RootFolder_ShouldCreateNewFolder() {
        string filePath = "Test";

        root.AddFolder(new FilePath(filePath));

        Assert.That(root.ContainsFolder(new FilePath(filePath)));
    }

    [Test]
    public void AddFolder_SubFolder_ShouldCreateNewFolder() {
        string filePath = "Test";
        string nestedFilePath = "Test/Test1";

        root.AddFolder(new FilePath(filePath));
        root.AddFolder(new FilePath(nestedFilePath));

        Assert.That(root.ContainsFolder(new FilePath(nestedFilePath)));
    }

    [Test]
    public void AddFolder_NestedFolder_ShouldCreateAllFolders() {
        string filePath = "Test";
        string nestedFilePath = "Test/Test1";

        root.AddFolder(new FilePath(nestedFilePath));

        Assert.That(root.ContainsFolder(new FilePath(filePath)));
        Assert.That(root.ContainsFolder(new FilePath(nestedFilePath)));
    }

    [Test]
    public void AddFolder_ExistingFolder_ShouldThrowError() {
        string filePath = "Test";

        root.AddFolder(new FilePath(filePath));

        Assert.That(() => {root.AddFolder(new FilePath(filePath));}, Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void DeleteFolder_ExistingFolder_ShouldNoLongerExist() {
        string filePath = "Test";

        root.AddFolder(new FilePath(filePath));
        root.DeleteFolder(new FilePath(filePath));

        Assert.That(!root.ContainsFolder(new FilePath(filePath)));
    }

    [Test]
    public void DeleteFolder_ExistingFolderWithSubFolders_ShouldDeleteAllSubFolders() {
        string filePath = "Test";
        string nestedFilePath = "Test/Test1";

        root.AddFolder(new FilePath(nestedFilePath));
        root.DeleteFolder(new FilePath(filePath));

        Assert.That(!root.ContainsFolder(new FilePath(filePath)));
        Assert.That(!root.ContainsFolder(new FilePath(nestedFilePath)));
    }

    [Test]
    public void DeleteFolder_NonExistantFolder_ShouldThrowError() {
        string filePath = "Test";

        Assert.That(() => {root.DeleteFolder(new FilePath(filePath));}, Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void MoveFolder_NonExistantFolder_ShouldThrowError() {
        string sourceFilePath = "Test";
        string destinationFilePath = "";

        Assert.That(() => {root.MoveFolder(new FilePath(sourceFilePath), new FilePath(destinationFilePath));}, Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void MoveFolder_ExistingDestinationFolder_ShouldThrowError() {
        string sourceFilePath = "Test/TestSource";
        string destinationFilePath = "Test2";
        string newFilePath = "Test2/TestSource";
        
        root.AddFolder(new FilePath(sourceFilePath));
        root.AddFolder(new FilePath(newFilePath));

        Assert.That(() => {root.MoveFolder(new FilePath(sourceFilePath), new FilePath(destinationFilePath));}, Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void MoveFolder_ExistingSourceFolder_ShouldBeMoved() {
        string sourceFilePath = "Test/TestSource";
        string destinationFilePath = "Test2";
        string newFilePath = "Test2/TestSource";

        root.AddFolder(new FilePath(sourceFilePath));
        root.AddFolder(new FilePath(destinationFilePath));

        root.MoveFolder(new FilePath(sourceFilePath), new FilePath(destinationFilePath));
        Assert.That(!root.ContainsFolder(new FilePath(sourceFilePath)));
        Assert.That(root.ContainsFolder(new FilePath(newFilePath)));
    }

    [Test]
    public void MoveFolder_ExistingSourceFolder_ShouldCreateNewRoots() {
        string sourceFilePath = "Test/TestSource";
        string destinationFilePath = "Test2";
        string newFilePath = "Test2/TestSource";

        root.AddFolder(new FilePath(sourceFilePath));

        root.MoveFolder(new FilePath(sourceFilePath), new FilePath(destinationFilePath));
        Assert.That(!root.ContainsFolder(new FilePath(sourceFilePath)));
        Assert.That(root.ContainsFolder(new FilePath(destinationFilePath)));
        Assert.That(root.ContainsFolder(new FilePath(newFilePath)));
    }
}