using System.Diagnostics;
using System.IO.Pipes;


public class Worker : BackgroundService
{
    private string username;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("ProcElevPipe", PipeDirection.InOut))
            {
                pipeServer.WaitForConnection();
                using (StreamReader reader = new StreamReader(pipeServer))
                using (StreamWriter writer = new StreamWriter(pipeServer))
                
                {
                    string request = reader.ReadLine();
                    try
                    {
                        username = File.ReadAllText("username.txt");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please create username.txt of the user you will be running as. Then restart the program.");
                    }

                    
                    if (!string.IsNullOrWhiteSpace(request))
                    {
                        try
                        {
                            ProcessStartInfo psi = new ProcessStartInfo
                            {
                                FileName = "runas",
                                Arguments = $"/user:{username} /savecred \"{request}\"",
                                Verb = "runas"
                            };

                            Process.Start(psi);
                        }
                        catch (Exception ex)
                        {
                            writer.WriteLine("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        writer.WriteLine("Invalid request.");
                    }
                }
            }
        }
    }
}
