using System;
using System.IO;
using System.Net;
using System.Threading;

public class LocalWebServer
{
    private readonly HttpListener _listener;
    private readonly Thread _serverThread;
    private readonly string _basePath;
    private readonly int _port;

    public LocalWebServer(string basePath, int port = 8000)
    {
        _basePath = basePath;
        _port = port;
        _listener = new HttpListener();
        _listener.Prefixes.Add($"http://localhost:{_port}/");

        _serverThread = new Thread(() =>
        {
            try
            {
                _listener.Start();
                while (_listener.IsListening)
                {
                    var context = _listener.GetContext();
                    ProcessRequest(context);
                }
            }
            catch (HttpListenerException) { } // Handle graceful shutdown
        });
    }

    public void Start()
    {
        _serverThread.Start();
    }

    public void Stop()
    {
        _listener.Stop();
    }

    private void ProcessRequest(HttpListenerContext context)
    {
        string requestedFile = context.Request.Url.LocalPath.TrimStart('/');

        if (string.IsNullOrWhiteSpace(requestedFile))
            requestedFile = "ChatBotLink.html";

        string fullPath = Path.Combine(_basePath, requestedFile.Replace("/", Path.DirectorySeparatorChar.ToString()));

        if (File.Exists(fullPath))
        {
            byte[] content = File.ReadAllBytes(fullPath);
            context.Response.ContentType = GetMimeType(Path.GetExtension(fullPath));
            context.Response.OutputStream.Write(content, 0, content.Length);
        }
        else
        {
            context.Response.StatusCode = 404;
            using var writer = new StreamWriter(context.Response.OutputStream);
            writer.Write("<h1>404 - File Not Found</h1>");
        }

        context.Response.OutputStream.Close();
    }

    private string GetMimeType(string extension)
    {
        return extension.ToLower() switch
        {
            ".html" => "text/html",
            ".css" => "text/css",
            ".js" => "application/javascript",
            ".json" => "application/json",
            _ => "application/octet-stream",
        };
    }
}
