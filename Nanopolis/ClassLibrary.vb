﻿Public Class Map
    Public Shared GridCodes(,) As Integer
    Public Shared SelectorY As Integer = 0
    Public Shared SelectorX As Integer = 16
    Public Sub PrintMap(ByRef SelectorY, ByRef SelectorX)
        Console.Clear()
        For y As Integer = 0 To 1
            For CurrentLine As Integer = 0 To 3
                For x = 0 To 31
                    If CurrentLine = 0 Then
                        Select Case GridCodes(y, x)
                            Case -1
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.Write(". . .")
                                Console.ResetColor()
                            Case 0
                                Console.Write("___|")
                                Console.ForegroundColor = ConsoleColor.Gray
                                Console.Write("\")
                                Console.ResetColor()
                            Case 1
                                Console.ForegroundColor = ConsoleColor.Red
                                Console.Write("/\")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.Write(". .")
                                Console.ResetColor()
                            Case 2
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkRed
                                Console.Write("_____")
                                Console.ResetColor()
                            Case 3
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.Write(". . .")
                                Console.ResetColor()
                            Case 4
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("     ")
                                Console.ResetColor()
                            Case 5
                                Console.Write("|MA|_")
                            Case 6
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkYellow
                                Console.Write("_____")
                                Console.ResetColor()
                            Case 7
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkYellow
                                Console.Write(" ____")
                                Console.ResetColor()
                            Case 8
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkYellow
                                Console.Write("____ ")
                                Console.ResetColor()
                            Case 9
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkYellow
                                Console.Write(" ___ ")
                                Console.ResetColor()
                            Case 10
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkYellow
                                Console.Write(" ___ ")
                                Console.ResetColor()
                            Case 11
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkYellow
                                Console.Write("_____")
                                Console.ResetColor()
                            Case 12
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.Write(".")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write("||")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.Write(" .")
                                Console.ResetColor()
                            Case 13
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write("_")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("/ \")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("_")
                                Console.ResetColor()
                            Case 14
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write("_")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("/ \")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("_")
                                Console.ResetColor()
                            Case 15
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write("_____")
                                Console.ResetColor()
                            Case 16
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("_")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("/|")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("  ")
                                Console.ResetColor()
                            Case 17
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("| \")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("_")
                                Console.ResetColor()
                            Case 18
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("_")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("/|")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("  ")
                                Console.ResetColor()
                            Case 19
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("|\")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("__")
                                Console.ResetColor()
                            Case 20
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("__   ")
                                Console.ResetColor()
                            Case 21
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("  ___")
                                Console.ResetColor()
                            Case 22
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("_____")
                                Console.ResetColor()
                            Case 23
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.ForegroundColor = ConsoleColor.White
                                Console.Write("|:|")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                                Console.ResetColor()
                            Case 24
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("/ : \")
                                Console.ResetColor()
                            Case 25
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("/ : \")
                                Console.ResetColor()
                            Case 26
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("/ :|")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                            Case 27
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("|: \")
                                Console.ResetColor()
                            Case 28
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("/ :|")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                                Console.ResetColor()
                            Case 29
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("|: \")
                                Console.ResetColor()
                            Case 30
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("___  ")
                                Console.ResetColor()
                            Case 31
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("  ___")
                                Console.ResetColor()
                            Case 32
                                Console.BackgroundColor = ConsoleColor.Gray
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write("oOO  ")
                                Console.ResetColor()
                            Case 33
                                Console.Write("    _")
                            Case 34
                                Console.Write("_    ")
                            Case 35
                                Console.Write("|[][]")
                            Case 36
                                Console.Write("[][]|")
                            Case 37
                                Console.Write("_____")
                            Case 38
                                Console.BackgroundColor = ConsoleColor.Blue
                                Console.Write("~~~~~")
                                Console.BackgroundColor = ConsoleColor.Black
                            Case 39
                                Console.Write(":::::")
                            Case 40
                                Console.Write("x   x")
                            Case 41
                                Console.BackgroundColor = ConsoleColor.Gray
                                Console.Write("OOo  ")
                                Console.ResetColor()
                        End Select
                    ElseIf CurrentLine = 1 Then
                        Select Case GridCodes(y, x)
                            Case -1
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.Write(" . . ")
                                Console.ResetColor()
                            Case 0
                                Console.Write("I")
                                Console.ForegroundColor = ConsoleColor.Yellow
                                Console.Write("  N ")
                                Console.ResetColor()
                            Case 1
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.DarkYellow
                                Console.Write("[]")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("   ")
                                Console.ResetColor()
                            Case 2
                                Console.BackgroundColor = ConsoleColor.DarkRed
                                Console.Write("| ")
                                Console.BackgroundColor = ConsoleColor.DarkCyan
                                Console.Write("[]")
                                Console.BackgroundColor = ConsoleColor.DarkRed
                                Console.Write("|")
                                Console.ResetColor()
                            Case 3
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("_____")
                                Console.ResetColor()
                            Case 4
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("____ ")
                                Console.ResetColor()
                            Case 5
                                Console.Write("|LL|")
                                Console.BackgroundColor = ConsoleColor.DarkCyan
                                Console.Write("|")
                                Console.ResetColor()
                            Case 6
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.Write("|:::|")
                                Console.ResetColor()
                            Case 7
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("|")
                                Console.ForegroundColor = ConsoleColor.DarkYellow
                                Console.Write("h ")
                                Console.ForegroundColor = ConsoleColor.White
                                Console.Write(" |")
                                Console.ResetColor()
                            Case 8
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(".:::|")
                                Console.ResetColor()
                            Case 9
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.Write("|:::|")
                                Console.ResetColor()
                            Case 10
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkYellow
                                Console.Write("|   |")
                                Console.ResetColor()
                            Case 11
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("_____")
                                Console.ResetColor()
                            Case 12
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write(" ")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("||")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("  ")
                                Console.ResetColor()
                            Case 13
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write("_   _")
                                Console.ResetColor()
                            Case 14
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write("_   _")
                                Console.ResetColor()
                            Case 15
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write("_   _")
                                Console.ResetColor()
                            Case 16
                                Console.Write("_ |  ")
                            Case 17
                                Console.Write(" | __")
                            Case 18
                                Console.Write("__/  ")
                            Case 19
                                Console.Write(" \___")
                            Case 20
                                Console.Write("_ \  ")
                            Case 21
                                Console.Write(" /  _")
                            Case 22
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("-----")
                                Console.ResetColor()
                            Case 23
                                Console.Write(" |:| ")
                            Case 24
                                Console.Write("-   -")
                            Case 25
                                Console.Write("-   -")
                            Case 26
                                Console.Write("-   -")
                            Case 27
                                Console.Write("-   -")
                            Case 28
                                Console.Write("-- | ")
                            Case 29
                                Console.Write(" | --")
                            Case 30
                                Console.Write("-- \ ")
                            Case 31
                                Console.Write(" /  -")
                            Case 32
                                Console.Write("__||_")
                            Case 33
                                Console.Write("___/ ")
                            Case 34
                                Console.Write(" \___")
                            Case 35
                                Console.Write("|[][]")
                            Case 36
                                Console.Write("[][]|")
                            Case 37
                                Console.Write("|POL|")
                            Case 38
                                Console.BackgroundColor = ConsoleColor.Blue
                                Console.Write("~~~~~")
                                Console.ResetColor()
                            Case 39
                                Console.Write(" | | ")
                            Case 40
                                Console.Write("| x |")
                            Case 41
                                Console.Write("")
                        End Select
                    ElseIf CurrentLine = 2 Then
                        Select Case GridCodes(y, x)
                            Case -1
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.Write(". . .")
                                Console.ResetColor()
                            Case 0
                                Console.ForegroundColor = ConsoleColor.Yellow
                                Console.Write("   N ")
                                Console.ResetColor()
                            Case 1
                                Console.ForegroundColor = ConsoleColor.Red
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("  ")
                                Console.BackgroundColor = ConsoleColor.Black
                                Console.Write("/\")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                                Console.ResetColor()
                            Case 2
                                Console.BackgroundColor = ConsoleColor.DarkRed
                                Console.Write("| ")
                                Console.BackgroundColor = ConsoleColor.DarkCyan
                                Console.Write("[]")
                                Console.BackgroundColor = ConsoleColor.DarkRed
                                Console.Write("|")
                                Console.ResetColor()
                            Case 3
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write("[GAS]")
                                Console.ResetColor()
                            Case 4
                                Console.BackgroundColor = ConsoleColor.Red
                                Console.Write("M")
                                Console.BackgroundColor = ConsoleColor.DarkRed
                                Console.Write("A")
                                Console.BackgroundColor = ConsoleColor.Red
                                Console.Write("R")
                                Console.BackgroundColor = ConsoleColor.DarkRed
                                Console.Write("T")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                                Console.ResetColor()
                            Case 5
                                Console.BackgroundColor = ConsoleColor.DarkCyan
                                Console.Write("[][]|")
                                Console.ResetColor()
                            Case 6
                                Console.Write("|h| |")
                            Case 7
                                Console.ForegroundColor = ConsoleColor.DarkYellow
                                Console.Write("|hTT|")
                                Console.ResetColor()
                            Case 8
                                Console.Write("||h||")
                            Case 9
                                Console.Write("|||||")
                            Case 10
                                Console.Write("|/U\|")
                            Case 11
                                Console.Write("     ")
                            Case 12
                                Console.Write(" ||  ")
                            Case 13
                                Console.Write(" \ / ")
                            Case 14
                                Console.Write(" \ / ")
                            Case 15
                                Console.Write(" \ / ")
                            Case 16
                                Console.Write(" \|  ")
                            Case 17
                                Console.Write(" |/  ")
                            Case 18
                                Console.Write("     ")
                            Case 19
                                Console.Write("     ")
                            Case 20
                                Console.Write(" \|  ")
                            Case 21
                                Console.Write(" | / ")
                            Case 22
                                Console.Write("_____")
                            Case 23
                                Console.Write(" |:| ")
                            Case 24
                                Console.Write("     ")
                            Case 25
                                Console.Write("     ")
                            Case 26
                                Console.Write("_____")
                            Case 27
                                Console.Write("     ")
                            Case 28
                                Console.Write("___/ ")
                            Case 29
                                Console.Write(" \___")
                            Case 30
                                Console.Write("  :| ")
                            Case 31
                                Console.Write(" |:  ")
                            Case 32
                                Console.Write("|   |")
                            Case 33
                                Console.Write("|[][]")
                            Case 34
                                Console.Write("[][]|")
                            Case 35
                                Console.Write("    |")
                            Case 36
                                Console.Write("|    ")
                            Case 37
                                Console.Write("|ICE|")
                            Case 38
                                Console.BackgroundColor = ConsoleColor.Blue
                                Console.Write("~~~~~")
                                Console.ResetColor()
                            Case 39
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(":::::")
                                Console.ResetColor()
                            Case 40
                                Console.Write("X | X")
                            Case 41
                                Console.Write("_||__")
                        End Select
                    ElseIf CurrentLine = 3 Then
                        Select Case GridCodes(y, x)
                            Case -1
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.Write(" . . ")
                                Console.ResetColor()
                            Case 0
                                Console.ForegroundColor = ConsoleColor.Yellow
                                Console.Write("  [N]")
                                Console.ResetColor()
                            Case 1
                                Console.ForegroundColor = ConsoleColor.DarkYellow
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("  ")
                                Console.BackgroundColor = ConsoleColor.DarkYellow
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write("[]")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                                Console.ResetColor()
                            Case 2
                                Console.BackgroundColor = ConsoleColor.DarkCyan
                                Console.Write("[]")
                                Console.BackgroundColor = ConsoleColor.DarkRed
                                Console.Write("_[|")
                                Console.ResetColor()
                            Case 3
                                Console.Write("|  ")
                                Console.BackgroundColor = ConsoleColor.DarkCyan
                                Console.Write("[]")
                                Console.ResetColor()
                            Case 4
                                Console.BackgroundColor = ConsoleColor.DarkCyan
                                Console.Write("[]")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.Write("_|")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                                Console.ResetColor()
                            Case 5
                                Console.BackgroundColor = ConsoleColor.DarkCyan
                                Console.Write("[][]|")
                                Console.ResetColor()
                            Case 6
                                Console.Write("|___|")
                            Case 7
                                Console.Write("|_h_|")
                            Case 8
                                Console.Write("|___|")
                            Case 9
                                Console.Write("|___L")
                            Case 10
                                Console.Write("|___|")
                            Case 11
                                Console.Write("     ")
                            Case 12
                                Console.Write(" ||  ")
                            Case 13
                                Console.Write(" ||  ")
                            Case 14
                                Console.Write(" ||  ")
                            Case 15
                                Console.Write(" ||  ")
                            Case 16
                                Console.Write(" ||  ")
                            Case 17
                                Console.Write(" ||  ")
                            Case 18
                                Console.Write("     ")
                            Case 19
                                Console.Write("     ")
                            Case 20
                                Console.Write(" ||  ")
                            Case 21
                                Console.Write(" ||  ")
                            Case 22
                                Console.Write("     ")
                            Case 23
                                Console.Write(" |:| ")
                            Case 24
                                Console.Write("\ : /")
                            Case 25
                                Console.Write("\ : /")
                            Case 26
                                Console.Write("     ")
                            Case 27
                                Console.Write("\ : /")
                            Case 28
                                Console.Write("     ")
                            Case 29
                                Console.Write("     ")
                            Case 30
                                Console.Write("\ :| ")
                            Case 31
                                Console.Write(" |: /")
                            Case 32
                                Console.Write("|___|")
                            Case 33
                                Console.Write("|[][]")
                            Case 34
                                Console.Write("[][]|")
                            Case 35
                                Console.Write("   / ")
                            Case 36
                                Console.Write(" \   ")
                            Case 37
                                Console.Write("|_[]|")
                            Case 38
                                Console.BackgroundColor = ConsoleColor.Blue
                                Console.Write("~~~~~")
                                Console.ResetColor()
                            Case 39
                                Console.Write("| | |")
                            Case 40
                                Console.Write("|   |")
                            Case 41
                                Console.Write("|___|")
                        End Select
                    End If
                Next
                Console.WriteLine()
            Next
        Next
        Console.Write("Y" & Int(SelectorY))
        Console.Write("X" & Int(SelectorX))
        MapSelection(SelectorY, SelectorX, GridCodes)
    End Sub
    Public Sub MapSelection(ByRef SelectorY, ByRef SelectorX, ByRef GridCodes)
        Console.TreatControlCAsInput = True
        Dim map As Map = New Map()
        Dim Lot As Lot = New Lot()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("Navigate[WASD] | Select[ENTER]:")
        Console.ResetColor()
        Dim Selected As Boolean = False
        Dim Key As ConsoleKey
        Dim Choice As ConsoleKey
        While Selected = False
            Key = Console.ReadKey(True).Key
            If Key = ConsoleKey.A Then
                SelectorX -= 1
                PrintMap(SelectorY, SelectorX)
            ElseIf Key = ConsoleKey.D Then
                SelectorX += 1
                PrintMap(SelectorY, SelectorX)
            ElseIf Key = ConsoleKey.S Then
                SelectorY += 1
                PrintMap(SelectorY, SelectorX)
            ElseIf Key = ConsoleKey.W Then
                SelectorY -= 1
                PrintMap(SelectorY, SelectorX)
            ElseIf Key = ConsoleKey.Enter Then
                Selected = True
            End If
            Console.BackgroundColor = ConsoleColor.Gray
            Console.ForegroundColor = ConsoleColor.Black
            Console.WriteLine("Demolish[D] | Build[B] | Cancel[C]")
            Console.ResetColor()
            Choice = Console.ReadKey(True).Key
            If Choice = ConsoleKey.D Then
                Lot.Demolish(Map.GridCodes, SelectorY, SelectorX)
            ElseIf Choice = ConsoleKey.B Then
                Lot.Build(Map.GridCodes, SelectorY, SelectorX, map)
            ElseIf Choice = ConsoleKey.C Then
                map.MapSelection(SelectorY, SelectorX, Map.GridCodes)
            End If
        End While
    End Sub
End Class

Public Class Lot
    Public xPos As Integer
    Public yPos As Integer
    Public LandValue As Integer
    Public LandValueModifier As Integer
    Public Function GetPos(ByVal Gridcodes(,), yPos, xPos)
        Return yPos
        Return xPos
    End Function
    Public Sub Build(ByRef GridCodes, ByRef yPos, ByRef xPos, ByRef map)
        Randomize()
        Dim ShopType As Integer = Math.Round((Rnd() * 2) + 1)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("Residential[1] | Commercial[2] | Industrial[3]($20)| Road[4] | Power[5] | Park[6] | Police[7]($75) | Parliament[8]($20000) | Nature[9]")
        Console.ResetColor()
        Dim input As ConsoleKeyInfo = Console.ReadKey(True)
        Select Case input.Key
            Case ConsoleKey.D1
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Low density[1]($10) | High density[2]($20)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    GridCodes(yPos, xPos) = 1
                    'Dim 
                ElseIf input.Key = ConsoleKey.D2 Then
                    GridCodes(yPos, xPos) = 2
                    'Dim
                End If
            Case ConsoleKey.D2
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Low density[1]($15) | High density[2]($25)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    GridCodes(yPos, xPos) = ShopType
                    'Dim
                ElseIf input.Key = ConsoleKey.D2 Then
                    GridCodes(yPos, xPos) = 5
                    'Dim
                End If
            Case ConsoleKey.D3
                GridCodes(yPos, xPos) = 5
        End Select
        map.PrintMap(0, 16)
    End Sub
    Public Sub Demolish(ByRef GridCodes, ByRef yPos, ByRef xPos)

    End Sub

    Public Function ChangeLandValue()

    End Function
    Public Function GetLandValue()

    End Function

End Class
Public Class Roads
    Inherits Lot

End Class
Public Class Nature
    Inherits Lot

End Class
Public Class ResidentialLot
    Inherits Lot
    Public Size As Integer
End Class
Public Class CommercialLot
    Inherits Lot
    Public Size As Integer
End Class
Public Class Park
    Inherits Lot

End Class
Public Class Industry
    Inherits Lot

End Class
Public Class Parliament
    Inherits Lot

End Class
Public Class Construction
    Inherits Lot

End Class
Public Class PoliceStations
    Inherits Lot

End Class
Public Class Energy
    Inherits Lot

End Class