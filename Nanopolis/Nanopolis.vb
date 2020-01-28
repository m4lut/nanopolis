Imports System.IO
Imports Newtonsoft.Json
Public Structure GameSettings
    Public Resolution As String
    Public TextureFile As String
    Public IsTutorialGame As Boolean
End Structure

Module Module1
    Sub Main()
        MsgBox("Welcome to Nanopolis!" & vbCrLf & "Developed by Maksim Al-Utaibi" & vbCrLf & "Make sure to maximise the console window when playing.", vbOKOnly)
        Dim map As Map = New Map()
        StartMenu()
    End Sub
    Sub StartMenu(Optional GameSettings = Nothing)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("---MAIN MENU---" & vbCrLf)
        Console.ResetColor()
        Console.WriteLine("Please enter one of the keys below:")
        Console.WriteLine("[1] New game")
        Console.WriteLine("[2] Load a previously saved game (Not yet implemented!)")
        Console.WriteLine("[3] Start a tutorial game (Not yet implemented!)")
        Console.WriteLine("[4] View key bindings (Not yet implemented!)")
        Console.WriteLine("[5] Graphics options")
        Console.WriteLine("[6] Quit to desktop")
        Dim MenuCode As ConsoleKeyInfo = Console.ReadKey(False)
        If MenuCode.Key = ConsoleKey.D1 Then
            CreateNewGame(True, Nothing, Nothing, GameSettings)
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
        Dim MenuCode As ConsoleKeyInfo = Console.ReadKey(False)
        If MenuCode.Key = ConsoleKey.D1 Then
            CreateNewGame(False, game, map)
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
    Sub GraphicsMenu(map, isStart, game)
        Console.Clear()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("--GRAPHICS OPTIONS--")
        Console.ResetColor()
        Console.WriteLine("[1] Change map size")
        Console.WriteLine("[2] Change texture pack")
        Console.WriteLine("[ESC] Return")
        Dim input As ConsoleKeyInfo = Console.ReadKey(True)
        If input.Key = ConsoleKey.Escape Then
            If isStart = True Then
                StartMenu()
            Else
                MainMenu(game, map)
            End If
        ElseIf input.Key = ConsoleKey.D1 Then
            Console.Clear()
            Console.BackgroundColor = ConsoleColor.Gray
            Console.ForegroundColor = ConsoleColor.Black
            Console.WriteLine("---CHANGE MAP SIZE---")
            Console.ResetColor()
            Console.WriteLine("[1] Low resolution / high scaling")
            Console.WriteLine("[2] High resolution")
            Console.WriteLine("[ESC] Return")
            Dim input2 As ConsoleKeyInfo = Console.ReadKey(True)
        End If
    End Sub
    Sub KeyBindMenu(map, isStart, game)
        Console.Clear()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("--KEY BINDINGS--")
        Console.ResetColor()
        Console.WriteLine("Press [ESC] to return" & vbCrLf)
        Console.WriteLine("Navigation:")
        Console.WriteLine("[W] Up one block")
        Console.WriteLine("[A] Left one block")
        Console.WriteLine("[S] Down one block")
        Console.WriteLine("[D] Right one block")
        Console.WriteLine("[UPARROW] Up five blocks")
        Console.WriteLine("[LEFTARROW] Left five blocks")
        Console.WriteLine("[DOWNARROW] Down five blocks")
        Console.WriteLine("[RIGHTARROW] Right five blocks")
        Console.WriteLine("[ENTER] Select lot" & vbCrLf)
        Console.WriteLine("Construction:")
        Console.WriteLine("[B] Enter building submenu (after selecting a lot)")
        Console.WriteLine("[1-9] Choose building category")
        Console.WriteLine("[C] Cancel construction, return to navigation" & vbCrLf)
        Console.WriteLine("Demolition:")
        Console.WriteLine("[D] Demolish building (after selecting a lot)" & vbCrLf)
        Console.WriteLine("Time:")
        Console.WriteLine("[N] Finish week in manual mode")
        Console.WriteLine("[SPACE] Toggle between auto and manual week mode" & vbCrLf)
        Console.WriteLine("Managing the government:")
        Console.WriteLine("[G] Enter the government menu")
        Console.WriteLine("[C] Cancel")
        Console.WriteLine("[1] Treasury department")
        Console.WriteLine("[2] Executive cabinet")
        Console.WriteLine("[3] The legislature" & vbCrLf)
        Console.WriteLine("Department of the Treasury:")
        Console.WriteLine("[1] Adjust tax rates")
        Console.WriteLine("[2] View balance sheet")
        Console.WriteLine("Executive Cabinet: ")
        Console.WriteLine("[1] Sign executive action")
        Console.WriteLine("[C] Cancel")
        Console.WriteLine("The Legislature:")
        Console.WriteLine("[1] Dissolve parliament and trigger a General Election")
        Console.WriteLine("[2] Propose a bill for Parliament to vote on")
        Console.WriteLine("[C] Cancel")
        Console.WriteLine("")
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
        Console.Clear()
        Dim PathName As String
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("Input file name, or type [C] and [RETURN] to return to main menu:")
        Console.ResetColor()
        Try
            PathName = Console.ReadLine()
            'insert some JSON serealization here
            If PathName = "C" Then
                If isStart = True Then
                    StartMenu()
                Else
                    MainMenu(game, map)
                End If
            End If
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
    Sub CreateNewGame(IsStart, ByRef Game, ByRef Map, Optional GameSettings = Nothing)
        Dim newGame As Game = New Game()
        newGame.NewGame(IsStart, Game, Map)
    End Sub
    Sub SaveGame(game, map)
        Console.WriteLine("Return[ESC]")
        Dim filename As String
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("Enter the name for this save file:")
        Console.ResetColor()
        filename = Console.ReadLine()
        filename = filename & ".json"
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
End Module