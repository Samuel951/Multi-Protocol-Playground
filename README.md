# 🧪 Multiprotocol Playground

This repository is a **playground of networking projects** implemented in C# with **.NET 7** and **.NET 8**.  
Each project explores a different protocol — TCP, UDP, HTTP, WebSockets, DNS, MQTT — with simple working servers or clients that you can run and test locally.  

I built this repo to:  
- **Learn by doing** → understand how fundamental protocols actually work at the socket/API level.  
- **Explore use cases** → see how protocols like gRPC, MQTT, and QUIC fit into modern systems.  
- **Prepare for HFT (High-Frequency Trading) contexts** → where low-latency networking, protocol choice, and debugging tools are crucial.  
- **Have a reusable lab** → I can extend these toys into future experiments.  

---

## ⚙️ Prerequisites

These samples are meant to run in **WSL2 with Ubuntu**.  

### Install basics
```bash
# Update packages
sudo apt update && sudo apt -y upgrade

# Get wget for fetching installers/files
sudo apt -y install wget curl

# Install nmap (for ncat, netcat equivalents, testing TCP/UDP)
sudo apt -y install nmap

# Add Microsoft package feed
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt update

# Install both SDKs
sudo apt -y install dotnet-sdk-7.0 dotnet-sdk-8.0

# Verify
dotnet --info
```

##📂 Projects

Each project has its own folder and a detailed markdown file with:
- A summary of how the protocol works.
- Instructions for running the code.
- Test commands (using curl, nc, dig, grpcurl, etc).
- Pros/cons and potential HFT relevance.

- DnsToy.md
- HttpApi.md
- MqttLab.md
- TcpEcho.md
- UdpEcho.md
- WebSocketChat.md

##🏦 Why This Matters for HFT

In high-frequency trading, milliseconds — and even microseconds — matter.

While these projects are toy-level, they model the building blocks of real systems:

- TCP/UDP → feed handlers, order gateways.
- HTTP/WebSockets/gRPC → APIs for control, orchestration, or telemetry.
- DNS → custom resolvers inside private trading networks.
- MQTT → lightweight pub/sub for monitoring or IoT-linked market data.

Having hands-on understanding of protocol behavior, latency trade-offs, and testing tools is essential in low-latency financial systems. This repo is my sandbox to practice those skills.
