using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            catch (HttpListenerException) { }
        });
    }

    public void Start() => _serverThread.Start();

    public void Stop() => _listener.Stop();

    private void ProcessRequest(HttpListenerContext context)
    {
        // ✅ Handle POST: /submit
        if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/submit")
        {
            HandleFormSubmission(context);
            return;
        }

        // ✅ Handle GET: static file serving
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

    private void HandleFormSubmission(HttpListenerContext context)
    {
        using var reader = new StreamReader(context.Request.InputStream);
        var body = reader.ReadToEnd();

        object newRequest;
        try
        {
            newRequest = JsonConvert.DeserializeObject<object>(body);
        }
        catch (JsonReaderException)
        {
            context.Response.StatusCode = 400;
            using var writer = new StreamWriter(context.Response.OutputStream);
            writer.Write("{\"error\":\"Invalid JSON\"}");
            context.Response.OutputStream.Close();
            return;
        }

        string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "requests.json");
        List<object> existingRequests;

        if (File.Exists(jsonPath))
        {
            string existingJson = File.ReadAllText(jsonPath);
            try
            {
                existingRequests = JsonConvert.DeserializeObject<List<object>>(existingJson) ?? new List<object>();
            }
            catch
            {
                existingRequests = new List<object>(); 
            }
        }
        else
        {
            existingRequests = new List<object>();
        }

        existingRequests.Add(newRequest);
        File.WriteAllText(jsonPath, JsonConvert.SerializeObject(existingRequests, Formatting.Indented));

        context.Response.StatusCode = 200;
        using var writerOut = new StreamWriter(context.Response.OutputStream);
        writerOut.Write("{\"status\":\"ok\"}");
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
            ".png" => "image/png",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            _ => "application/octet-stream",
        };
    }
}