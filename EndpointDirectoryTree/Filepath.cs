using System.Text;

namespace EndpointDirectoryTree;
public class FilePath {
    private string filePath;
    private Queue<string> folders;
    private bool isValidFilePath;
    private StringBuilder currentFilePath;

    public FilePath(string filePath) {
        this.filePath = filePath == "/" ? "" : filePath;        
        ValidateFilePath();
        folders = CreateFoldersQueue();
        currentFilePath = new StringBuilder();
    }

    private void ValidateFilePath() {
        // Can add logic here to check for certain chars, empty folder names, etc.
        isValidFilePath = true;
    }

    private Queue<string> CreateFoldersQueue() {
        if (isValidFilePath && !string.IsNullOrEmpty(filePath)) {
            string[] tokens = filePath.Split('/');
            return new Queue<string>(tokens);
        } else {
            return new Queue<string>();
        }
    }

    /// <summary>
    /// Get the next folder name in the hierarchy.  Also Advances the hierarchy 1 level.
    /// </summary>
    /// <returns>The name of the next folder.</returns>
    public string GetNextFolder() {
        if (folders.Count == 0) {
            return "";
        }
        
        /*
            For cleaner code (single purpose), this should be a peek, and a separate "AdvanceToNextFolder()" method added,
            however the current design always does them at the same time, so this saves a call.
        */
        string nextFolder = folders.Dequeue();
        if (currentFilePath.Length > 0) {
            currentFilePath.Append("/");
        }
        currentFilePath.Append(nextFolder);

        return nextFolder;
    }

    public bool IsValidFilePath() {
        return isValidFilePath;
    }

    /// <summary>
    /// Gets how many folders are left in the filepath.
    /// </summary>
    /// <returns>The number of folders left in the filepath.</returns>
    public int GetFoldersLeftCount() {
        return folders.Count;
    }

    /// <summary>
    /// Returns the original filepath string.
    /// </summary>
    /// <returns>The original filepath.</returns>
    public override string ToString() {
        return filePath;
    }

    /// <summary>
    /// Gets the full filepath upto the current folder.
    /// </summary>
    /// <returns>The current folder's filepath.</returns>
    public string GetCurrentFolderFilePath() {
        return currentFilePath.ToString();
    }
}
