Imports System.IO
Imports Newtonsoft.Json
Module Module1
    Sub Main()
        MsgBox("Welcome to Nanopolis!" & vbCrLf & "Developed by Maksim Al-Utaibi" & vbCrLf & "Make sure to maximise the console window when playing.", vbOKOnly)
        Dim map As Map = New Map()
        StartMenu()
    End Sub
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
            NewGame()
        ElseIf MenuCode.Key = ConsoleKey.D2 Then
            LoadGame(Nothing, True)
        ElseIf MenuCode.Key = ConsoleKey.D3 Then
            Tutorial()
        ElseIf MenuCode.Key = ConsoleKey.D4 Then
            KeyBindMenu(Nothing, True)
        ElseIf MenuCode.Key = ConsoleKey.D5 Then
            GraphicsMenu(Nothing, True)
        ElseIf MenuCode.Key = ConsoleKey.D6 Then
            Stop
        Else
            StartMenu()
        End If
    End Sub
    Sub MainMenu(map)
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
            NewGame()
        ElseIf MenuCode.Key = ConsoleKey.D2 Then
            LoadGame(map, False)
        ElseIf MenuCode.Key = ConsoleKey.D3 Then
            Tutorial()
        ElseIf MenuCode.Key = ConsoleKey.D4 Then
            KeyBindMenu(map, False)
        ElseIf MenuCode.Key = ConsoleKey.D5 Then
            GraphicsMenu(map, False)
        ElseIf MenuCode.Key = ConsoleKey.D6 Then
            Stop
        ElseIf MenuCode.Key = ConsoleKey.Escape Then
            map.printmap(0, 16)
        Else
            MainMenu(map)
        End If
    End Sub
    Function GraphicsMenu(map, isStart)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("--GRAPHICS OPTIONS--")
        Console.ResetColor()
        Dim input As ConsoleKeyInfo = Console.ReadKey(True)
        If input.Key = ConsoleKey.Escape Then
            If isStart = True Then
                StartMenu()
            Else
                MainMenu(map)
            End If
        End If
    End Function
    Sub KeyBindMenu(map, isStart)
        Console.Clear()
        Console.WriteLine("--KEY BINDINGS--")
        Dim input As ConsoleKeyInfo = Console.ReadKey(True)
        If input.Key = ConsoleKey.Escape Then
            If isStart = True Then
                StartMenu()
            Else
                MainMenu(map)
            End If
        End If
    End Sub
    Sub LoadGame(map, isStart)
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
                MainMenu(map)
            End If
        End Try
    End Sub
    Sub Tutorial()
        Console.ReadLine()
        StartMenu()
    End Sub
    Public Sub NewGame()
        Randomize()
        Dim grassProb As Integer
        Dim waterProb As Integer
        Dim forestProb As Integer
        Dim totalProb As Integer
        Console.WriteLine("Create a plain map?[y/n]")
        Dim plainMapChoice As ConsoleKeyInfo = Console.ReadKey(True)
        If plainMapChoice.Key = ConsoleKey.N Then
            Dim map As Map = New Map()
            Try
                Console.WriteLine("Set map parameters:")
                Console.WriteLine("Likelihood of water(0-100): ")
                waterProb = Console.ReadLine()
                totalProb += waterProb
                Console.WriteLine("Likelihood of grass fields(0-100): ")
                grassProb = Console.ReadLine()
                totalProb += grassProb
                Console.WriteLine("Likelihood of forest(0-100): ")
                forestProb = Console.ReadLine()
                totalProb += forestProb
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                StartMenu()
            End Try
            Console.WriteLine("Generating map...")
            For i As Integer = 0 To 29
                For j As Integer = 0 To 32
                    Dim GeneratedTile As Single = Rnd()
                    GeneratedTile = GeneratedTile * totalProb
                    If GeneratedTile < waterProb Then
                        Dim water As Water = New Water()
                        Map.GridCodes(i, j) = 38
                        Lot.LotObjectMatrix(i, j) = water
                    ElseIf GeneratedTile > waterProb And GeneratedTile <= (grassProb + waterProb) Then
                        Dim grass As Grass = New Grass()
                        Map.GridCodes(i, j) = -1
                        Lot.LotObjectMatrix(i, j) = grass
                    ElseIf GeneratedTile > (grassProb + waterProb) Then
                        Dim forest As Forest = New Forest()
                        Map.GridCodes(i, j) = 39
                        Lot.LotObjectMatrix(i, j) = forest
                    End If
                Next
            Next
            map.PrintMap(14, 16)
        ElseIf plainMapChoice.Key = ConsoleKey.Y Then
            Dim map As Map = New Map
            For i As Integer = 0 To 29
                For j As Integer = 0 To 32
                    Map.GridCodes(i, j) = -1
                Next
            Next
            map.PrintMap(14, 16)
        ElseIf plainMapChoice.Key = ConsoleKey.Escape Then
            StartMenu()
        Else NewGame()
        End If
    End Sub
    Sub SaveGame(map)
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
            FileWriter.WriteLine(Map.GridCodes.ToString)
            FileWriter.Close()
        Catch ex As Exception
            MainMenu(map)
        End Try
        Console.Clear()
        map.PrintMap(0, 16)
    End Sub
    Sub NextTurn()
        'game computes all the changes on the map eg. changes to land value and population etc.
    End Sub

End Module