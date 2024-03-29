﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    private readonly MapData[][] _map;

    public PathFinder(MapData[][] map)
    {
        _map = map;
    }

    private class PathNode
    {
        public Point Position;
        public int LengthFromStart;
        public PathNode Parent;
        public int Heuristic;
        public int FullLength => LengthFromStart + Heuristic;
    }

    public List<Point> FindPath(Point start, Point goal, bool allowDiagonal)
    {
        var closedSet = new Collection<PathNode>();
        var openSet = new Collection<PathNode>();

        PathNode startNode = new PathNode()
        {
            Position = start,
            Parent = null,
            LengthFromStart = 0,
            Heuristic = GetHeuristic(start, goal)
        };
        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            
            var currentNode = openSet.OrderBy(node => node.FullLength).First();
            if (currentNode.Position.Equals(goal))
            {
                return GetPathForNode(currentNode);
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            foreach (var neighbourNode in GetNeighbours(currentNode, goal, allowDiagonal))
            {
                if (closedSet.Count(node => node.Position.Equals(neighbourNode.Position)) > 0)
                {
                    continue;
                }
                var openNode = openSet.FirstOrDefault(node => node.Position.Equals(neighbourNode.Position));
                if (openNode == null)
                {
                    openSet.Add(neighbourNode);
                }
                else if (openNode.LengthFromStart > neighbourNode.LengthFromStart)
                {
                    openNode.Parent = currentNode;
                    openNode.LengthFromStart = neighbourNode.LengthFromStart;
                }
            }
        }
        return null;
    }

    private int GetHeuristic(Point from, Point to)
    {
        return Mathf.Abs(from.X - to.X) + Mathf.Abs(from.Y - to.Y);
    }

    private int GetDistanceBetweenNeighbours()
    {
        return 1;
    }

    private List<Point> GetPathForNode(PathNode pathNode)
    {
        var result = new List<Point>();
        var currentNode = pathNode;
        while (currentNode != null)
        {
            result.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }
        result.Reverse();
        result.RemoveAt(0);
        return result;
    }

    private Collection<PathNode> GetNeighbours(PathNode pathNode, Point goal, bool allowDiagonal)
    {
        var result = new Collection<PathNode>();

        List<Point> neighbours = new List<Point>();
        neighbours.Add(new Point(pathNode.Position.X + 1, pathNode.Position.Y));
        neighbours.Add(new Point(pathNode.Position.X - 1, pathNode.Position.Y));
        neighbours.Add(new Point(pathNode.Position.X, pathNode.Position.Y + 1));
        neighbours.Add(new Point(pathNode.Position.X, pathNode.Position.Y - 1));

        if (allowDiagonal)
        {
            neighbours.Add(new Point(pathNode.Position.X + 1, pathNode.Position.Y + 1));
            neighbours.Add(new Point(pathNode.Position.X + 1, pathNode.Position.Y - 1));
            neighbours.Add(new Point(pathNode.Position.X - 1, pathNode.Position.Y + 1));
            neighbours.Add(new Point(pathNode.Position.X - 1, pathNode.Position.Y - 1));
        }

        foreach (var point in neighbours)
        {
            if (point.X < 0 || point.X >= _map.Length)
            {
                continue;
            }
            if (point.Y < 0 || point.Y >= _map[0].Length)
            {
                continue;
            }
            if (_map[point.X][point.Y].Type != OnMapType.Empty)
            {
                continue;
            }
            var neighbourNode = new PathNode()
            {
                Position = point,
                Parent = pathNode,
                LengthFromStart = pathNode.LengthFromStart + GetDistanceBetweenNeighbours(),
                Heuristic = GetHeuristic(point, goal)
            };
            result.Add(neighbourNode);
        }
        return result;
    }
}
