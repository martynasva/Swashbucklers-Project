﻿using System;
using System.Collections.Generic;

namespace Encounter
{
    public class WaypointController     {
        private List<WaypointViewModel> _waypoints;
        private double x1;
        private double x2;
        private double y1;
        private double y2;

        public WaypointController()
        {
            _waypoints = new List<WaypointViewModel>();
        }

        /*
        public WaypointViewModel CreateNewWaypoint()
        {
            var waypoint = new WaypointViewModel();
            _waypoints.Add(waypoint);

            waypoint.Index = _waypoints.Count;
            waypoint.Name = "Name";
            waypoint.Coord = new Coordinates(0.00, 0.00);

            return waypoint;  
        }
        */

        public double Distance()
        {
            double DistanceValue = 0;

            
            

            for (int i = 0; i < _waypoints.Count - 1; i++)
            {

                x1 = _waypoints[i].Coordinates.longitude;
                y1 = _waypoints[i].Coordinates.latitude;
                x2 = _waypoints[i + 1].Coordinates.longitude;
                y2 = _waypoints[i + 1].Coordinates.latitude;
               
                DistanceValue += Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));             
                 
            }
            //casting to integer km narroving type conversion
            return (int)DistanceValue;
        }

        public void RemoveWaypoint(int index)
        {
            if (index < _waypoints.Count)
            {
                for (int i = index; i < _waypoints.Count; i++)
                {
                    _waypoints[i].Index -= 1;
                }
                _waypoints.RemoveAt(index);
            }
        }

        public int GetWaypointsCount()
        {
           return _waypoints.Count;
        }

        public WaypointViewModel GetWaypoint(int index)
        {
            return index <= _waypoints.Count ? _waypoints[index] : null;
        }

        public void ChangeWaypointIndex(int index, int newIndex)
        {
            if (newIndex < index)
            {
                for (int i = newIndex; i <= index; i++)
                {
                    _waypoints[i].Index += 1;
                }
            }
            else
            {
                for (int i = index+1; i <= newIndex; i++)
                {
                    _waypoints[i].Index -= 1;
                }
            }
            var tempW = _waypoints[index];
            tempW.Index = newIndex+1;
            _waypoints.RemoveAt(index);
            _waypoints.Insert(newIndex, tempW);
        }
        //Widening type conversion from decimal to double
        public double ConvertCurrency(decimal price, string currencyFrom, string currencyTo)
        {
            double rate;
            if (currencyFrom == "USD" && currencyTo == "EUR")
            {
                rate = 1 / 1.569;
            }
            else if (currencyFrom == "EUR" && currencyTo == "USD")
            {
                rate = 1.569;
            }
            else if (currencyFrom == "GBP" && currencyTo == "EUR")
            {
                rate = 1/ 0.84890;
            }
            else if (currencyFrom == "EUR" && currencyTo == "GBP")
            {
                rate = 0.84890;
            }
            else
            {
                throw new ArgumentException("Can't convert from " + currencyFrom + " to " + currencyTo);
            }
            return ((double)price) * rate;
        }

        
    }
}