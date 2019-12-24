'goal: set up building object list,calculate land value
Imports System.IO
Imports Newtonsoft.Json
Module Module1
    Sub Main()
        MsgBox("Welcome to Nanopolis!" & vbCrLf & "Developed by Maksim Al-Utaibi" & vbCrLf & "Make sure to maximise the console window when playing.", vbOKOnly)
        Dim map As Map = New Map()
        StartMenu()
    End Sub
    Function NewMap()
        Dim map As Map = New Map()
    End Function

    Sub StartMenu()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("---MAIN MENU---" & vbCrLf)
        Console.ResetColor()
        Console.WriteLine("Please enter one of the keys below:")
        Console.WriteLine("[1] New game")
        Console.WriteLine("[2] Load a previously saved game")
        Console.WriteLine("[3] Start a tutorial game")
        Console.WriteLine("[4] View key bindings")
        Console.WriteLine("[5] Graphics options")
        Console.WriteLine("[6] Quit to desktop")
        Dim MenuCode As ConsoleKeyInfo = Console.ReadKey(True)
        If MenuCode.Key = ConsoleKey.D1 Then
            NewGame(True, Nothing, Nothing)
        ElseIf MenuCode.Key = ConsoleKey.D2 Then
            LoadGame(Nothing, Nothing, True)
        ElseIf MenuCode.Key = ConsoleKey.D3 Then
            Tutorial()
        ElseIf MenuCode.Key = ConsoleKey.D4 Then
            KeyBindMenu(Nothing, True, Nothing)
        ElseIf MenuCode.Key = ConsoleKey.D5 Then
            GraphicsMenu(Nothing, True, Nothing)
        ElseIf MenuCode.Key = ConsoleKey.D6 Then
            Stop
        Else
            StartMenu()
        End If
    End Sub
    Sub MainMenu(game, map)
        Console.Clear()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("---MAIN MENU---" & vbCrLf)
        Console.ResetColor()
        Console.WriteLine("Please enter one of the following keys:")
        Console.WriteLine("[1] New game (WARNING: Save your game before starting a new game!)")
        Console.WriteLine("[2] Load a previously saved game")
        Console.WriteLine("[3] Start a tutorial game")
        Console.WriteLine("[4] View key bindings")
        Console.WriteLine("[5] Graphics options")
        Console.WriteLine("[6] Quit to desktop")
        Console.WriteLine("[ESC] Return to game")
        Dim MenuCode As ConsoleKeyInfo = Console.ReadKey(True)
        If MenuCode.Key = ConsoleKey.D1 Then
            NewGame(False, game, map)
        ElseIf MenuCode.Key = ConsoleKey.D2 Then
            LoadGame(game, map, False)
        ElseIf MenuCode.Key = ConsoleKey.D3 Then
            Tutorial()
        ElseIf MenuCode.Key = ConsoleKey.D4 Then
            KeyBindMenu(map, False, game)
        ElseIf MenuCode.Key = ConsoleKey.D5 Then
            GraphicsMenu(map, False, game)
        ElseIf MenuCode.Key = ConsoleKey.D6 Then
            Stop
        ElseIf MenuCode.Key = ConsoleKey.Escape Then
            Dim pos As Position
            pos.y = 13
            pos.x = 16
            game.PrintMap(pos, map, game)
        Else
            MainMenu(game, map)
        End If
    End Sub
    Function GraphicsMenu(map, isStart, game)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("--GRAPHICS OPTIONS--")
        Console.ResetColor()
        Dim input As ConsoleKeyInfo = Console.ReadKey(True)
        If input.Key = ConsoleKey.Escape Then
            If isStart = True Then
                StartMenu()
            Else
                MainMenu(game, map)
            End If
        End If
    End Function
    Sub KeyBindMenu(map, isStart, game)
        Console.Clear()
        Console.WriteLine("--KEY BINDINGS--")
        Dim input As ConsoleKeyInfo = Console.ReadKey(True)
        If input.Key = ConsoleKey.Escape Then
            If isStart = True Then
                StartMenu()
            Else
                MainMenu(map, game)
            End If
        End If
    End Sub
    Sub LoadGame(game, map, isStart)
        Console.WriteLine("Return[ESC]")
        Dim PathName As String
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        'Console.Write("Input file name:")
        Console.ResetColor()
        PathName = Console.ReadLine()
        Try
            'insert some JSON serealization here
            Console.ReadLine()
        Catch ex As Exception
            Console.BackgroundColor = ConsoleColor.Red
            Console.ForegroundColor = ConsoleColor.Black
            Console.WriteLine("Error")
            Console.WriteLine(ex.Message)
            Console.ResetColor()
            If isStart = True Then
                StartMenu()
            Else
                MainMenu(game, map)
            End If
        End Try
    End Sub
    Sub Tutorial()
        Console.ReadLine()
        StartMenu()
    End Sub
    Public Sub NewGame(IsStart, CurrentGame, Map)
        Console.WriteLine("Are you sure? No [ESC] | Yes [ENTER]")
        Dim Input As ConsoleKeyInfo = Console.ReadKey(True)
        If Input.Key = ConsoleKey.Enter Then
            Dim game As Game = New Game()
            game.NewMap(game, IsStart)
        Else
            If IsStart = True Then
                StartMenu()
            Else
                MainMenu(CurrentGame, Map)
            End If
        End If
    End Sub
    Sub SaveGame(game, map)
        Console.WriteLine("Return[ESC]")
        Dim filename As String
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("Enter the name for this save file:")
        Console.ResetColor()
        filename = Console.ReadLine()
        filename = filename & ".txt"
        Try
            Dim FileWriter As New System.IO.StreamWriter(filename)
            FileWriter.WriteLine(map.GridCodes.ToString)
            FileWriter.Close()
        Catch ex As Exception
            MainMenu(game, map)
        End Try
        Console.Clear()
        game.PrintMap(0, 16)
    End Sub
    Sub NextTurn()
        'game computes all the changes on the map eg. changes to land value and population etc.
    End Sub

End Module