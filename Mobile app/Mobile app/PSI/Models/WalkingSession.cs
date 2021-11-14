﻿using Map3.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace PSI.Models
{
    public class WalkingSession
    {
        private static WalkingSession Current;
        private readonly List<VisualWaypoint> WaypointsLeft;
        private Location LastKnownLocation;
        

        private WalkingSession(List<VisualWaypoint> waypoints)
        {
            WaypointsLeft = waypoints;
        }

        public static VisualWaypoint CurrentGoal()
        {
            if (!HasGoalsLeft())
            {
                return null;
            }
            return Current.WaypointsLeft[0];
        }

        public static bool HasGoalsLeft()
        {
            return Current != null && Current.WaypointsLeft != null && Current.WaypointsLeft.Count > 0;
        }

        public static void ResetTo(List<VisualWaypoint> waypointsLeft)
        {
            Current = new WalkingSession(waypointsLeft);
        }

        public static void Finish()
        {
            Current = null;
        }

        public static bool IsGoalReached(Location location)
        {
            if (location == null)
            {
                return false;
            }

            VisualWaypoint currentGoal = CurrentGoal();
            if (currentGoal == null)
            {
                return true;
            }
            return (Math.Abs(location.Latitude - currentGoal.Lat) < 0.001) && (Math.Abs(location.Longitude - currentGoal.Long) < 0.001);
        }

        public static VisualWaypoint MoveToNextGoal()
        {
            if (!HasGoalsLeft())
            {
                return null;
            }

            Current.WaypointsLeft.RemoveAt(0);

            if (Current.WaypointsLeft.Count == 0)
            {
                return null;
            }
            return Current.WaypointsLeft[0];
        }

        public static bool IsTheLastGoal()
        {
            return Current != null && Current.WaypointsLeft != null && Current.WaypointsLeft.Count == 1;
        }

        public static bool CheckMoved(Location lastKnownLocation)
        {
            if (Current == null)
            {
                return false;
            }

            if (Current.LastKnownLocation == null)
            {
                Current.LastKnownLocation = lastKnownLocation;
                return true;
            }

            if (lastKnownLocation.Latitude == Current.LastKnownLocation.Latitude && lastKnownLocation.Longitude == Current.LastKnownLocation.Longitude)
            {
                return false;
            }

            Current.LastKnownLocation = lastKnownLocation;
            return true;
        }
    }
}