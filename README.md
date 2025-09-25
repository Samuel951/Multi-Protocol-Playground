# Multi-Protocol-Playground

collection of .NET 7 samples that demonstrate different network protocols end-to-end. Each project spins up a minimal server you can interact with using standard tools (nc, curl, grpcurl, wscat, etc.). Perfect for learning, experimenting, or showing off protocol knowledge.





Protocols \& How to Test



TCP

Run the TCP echo server, then:



nc 127.0.0.1 5000





Type a message → server replies with echo: <message>.



UDP

Run the UDP echo server, then:



echo -n hi | nc -u -w1 127.0.0.1 5001





HTTP (REST API)



curl -i http://127.0.0.1:5000/health





HTTPS + HTTP/2

Point Kestrel to a local dev cert (localhost.pfx), then:



curl -I --http2 https://localhost:8443/health -k





WebSocket

Run the WebSocket chat server, then connect two clients:



npx wscat -c ws://127.0.0.1:5000/ws





gRPC

Make a request against the Greeter service:



grpcurl -insecure -d '{"name":"Ada"}' https://localhost:5001 greet.Greeter/SayHello





MQTT

Run a Mosquitto broker (e.g. via Docker):



docker run -p 1883:1883 eclipse-mosquitto:2





Then:



dotnet run --project MqttLab -- sub   # shell 1

dotnet run --project MqttLab          # shell 2 (publishes a message)





DNS

Run the toy DNS server, then:



dig @127.0.0.1 -p 5353 test.local A





QUIC (HTTP/3)

Run the QUIC sample server, then in another terminal:



dotnet run --project QuicPing -- client



🛠️ Requirements



.NET 7 SDK



nc / ncat (for TCP/UDP)



curl with HTTP/2/3 support



wscat (npm install -g wscat)



grpcurl



dig (part of dnsutils / bind-utils)



Docker (for MQTT broker)

