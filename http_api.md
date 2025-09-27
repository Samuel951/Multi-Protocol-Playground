## HTTP API (Minimal API with ASP.NET Core)

## Summary  
HTTP (Hypertext Transfer Protocol) is the foundation of the web. It operates over TCP (and increasingly over TLS for HTTPS) and uses a **request/response model** where clients send requests (GET, POST, etc.) and servers respond with status codes and data.  

This project demonstrates a **Minimal API** using ASP.NET Core (`dotnet new web`). It provides a health check endpoint (`/health`) and an echo endpoint (`/echo`) to show how lightweight HTTP services can be built without the overhead of MVC or full Web API frameworks.

---

## How HTTP Works  
- **Client-server model**: clients initiate all requests; servers respond.  
- **Stateless**: each request is independent (though cookies/sessions can add state).  
- **Text-based (HTTP/1.1)**: headers + optional body.  
- **Binary framing (HTTP/2, HTTP/3)**: multiplexed streams, header compression.  
- **Secure by default (HTTPS)**: TLS encrypts requests and responses.

```
# Run server
cd HttpApi
dotnet run -f net8.0
# Kestrel will listen on http://localhost:5000 by default

# Test health endpoint
curl -i http://127.0.0.1:5000/health
# -> {"ok":true}

# Test echo endpoint
curl -s http://127.0.0.1:5000/echo -X POST -d 'hello world'
# -> {"you_sent":"hello world"}
```

---

## General Use Cases for HTTP  
- REST APIs and microservices.  
- Browsers fetching web pages.  
- Mobile apps communicating with backends.  
- IoT devices posting telemetry.  
- Anything requiring interoperable request/response semantics.  

---

## Pros & Cons of HTTP  

**Pros**  
- Universally supported and understood.  
- Works seamlessly across firewalls, NAT, and proxies.  
- Flexible (supports JSON, XML, binary payloads).  
- Modern versions (HTTP/2, HTTP/3) are efficient and fast.  

**Cons**  
- Request/response only; no native push (WebSockets or SSE needed for realtime).  
- Overhead in headers compared to custom binary protocols.  
- Latency from handshakes and TLS negotiation (though mitigated by keep-alive).  

---

## HTTP in HFT Context  
In high-frequency trading, HTTP is not used for critical **market data or order entry**, since those require ultra-low latency. Instead, HTTP APIs are invaluable for:  
- **Configuration** → REST endpoints for strategy parameters or risk controls.  
- **Monitoring** → `/health` endpoints for load balancers and orchestration.  
- **Control planes** → orchestration tools and dashboards use HTTP for management.  
- **Integration** → connecting with non-latency-critical enterprise systems.  

---
