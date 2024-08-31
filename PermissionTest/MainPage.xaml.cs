namespace PermissionTest
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly FileService fileService = new FileService();
        public MainPage()
        {
            InitializeComponent();
            // Ensure the 'utpal' folder exists
            _ = fileService.EnsureFolderExistsAsync();

            // Write a file
            _ = fileService.WriteFileAsync("example.txt", "Hello, world!");

            // Read a file
            string content = _ = fileService.ReadFileAsync("example.txt").Result;
            Console.WriteLine(content);

            // List files
            string[] files = _ = fileService.ListFilesAsync().Result;
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
