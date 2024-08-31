namespace PermissionTest;

using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using System;


public class FileService
{
    private readonly string folderName = "utpal";

    private string GetFolderPath()
    {
        string internalStoragePath = FileSystem.AppDataDirectory;
        return Path.Combine(internalStoragePath, folderName);
    }

    public async Task EnsureFolderExistsAsync()
    {
        string mainDir = FileSystem.Current.AppDataDirectory;

        mainDir = "/data/user/0/utpal";
        string folderPath = GetFolderPath();
        folderPath = mainDir;
        if (!Directory.Exists(folderPath))
        {
            try
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine($"Directory created: {folderPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating directory: {ex.Message}");
            }
        }
        await ReadTextFile(mainDir);
    }

    public async Task<string> ReadTextFile(string filePath)
    {
        using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(filePath);
        using StreamReader reader = new StreamReader(fileStream);

        return await reader.ReadToEndAsync();
    }
    public async Task<string[]> ListFilesAsync()
    {
        string folderPath = GetFolderPath();
        if (Directory.Exists(folderPath))
        {
            try
            {
                return Directory.GetFiles(folderPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing files: {ex.Message}");
                return Array.Empty<string>();
            }
        }
        return Array.Empty<string>();
    }

    public async Task<string> ReadFileAsync(string fileName)
    {
        string filePath = Path.Combine(GetFolderPath(), fileName);
        if (File.Exists(filePath))
        {
            try
            {
                return await File.ReadAllTextAsync(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return null;
            }
        }
        return null;
    }

    public async Task WriteFileAsync(string fileName, string content)
    {
        string filePath = Path.Combine(GetFolderPath(), fileName);
        try
        {
            await File.WriteAllTextAsync(filePath, content);
            Console.WriteLine($"File written: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing file: {ex.Message}");
        }
    }
}
