using System.Net;
using System.Net.Sockets;
using System.Buffers.Binary;

using var udp = new UdpClient(new IPEndPoint(IPAddress.Any, 5533));
Console.WriteLine("DNS toy :5533 -> A test.local = 1.2.3.4");

while (true)
{
    var result = await udp.ReceiveAsync();
    var buf = result.Buffer;
    var remote = result.RemoteEndPoint;

    ushort id = BinaryPrimitives.ReadUInt16BigEndian(buf.AsSpan(0,2));

    // parse qname (very naive)
    int i = 12; var labels = new List<string>();
    while (buf[i] != 0) { var len = buf[i++]; labels.Add(System.Text.Encoding.ASCII.GetString(buf, i, len)); i += len; }
    var qname = string.Join(".", labels) + ".";

    // header
    var resp = new byte[buf.Length + 16]; // enough for one A record
    buf.AsSpan(0, i + 5).CopyTo(resp); // copy header+question
    // set flags QR=1, RD copied, RA=1, RCODE=0
    BinaryPrimitives.WriteUInt16BigEndian(resp.AsSpan(2, 2), 0x8180);
    // QDCOUNT=1, ANCOUNT=1
    BinaryPrimitives.WriteUInt16BigEndian(resp.AsSpan(4, 2), 1);
    BinaryPrimitives.WriteUInt16BigEndian(resp.AsSpan(6, 2), 1);

    int pos = i + 5; // end of question (qname 0x00 + qtype(2) + qclass(2))

    // answer: name (pointer to qname), type A (1), class IN (1), TTL 60, RDLENGTH 4, RDATA 1.2.3.4
    resp[pos++] = 0xC0; resp[pos++] = 0x0C;                // pointer to qname
    BinaryPrimitives.WriteUInt16BigEndian(resp.AsSpan(pos, 2), 1); pos += 2; // A
    BinaryPrimitives.WriteUInt16BigEndian(resp.AsSpan(pos, 2), 1); pos += 2; // IN
    BinaryPrimitives.WriteUInt32BigEndian(resp.AsSpan(pos, 4), 60); pos += 4; // TTL
    BinaryPrimitives.WriteUInt16BigEndian(resp.AsSpan(pos, 2), 4); pos += 2; // RDLEN
    resp[pos++] = 1; resp[pos++] = 2; resp[pos++] = 3; resp[pos++] = 4;

    await udp.SendAsync(resp.AsMemory(0, pos), remote);
}