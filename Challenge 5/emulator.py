from http.server import BaseHTTPRequestHandler, HTTPServer
import time
import logging

hostName = "localhost"
serverPort = 80

def utf16_to_ascii(data):
    return data.decode("utf-16")   

class MyServer(BaseHTTPRequestHandler):
    def do_POST(self):
        print("The current request line send is: %s\n" % self.requestline)
        print("The current command is %s\n" % self.command)
        print("The requested path is %s\n" % self.path)
        content_length = int(self.headers['Content-Length'])
        post_data = self.rfile.read(content_length)
        post_data_str = utf16_to_ascii(post_data)
        self.send_response(200)
        self.end_headers()
        if "ydN8BXq16RE=" in post_data_str:
            print("Writing first post response\n")
            self.wfile.write(bytes("TdQdBRa1nxGU06dbB27E7SQ7TJ2+cd7zstLXRQcLbmh2nTvDm1p5IfT/Cu0JxShk6tHQBRWwPlo9zA1dISfslkLgGDs41WK12ibWIflqLE4Yq3OYIEnLNjwVHrjL2U4Lu3ms+HQc4nfMWXPgcOHb4fhokk93/AJd5GTuC5z+4YsmgRh1Z90yinLBKB+fmGUyagT6gon/KHmJdvAOQ8nAnl8K/0XG+8zYQbZRwgY6tHvvpfyn9OXCyuct5/cOi8KWgALvVHQWafrp8qB/JtT+t5zmnezQlp3zPL4sj2CJfcUTK5copbZCyHexVD4jJN+LezJEtrDXP1DJNg==", "utf-8"))
        
        if "VYBUpZdG" in post_data_str:
            print("Writing second post response\n")
            self.wfile.write(bytes("F1KFlZbNGuKQxrTD/ORwudM8S8kKiL5F906YlR8TKd8XrKPeDYZ0HouiBamyQf9/Ns7u3C2UEMLoCA0B8EuZp1FpwnedVjPSdZFjkieYqWzKA7up+LYe9B4dmAUM2lYkmBSqPJYT6nEg27n3X656MMOxNIHt0HsOD0d+", "utf-8"))


def main():    
    webServer = HTTPServer((hostName, serverPort), MyServer)
    print("FLARE-ON Server started http://%s:%s" % (hostName, serverPort))

    try:
        webServer.serve_forever()
    except KeyboardInterrupt:
        pass

    webServer.server_close()
    print("FLARE-ON server stopped.")

if __name__ == '__main__':
	main()