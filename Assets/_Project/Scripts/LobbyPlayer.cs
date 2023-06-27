using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public struct LobbyPlayer : INetworkSerializable, IEquatable<LobbyPlayer>
{
    public ulong PlayerID;
    public int SeatIndex;

    public bool Equals(LobbyPlayer other)
    {
        return true;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {

        serializer.SerializeValue(ref SeatIndex);
        serializer.SerializeValue(ref PlayerID);
    }
}
