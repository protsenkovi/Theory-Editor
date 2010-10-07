using System;
using System.Collections.Generic;
using System.Windows;

using Protsenko.TheoryEditor.Core.Events;

namespace Protsenko.TheoryEditor.Core
{
    public class EVisualInfo
    {
        private Point position;
        private double width;
        private double height;
        private double positionInGraphX;
        private double positionInGraphY;

        public Point Position { get { return position; } set { } }
        public double Width { get { return width; } set { width = value; Updated(this); } }
        public double Height { get { return height; } set { height = value; Updated(this); } }
        public double PositionInGraphX { get { return positionInGraphX; } set { positionInGraphX = value; Updated(this); } }
        public double PositionInGraphY { get { return positionInGraphY; } set { positionInGraphY = value; Updated(this); } }

        public event Updated Updated;

        public EVisualInfo()
        {
        }

        public EVisualInfo(double x,double y,double width,double height,double positionX,double positionY)
        {
            Point p = position;
            p.X = x; 
            p.Y = y;
            this.width = width;
            this.height = height;
            this.positionInGraphX = positionX;
            this.positionInGraphY = positionY;
        }

        public void SetInfo(double x, double y,double width, double height, double positionX, double positionY)
        {
            Point p = position;
            p.X = x;
            p.Y = y;
            this.width = width;
            this.height = height;
            this.positionInGraphX = positionX;
            this.positionInGraphY = positionY;
        }

        public void SetPosition(double x, double y)
        {
            position.X = x;
            position.Y = y;
            Updated(this);
        }
    }
}
