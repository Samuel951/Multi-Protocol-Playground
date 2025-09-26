#TCP Echo Server (C#)

**Summary**

The TCP Echo server is one of the simplest network services: it listens for incoming TCP connections on port 5000, reads any data a client sends, and sends back the same data prefixed with "echo:". You can think of it as a “network mirror” for testing connectivity and latency.

**How it works**

- Uses a TcpListener to bind to 0.0.0.0:5000.
- Waits for clients (e.g., via nc or nmap --script echo).
- For each client, spins up a handler (HandleAsync) to process the connection.
- Reads raw bytes from the socket stream, decodes as UTF-8, and writes back a response.
- The connection stays open until the client closes it.

**Run & Test**
- Install testing tools
```
sudo apt install nmap -y
```
- Run server
```
cd TcpEcho
dotnet run
```
- Test connectivity and behaviour
```
nc 127.0.0.1 5000
type: hello
response: echo: hello
```
**Use Cases**

- Network debugging: quickly check if TCP connectivity works.
- Measuring round-trip latency between client/server.
- Verifying firewall, NAT, or VPN setups.
- Teaching/learning socket programming basics.

**Pros**

- Very lightweight, minimal code.
- Easy to test with standard tools (nc, nmap).
- Reliable transport (TCP guarantees ordering + delivery).

**Cons**

- No security: anyone can connect and send arbitrary data.
- Blocking resources per connection (not scalable for thousands of clients).
- Echo servers don’t perform useful application logic by themselves.

**In HFT Context**

At a high-frequency trading firm, a TCP Echo service wouldn’t be used directly for trading, but it illustrates key concepts:
- Baseline Latency Testing → measure pure TCP round-trip times without application overhead.
- Connectivity Validation → confirm network paths and ports are reachable across low-latency links (colo → trading gateway).
- Reference Implementation → a minimal echo is often the first step before building custom feed handlers or order gateways, where every microsecond counts.
