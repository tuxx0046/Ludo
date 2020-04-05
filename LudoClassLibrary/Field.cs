using System;
using System.Collections.Generic;
using System.Text;


namespace LudoClassLibrary
{
    /// <summary>
    /// A field on the ludo board
    /// </summary>
    public class Field
    {
        #region Fields
        public string fieldId;
        /// <summary>
        /// Set to true in constructor if field is end goal
        /// </summary>
        public bool isGoal { get; } = false;
        /// <summary>
        /// Use to determine color and number of occupants. More than 0, means it is occupied
        /// </summary>
        public List<Piece> occuppants = new List<Piece>();
        #endregion

        #region Constructors
        /// <summary>
        /// Create a field for use in ludo board
        /// </summary>
        /// <param name="fieldId">Used to find the correct field. For ease, first normal field should start as "Field0".<br />
        /// There are 52 normal fields, and 6 color-specific fields for each color.<br />
        /// Can be named "Blue0", "Red0" etc.</param>
        public Field(string fieldId)
        {
            this.fieldId = fieldId;
        }

        /// <summary>
        /// Create a field for ludo board and set as goal.
        /// </summary>
        /// <param name="fieldId">Should reflect the color</param>
        /// <param name="isGoal">Last field on the route should be set as goal</param>
        public Field(string fieldId, bool isGoal)
        {
            this.fieldId = fieldId;
            this.isGoal = isGoal;
        }
        #endregion
             

        #region Methods
        /// <summary>
        /// Use to check if field is occupied, and to see if the occupant or incoming piece should be hit home etc.
        /// </summary>
        /// <returns>Number of occupants</returns>
        public int CountOccupants()
        {
            int numberOfOccupants = occuppants.Count;
            return numberOfOccupants;
        }

        /// <summary>
        /// Add an occupant to the field. Will only add if incoming LudoColor matches already existing occupants'.
        /// </summary>
        /// <param name="incoming">Incoming Piece</param>
        public void GetOccupant(Piece incoming)
        {
            if (occuppants.Count > 0 && occuppants[0].color == incoming.color)
            {
                occuppants.Add(incoming);
            }
            else if (occuppants.Count == 0)
            {
                occuppants.Add(incoming);
            }
        }

        /// <summary>
        /// Remove a piece from the field. Will only remove if the player's LudoColor matches the occupants'. <br />
        /// Returns false if LudoColor doesn't match.
        /// </summary>
        public bool RemoveOccupant(Player player)
        {
            if (occuppants.Count > 0 && occuppants[0].color == player.color)
            {
                occuppants.RemoveAt(0);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
