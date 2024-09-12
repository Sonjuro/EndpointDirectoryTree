namespace EndpointDirectoryTree;

internal class Program {
    private static DirectoryTree directoryTree;

    static void Main(string[] args)
    {
        directoryTree = new DirectoryTree();

        Console.WriteLine("Enter a command:");
        while (true)
        {
            string? input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input)) {
                ProcessCommand(input);
            }
        }
    }

    /// <summary>
    /// Parses the input into COMMAND and PARAMS (if applicable), then processes the request.
    /// </summary>
    /// <param name="input">The user input to process.</param>
    static void ProcessCommand(string input) {
        string[] tokens = input.Split(' ');

        try
        {
            switch (tokens[0].ToUpper())
            {
                case "LIST": 
                    Console.Write(directoryTree.ListFolders());
                    break;
                case "CREATE": 
                    directoryTree.CreateFolder(tokens);
                    break;
                case "MOVE": 
                    directoryTree.MoveFolder(tokens);
                    break;
                case "DELETE": 
                    directoryTree.DeleteFolder(tokens);
                    break;
                case "HELP":
                    ProcessHelpCommand();
                    break;
                case "EXIT":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine(Constants.INVALID_COMMAND);
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    static void ProcessHelpCommand() {
        Console.WriteLine("To print out the current directory tree, type 'LIST'.");
        Console.WriteLine("To create a new folder, type 'CREATE <NEW_FOLDER_PATH>'.");
        Console.WriteLine("To delete an existing folder, type 'DELETE <EXISTING_FOLDER_PATH>'.");
        Console.WriteLine("To move an existing folder, type 'MOVE <SOURCE_FOLDER_PATH> <DESTINATION_ROOT_PATH>'.");
        Console.WriteLine("To end this session, type 'EXIT'.");
    }
}
