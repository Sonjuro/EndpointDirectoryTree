dotnet build -c release
dotnet run -c release

# EndpointDirectoryTree

EndpointDirectoryTree is a C# Command Line Application to simulate a directory tree.

## Building and Running

Ensure that `.Net SDK 8.0` is installed.

This project was created in VS Code with `.NET Install Tool`, `C#`, `C# Dev Kit`, and `.NET Core Test Explorer` extensions installed.

From the root `EndpointDirectoryTree` solution folder (with the `.sln` file), run `dotnet build -c release` to create the release build.

From the `EndpointDirectoryTree` project folder (with the `.csproj` file), run `dotnet run -c release` to run the release build.

## Usage

EndpointDirectoryTree supports creating, deleting, and moving folders, as well as printing the full directory tree.  The following commands are allowed:

* To list out these commands, type `HELP`
* To print out the current directory tree, type `LIST`.  Folders are sorted by name, and subfolders are indented.
* To create a new folder, type `CREATE <NEW_FOLDER_PATH>`, where `<NEW_FOLDER_PATH>` is a valid filepath ending with the new folder's name. Any folders in the `<NEW_FOLDER_PATH>` that do not exist will be created.
  * eg: `CREATE fruits/apples`
* To delete an existing folder, type `DELETE <EXISTING_FOLDER_PATH>`. All subfolders will also be deleted.
  * eg: `DELETE fruits/apples`
* To move an existing folder, type `MOVE <SOURCE_FOLDER_PATH> <DESTINATION_ROOT_PATH>`. Any folders in the `<DESTINATION_ROOT_PATH>` that do not exist will be created. *NOTE: `<DESTINATION_ROOT_PATH>` does not contain the SourceFolder's name.
  * eg: `MOVE grains/squash vegetables`
  * To move a folder to the root directory, enter `/` for the `<DESTINATION_ROOT_PATH>`
* To end the session, type `EXIT`

Multiple commands can be pasted into the terminal.  As an example, the following script:

```
CREATE fruits
CREATE vegetables
CREATE grains
CREATE fruits/apples
CREATE fruits/apples/fuji
LIST
CREATE grains/squash
MOVE grains/squash vegetables
CREATE foods
MOVE grains foods
MOVE fruits foods
MOVE vegetables foods
LIST
DELETE fruits/apples
DELETE foods/fruits/apples
LIST
```
results in the following result:
```
CREATE fruits
CREATE vegetables
CREATE grains
CREATE fruits/apples
CREATE fruits/apples/fuji
LIST
fruits
  apples
    fuji
grains
vegetables
CREATE grains/squash
MOVE grains/squash vegetables
CREATE foods
MOVE grains foods
MOVE fruits foods
MOVE vegetables foods
LIST
foods
  fruits
    apples
      fuji
  grains
  vegetables
    squash
DELETE fruits/apples
Cannot delete fruits/apples - fruits does not exist.
DELETE foods/fruits/apples
LIST
foods
  fruits
  grains
  vegetables
    squash
```