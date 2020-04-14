using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace LocaL{
    // General struct to access each player's gamepad input controls.
    // Expand by adding Actions to InputMaster, extending handling in
    // InputCTRL's InitInputCTRL() and adding adequate fields below.
    public struct PlayerInput{
        public Vector2 movement;
        public float range;
        public float melee;
        public float jump;
        public float block;
        public float add1;
        public float add2;
    };


    // Class describing player that uses given gamepad
    // Up to 8 players supported
    public class Player{
        #region Variables
        // Gamepad this player is using
        public Gamepad gamepad;
        // Unique color
        public Color color;
        // Unique name from the name base
        public string name;
        // Unique, abstract ID (0 to 7)
        public int id;
        // Sets whether this player should be active (ex. pressed start/didnt time out)
        public bool isActive = true;

        // Lock object to make sure there's no race condition
        // in case players connect at the same time
        private readonly object playerLock = new object();

        public static int numPlayers = 0;
        public static readonly int maxPlayers = 8;
        #endregion

        #region Generic methods
        // Constructor, assigns unique fields (ID, name, color) and gamepad
        public Player(Gamepad pad) {
            numPlayers++;
            gamepad = pad;
            lock(playerLock) {
                color = GetRandomColor();
                name = GetRandomName();
                id = GetRandomId();
            }
        }
        ~Player() {
            // Return IDs and names to the pool if player's controller gets disconnected
            if (!names.Contains(name)) {
                names.Add(name);
            }
            if (!colors.Contains(color)) {
                colors.Add(color);
            }
            if (!ids.Contains(id)) {
                ids.Add(id);
            }
            numPlayers--;
        }
        public void Print() {
            Debug.Log("Player " + name + ", id " + id + ", pad " + gamepad.displayName.ToString() + ", col " + color.ToString());
        }
        #endregion

        #region Obtaining unique fields
        private string GetRandomName() {
            int randomIndex = Random.Range(0, names.Count);
            string name = names[randomIndex];
            names.RemoveAt(randomIndex);
            return name;
        }
        private Color GetRandomColor() {
            int randomIndex = Random.Range(0, colors.Count);
            Color col = colors[randomIndex];
            colors.RemoveAt(randomIndex);
            return col;
        }
        private int GetRandomId() {
            int randomIndex = Random.Range(0, ids.Count);
            int id_ = ids[randomIndex];
            ids.RemoveAt(randomIndex);
            return id_;
        }
        #endregion

        #region IDs, names and color lists
        // Unique player ids, modifying not recommended.
        private static List<int> ids = new List<int> {
            0,1,2,3,4,5,6,7
        };

        // List of possible unique player colors, can be modified provided
        // that there are at least as many entries as the supported
        // max number of players.
        private static List<Color> colors = new List<Color> {
            new Color(1.0f, 0.0f, 0.0f),
            new Color(1.0f, 1.0f, 0.0f),
            new Color(1.0f, 1.0f, 1.0f),
            new Color(0.0f, 1.0f, 0.0f),
            new Color(0.0f, 1.0f, 1.0f),
            new Color(0.0f, 0.0f, 1.0f),
            new Color(1.0f, 0.0f, 1.0f),
            new Color(0.0f, 0.0f, 0.0f)
        };

        // List of possible name entries, can be modified provided
        // that there are at least as many entries as the supported
        // max number of players.
        private static List<string> names = new List<string>{
            "Alpha",
            "Bravo",
            "Charlie",
            "Delta",
            "Echo",
            "Foxtrot",
            "Golf",
            "Hotel",
            "India",
            "Juliett",
            "Kilo",
            "Lima",
            "Mike",
            "November",
            "Oscar",
            "Papa",
            "Quebec",
            "Romeo",
            "Sierra",
            "Tango",
            "Uniform",
            "Victor",
            "Whiskey",
            "Xray",
            "Yankee",
            "Zulu"
        };
        #endregion
    }
}