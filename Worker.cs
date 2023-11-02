using System.Diagnostics;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.IO; // Add this using directive

public class Worker : BackgroundService
{
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;

    IntPtr consoleWindow = Process.GetCurrentProcess().MainWindowHandle;

    private string username;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (File.Exists("username.txt")) // Check if the file exists
        {
            username = File.ReadAllText("username.txt");
        }
        else
        {
            Console.WriteLine("Enter an admin username:");
            username = Console.ReadLine();
            File.WriteAllText("username.txt", username);
            //hide window
            ShowWindow(consoleWindow, SW_HIDE);
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("ProcElevPipe", PipeDirection.InOut))
            {
                pipeServer.WaitForConnection();
                using (StreamReader reader = new StreamReader(pipeServer))
                using (StreamWriter writer = new StreamWriter(pipeServer))
                {
                    string request = reader.ReadLine();

                    if (!string.IsNullOrWhiteSpace(request))
                    {
                        try
                        {
                            ProcessStartInfo psi = new ProcessStartInfo
                            {
                                FileName = "runas",
                                Arguments = $"/user:{username} /savecred \"cmd /C start {request}\"",
                                Verb = "runas"
                            };

                            Process.Start(psi);
                        }
                        catch (Exception ex)
                        {
                            // Handle the exception
                            // writer.WriteLine("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        // Handle the case where the request is empty
                        // writer.WriteLine("Invalid request.");
                    }
                }
            }
        }
    }
}
