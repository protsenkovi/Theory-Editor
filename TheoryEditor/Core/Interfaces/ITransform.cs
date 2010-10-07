using System.Collections.Generic;
using System.Windows;

namespace Protsenko.TheoryEditor.Core.Interfaces
{
    interface ITransform
    {
        void translateElement(Point point, double dx, double dy);
        void translateElements(List<Point> points, double dx, double dy);
        void zoom(double x, double y, double dz,List<Point> points);
        void rotate(double x, double y, double angle,List<Point> points);
    }
}
