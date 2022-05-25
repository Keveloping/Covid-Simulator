using System.Collections.Generic;

namespace Covid {
    public class DrawMgr {


        static List<IDrawable> items;


        static DrawMgr () {
            items = new List<IDrawable> ();
        }

        public static void AddItem (IDrawable item) {
            items.Add (item);
        }

        public static void RemoveItem (IDrawable item) {
            items.Remove (item);
        }

        public static void Draw () {
            for (int i = 0; i < items.Count; i++) {
                if (items[i].IsActive) {
                    items[i].Draw ();
                }
            }
        }

        public static void ClearAll () {
            items.Clear ();
        }

    }
}
