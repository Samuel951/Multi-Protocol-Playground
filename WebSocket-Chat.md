## WebSocket Chat (ASP.NET Core)

## Summary
WebSockets provide a **full-duplex, persistent communication channel** between a client and a server over a single TCP connection. Unlike HTTP’s request/response model, WebSockets allow **bi-directional, real-time messaging**, making them ideal for chat apps, trading dashboards, multiplayer games, and collaborative tools.

The **WebSocket Chat server** in this repo demonstrates this by allowing multiple clients to connect and broadcast messages to each other. It runs on Kestrel (ASP.NET Core) and exposes a `/ws` endpoint for WebSocket connections.

---

## How WebSockets Work
- **Handshake**: Starts as an HTTP(S) request with `Upgrade: websocket`.  
- **Persistent connection**: After upgrade, the connection stays open until explicitly closed.  
- **Full-duplex**: Both server and client can send messages anytime.  
- **Framed messages**: Supports text and binary payloads.  
- **Low overhead**: No repeated HTTP headers after the upgrade.

## Run & Test  


- Install testing tool
```
sudo apt install nodejs npm -y
sudo npm install -g wscat
```

- Run server
```
cd WebSocketChat
dotnet run
```
- Kestrel will listen on :5000

- Test connectivity
- In one terminal:
```
wscat -c ws://127.0.0.1:5000/ws
```
- In another terminal:
```
wscat -c ws://127.0.0.1:5000/ws
```
- Type a message in one client, and it appears in the other.
---

##  General Use Cases for WebSockets
- Realtime chat and collaboration apps.  
- Trading dashboards (live order books, tickers, alerts).  
- Multiplayer online games.  
- Live telemetry or IoT dashboards.  
- Push notifications without polling.  

---

##  Pros & Cons of WebSockets

**Pros**  
- Real-time, bi-directional communication.  
- Efficient compared to repeated HTTP requests (polling).  
- Works over existing web infrastructure (80/443).  
- Widely supported in browsers and libraries.  

**Cons**  
- More complex to implement than plain HTTP.  
- Not ideal for one-off requests (HTTP is simpler).  
- Requires stateful connections, which can affect scalability.  
- Proxies/firewalls may block or interfere in some setups.  

---

## WebSockets in HFT Context
In high-frequency trading, WebSockets are **not used in latency-critical market data or order entry**, since those rely on UDP or TCP FIX gateways.  
But they are very useful for:  
- **Trading GUIs** → push live market data, positions, and P&L to dashboards.  
- **Control & Monitoring** → feed operational metrics to web consoles.  
- **Prototypes/Demos** → quickly show streaming data without building a custom binary protocol.  

---


