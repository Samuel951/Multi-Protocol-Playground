# DNS Toy (UDP Responder)

## Summary  
DNS (Domain Name System) is the protocol that translates **human-readable names** (like `example.com`) into **IP addresses** that computers use. It typically runs over UDP port **53** (with TCP fallback for large responses or zone transfers).  

This project implements a **toy DNS server** that listens on UDP port **5533** (non-privileged, so it doesn’t require root) and always responds with the same A record:  
- Query: `test.local`  
- Answer: `1.2.3.4`  

It’s a teaching example showing how raw DNS packets are parsed and constructed.

---

## How DNS Works  
- **Query/Response**: clients send a binary-encoded query, servers reply with a resource record.  
- **Hierarchical**: root → TLDs (e.g., `.com`) → authoritative servers.  
- **Resource Records**: A (IPv4), AAAA (IPv6), MX (mail), TXT, etc.  
- **Transport**: usually UDP/53; TCP used if responses don’t fit in 512 bytes or for zone transfers.  
- **Caching**: recursive resolvers cache answers based on TTL.

## Run & Test  

```
# Run server
cd DnsToy
dotnet run -f net7.0
# -> "DNS toy :5533 -> A test.local = 1.2.3.4"

# Test with dig
dig @127.0.0.1 -p 5533 test.local A

# Expected Answer Section:
# test.local. 60 IN A 1.2.3.4
```

---

## General Use Cases for DNS  
- Web browsing (domain → IP).  
- Email delivery (MX lookups).  
- Service discovery (SRV records).  
- Load balancing via DNS round robin.  
- Internal/private name resolution.  

---

## Pros & Cons of DNS  

**Pros**  
- Core part of the Internet, universally supported.  
- Fast lookups via caching and UDP.  
- Flexible with many record types.  

**Cons**  
- UDP transport can be spoofed without protections.  
- Centralised hierarchy (can be a single point of failure).  
- Attack vector: DNS poisoning, amplification attacks.  

---

## 🏦 DNS in HFT Context  
In high-frequency trading, DNS is **not in the latency-critical data path** (trading systems almost always use direct IPs). However, DNS is still important for:  
- **Bootstrapping**: resolving exchange gateways or API endpoints.  
- **Ops/infra**: internal DNS for service discovery across data centres.  
- **Testing/education**: toy DNS servers like this one illustrate packet-level networking that underpins more advanced protocols.  

---

