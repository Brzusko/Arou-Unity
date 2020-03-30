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

    }

    public class OnFaileArgs : EventArgs
    {
        public int Score { get; set; }
    }
}
