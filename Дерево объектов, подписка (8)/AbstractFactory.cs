using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Дерево_объектов__подписка__8_;
abstract class AbstractFigureFactory {
    public abstract Figure createFigure(String figure, Point p1, Point p2);
    public abstract Storage<Figure> load(string path);

}

class ConcreteFigureFactory : AbstractFigureFactory {
    public override Figure createFigure(String figure, Point p1, Point p2) {
        switch (figure) {
            case "Line":
                return new Line(p1, p2);
            case "Circle":
                return new Circle(p1, p2);
            case "Rectangle":
                return new RRectangle(p1, p2);
            case "Triangle":
                return new Triangle(p1, p2);
            case "Group":
                return new Group(p1, p2);
            default:
                return null;
        }
    }

    public override Storage<Figure> load(string path) {
        Storage<Figure> tmp = new Storage<Figure>();
        Storage<Figure> storage = new Storage<Figure>();
        try {
            using (StreamReader sr = new StreamReader(path))
                while (!sr.EndOfStream) {
                    string line = sr.ReadLine();
                    string[] parts = line.Split(" ; ");


                    string figure = parts[0];

                    if (figure == "Group") {
                        tmp = recursivePass(sr);
                        Group g = new Group(new Point(0, 0), new Point(0, 0));
                        for (tmp.first(); !tmp.eol(); tmp.next()) {
                            g.groupStorage.push_front(tmp.getIterator());
                            tmp.remove();
                        }
                        g.selectionIntialize();
                        g.setSelected(false);
                            storage.push_front(g);
                    }
                    else {
                        parse(storage, parts);
                    }


                }
        }
        catch { }
        return storage;
    }

    Storage<Figure> recursivePass(StreamReader sr) {
        string line = sr.ReadLine();
        string[] parts = line.Split(" ; ");
        Storage<Figure> tmp = new Storage<Figure>();
        while (parts[0] != "}") {
            if (parts[0] == "Group") {
                Storage<Figure> rec = recursivePass(sr);
                Group g = new Group(new Point(0, 0), new Point(0, 0));

                for (rec.first(); !rec.eol(); rec.next()) {
                    g.groupStorage.push_front(rec.getIterator());
                }
                g.selectionIntialize();
                g.setSelected(false);
                tmp.push_front(g);
            }
            else {
                parse(tmp, parts);
            }
            line = sr.ReadLine();
            parts = line.Split(" ; ");
        }
        return tmp;
    }
    void parse (Storage<Figure> storage, string[] parts) {
        Point p1 = new Point(int.Parse(parts[1]), int.Parse(parts[2]));
        Point p2 = new Point(int.Parse(parts[3]), int.Parse(parts[4]));
        Color color = Color.FromArgb(int.Parse(parts[5]));

        storage.push_front(createFigure(parts[0], p1, p2));
        storage.getObject(0).setColor(color);
        storage.getObject(0).setSelected(false);
    }
}



