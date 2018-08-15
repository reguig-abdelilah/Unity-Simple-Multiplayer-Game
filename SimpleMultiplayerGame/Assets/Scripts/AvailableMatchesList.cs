using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking.Match;

 public static class AvailableMatchesList
    {
        private static List<MatchInfoSnapshot> matches = new List<MatchInfoSnapshot>();
        public static event Action<List<MatchInfoSnapshot>> OnAvailableMatchesChanged = delegate { };
        public static void HandleNewMatchList(List<MatchInfoSnapshot> matchList)
        {
            matches = matchList;
            OnAvailableMatchesChanged(matches);
        }
    }

