﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LudoClassLibrary
{
    /// <summary>
    /// Ludo board
    /// </summary>
    public class Board
    {
        #region Fields
        // Fields where all players can move and interact
        public Field[] normalFields = new Field[52];

        // The routes assigned to the Pieces to follow - 51 normal fields + 6 color-specific
        public Field[] blueRoute = new Field[57];
        public Field[] greenRoute = new Field[57];
        public Field[] redRoute = new Field[57];
        public Field[] yellowRoute = new Field[57];

        // The starting point for all Pieces and where they'll end up if thrown back
        public Field[] blueBase;
        public Field[] greenBase;
        public Field[] redBase;
        public Field[] yellowBase;

        public Dictionary<LudoColor, Field[]> routes;
        #endregion

        #region Methods
        /// <summary>
        /// Initializes fields and routes
        /// </summary>
        public void InitWholeBoardAndCreateRoutes()
        {
            #region Base fields
            blueBase = new Field[]
            {
                new Field("BlueBase0"),
                new Field("BlueBase1"),
                new Field("BlueBase2"),
                new Field("BlueBase3")
            };

            greenBase = new Field[]
            {
                new Field("GreenBase0"),
                new Field("GreenBase1"),
                new Field("GreenBase2"),
                new Field("GreenBase3")
            };

            redBase = new Field[]
            {
                new Field("RedBase0"),
                new Field("RedBase1"),
                new Field("RedBase2"),
                new Field("RedBase3")
            };

            yellowBase = new Field[]
            {
                new Field("YellowBase0"),
                new Field("YellowBase1"),
                new Field("YellowBase2"),
                new Field("YellowBase3")
            };
            #endregion

            #region Normal fields
            normalFields = new Field[]
            {
                new Field("Field0"),
                new Field("Field1"),
                new Field("Field2"),
                new Field("Field3"),
                new Field("Field4"),
                new Field("Field5"),
                new Field("Field6"),
                new Field("Field7"),
                new Field("Field8"),
                new Field("Field9"),
                new Field("Field10"),
                new Field("Field11"),
                new Field("Field12"),
                new Field("Field13"),
                new Field("Field14"),
                new Field("Field15"),
                new Field("Field16"),
                new Field("Field17"),
                new Field("Field18"),
                new Field("Field19"),
                new Field("Field20"),
                new Field("Field21"),
                new Field("Field22"),
                new Field("Field23"),
                new Field("Field24"),
                new Field("Field25"),
                new Field("Field26"),
                new Field("Field27"),
                new Field("Field28"),
                new Field("Field29"),
                new Field("Field30"),
                new Field("Field31"),
                new Field("Field32"),
                new Field("Field33"),
                new Field("Field34"),
                new Field("Field35"),
                new Field("Field36"),
                new Field("Field37"),
                new Field("Field38"),
                new Field("Field39"),
                new Field("Field40"),
                new Field("Field41"),
                new Field("Field42"),
                new Field("Field43"),
                new Field("Field44"),
                new Field("Field45"),
                new Field("Field46"),
                new Field("Field47"),
                new Field("Field48"),
                new Field("Field49"),
                new Field("Field50"),
                new Field("Field51")
            };
            #endregion

            #region Color-specific fields
            Field blueField0 = new Field("BlueField0");
            Field blueField1 = new Field("BlueField1");
            Field blueField2 = new Field("BlueField2");
            Field blueField3 = new Field("BlueField3");
            Field blueField4 = new Field("BlueField4");
            Field blueField5 = new Field("BlueField5", true);

            Field greenField0 = new Field("GreenField0");
            Field greenField1 = new Field("GreenField1");
            Field greenField2 = new Field("GreenField2");
            Field greenField3 = new Field("GreenField3");
            Field greenField4 = new Field("GreenField4");
            Field greenField5 = new Field("GreenField5", true);

            Field redField0 = new Field("RedField0");
            Field redField1 = new Field("RedField1");
            Field redField2 = new Field("RedField2");
            Field redField3 = new Field("RedField3");
            Field redField4 = new Field("RedField4");
            Field redField5 = new Field("RedField5", true);

            Field yellowField0 = new Field("YellowField0");
            Field yellowField1 = new Field("YellowField1");
            Field yellowField2 = new Field("YellowField2");
            Field yellowField3 = new Field("YellowField3");
            Field yellowField4 = new Field("YellowField4");
            Field yellowField5 = new Field("YellowField5", true);
            #endregion

            #region Routes
            #region Blue Route
            // Set blueRoute - starting field from normalFields[1]
            for (int i = 0; i < normalFields.Length - 1; i++)
            {
                // Set normal fields appearing in blueRoute
                blueRoute[i] = normalFields[i + 1];
            }

            // Color-specific fields in blueRoute
            blueRoute[51] = blueField0;
            blueRoute[52] = blueField1;
            blueRoute[53] = blueField2;
            blueRoute[54] = blueField3;
            blueRoute[55] = blueField4;
            blueRoute[56] = blueField5;
            #endregion

            #region Green Route
            // Set greenRoute - starts at normalFields[14]
            greenRoute[0] = normalFields[14];
            greenRoute[1] = normalFields[15];
            greenRoute[2] = normalFields[16];
            greenRoute[3] = normalFields[17];
            greenRoute[4] = normalFields[18];
            greenRoute[5] = normalFields[19];
            greenRoute[6] = normalFields[20];
            greenRoute[7] = normalFields[21];
            greenRoute[8] = normalFields[22];
            greenRoute[9] = normalFields[23];
            greenRoute[10] = normalFields[24];
            greenRoute[11] = normalFields[25];
            greenRoute[12] = normalFields[26];
            greenRoute[13] = normalFields[27];
            greenRoute[14] = normalFields[28];
            greenRoute[15] = normalFields[29];
            greenRoute[16] = normalFields[30]; 
            greenRoute[17] = normalFields[31]; 
            greenRoute[18] = normalFields[32]; 
            greenRoute[19] = normalFields[33]; 
            greenRoute[20] = normalFields[34]; 
            greenRoute[21] = normalFields[35]; 
            greenRoute[22] = normalFields[36]; 
            greenRoute[23] = normalFields[37]; 
            greenRoute[24] = normalFields[38];
            greenRoute[25] = normalFields[39]; 
            greenRoute[26] = normalFields[40]; 
            greenRoute[27] = normalFields[41]; 
            greenRoute[28] = normalFields[42]; 
            greenRoute[29] = normalFields[43]; 
            greenRoute[30] = normalFields[44]; 
            greenRoute[31] = normalFields[45]; 
            greenRoute[32] = normalFields[46]; 
            greenRoute[33] = normalFields[47]; 
            greenRoute[34] = normalFields[48]; 
            greenRoute[35] = normalFields[49]; 
            greenRoute[36] = normalFields[50];
            greenRoute[37] = normalFields[51]; 
            greenRoute[38] = normalFields[0]; 
            greenRoute[39] = normalFields[1]; 
            greenRoute[40] = normalFields[2]; 
            greenRoute[41] = normalFields[3]; 
            greenRoute[42] = normalFields[4]; 
            greenRoute[43] = normalFields[5]; 
            greenRoute[44] = normalFields[6];
            greenRoute[45] = normalFields[7]; 
            greenRoute[46] = normalFields[8]; 
            greenRoute[47] = normalFields[9]; 
            greenRoute[48] = normalFields[10]; 
            greenRoute[49] = normalFields[11]; 
            greenRoute[50] = normalFields[12];
            greenRoute[51] = greenField0;
            greenRoute[52] = greenField1;
            greenRoute[53] = greenField2;
            greenRoute[54] = greenField3;
            greenRoute[55] = greenField4;
            greenRoute[56] = greenField5;
            #endregion

            #region Red Route
            redRoute[0] = normalFields[40];
            redRoute[1] = normalFields[41]; 
            redRoute[2] = normalFields[42]; 
            redRoute[3] = normalFields[43];
            redRoute[4] = normalFields[44];
            redRoute[5] = normalFields[45];
            redRoute[6] = normalFields[46];
            redRoute[7] = normalFields[47];
            redRoute[8] = normalFields[48]; 
            redRoute[9] = normalFields[49]; 
            redRoute[10] = normalFields[50]; 
            redRoute[11] = normalFields[51];
            redRoute[12] = normalFields[0]; 
            redRoute[13] = normalFields[1]; 
            redRoute[14] = normalFields[2]; 
            redRoute[15] = normalFields[3]; 
            redRoute[16] = normalFields[4]; 
            redRoute[17] = normalFields[5]; 
            redRoute[18] = normalFields[6]; 
            redRoute[19] = normalFields[7];
            redRoute[20] = normalFields[8];
            redRoute[21] = normalFields[9];
            redRoute[22] = normalFields[10];
            redRoute[23] = normalFields[11];
            redRoute[24] = normalFields[12]; 
            redRoute[25] = normalFields[13]; 
            redRoute[26] = normalFields[14];
            redRoute[27] = normalFields[15];
            redRoute[28] = normalFields[16];
            redRoute[29] = normalFields[17];
            redRoute[30] = normalFields[18];
            redRoute[31] = normalFields[19];
            redRoute[32] = normalFields[20];
            redRoute[33] = normalFields[21];
            redRoute[34] = normalFields[22];
            redRoute[35] = normalFields[23];
            redRoute[36] = normalFields[24]; 
            redRoute[37] = normalFields[25];
            redRoute[38] = normalFields[26]; 
            redRoute[39] = normalFields[27];
            redRoute[40] = normalFields[28];
            redRoute[41] = normalFields[29];
            redRoute[42] = normalFields[30]; 
            redRoute[43] = normalFields[31]; 
            redRoute[44] = normalFields[32];
            redRoute[45] = normalFields[33]; 
            redRoute[46] = normalFields[34]; 
            redRoute[47] = normalFields[35];
            redRoute[48] = normalFields[36];
            redRoute[49] = normalFields[37];
            redRoute[50] = normalFields[38];
            redRoute[51] = redField0;
            redRoute[52] = redField1;
            redRoute[53] = redField2;
            redRoute[54] = redField3;
            redRoute[55] = redField4;
            redRoute[56] = redField5;
            #endregion

            #region Yellow Route
            yellowRoute[0] = normalFields[27];
            yellowRoute[1] = normalFields[28];
            yellowRoute[2] = normalFields[29];
            yellowRoute[3] = normalFields[30];
            yellowRoute[4] = normalFields[31];
            yellowRoute[5] = normalFields[32];
            yellowRoute[6] = normalFields[33];
            yellowRoute[7] = normalFields[34];
            yellowRoute[8] = normalFields[35];
            yellowRoute[9] = normalFields[36];
            yellowRoute[10] = normalFields[37];
            yellowRoute[11] = normalFields[38];
            yellowRoute[12] = normalFields[39];
            yellowRoute[13] = normalFields[40];
            yellowRoute[14] = normalFields[41];
            yellowRoute[15] = normalFields[42];
            yellowRoute[16] = normalFields[43];
            yellowRoute[17] = normalFields[44];
            yellowRoute[18] = normalFields[45];
            yellowRoute[19] = normalFields[46];
            yellowRoute[20] = normalFields[47];
            yellowRoute[21] = normalFields[48];
            yellowRoute[22] = normalFields[49];
            yellowRoute[23] = normalFields[50];
            yellowRoute[24] = normalFields[51];
            yellowRoute[25] = normalFields[0];
            yellowRoute[26] = normalFields[1];
            yellowRoute[27] = normalFields[2];
            yellowRoute[28] = normalFields[3];
            yellowRoute[29] = normalFields[4];
            yellowRoute[30] = normalFields[5];
            yellowRoute[31] = normalFields[6];
            yellowRoute[32] = normalFields[7];
            yellowRoute[33] = normalFields[8];
            yellowRoute[34] = normalFields[9];
            yellowRoute[35] = normalFields[10];
            yellowRoute[36] = normalFields[11];
            yellowRoute[37] = normalFields[12];
            yellowRoute[38] = normalFields[13];
            yellowRoute[39] = normalFields[14];
            yellowRoute[40] = normalFields[15];
            yellowRoute[41] = normalFields[16];
            yellowRoute[42] = normalFields[17];
            yellowRoute[43] = normalFields[18];
            yellowRoute[44] = normalFields[19];
            yellowRoute[45] = normalFields[20];
            yellowRoute[46] = normalFields[21];
            yellowRoute[47] = normalFields[22];
            yellowRoute[48] = normalFields[23];
            yellowRoute[49] = normalFields[24];
            yellowRoute[50] = normalFields[25];
            yellowRoute[51] = yellowField0;
            yellowRoute[52] = yellowField1;
            yellowRoute[53] = yellowField2;
            yellowRoute[54] = yellowField3;
            yellowRoute[55] = yellowField4;
            yellowRoute[56] = yellowField5;
            #endregion
            #endregion

            routes.Add(LudoColor.Blue, blueRoute);
            routes.Add(LudoColor.Green, greenRoute);
            routes.Add(LudoColor.Red, redRoute);
            routes.Add(LudoColor.Yellow, yellowRoute);

        }

        /// <summary>
        /// Adds a piece to a field on route
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="index"></param>
        public void AddPieceToFieldOnRoute(Piece piece, int index)
        {
            if (piece.color == LudoColor.Blue)
            {
                blueRoute[index].AddOccupant(piece);
            }
            else if (piece.color == LudoColor.Green)
            {
                greenRoute[index].AddOccupant(piece);
            }
            else if (piece.color == LudoColor.Red)
            {
                redRoute[index].AddOccupant(piece);
            }
            else
            {
                yellowRoute[index].AddOccupant(piece);
            }
        }

        #endregion
        /*
        public void MovePiece(Field[] route, Piece chosenpiecetomove, int dicenumber)
        {
            // if piece is in base, move it out to index 0 on responding route (remove piece from base!)
            if (chosenpiecetomove.inBase == true && dicenumber == 6)
            {
                chosenpiecetomove.inBase = false;
                chosenpiecetomove.fieldPosition = 0;
                route[0].piecesOnField.Add(chosenpiecetomove);
                // Multiple pieces on same field be expressed differently. Multiple should be double letters fx. GG or RR etc.
                UpdateFieldContent(route[0]);

            }
            // normal movement
            else
            {
                // If not reached goal yet
                if (chosenpiecetomove.fieldPosition + dicenumber < route.Length)
                {
                    // remove from current position & move to new position
                    route[chosenpiecetomove.fieldPosition].piecesOnField.Remove(chosenpiecetomove);
                    UpdateFieldContent(route[chosenpiecetomove.fieldPosition]);
                    chosenpiecetomove.fieldPosition += dicenumber;
                    CheckFieldForOpponentsAndMove(route, chosenpiecetomove);
                }
                // If piece "passes" the goal, move backwards (opponents do not exist on the end fields)
                else if (chosenpiecetomove.fieldPosition + dicenumber > route.Length)
                {
                    route[chosenpiecetomove.fieldPosition].piecesOnField.Remove(chosenpiecetomove);
                    UpdateFieldContent(route[chosenpiecetomove.fieldPosition]);
                    chosenpiecetomove.fieldPosition += dicenumber;
                    int fieldsToMoveBack = chosenpiecetomove.fieldPosition - route.Length;
                    int destinationField = route.Length - fieldsToMoveBack;
                    route[destinationField].piecesOnField.Add(chosenpiecetomove);
                    UpdateFieldContent(route[destinationField]);
                }
                // Reached goal
                else //(chosenpiecetomove.fieldPosition + dicenumber == route.Length)
                {
                    route[chosenpiecetomove.fieldPosition].piecesOnField.Remove(chosenpiecetomove);
                    UpdateFieldContent(route[chosenpiecetomove.fieldPosition]);
                    chosenpiecetomove.fieldPosition += dicenumber;
                    route[chosenpiecetomove.fieldPosition].piecesOnField.Add(chosenpiecetomove);
                    chosenpiecetomove.reachedGoal = true;
                    UpdateFieldContent(route[route.Length - 1]);
                }
            }

        }

        public void CheckFieldForOpponentsAndMove(Field[] route, Piece chosenpiecetomove)
        {
            // Check field for opponents and kick opponent back to base if only one opponent is there
            if (route[chosenpiecetomove.fieldPosition].OccupantColour != chosenpiecetomove.Colour && route[chosenpiecetomove.fieldPosition].piecesOnField.Count == 1)
            {
                route[chosenpiecetomove.fieldPosition].piecesOnField[0].inBase = true;
                route[chosenpiecetomove.fieldPosition].piecesOnField[0].fieldPosition = 0;
                route[chosenpiecetomove.fieldPosition].piecesOnField.RemoveAt(0);
                route[chosenpiecetomove.fieldPosition].OccupantColour = "empty";

                // update the info of the moved piece and new field
                route[chosenpiecetomove.fieldPosition].piecesOnField.Add(chosenpiecetomove);
                UpdateFieldContent(route[chosenpiecetomove.fieldPosition]);
            }
            // If opponents are more than one then the chosen piece gets kicked back to base
            else if (route[chosenpiecetomove.fieldPosition].OccupantColour != chosenpiecetomove.Colour && route[chosenpiecetomove.fieldPosition].piecesOnField.Count > 1)
            {
                chosenpiecetomove.inBase = true;
                chosenpiecetomove.fieldPosition = 0;
            }
            // if no opponents are there
            else
            {
                route[chosenpiecetomove.fieldPosition].piecesOnField.Add(chosenpiecetomove);
                UpdateFieldContent(route[chosenpiecetomove.fieldPosition]);
            }

        }

        public void UpdateFieldContent(Field fieldtobeopdated)
        {
            if (fieldtobeopdated.piecesOnField.Count > 1)
            {
                fieldtobeopdated.FieldContent = fieldtobeopdated.piecesOnField[0].fieldAppearanceMoreThanOne;
                fieldtobeopdated.Occupied = true;
                fieldtobeopdated.OccupantColour = fieldtobeopdated.piecesOnField[0].Colour;
            }
            else if (fieldtobeopdated.piecesOnField.Count == 1)
            {
                fieldtobeopdated.FieldContent = fieldtobeopdated.piecesOnField[0].fieldAppearance;
                fieldtobeopdated.Occupied = true;
                fieldtobeopdated.OccupantColour = fieldtobeopdated.piecesOnField[0].Colour;
            }
            else
            {
                fieldtobeopdated.FieldContent = fieldtobeopdated.defaultField;
                fieldtobeopdated.Occupied = false;
                fieldtobeopdated.OccupantColour = "empty";
            }
        }


        public void DisplayBoard()
        {
            string[] boardRows = new string[16];
            for (int i = 0; i < boardFields.GetLength(0); i++)
            {
                for (int j = 0; j < boardFields.GetLength(1); j++)
                {
                    if (boardFields[i, j].Occupied == false)
                    {
                        boardFields[i, j].FieldContent = boardFields[i, j].defaultField;
                    }
                    boardRows[i] += boardFields[i, j].FieldContent;
                }
            }
            foreach (var row in boardRows)
            {
                Console.WriteLine(row);
            }
        }

        public bool CheckIfAllPiecesInBase(Piece[] pieces)
        {
            int numberOfPiecesInBase = 0;
            for (int i = 0; i < pieces.Length; i++)
            {
                if (pieces[i].inBase == true)
                {
                    numberOfPiecesInBase++;
                }
            }

            if (numberOfPiecesInBase == 4)
            {
                return true;

            }
            else { return false; }
        }

        public static bool CheckIfAllPiecesReachedGoal(Piece[] setofpieces)
        {
            int numberOfPiecesInGoal = 0;
            for (int i = 0; i < setofpieces.Length; i++)
            {
                if (setofpieces[i].reachedGoal == true)
                {
                    numberOfPiecesInGoal++;
                }
            }

            if (numberOfPiecesInGoal == 4)
            {
                return true;
            }
            else { return false; }
        }
        #endregion
        /*
        public void demoBoard()
        {
            // DisplayBoard shows default if fields not occupied.
            Console.ForegroundColor = ConsoleColor.Green;
            for (int j = 0; j < Program.greenPieces.Length; j++)
            {
                for (int i = 0; i < 57; i++)
                {
                    greenRoute[i].FieldContent = Program.greenPieces[j].fieldAppearance;
                    Console.Clear();
                    DisplayBoard();
                    System.Threading.Thread.Sleep(20);
                    greenRoute[i].FieldContent = greenRoute[i].defaultField;
                }
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int j = 0; j < Program.bluePieces.Length; j++)
            {
                for (int i = 0; i < 57; i++)
                {
                    blueRoute[i].FieldContent = Program.bluePieces[j].fieldAppearance;
                    Console.Clear();
                    DisplayBoard();
                    System.Threading.Thread.Sleep(20);
                    blueRoute[i].FieldContent = blueRoute[i].defaultField;
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int j = 0; j < Program.yellowPieces.Length; j++)
            {
                for (int i = 0; i < 57; i++)
                {
                    yellowRoute[i].FieldContent = Program.yellowPieces[j].fieldAppearance;
                    Console.Clear();
                    DisplayBoard();
                    System.Threading.Thread.Sleep(20);
                    yellowRoute[i].FieldContent = yellowRoute[i].defaultField;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            for (int j = 0; j < Program.redPieces.Length; j++)
            {
                for (int i = 0; i < 57; i++)
                {
                    redRoute[i].FieldContent = Program.redPieces[j].fieldAppearance;
                    Console.Clear();
                    DisplayBoard();
                    System.Threading.Thread.Sleep(20);
                    redRoute[i].FieldContent = redRoute[i].defaultField;
                }
            }
        }
        */
        /// Board sketch
        /*                 
        ("0112334556778991111131151171192212232252272293");
        ("______________________________________________"); // 0
        ("                  |__|__|__|                 |"); // 1
        ("                  |__|Y_|Y_|                 |"); // 2 
        ("       G1 G2      |__|Y_|__|      Y1 Y2      |"); // 3
        ("       G3 G4      |__|Y_|__|      Y3 Y4      |"); // 4
        ("                  |__|Y_|__|                 |"); // 5
        ("__________________|__|Y_|__|_________________|"); // 6
        ("|__|G_|__|__|__|__|/      \\|__|__|__|__|__|__|"); // 7
        ("|__|G_|G_|G_|G_|G_|  GOAL  |_B|_B|_B|_B|_B|__|"); // 8
        ("|__|__|__|__|__|__|\\      /|__|__|__|__|_B|__|"); // 9
        ("                  |__|R_|__|                 |"); // 10
        ("                  |__|R_|__|                 |"); // 11
        ("       R1 R2      |__|R_|__|      B1 B2      |"); // 12
        ("       R3 R4      |__|R_|__|      B3 B4      |"); // 13
        ("                  |R_|R_|__|                 |"); // 14
        ("__________________|__|__|__|_________________|"); // 15

        Green numbering example
        ("0112334556778991111131151171192212232252272293");
        ("______________________________________________"); // 0
        ("                  |10|11|12|                 |"); // 1
        ("                  |9_|Y_|13|                 |"); // 2 
        ("       G1 G2      |8_|Y_|14|      Y1 Y2      |"); // 3
        ("       G3 G4      |7_|Y_|15|      Y3 Y4      |"); // 4
        ("                  |6_|Y_|16|                 |"); // 5
        ("__________________|5_|Y_|17|_________________|"); // 6
        ("|51|0_|1_|2_|3_|4_|/      \\|18|19|20|21|22|23|"); // 7
        ("|50|G_|G_|G_|G_|G_|  GOAL  |_B|_B|_B|_B|_B|24|"); // 8
        ("|49|48|47|46|45|44|\\      /|30|29|28|27|26|25|"); // 9
        ("                  |43|R_|31|                 |"); // 10
        ("                  |42|R_|32|                 |"); // 11
        ("       R1 R2      |41|R_|33|      B1 B2      |"); // 12
        ("       R3 R4      |40|R_|34|      B3 B4      |"); // 13
        ("                  |39|R_|35|                 |"); // 14
        ("__________________|38|37|36|_________________|"); // 15

        */

    }
}
