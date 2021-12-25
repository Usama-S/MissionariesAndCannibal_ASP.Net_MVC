using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MissionariesAndCannibals
{
    public class State
    {
        public int cannibalLeft = 0;
        public int missionaryLeft = 0;
        public string boat = "left";
        public int cannibalRight = 0;
        public int missionaryRight = 0;
        public State parent = null;

        // constructor
        public State(int cannibalLeft, int missionaryLeft,
            string boat,
            int cannibalRight, int missionaryRight)
        {
            this.cannibalLeft = cannibalLeft;
            this.missionaryLeft = missionaryLeft;
            this.boat = boat;
            this.cannibalRight = cannibalRight;
            this.missionaryRight = missionaryRight;
            this.parent = null;
        }



        // goal test
        public bool IsGoal()
        {
            if (cannibalLeft == 0 && missionaryLeft == 0)
                return true;
            else
                return false;
        }

        // utility to check if a move is valid or not
        public bool IsValid()
        {
            if (missionaryLeft >= 0 && missionaryRight >= 0
                       && cannibalLeft >= 0 && cannibalRight >= 0
                       && (missionaryLeft == 0 || missionaryLeft >= cannibalLeft)
                       && (missionaryRight == 0 || missionaryRight >= cannibalRight))
                return true;

            else
                return false;
        }

        public bool IsEqual(State otherState)
        {
            return (cannibalLeft == otherState.cannibalLeft && missionaryLeft == otherState.missionaryLeft
                       && boat == otherState.boat && cannibalRight == otherState.cannibalRight
                       && missionaryRight == otherState.missionaryRight);
        }
    }
}