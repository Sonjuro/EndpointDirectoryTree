namespace EndpointDirectoryTree {
    public class DirectoryTree {
        Folder root;

        public DirectoryTree() {
            root = new Folder("");
        }

        public string ListFolders() {
            return root.ListFolders();
        }
        
        /// <summary>
        /// Validates the input, and then creates the specified folder.
        /// </summary>
        /// <param name="input">The input array, containing CREATE and NEW_FOLDER_PATH</param>
        /// <exception cref="ArgumentException">NEW_FOLDER_PATH was not specified or is invalid.</exception>
        public void CreateFolder(string[] input) {        
            if (input.Length < 2) {
                throw new ArgumentException(Constants.INVALID_COMMAND);
            } 

            FilePath newFilePath = new FilePath(input[1]);
            if (!newFilePath.IsValidFilePath()) {
                throw new ArgumentException(Constants.INVALID_NEW_FILEPATH);
            }

            root.AddFolder(newFilePath);
        }

        /// <summary>
        /// Validates the input, and then moves the specified folder.
        /// </summary>
        /// <param name="input">The input array, containing MOVE, SOURCE_FOLDER_PATH, and DESTINATION_ROOT_PATH</param>
        /// <exception cref="ArgumentException">Either SOURCE_FOLDER_PATH or DESTINATION_ROOT_PATH was not specified or is invalid.</exception>
        public void MoveFolder(string[] input) {        
            if (input.Length < 3) {
                throw new ArgumentException(Constants.INVALID_COMMAND);
            } 

            FilePath newRootPath = new FilePath(input[2]);
            if (!newRootPath.IsValidFilePath()) {
                throw new ArgumentException(Constants.INVALID_NEW_FILEPATH);
            }

            FilePath existingFilePath = new FilePath(input[1]);
            if (!existingFilePath.IsValidFilePath()) {
                throw new ArgumentException(Constants.INVALID_EXISTING_FILEPATH);
            }

            root.MoveFolder(existingFilePath, newRootPath);
        }

        /// <summary>
        /// Validates the input, and then deletes the specified folder.
        /// </summary>
        /// <param name="input">The input array, containing DELETE and EXISTING_FOLDER_PATH</param>
        /// <exception cref="ArgumentException">EXISTING_FOLDER_PATH was not specified or is invalid.</exception>
        public void DeleteFolder(string[] input) {         
            if (input.Length < 2) {
                throw new ArgumentException(Constants.INVALID_COMMAND);
            } 

            FilePath existingFilePath = new FilePath(input[1]);
            if (!existingFilePath.IsValidFilePath()) {
                throw new ArgumentException(Constants.INVALID_EXISTING_FILEPATH);
            }

            root.DeleteFolder(existingFilePath);
        }
    }
}   
