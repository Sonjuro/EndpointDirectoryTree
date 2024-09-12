using System.Text;

namespace EndpointDirectoryTree;
public class Folder {
    private string folderName;
    private Dictionary<string, Folder> subfolders;
    private bool isRoot;

    public Folder(string folderName) {
        this.folderName = folderName;
        subfolders = new Dictionary<string, Folder>();
        isRoot = string.IsNullOrEmpty(folderName);
    }

    public bool ContainsFolder(FilePath folderName) {
        string nextFolderName = folderName.GetNextFolder();

        if (folderName.GetFoldersLeftCount() == 0) {
            return subfolders.ContainsKey(nextFolderName);
        } else {
            if (!subfolders.ContainsKey(nextFolderName)) {
                return false;
            }
            Folder nextSubFolder = subfolders[nextFolderName];
            return nextSubFolder.ContainsFolder(folderName);
        }
    }

    /// <summary>
    /// Adds the new folder at the specified filepath, creating any missing folders on the way.
    /// </summary>
    /// <param name="folderToAdd">The folder to add.</param>
    /// <exception cref="ArgumentException">The new folder already exists.</exception>
    public void AddFolder(FilePath folderToAdd) {
        if (isRoot) {
            FilePath newFilePath = new FilePath(folderToAdd.ToString());
            if (ContainsFolder(newFilePath)) {
                throw new ArgumentException(newFilePath + " already exists.");
            }
        }

        string nextFolderName = folderToAdd.GetNextFolder();

        if (folderToAdd.GetFoldersLeftCount() == 0) {
            subfolders.Add(nextFolderName, new Folder(nextFolderName));
        } else {
            Folder nextSubFolder;

            if (!subfolders.ContainsKey(nextFolderName)) {
                nextSubFolder = new Folder(nextFolderName);
                subfolders.Add(nextFolderName, nextSubFolder);
            } else {
                nextSubFolder = subfolders[nextFolderName];
            }

            nextSubFolder.AddFolder(folderToAdd);
        }
    }

    /// <summary>
    /// Moves the sourceFolder to inside of the destinationFolder.
    /// </summary>
    /// <param name="sourceFolder">The folder to move.</param>
    /// <param name="destinationFolder">The desired parent folder for sourceFolder.  Do not include sourceFolder's name.</param>
    /// <exception cref="ArgumentException">The sourceFolder does not exist or a folder in the destinationFolder already exists with that name.</exception>
    public void MoveFolder(FilePath sourceFolder, FilePath destinationFolder) {
        Folder oldRootFolder = this;
        string nextFolderName = sourceFolder.GetNextFolder();

        while (sourceFolder.GetFoldersLeftCount() > 0) {
            if (!oldRootFolder.subfolders.ContainsKey(nextFolderName)) {
                throw new ArgumentException("Cannot move " + sourceFolder + " - " + sourceFolder.GetCurrentFolderFilePath() + " does not exist.");
            }
            oldRootFolder = oldRootFolder.subfolders[nextFolderName];
            nextFolderName = sourceFolder.GetNextFolder();
        }

        if (!oldRootFolder.subfolders.ContainsKey(nextFolderName)) {
            throw new ArgumentException("Cannot move " + sourceFolder + " - " + sourceFolder.GetCurrentFolderFilePath() + " does not exist.");
        }
        Folder folderToMove = oldRootFolder.subfolders[nextFolderName];

        FilePath newFilePath = new FilePath(destinationFolder + "/" + folderToMove.folderName);
        if (ContainsFolder(newFilePath)) {
            throw new ArgumentException(newFilePath + " already exists.");
        }

        Folder newRootFolder = this;
        while (destinationFolder.GetFoldersLeftCount() > 0) {
            nextFolderName = destinationFolder.GetNextFolder();

            if (!newRootFolder.subfolders.ContainsKey(nextFolderName)) {
                Folder nextSubFolder = new Folder(nextFolderName);
                newRootFolder.subfolders.Add(nextFolderName, nextSubFolder);
                newRootFolder = nextSubFolder;
            } else {
                newRootFolder = newRootFolder.subfolders[nextFolderName];
            }
        }

        newRootFolder.subfolders.Add(folderToMove.folderName, folderToMove);
        oldRootFolder.subfolders.Remove(folderToMove.folderName);
    }

    /// <summary>
    /// Deletes the specified folder, and any subfolders.
    /// </summary>
    /// <param name="folderToDelete">The folder to delete.</param>
    /// <exception cref="ArgumentException">The specified folder does not exist.</exception>
    public void DeleteFolder(FilePath folderToDelete) {
        string nextFolderName = folderToDelete.GetNextFolder();

        if (!subfolders.ContainsKey(nextFolderName)) {
            throw new ArgumentException("Cannot delete " + folderToDelete + " - " + folderToDelete.GetCurrentFolderFilePath() + " does not exist.");
        }
        Folder nextSubFolder = subfolders[nextFolderName];

        if (folderToDelete.GetFoldersLeftCount() == 0) {
            nextSubFolder.DeleteAllSubFolders();
            subfolders.Remove(nextFolderName);
        } else {
            nextSubFolder.DeleteFolder(folderToDelete);
        }
    }

    private void DeleteAllSubFolders() {
        foreach (Folder folder in subfolders.Values) {
            folder.DeleteAllSubFolders();
        }
        subfolders.Clear();
    }

    /// <summary>
    /// Creates a string representation of the current Directory Tree.  Subfolders are indented.
    /// </summary>
    /// <param name="indent">The starting indentation.  Defaults to "".</param>
    /// <returns>The string printout of the Directory Tree.</returns>
    public string ListFolders(string indent = "") {
        StringBuilder sb = new StringBuilder(isRoot ? "" : indent + folderName + "\n");

        string nextIndent = isRoot ? indent : indent + Constants.TAB_SPACE;
        foreach (KeyValuePair<string, Folder> folder in subfolders.OrderBy(f => f.Key)) {
            sb.Append(folder.Value.ListFolders(nextIndent));
        }

        return sb.ToString();
    }
}
