## MQTT (Message Queuing Telemetry Transport) — Client Example

## Summary  
MQTT is a **lightweight publish/subscribe messaging protocol** designed for constrained devices and unreliable networks. It runs over TCP (typically port **1883**, or **8883** when secured with TLS). Clients connect to a **broker**, which routes messages based on **topics**.  

The **MQTT client** in this repo demonstrates connecting to a broker, publishing a message (`23.4` to topic `sensors/temp`), and subscribing to the same topic to receive messages in real time.

---

## How MQTT Works  
- **Broker-based**: clients don’t talk directly; all messages go through the broker.  
- **Pub/Sub**: publishers send to a topic, subscribers receive from topics they subscribed to.  
- **QoS Levels**:  
  - 0: At most once (fire and forget).  
  - 1: At least once (acknowledged).  
  - 2: Exactly once (two-phase handshake).  
- **Lightweight header**: optimised for IoT and low-bandwidth environments.

## Run & Test  
```
# Install Docker if not already installed, then run Mosquitto broker
docker run -it --rm -p 1883:1883 eclipse-mosquitto

# Run subscriber in one terminal
cd MqttLab
dotnet run -f net7.0 -- sub
# -> prints "Subscribed, press Ctrl+C to exit."

# Run publisher in another terminal
cd MqttLab
dotnet run -f net7.0
# -> prints "published"

# Subscriber terminal should show:
# got: 23.4
```

---

## General Use Cases for MQTT  
- IoT devices sending sensor data.  
- Smart home automation (lights, thermostats, appliances).  
- Vehicle telematics and fleet management.  
- Mobile push-style communications.  
- Monitoring and alerting systems.  

---

## Pros & Cons of MQTT  

**Pros**  
- Extremely lightweight, works well on low-power devices.  
- Scales for many-to-many communication via topics.  
- QoS levels allow apps to choose reliability vs. speed.  
- Runs over TCP, making it firewall/NAT friendly.  

**Cons**  
- Requires a broker — no peer-to-peer out of the box.  
- Not as fast as raw UDP for ultra-low latency.  
- Topic-based model may be overkill for simple request/response.  
- Less common in traditional enterprise apps (more IoT-focused).  

---

## MQTT in HFT Context  
In high-frequency trading, MQTT is **not used on critical trading paths** because it adds broker overhead and isn’t ultra-low-latency. However, it’s useful for:  
- **Operational monitoring** → publish system metrics, logs, and alerts.  
- **Infrastructure telemetry** → lightweight way to collect server/colo health data.  
- **Prototyping/demoing** → quickly simulate streaming data flows without building a custom protocol.  

---
