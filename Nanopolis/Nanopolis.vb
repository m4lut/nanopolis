Imports System.IO
Imports Newtonsoft.Json
Public Structure GameSettings
    Const DefaultMapWidth = 32
    Const DefaultTextureFile = "textures.json"
    Const DefaultIsTutorialGame = False
    Public MapWidth As Integer
    Public TextureFile As String
    Public IsTutorialGame As Boolean
End Structure
Public Class JSON_Result
    Public GameSettings As GameSettings
    Public TotalPowerSupply As Integer
    Public TotalPowerDemand As Integer
    Public GameMap As Map
    Public GridCodes(24, 45) As Integer
End Class
Module Module1
    Sub Main()
        MsgBox("Welcome to Nanopolis!" & vbCrLf & "Developed by Maksim Al-Utaibi" & vbCrLf & "Make sure to maximise the console window when playing.", vbOKOnly)
        Dim map As Map = New Map()
        Dim gameSettings As GameSettings = New GameSettings()
        gameSettings.TextureFile = GameSettings.DefaultTextureFile
        gameSettings.MapWidth = GameSettings.DefaultMapWidth
        gameSettings.IsTutorialGame = GameSettings.DefaultIsTutorialGame
        StartMenu(gameSettings)
    End Sub
    Sub StartMenu(ByRef GameSettings)
        Console.Clear()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("---MAIN MENU---" & vbCrLf)
        Console.ResetColor()
        Console.WriteLine("Please enter one of the keys below:")
        Console.WriteLine("[1] New game")
        Console.WriteLine("[2] Load a previously saved game (Not yet implemented!)")
        Console.WriteLine("[3] Start a tutorial game (Not yet implemented!)")
        Console.WriteLine("[4] View key bindings")
        Console.WriteLine("[5] Graphics options")
        Console.WriteLine("[ESC] Quit to desktop")
        Dim MenuCode As ConsoleKeyInfo = Console.ReadKey(True)
        If MenuCode.Key = ConsoleKey.D1 Then
            CreateNewGame(True, Nothing, Nothing, GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D2 Then
            LoadGame(Nothing, Nothing, True, GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D3 Then
            Tutorial(GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D4 Then
            KeyBindMenu(Nothing, True, Nothing, GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D5 Then
            GraphicsMenu(Nothing, True, Nothing, GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.Escape Then
            Stop
        Else
            StartMenu(GameSettings)
        End If
    End Sub
    Sub MainMenu(ByRef Game, ByRef map)
        Dim GameSettings As GameSettings
        GameSettings.IsTutorialGame = Game.GameSettings.IsTutorialGame
        GameSettings.MapWidth = Game.MapWidth
        GameSettings.TextureFile = Game.TextureFile
        Console.Clear()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("---MAIN MENU---" & vbCrLf)
        Console.ResetColor()
        Console.WriteLine("Please enter one of the following keys:")
        Console.WriteLine("[1] New game (WARNING: Save your game before starting a new game!)")
        Console.WriteLine("[2] Load a previously saved game (not yet implemented)")
        Console.WriteLine("[3] Start a tutorial game (not yet implemented!)")
        Console.WriteLine("[4] View key bindings")
        Console.WriteLine("[5] Graphics options")
        Console.WriteLine("[6] Quit to desktop")
        Console.WriteLine("[ESC] Return to game")
        Dim MenuCode As ConsoleKeyInfo = Console.ReadKey(True)
        If MenuCode.Key = ConsoleKey.D1 Then
            CreateNewGame(False, Game, map, GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D2 Then
            LoadGame(Game, map, False, GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D3 Then
            Tutorial(GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D4 Then
            KeyBindMenu(map, False, Game, GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D5 Then
            GraphicsMenu(map, False, Game, GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D6 Then
            Stop
        ElseIf MenuCode.Key = ConsoleKey.Escape Then
            Dim pos As Position
            pos.y = 13
            pos.x = 16
            Game.PrintMap(pos, map, Game)
        Else
            MainMenu(Game, map)
        End If
    End Sub
    Sub GraphicsMenu(ByRef map, isStart, ByRef game, ByRef GameSettings)
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
                StartMenu(GameSettings)
            Else
                MainMenu(game, map)
            End If
        ElseIf input.Key = ConsoleKey.D1 Then
            Console.Clear()
            Console.BackgroundColor = ConsoleColor.Gray
            Console.ForegroundColor = ConsoleColor.Black
            Console.WriteLine(" CHANGE MAP SIZE")
            Console.ResetColor()
            Console.WriteLine(" [1] Low resolution / High scaling")
            Console.WriteLine(" [2] High resolution")
            Console.WriteLine(" [ESC] Return")
            Dim input2 As ConsoleKeyInfo = Console.ReadKey(True)
            If input2.Key = ConsoleKey.D1 Then
                GameSettings.MapWidth = 32
                If game <> Nothing Then
                    game.GameSettings.MapWidth = 32
                End If
                GraphicsMenu(map, isStart, game, GameSettings)
            ElseIf input2.key = ConsoleKey.D2 Then
                GameSettings.MapWidth = 45
                If game <> Nothing Then
                    game.GameSettings.MapWidth = 45
                End If
                GraphicsMenu(map, isStart, game, GameSettings)
            ElseIf input2.Key = ConsoleKey.Escape Then
                If isStart = True Then
                    StartMenu(GameSettings)
                End If
                GraphicsMenu(map, isStart, game, GameSettings)
            End If
        End If
    End Sub
    Sub KeyBindMenu(map, isStart, game, GameSettings)
        Console.Clear()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("--KEY BINDINGS--" & vbCrLf)
        Console.ResetColor()
        Console.WriteLine("Navigation:")
        Console.WriteLine(" [W] Up one block")
        Console.WriteLine(" [A] Left one block")
        Console.WriteLine(" [S] Down one block")
        Console.WriteLine(" [D] Right one block")
        Console.WriteLine(" [UPARROW] Up five blocks")
        Console.WriteLine(" [LEFTARROW] Left five blocks")
        Console.WriteLine(" [DOWNARROW] Down five blocks")
        Console.WriteLine(" [RIGHTARROW] Right five blocks")
        Console.WriteLine(" [ENTER] Select lot" & vbCrLf)
        Console.WriteLine("Construction:")
        Console.WriteLine(" [B] Enter building submenu (after selecting a lot)")
        Console.WriteLine(" [1-9] Choose building category")
        Console.WriteLine(" [C] Cancel construction, return to navigation" & vbCrLf)
        Console.WriteLine("Demolition:")
        Console.WriteLine(" [D] Demolish building (after selecting a lot)" & vbCrLf)
        Console.WriteLine("Time:")
        Console.WriteLine(" [N] Finish week in manual mode")
        Console.WriteLine(" [SPACE] Toggle between auto and manual week mode" & vbCrLf)
        Console.WriteLine("Managing the government:")
        Console.WriteLine(" [G] Enter the government menu")
        Console.WriteLine(" [C] Cancel")
        Console.WriteLine(" [1] Treasury department")
        Console.WriteLine(" [2] Executive cabinet")
        Console.WriteLine(" [3] The legislature" & vbCrLf)
        Console.WriteLine(" Department of the Treasury:")
        Console.WriteLine("  [1] Adjust tax rates")
        Console.WriteLine("  [2] Adjust interest rate")
        Console.WriteLine("  [3] Issue bonds")
        Console.WriteLine("  [4] View balance sheet" & vbCrLf)
        Console.WriteLine(" Executive Cabinet: ")
        Console.WriteLine("  [1] Sign executive action")
        Console.WriteLine("  [C] Cancel" & vbCrLf)
        Console.WriteLine(" The Legislature:")
        Console.WriteLine("  [1] Dissolve parliament and trigger a General Election")
        Console.WriteLine("  [2] Propose a bill for Parliament to vote on")
        Console.WriteLine("  [C] Cancel")
        Console.WriteLine("")
        Console.WriteLine("Press [ESC] to return")
        Dim input As ConsoleKeyInfo = Console.ReadKey(True)
        If input.Key = ConsoleKey.Escape Then
            If isStart = True Then
                StartMenu(GameSettings)
            Else
                MainMenu(map, game)
            End If
        End If
    End Sub
    Sub LoadGame(ByRef game, map, isStart, GameSettings)
        Console.Clear()
        Dim PathName As String = Nothing
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("Input file name, or leave empty to return to the main menu:")
        Console.ResetColor()
        Try
            PathName = Console.ReadLine()
            'insert some JSON serealization here
            If PathName = Nothing Then
                If isStart = True Then
                    StartMenu(GameSettings)
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
                StartMenu(GameSettings)
            Else
                MainMenu(game, map)
            End If
        End Try
    End Sub
    Sub Tutorial(ByRef GameSettings)
        Console.ReadLine()
        GameSettings.IsTutorial = True
        StartMenu(GameSettings)
    End Sub
    Sub CreateNewGame(IsStart, ByRef CurrentGame, ByRef Map, GameSettings)
        Dim newGame As Game = New Game()
        newGame.GameSettings.IsTutorialGame = GameSettings.IsTutorialGame
        newGame.GameSettings.MapWidth = GameSettings.MapWidth
        newGame.GameSettings.TextureFile = GameSettings.TextureFile
        Dim newLotObjectMatrix(24, GameSettings.MapWidth) As Lot
        newGame.LotObjectMatrix = newLotObjectMatrix
        newGame.NewGame(IsStart, CurrentGame, Map, GameSettings, newGame)
    End Sub
    Sub SaveGame(game, map)
        Console.Clear()
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
        Finally
            MainMenu(game, map)
        End Try
        Console.Clear()
        game.PrintMap(0, 16)
    End Sub
End Module