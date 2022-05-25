using System.Collections.Generic;

namespace Covid {
    public static class PhysicsMgr {

        static List<Rigidbody> items;

        static PhysicsMgr () {
            items = new List<Rigidbody> ();
        }

        public static void AddItem (Rigidbody item) {
            items.Add (item);
        }

        public static void RemoveItem (Rigidbody item) {
            items.Remove (item);
        }

        public static void FixedUpdate () {
            for (int i = 0; i <items.Count; i++) {
                if (items[i].IsActive) {
                    items[i].FixedUpdate ();
                }
            }
        }

        public static void CheckCollisions () {
            for (int i = 0; i < items.Count - 1; i++) {
                if (items[i].IsActive && items[i].IsCollisionsAffected) {
                    //posso controllare le collisioni
                    for (int j = i + 1; j < items.Count; j++) {
                        if (items[j].IsActive && items[j].IsCollisionsAffected) {
                            bool firstCheck = items[i].CollisionTypeMatches (items[j].Type);
                            bool secondCheck = items[j].CollisionTypeMatches (items[i].Type);
                            if (firstCheck || secondCheck) {
                                if (items[i].Collides (items[j])) {
                                    if (firstCheck) {
                                        items[i].GameObject.OnCollide (items[j].GameObject);
                                    }
                                    if (secondCheck) {
                                        items[j].GameObject.OnCollide (items[i].GameObject);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        public static void ClearAll () {
            items.Clear ();
        }

    }
}
