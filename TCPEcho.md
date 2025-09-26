#TCP Echo Server (C#)

**Summary**

TCP is the dominant transport-layer protocol of the Internet. It provides a reliable, ordered, byte-stream abstraction over IP. Unlike UDP, which is connectionless and best-effort, TCP establishes a connection between two endpoints and guarantees that all bytes arrive in order and without loss (or the connection fails).

The TCP Echo server in this repo is a teaching/demo tool: it listens on port 5000, accepts connections, and sends back whatever you type. It’s not useful as an application, but it clearly shows how TCP sockets work: connection setup, stream reading/writing, and orderly teardown.

**How it works**

- Connection-oriented: uses a 3-way handshake (SYN, SYN-ACK, ACK).
- Reliable: retransmits lost packets, ensures all bytes arrive.
- Ordered: delivers data in sequence, even if packets arrive out of order.
- Flow & congestion control: adapts speed to network conditions.
- Full-duplex: both sides can send/receive simultaneously.

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
- Web (HTTP/1.1, HTTP/2, HTTPS).
- Databases and APIs.
- Email protocols (SMTP, IMAP, POP3).
- Secure shells (SSH).
- Streaming when reliability matters more than latency.

**Pros**
- Reliable and widely supported everywhere.
- Abstracts away retransmission, packet loss, and reordering.
- Suitable for long-lived connections (web, DB, remote login).

**Cons**
- Higher latency than UDP due to handshakes and retransmissions.
- Head-of-line blocking: one lost packet can delay subsequent data.
- More overhead in high-throughput, low-latency environments.

**In HFT Context**

In high-frequency trading (HFT), TCP is often too heavy for the critical trading path, but it still plays important roles:
- Market Data APIs: some feeds are over TCP (e.g., FIX protocol).
- Order Entry: many exchanges still accept orders over TCP FIX gateways.
- Control Channels: configuration, heartbeats, session management.
- Testing/Validation: echo-like services are used internally to measure TCP latency and connection health before engaging production systems.

For ultra-low latency (nanoseconds/microseconds), firms prefer UDP or custom kernel-bypass networking (Solarflare/OpenOnload, DPDK). But TCP remains indispensable for reliable infrastructure around trading systems.
