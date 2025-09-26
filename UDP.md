## UDP (User Datagram Protocol) — Echo Example

##  Summary
UDP is the other major **transport-layer protocol** alongside TCP. Unlike TCP, it is **connectionless, unreliable, and unordered** — packets (datagrams) are simply sent to a destination IP/port without guarantees of delivery, ordering, or duplication protection.  

The **UDP Echo server** in this repo is a teaching/demo tool: it listens on port `5001`, receives datagrams, and sends them back. This illustrates UDP’s one-shot “send and forget” model: each message is independent, small, and fast to process.

---

##  How UDP Works
- **Connectionless**: no handshake; senders just push datagrams.  
- **Unreliable**: no retransmission, no delivery guarantee.  
- **Message-oriented**: preserves datagram boundaries (one send = one recv).  
- **Low overhead**: just an 8-byte header (src/dst port, length, checksum).  
- **Best-effort delivery**: the network may drop, delay, or reorder packets.  

##  Run & Test  

```
# Install testing tools
sudo apt install nmap -y

# Run server
cd UdpEcho
dotnet run -f net7.0

# Test connectivity and behaviour
echo -n "ping" | nc -u -w1 127.0.0.1 5001
# server logs "UDP 127.0.0.1:xxxxx ping"
# client receives "echo: ping"

```
---

##  General Use Cases for UDP
- Real-time streaming (VoIP, video, gaming).  
- DNS queries (fast, lightweight lookups).  
- Time sync protocols (NTP).  
- Service discovery (mDNS, SSDP).  
- Custom low-latency market data feeds.  

---

##  Pros & Cons of UDP

**Pros**  
- Extremely low latency (no handshake or retransmissions).  
- Small protocol overhead, very efficient.  
- Ideal for multicast/broadcast and one-to-many communication.  
- Fits applications where **fresh data matters more than reliable delivery**.  

**Cons**  
- No built-in reliability or ordering.  
- No congestion control — apps must implement their own if needed.  
- Packets can be lost, duplicated, or arrive out of order.  
- Firewalls/NATs sometimes block or restrict UDP traffic.  

---

##  UDP in HFT Context
In high-frequency trading, UDP is often preferred for **market data distribution** because:  
- Exchanges multicast market data over UDP, allowing all participants to receive updates simultaneously with minimal latency.  
- Dropped packets are acceptable — traders want the *latest* quote/book update, not guaranteed delivery of every past one.  
- Low overhead and zero handshake mean faster dissemination of tick data.  

However, **order entry** usually still runs over TCP/FIX (for reliability). UDP shines in **feed handlers**, where missing a few updates is acceptable, but speed is paramount.  

---



