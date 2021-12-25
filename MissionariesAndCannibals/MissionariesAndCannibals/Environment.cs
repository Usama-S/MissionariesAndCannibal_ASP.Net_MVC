using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MissionariesAndCannibals
{
    public class Environment
    {
        // Utility method to create successor nodes of a node
        public ArrayList Successors(State currentState)
        {
            var children = new ArrayList();


            if (currentState.boat.Equals("left"))
            {
                var newState = new State(currentState.cannibalLeft, currentState.missionaryLeft - 2, "right",
                                          currentState.cannibalRight, currentState.missionaryRight + 2);
                // Two missionaries cross left to right.
                if (newState.IsValid())
                {
                    newState.parent = currentState;
                    children.Add(newState);
                }
                newState = new State(currentState.cannibalLeft - 2, currentState.missionaryLeft, "right",
                                          currentState.cannibalRight + 2, currentState.missionaryRight);
                // Two cannibals cross left to right.
                if (newState.IsValid())
                {
                    newState.parent = currentState;
                    children.Add(newState);
                }
                newState = new State(currentState.cannibalLeft - 1, currentState.missionaryLeft - 1, "right",
                                          currentState.cannibalRight + 1, currentState.missionaryRight + 1);

                // One missionary and one cannibal cross left to right.
                if (newState.IsValid())
                {
                    newState.parent = currentState;
                    children.Add(newState);
                }
                newState = new State(currentState.cannibalLeft, currentState.missionaryLeft - 1, "right",
                                          currentState.cannibalRight, currentState.missionaryRight + 1);

                // One missionary crosses left to right.
                if (newState.IsValid())
                {
                    newState.parent = currentState;
                    children.Add(newState);
                }
                newState = new State(currentState.cannibalLeft - 1, currentState.missionaryLeft, "right",
                                          currentState.cannibalRight + 1, currentState.missionaryRight);
                // One cannibal crosses left to right.
                if (newState.IsValid())
                {
                    newState.parent = currentState;
                    children.Add(newState);
                }
            }
            else
            {
                var newState = new State(currentState.cannibalLeft, currentState.missionaryLeft + 2, "left",
                                          currentState.cannibalRight, currentState.missionaryRight - 2);
                // Two missionaries cross right to left.
                if (newState.IsValid())
                {
                    newState.parent = currentState;
                    children.Add(newState);
                }
                newState = new State(currentState.cannibalLeft + 2, currentState.missionaryLeft, "left",
                                          currentState.cannibalRight - 2, currentState.missionaryRight);
                // Two cannibals cross right to left.
                if (newState.IsValid())
                {
                    newState.parent = currentState;
                    children.Add(newState);
                }
                newState = new State(currentState.cannibalLeft + 1, currentState.missionaryLeft + 1, "left",
                                          currentState.cannibalRight - 1, currentState.missionaryRight - 1);
                // One missionary and one cannibal cross right to left.
                if (newState.IsValid())
                {
                    newState.parent = currentState;
                    children.Add(newState);
                }
                newState = new State(currentState.cannibalLeft, currentState.missionaryLeft + 1, "left",
                                          currentState.cannibalRight, currentState.missionaryRight - 1);
                // One missionary crosses right to left.
                if (newState.IsValid())
                {
                    newState.parent = currentState;
                    children.Add(newState);
                }
                newState = new State(currentState.cannibalLeft + 1, currentState.missionaryLeft, "left",
                                          currentState.cannibalRight - 1, currentState.missionaryRight);
                // One cannibal crosses right to left.
                if (newState.IsValid())
                {
                    newState.parent = currentState;
                    children.Add(newState);
                }
            }

            return children;

        }


        // breadth first algorithm
        public State BFS()
        {
            var initialState = new State(3, 3, "left", 0, 0);


            if (initialState.IsGoal())
                return initialState;

            List<State> frontier = new List<State>();

            var explored = new HashSet<State>();

            frontier.Add(initialState);

            while (frontier.Count > 0)
            {
                State state = frontier.ElementAt(0);
                frontier.RemoveAt(0);

                if (state.IsGoal())
                    return state;

                explored.Add(state);

                var children = Successors(state);

                foreach (var item in children)
                {
                    var child = (State)item;
                    if (!explored.Contains(child) || !frontier.Contains(child))
                        frontier.Add(child);
                }
            }

            return null;
        }


        // utility method to print solution
        public List<State> GetSolution(State solution)
        {
            var path = new List<State>();
            path.Add(solution);

            if (solution.parent != null)
            {
                var parent = solution.parent;

                while (parent != null)
                {
                    path.Add(parent);
                    parent = parent.parent;
                }
            }


            List<State> result = new List<State>();

            // print the list in reverse to print the root node or the start node first
            for (int i = path.Count - 1; i >= 0; i--)
            {
                var state = path[i];
                state.parent = null;

                result.Add(state);
            }

            return result;
        }
    }
}