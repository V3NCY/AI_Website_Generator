from http.server import BaseHTTPRequestHandler, HTTPServer
import json
import os

class Handler(BaseHTTPRequestHandler):
    def do_OPTIONS(self):
        self.send_response(200, "ok")
        self.send_header("Access-Control-Allow-Origin", "*")
        self.send_header("Access-Control-Allow-Methods", "POST, OPTIONS")
        self.send_header("Access-Control-Allow-Headers", "Content-Type")
        self.end_headers()

    def do_POST(self):
        if self.path == "/submit":
            try:
                content_length = int(self.headers['Content-Length'])
                body = self.rfile.read(content_length)
                new_request = json.loads(body)

                # Път до файла
                file_path = "requests.json"

                # Четем съществуващите заявки или създаваме празен списък
                if os.path.exists(file_path):
                    with open(file_path, "r", encoding="utf-8") as f:
                        try:
                            requests = json.load(f)
                        except json.JSONDecodeError:
                            requests = []
                else:
                    requests = []

                # Добавяме новата заявка
                requests.append(new_request)

                # Записваме обратно масива
                with open(file_path, "w", encoding="utf-8") as f:
                    json.dump(requests, f, ensure_ascii=False, indent=2)

                self.send_response(200)
                self.send_header("Access-Control-Allow-Origin", "*")
                self.end_headers()
                self.wfile.write(b"OK")

            except Exception as e:
                self.send_response(500)
                self.send_header("Access-Control-Allow-Origin", "*")
                self.end_headers()
                error_msg = f"Server error: {str(e)}"
                self.wfile.write(error_msg.encode("utf-8"))

httpd = HTTPServer(("localhost", 8888), Handler)
print("✅ Listening on http://localhost:8888")
httpd.serve_forever()
