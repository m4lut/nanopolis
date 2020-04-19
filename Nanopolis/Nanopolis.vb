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
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("1")
        Console.ResetColor()
        Console.WriteLine(" New game")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("2")
        Console.ResetColor()
        Console.WriteLine(" Load a previously saved game (Not yet implemented!)")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("3")
        Console.ResetColor()
        Console.WriteLine(" View key bindings")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("4")
        Console.ResetColor()
        Console.WriteLine(" Graphics options")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("ESC")
        Console.ResetColor()
        Console.WriteLine(" Quit to desktop")
        Dim MenuCode As ConsoleKeyInfo = Console.ReadKey(True)
        If MenuCode.Key = ConsoleKey.D1 Then
            CreateNewGame(True, Nothing, GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D2 Then
            LoadGame(Nothing, True, GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D3 Then
            Tutorial(GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D4 Then
            KeyBindMenu(True, Nothing, GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.D5 Then
            GraphicsMenu(Nothing, True, Nothing, GameSettings)
        ElseIf MenuCode.Key = ConsoleKey.Escape Then
            Stop
        Else
            StartMenu(GameSettings)
        End If
    End Sub
    Sub MainMenu(ByRef Game)
        Dim inMenu As Boolean = True
        Dim GameSettings As GameSettings
        GameSettings.IsTutorialGame = Game.GameSettings.IsTutorialGame
        GameSettings.MapWidth = Game.GameSettings.MapWidth
        GameSettings.TextureFile = Game.GameSettings.TextureFile
        While inMenu
            Console.Clear()
            Console.BackgroundColor = ConsoleColor.Gray
            Console.ForegroundColor = ConsoleColor.Black
            Console.WriteLine("---MAIN MENU---" & vbCrLf)
            Console.ResetColor()
            Console.WriteLine("Please enter one of the following keys:")
            Console.BackgroundColor = ConsoleColor.Gray
            Console.ForegroundColor = ConsoleColor.Black
            Console.Write("1")
            Console.ResetColor()
            Console.WriteLine(" New game (WARNING: Save your game before starting a new game!)")
            Console.BackgroundColor = ConsoleColor.Gray
            Console.ForegroundColor = ConsoleColor.Black
            Console.Write("2")
            Console.ResetColor()
            Console.WriteLine(" Load a previously saved game (not yet implemented!)")
            Console.BackgroundColor = ConsoleColor.Gray
            Console.ForegroundColor = ConsoleColor.Black
            Console.Write("3")
            Console.ResetColor()
            Console.WriteLine(" View key bindings")
            Console.BackgroundColor = ConsoleColor.Gray
            Console.ForegroundColor = ConsoleColor.Black
            Console.Write("4")
            Console.ResetColor()
            Console.WriteLine(" Quit to start menu")
            Console.BackgroundColor = ConsoleColor.Gray
            Console.ForegroundColor = ConsoleColor.Black
            Console.Write("5")
            Console.ResetColor()
            Console.WriteLine(" Quit to desktop")
            Console.BackgroundColor = ConsoleColor.Gray
            Console.ForegroundColor = ConsoleColor.Black
            Console.Write("ESC")
            Console.ResetColor()
            Console.WriteLine(" Return to game")
            Dim MenuCode As ConsoleKeyInfo = Console.ReadKey(True)
            If MenuCode.Key = ConsoleKey.D1 Then
                CreateNewGame(False, Game, GameSettings)
            ElseIf MenuCode.Key = ConsoleKey.D2 Then
                LoadGame(Game, False, GameSettings)
            ElseIf MenuCode.Key = ConsoleKey.D3 Then
                Tutorial(GameSettings)
            ElseIf MenuCode.Key = ConsoleKey.D4 Then
                KeyBindMenu(False, Game, GameSettings)
            ElseIf MenuCode.Key = ConsoleKey.D5 Then
                StartMenu(Nothing)
            ElseIf MenuCode.Key = ConsoleKey.D6 Then
                Stop
            ElseIf MenuCode.Key = ConsoleKey.Escape Then
                Dim pos As Position
                pos.y = 13
                pos.x = 16
                Return
            ElseIf MenuCode.Key = ConsoleKey.F1 Then
                Game.ShowTestMap(Game)
            Else
                Continue While
            End If
        End While
        Return
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
                MainMenu(game)
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
    Sub KeyBindMenu(isStart, game, GameSettings)
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
                MainMenu(game)
            End If
        End If
    End Sub
    Sub LoadGame(ByRef game, isStart, GameSettings)
        Console.Clear()
        Const maxLines As Integer = 46
        Dim PathName As String
        Dim Line As String
        Dim Character As Char
        Dim CharacterNo As Integer
        Dim Cells(32) As String
        Dim Cell As String
        Dim CellNo As Integer = 1
        Dim NewGame As Game = New Game()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("LOAD GAME")
        Console.WriteLine("Input file name, or leave empty to return to the main menu:")
        Console.ResetColor()
        Console.Write(" ")
        Try
            PathName = Console.ReadLine()
            PathName &= ".txt"
            Dim Reader As New StreamReader(PathName)
            While Reader.EndOfStream <> True
                Line = Reader.ReadLine()
                If Line.Substring(32) = 0 Then
                    GameSettings.MapWidth = 32
                Else
                    GameSettings.MapWidth = 45
                End If
                For j As Integer = 0 To 25
                    For i As Integer = 0 To GameSettings.MapWidth
                        While Cell.Substring(",") = -1
                            Character = Cell(CharacterNo)
                            Cell &= Character
                            If Cell = "Grass" Then
                                Dim Grass As Grass = New Grass()
                                NewGame.LotObjectMatrix(j, i) = Grass
                            End If
                        End While
                    Next
                Next
            End While
            If PathName = Nothing Then
                If isStart = True Then
                    StartMenu(GameSettings)
                Else
                    MainMenu(game)
                End If
            End If
        Catch ex As Exception
            Console.BackgroundColor = ConsoleColor.Red
            Console.ForegroundColor = ConsoleColor.Black
            Console.WriteLine("Error, press any key to continue...")
            Console.WriteLine(ex.Message)
            Console.ResetColor()
            Console.ReadLine()
            If isStart = True Then
                StartMenu(GameSettings)
            Else
                MainMenu(game)
            End If
        End Try
    End Sub
    Sub Tutorial(ByRef GameSettings)
        Console.ReadLine()
        GameSettings.IsTutorial = True
        StartMenu(GameSettings)
    End Sub
    Sub CreateNewGame(IsStart, ByRef CurrentGame, GameSettings)
        Dim newGame As Game = New Game()
        newGame.GameSettings.IsTutorialGame = GameSettings.IsTutorialGame
        newGame.GameSettings.MapWidth = GameSettings.MapWidth
        newGame.GameSettings.TextureFile = GameSettings.TextureFile
        Dim newLotObjectMatrix(24, GameSettings.MapWidth) As Lot
        newGame.LotObjectMatrix = newLotObjectMatrix
        newGame.NewGame(IsStart, CurrentGame, GameSettings, newGame)
    End Sub
    Sub SaveGame(game, map)
        Console.Clear()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("ESC")
        Console.ResetColor()
        Console.WriteLine("Return")
        Dim filename As String
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("Enter the name for this save file:")
        Console.ResetColor()
        filename = Console.ReadLine()
        filename = filename & ".json"
        Try
            Dim FileWriter As New StreamWriter(filename)
            FileWriter.WriteLine(map.GridCodes.ToString)
            FileWriter.Close()
        Catch ex As Exception
            MainMenu(game)
        Finally
            MainMenu(game)
        End Try
        Console.Clear()
        game.PrintMap(0, 16)
    End Sub
    Sub Play(Game, Pos)
        Dim input As ConsoleKeyInfo
        Dim InMenu As Boolean = False
        While Not InMenu
            Game.GameMap.PrintMap(Pos, Game)
            input = Console.ReadKey(True)
            Select Case input.Key
                Case ConsoleKey.W
                    Pos.y -= 1
                    Continue While
                Case ConsoleKey.A
                    Pos.x -= 1
                    Continue While
                Case ConsoleKey.S
                    Pos.y += 1
                    Continue While
                Case ConsoleKey.D
                    Pos.x += 1
                    Continue While
                Case ConsoleKey.UpArrow
                    Pos.y -= 5
                    Continue While
                Case ConsoleKey.LeftArrow
                    Pos.x -= 5
                    Continue While
                Case ConsoleKey.DownArrow
                    Pos.y += 5
                    Continue While
                Case ConsoleKey.RightArrow
                    Pos.x += 5
                    Continue While
                Case ConsoleKey.Escape
                    MainMenu(Game)
                Case ConsoleKey.Enter
                    Console.BackgroundColor = ConsoleColor.Gray
                    Console.ForegroundColor = ConsoleColor.Black
                    Console.Write("B")
                    Console.ResetColor()
                    Console.Write(" Build ")
                    Console.BackgroundColor = ConsoleColor.Gray
                    Console.ForegroundColor = ConsoleColor.Black
                    Console.Write("D")
                    Console.ResetColor()
                    Console.Write(" Demolish ")
                    Console.BackgroundColor = ConsoleColor.Gray
                    Console.ForegroundColor = ConsoleColor.Black
                    Console.Write("C")
                    Console.ResetColor()
                    Console.WriteLine(" Cancel ")
                    input = Console.ReadKey(True)
                    If input.Key = ConsoleKey.B Then
                        Dim lot As Lot = New Lot()
                        lot.Build(Pos, Game)
                    ElseIf input.Key = ConsoleKey.D Then
                        Game.LotObjectMatrix(Pos.y, Pos.x).Demolish(Pos, Game)
                    ElseIf input.Key = ConsoleKey.Escape Then
                        MainMenu(Game)
                    Else
                        Continue While
                    End If
                Case ConsoleKey.N
                    Game.FinishWeek(Game)
                Case ConsoleKey.G
                    Game.CityGovernment.ShowGovernmentMenu(Game)
            End Select
        End While
        MainMenu(Game)
    End Sub
End Module