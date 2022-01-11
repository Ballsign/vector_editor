using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Дерево_объектов__подписка__8_ {
    internal class Tree {
        TreeView tree;
        Storage<Figure> figureStorage;
        public Tree(Storage<Figure> storage, TreeView treeview) {
            figureStorage = storage;
            tree = treeview;
        }
        public void updateTreeview(object sender, EventArgs e) {
            tree.BeginUpdate();
            tree.Nodes.Clear();
            tree.Nodes.Add("Storage");

            for (figureStorage.first(); !figureStorage.eol(); figureStorage.next()) {
                proccesNode(tree.Nodes[0], figureStorage.getIterator(), figureStorage.getIterator());
                Debug.WriteLine(figureStorage.getIterator().ToString());
            }

            tree.ExpandAll();
            tree.EndUpdate();
        }

        void proccesNode(TreeNode tn, Figure o, Figure tag_) {
            TreeNode t = tn.Nodes.Add(o.name);
            t.Tag = tag_;
            if (o.getSelected() && tag_ == o) {
                t.BackColor = SystemColors.Highlight;
                t.ForeColor = Color.White;
            }

            if (o.name == "Group") {
                Group group = o as Group;
                for (group.groupStorage.first(); !group.groupStorage.eol(); group.groupStorage.next()) {
                    proccesNode(t, group.groupStorage.getIterator(), tag_);
                }
            }

        }

/*        public void updateSelectTreeview() {
            int i = 0;
            for (figureStorage.first(); !figureStorage.eol(); figureStorage.next()) {
                if (figureStorage.getIterator().getSelected()) {
                    tree.Nodes[i].BackColor = SystemColors.Highlight;
                    tree.Nodes[i].ForeColor = Color.White;
                }
                else {
                    tree.Nodes[i].BackColor = Color.White;
                    tree.Nodes[i].ForeColor = Color.Black;
                }
                i++;
            }

        }*/
        public void tree_select(TreeNode tn) {

            for (figureStorage.first(); !figureStorage.eol(); figureStorage.next()) {
                if (figureStorage.getIterator() == tn.Tag)
                    figureStorage.getIterator().setSelected(true);
                else
                    figureStorage.getIterator().setSelected(false);
            }
        }
    }
}

