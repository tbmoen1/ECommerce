namespace ECommerce.IdGeneration;

public sealed class SnowflakeIdGenerator : IIdGenerator
{
    // 41 bits timestamp | 10 bits node | 12 bits sequence  (63 bits, positive long)
    private const int NodeIdBits = 10;
    private const int SequenceBits = 12;
    private const long MaxNodeId = (1L << NodeIdBits) - 1;      // 1023
    private const long SequenceMask = (1L << SequenceBits) - 1; // 4095
    private const int NodeIdShift = SequenceBits;
    private const int TimestampShift = SequenceBits + NodeIdBits;

    // THIS CANNOT CHANGE IN A EXISTING PROJECT
    private static readonly long Epoch =
        new DateTimeOffset(2026, 11, 1, 18, 20, 34, TimeSpan.Zero).ToUnixTimeMilliseconds();

    private readonly long _nodeId;
    private readonly Lock _lock = new();
    private long _lastTimestamp = -1;
    private long _sequence;

    public SnowflakeIdGenerator(long nodeId)
    {
        if (nodeId is < 0 or > MaxNodeId)
            throw new ArgumentOutOfRangeException(nameof(nodeId), nodeId,
                $"Node id must be between 0 and {MaxNodeId}.");

        _nodeId = nodeId;
    }

    public long NextId()
    {
        lock (_lock)
        {
            var timestamp = NowMs();

            if (timestamp < _lastTimestamp)
                throw new InvalidOperationException(
                    $"Clock moved backwards by {_lastTimestamp - timestamp} ms; refusing to generate IDs.");

            if (timestamp == _lastTimestamp)
            {
                _sequence = (_sequence + 1) & SequenceMask;
                if (_sequence == 0)
                    timestamp = WaitNextMs(_lastTimestamp);
            }
            else
            {
                _sequence = 0;
            }

            _lastTimestamp = timestamp;

            return ((timestamp - Epoch) << TimestampShift)
                 | (_nodeId << NodeIdShift)
                 | _sequence;
        }
    }

    private static long NowMs() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    private static long WaitNextMs(long lastTimestamp)
    {
        long timestamp;
        do { timestamp = NowMs(); } while (timestamp <= lastTimestamp);
        return timestamp;
    }
}
