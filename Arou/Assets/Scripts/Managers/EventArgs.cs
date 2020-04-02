using System;
using UnityEngine;

namespace EventArguments
{
    public class OnShootArgs : EventArgs
    {
        public Vector3 Force { get; set; }
    }

    public class OnScoreArgs : EventArgs
    {
        public int Score { get; set; }
    }

    public class OnFaileArgs : EventArgs
    {
        public int Score { get; set; }
    }

    public class OnDespawnArgs: EventArgs
    {
        public Pipe Pipe { get; set; }
    }

    public class OnSpawnArgs: EventArgs
    {
        public Pipe Pipe { get; set; }
    }
}
