#🔌 TCP Echo Server (C#)

📖 Summary

The TCP Echo server is one of the simplest network services: it listens for incoming TCP connections on port 5000, reads any data a client sends, and sends back the same data prefixed with "echo:". You can think of it as a “network mirror” for testing connectivity and latency.

⚙️ How it works

- Uses a TcpListener to bind to 0.0.0.0:5000.
- Waits for clients (e.g., via nc or nmap --script echo).
- For each client, spins up a handler (HandleAsync) to process the connection.
- Reads raw bytes from the socket stream, decodes as UTF-8, and writes back a response.
- The connection stays open until the client closes it.

Run & Test
- Install testing tools
```
sudo apt install nmap -y
```
- Run server
```
cd TcpEcho
dotnet run -f net7.0
```
- Test connectivity and behaviour
```
nc 127.0.0.1 5000
type: hello
response: echo: hello
```
