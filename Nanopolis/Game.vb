Imports System.IO
'0 at start menu displays 33x2 grid of various buildings, for debugging PrintMap()
Module Module1
    Sub StartMenu()
        MsgBox("Welcome to Nanopolis!" & vbCrLf & "Developed by Maksim Al-Utaibi" & vbCrLf & "Make sure to maximise the console window when playing.", vbOKOnly)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("---MAIN MENU---" & vbCrLf)
        Console.ResetColor()
        Console.WriteLine("Please enter one of the numbers below:")
        Dim MenuCode As Integer
        Console.WriteLine("1 New game")
        Console.WriteLine("2 Load a previously saved game")
        Console.WriteLine("3 Start a tutorial game")
        Console.WriteLine("4 Display key bindings")
        Console.WriteLine("5 Graphics options")
        Console.WriteLine("6 Quit to desktop")
        Try
            MenuCode = Console.ReadLine()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
        Select Case MenuCode
            Case 1
                NewGame()
            Case 2
                LoadGame()
            Case 3
                Tutorial()
            Case 4
                KeyBindMenu()
            Case 5
                GraphicsMenu()
            Case 6
                End
            Case 0
                PrintMap()
        End Select
    End Sub
    Sub PrintMap()
        Dim map As Map = New Map()
        Console.Clear()
        Map.Grid = {{-1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31}, {-1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31}}
        For y As Integer = 0 To 1
            For CurrentLine As Integer = 0 To 3
                For x = 0 To 31
                    If CurrentLine = 0 Then
                        Select Case Map.Grid(y, x)
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
                                Console.Write("   ")
                                Console.ResetColor()
                            Case 2
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkRed
                                Console.Write("_____")
                                Console.ResetColor()
                            Case 3
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("     ")
                                Console.ResetColor()
                            Case 4
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("     ")
                                Console.ResetColor()
                            Case 5
                                Console.Write("|MA|_")
                            Case 6
                                Console.Write("_____")
                            Case 7
                                Console.Write(" ____")
                            Case 8
                                Console.Write("____ ")
                            Case 9
                                Console.Write("|___ ")
                            Case 10
                                Console.Write(" ___|")
                            Case 11
                                Console.Write("_____")
                            Case 12
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write(" ")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write("||")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("  ")
                                Console.ResetColor()
                            Case 13
                                Console.Write("_/ \_")
                            Case 14
                                Console.Write("_/ \_")
                            Case 15
                                Console.Write("_____")
                            Case 16
                                Console.Write("_/|  ")
                            Case 17
                                Console.Write(" | \_")
                            Case 18
                                Console.Write("_/|  ")
                            Case 19
                                Console.Write(" |\__")
                            Case 20
                                Console.Write("__   ")
                            Case 21
                                Console.Write("  ___")
                            Case 22
                                Console.Write("_____")
                            Case 23
                                Console.Write(" |:| ")
                            Case 24
                                Console.Write("/ : \")
                            Case 25
                                Console.Write("/ : \")
                            Case 26
                                Console.Write("/ :| ")
                            Case 27
                                Console.Write(" |: \")
                            Case 28
                                Console.Write("/ :| ")
                            Case 29
                                Console.Write(" |: \")
                            Case 30
                                Console.Write("___  ")
                            Case 31
                                Console.Write("  ___")
                            Case 32
                                Console.Write("oOO  ")
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
                                Console.Write("OOo  ")
                        End Select
                    ElseIf CurrentLine = 1 Then
                        Select Case Map.Grid(y, x)
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
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("_____")
                                Console.ResetColor()
                            Case 12
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.Write(" ")
                                Console.BackgroundColor = ConsoleColor.Gray
                                Console.Write("||")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("  ")
                                Console.ResetColor()
                            Case 13
                                Console.Write("_   _")
                            Case 14
                                Console.Write("_   _")
                            Case 15
                                Console.Write("_____")
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
                                Console.Write("-----")
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
                        Select Case map.Grid(y, x)
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
                        Select Case Map.Grid(y, x)
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
        Console.ReadLine()
    End Sub
    Sub MainMenu()
        Console.Clear()
        Console.WriteLine("---MAIN MENU---" & vbCrLf)
        Console.WriteLine("Please enter one of the numbers below:")
        Dim MenuCode As Integer
        Console.WriteLine("1 New game")
        Console.WriteLine("2 Load a previously saved game")
        Console.WriteLine("3 Start a tutorial game")
        Console.WriteLine("4 Display key bindings")
        Console.WriteLine("5 Pick resolution")
        Console.WriteLine("6 Quit to desktop")
        MenuCode = Console.ReadLine()
        Select Case MenuCode
            Case 1
                NewGame()
            Case 2
                LoadGame()
            Case 3
                Tutorial()
            Case 4
                KeyBindMenu()
            Case 5
                GraphicsMenu()
            Case 6
                End
        End Select
    End Sub
    Function GraphicsMenu()
        Console.WriteLine("--GRAPHICS OPTIONS--")
        Dim MenuCode As Integer = Console.ReadLine()
    End Function
    Sub KeyBindMenu()
        Console.Clear()
        Console.WriteLine("--KEY BINDINGS--")
        Dim MenuCode As Integer = Console.ReadLine()
    End Sub
    Sub LoadGame()
        Console.WriteLine("--LOAD GAME--")
        Dim MenuCode As Integer = Console.ReadLine()
    End Sub
    Sub Tutorial()

    End Sub
    Sub NewGame()
        Console.WriteLine("Generating map...")
        Dim MenuCode As Integer = Console.ReadLine()
    End Sub
    Sub SaveGame()
        Dim MenuCode As Integer = Console.ReadLine()
    End Sub
    Sub MapSelection()

    End Sub
    Sub Main()
        StartMenu()
    End Sub

End Module