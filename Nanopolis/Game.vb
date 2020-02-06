﻿Public Structure Position
    Public x As Integer
    Public y As Integer
End Structure
Public Structure Texture
    Private _line0 As List(Of String)
    Private _line1 As List(Of String)
    Private _line2 As List(Of String)
    Private _line3 As List(Of String)
    Public Property Line0 As List(Of String)
        Get
            Return _line0
        End Get
        Set(value As List(Of String))
            _line0 = value
        End Set
    End Property
    Public Property Line1 As List(Of String)
        Get
            Return _line1
        End Get
        Set(value As List(Of String))
            _line1 = value
        End Set
    End Property
    Public Property Line2 As List(Of String)
        Get
            Return _line2
        End Get
        Set(value As List(Of String))
            _line2 = value
        End Set
    End Property
    Public Property Line3 As List(Of String)
        Get
            Return _line3
        End Get
        Set(value As List(Of String))
            _line3 = value
        End Set
    End Property
End Structure
Public Class Game
    Public GameSettings As GameSettings
    Const StartingPopulation As Integer = 10
    Public TotalPowerSupply As Integer
    Public TotalPowerDemand As Integer
    Public GameMap As Map
    Public CityGovernment As Government
    Public LotObjectMatrix(24, 32) As Lot
    Public HasWorkBuildings As Boolean = False
    Public HasShoppingPlace As Boolean = False
    Public TestMap As Map
    Public NoOfWeeksPlayed As Integer
    Sub ShowTestMap(Game)
        Dim testMap As Map = New Map()
        For i As Integer = 0 To 32
            For j As Integer = 0 To 24
                testMap.GridCodes(24, 32) = -1
            Next
        Next
        testMap.GridCodes(0, 0) = -1
        testMap.GridCodes(0, 1) = 0
        testMap.GridCodes(0, 2) = 1
        testMap.GridCodes(0, 3) = 2
        testMap.GridCodes(0, 4) = 3
        testMap.GridCodes(0, 5) = 4
        testMap.GridCodes(0, 6) = 5
        testMap.GridCodes(0, 7) = 6
        testMap.GridCodes(0, 8) = 7
        testMap.GridCodes(0, 9) = 8
        testMap.GridCodes(0, 10) = 9
        testMap.GridCodes(0, 11) = 10
        testMap.GridCodes(0, 12) = 11
        testMap.GridCodes(0, 13) = 12
        testMap.GridCodes(0, 14) = 13
        testMap.GridCodes(0, 15) = 14
        testMap.GridCodes(0, 16) = 15
        testMap.GridCodes(0, 17) = 16
        testMap.GridCodes(0, 18) = 17
        testMap.GridCodes(0, 19) = 18
        testMap.GridCodes(0, 20) = 19
        testMap.GridCodes(0, 21) = 20
        testMap.GridCodes(0, 22) = 21
        testMap.GridCodes(0, 23) = 22
        testMap.GridCodes(0, 24) = 23
        testMap.GridCodes(0, 25) = 24
        testMap.GridCodes(0, 26) = 25
        testMap.GridCodes(0, 27) = 26
        testMap.GridCodes(0, 28) = 27
        testMap.GridCodes(0, 29) = 28
        testMap.GridCodes(0, 30) = 29
        testMap.GridCodes(0, 31) = 30
        testMap.GridCodes(0, 32) = 31
        testMap.GridCodes(1, 0) = 32
        testMap.GridCodes(1, 1) = 33
        testMap.GridCodes(1, 2) = 34
        testMap.GridCodes(2, 1) = 35
        testMap.GridCodes(2, 2) = 36
        testMap.GridCodes(1, 5) = 37
        testMap.GridCodes(1, 6) = 38
        testMap.GridCodes(1, 7) = 39
        testMap.GridCodes(1, 8) = 40
        testMap.GridCodes(1, 9) = 41
        testMap.GridCodes(1, 10) = 42
        Game.TestMap = testMap
        Dim pos As Position
        Game.TestMap.PrintMap(pos, Game, True)
    End Sub
    Protected Sub IntroduceStartingPopulation(ByRef Game)
        Dim pos As Position
        For pos.y = 0 To 24
            For pos.x = 0 To (Game.GameSettings.MapWidth - 1)
                If Game.LotObjectMatrix(pos.y, pos.x).GetType.ToString = "Nanopolis.SmallResidential" Or Game.LotObjectMatrix(pos.y, pos.x).GetType.ToString = "Nanopolis.LargeResidential" Then
                    Game.LotObjectMatrix(pos.y, pos.x).DwellerAmount += StartingPopulation
                End If
            Next
        Next
    End Sub
    Public Sub NewGame(IsStart As Boolean, CurrentGame As Game, Map As Map, GameSettings As GameSettings, Game As Game)
        Console.WriteLine("Are you sure? No [ESC] | Yes [ENTER]")
        Dim Input As ConsoleKeyInfo = Console.ReadKey(True)
        If Input.Key = ConsoleKey.Enter Then
            Game.NewMap(Game, IsStart)
        Else
            If IsStart = True Then
                StartMenu(GameSettings)
            Else
                MainMenu(CurrentGame, Map)
            End If
        End If
    End Sub
    Public Function CalculateLandValueFromExternal(Pos, ByRef Game)
        Dim tempModifier As Integer
        Dim noOfParliamentPointers As Integer = 0
        Dim noOfLargeParkPointers As Integer = 0
        For j As Integer = -2 To 2
            For i As Integer = -2 To 2
                If (Pos.y + j) < 0 Or (Pos.y + j) > 24 Or (Pos.x + i) < 0 Or (Pos.x + i) > 32 Or j = 0 Or i = 0 Then
                    Continue For
                End If
                If noOfLargeParkPointers <> 0 And Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.LargeParkPointer" Then
                    noOfLargeParkPointers += 1
                    Continue For
                End If
                If noOfParliamentPointers <> 0 And Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.ParliamentPointer" Then
                    noOfParliamentPointers += 1
                    Continue For
                End If
                If Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.SmallResidential" Or Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.LargeResidential" And Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.Industry" Then
                    tempModifier -= 15
                End If
                If Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.SmallPark" Then
                    tempModifier += 15
                End If
                If Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.LargePark" Then
                    tempModifier += 35
                End If
                If Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.Grass" Then
                    tempModifier += 5
                End If
                If Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.Water" Then
                    tempModifier += 10
                End If
                If Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.Forest" Then
                    tempModifier += 10
                End If
                If Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.CoalStation" Then
                    tempModifier -= 15
                End If
                If Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.SmallCommercial" And Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.Industry" Then
                    tempModifier -= 10
                End If
                If Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.CoalStation" Then
                    tempModifier -= 15
                End If
                If noOfParliamentPointers = 0 And Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.Parliament" Then
                    tempModifier += 40
                End If
                If Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.Industry" And Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.SmallCommercial" Then
                    tempModifier += 5
                End If
                If Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.Industry" And Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.LargeCommercial" Then
                    tempModifier += 10
                End If
            Next
        Next
        Return tempModifier
    End Function
    Sub InitializeTypeDict(ByRef NewGame)
        Dim newTypeDict As New Dictionary(Of String, String)
        newTypeDict.Add("Nanopolis.Grass", "grass")
        newTypeDict.Add("Nanopolis.Construction", "construction")
        newTypeDict.Add("Nanopolis.SmallResidential", "smallResidential")
        newTypeDict.Add("Nanopolis.LargeResidential", "largeResidential")
        newTypeDict.Add("Nanopolis.SmallCommercial", "smallCommercial")
        newTypeDict.Add("Nanopolis.LargeCommercial", "largeCommercial")
        newTypeDict.Add("Nanopolis.SmallPark", "smallPark")
        newTypeDict.Add("Nanopolis.LargePark", "largePark")
        newTypeDict.Add("Nanopolis.LargeParkPointer", "largeParkPointer")
        newTypeDict.Add("Nanopolis.SmallRoad", "smallRoad")
        newTypeDict.Add("Nanopolis.LargeRoad", "largeRoad")
        newTypeDict.Add("Nanopolis.Industry", "industry")
        newTypeDict.Add("Nanopolis.Parliament", "parliament")
        newTypeDict.Add("Nanopolis.ParliamentPointer", "parliamentPointer")
        newTypeDict.Add("Nanopolis.PoliceStation", "policeStation")
        newTypeDict.Add("Nanopolis.Water", "water")
        newTypeDict.Add("Nanopolis.Forest", "forest")
        newTypeDict.Add("Nanopolis.WindFarm", "windFarm")
        newTypeDict.Add("Nanopolis.CoalStation", "coalStation")
        NewGame.TypeDict = newTypeDict
    End Sub
    Sub FinishWeek(ByRef Game)
        Dim pos As Position
        For pos.y = 0 To 24
            For pos.x = 0 To (Game.GameSettings.MapWidth - 1)
                Game.LotObjectMatrix(pos.y, pos.x).LandValue = Game.LotObjectMatrix(pos.y, pos.x).CalculateLandValue(pos, Game)
                If Game.LotObjectMatrix(pos.y, pos.x).GetType.ToString = "Nanopolis.Construction" Then
                    Console.WriteLine("NextTurnLot = " & Game.LotObjectMatrix(pos.y, pos.x).NextTurnLot)
                    Game.LotObjectMatrix(pos.y, pos.x).FinishConstruction(Game, pos)
                End If
                If Game.LotObjectMatrix(pos.y, pos.x).GetType.ToString = "Nanopolis.SmallResidential" Or Game.LotObjectMatrix(pos.y, pos.x).GetType.ToString = "Nanopolis.LargeResidential" Then
                    If Game.HasWorkBuildings = True Then
                        Game.LotObjectMatrix(pos.y, pos.x).GenerateWorkOrShoppingPlace(True, Game.LotObjectMatrix, pos)
                    End If
                End If
                Game.LotObjectMatrix(pos.y, pos.x).CrimeRate = Game.LotObjectMatrix(pos.y, pos.x).CalculateCrimeRate(pos, Game)
                Game.LotObjectMatrix(pos.y, pos.x).SetAbandonedWeeks(Game, pos)
            Next
        Next
        pos.y = 12
        pos.x = 16
        Game.NoOfWeeksPlayed += 1
        Game.GameMap.PrintMap(pos, Game)
    End Sub
    Sub NewMap(ByRef NewGame, IsStart)
        Const baselandvalue As Integer = 25
        Console.Clear()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("NEW GAME")
        Console.ResetColor()
        Randomize()
        Dim pos As Position
        pos.y = 12
        pos.x = 16
        Dim map As Map = New Map()
        Dim newGridCodes(24, NewGame.GameSettings.MapWidth) As Integer
        map.GridCodes = newGridCodes
        NewGame.GameMap = map
        Dim grassProb As Integer
        Dim waterProb As Integer
        Dim forestProb As Integer
        Dim totalProb As Integer
        Console.WriteLine("Create a plain map?[y/n]")
        Dim plainMapChoice As ConsoleKeyInfo = Console.ReadKey(True)
        If plainMapChoice.Key = ConsoleKey.N Then
            Do
                Console.WriteLine(" Set the likelihood of water, grass fields out of a 100, then the game will complete the probability of forest:")
                Console.WriteLine(" Likelihood of water(0-100): ")
                waterProb = Console.ReadLine()
                totalProb += waterProb
                If waterProb < 100 Then
                    Console.WriteLine(" Likelihood of grass fields(0-" & 100 - waterProb & "): ")
                    grassProb = Console.ReadLine()
                    totalProb += grassProb
                End If
                Console.WriteLine()
            Loop While totalProb > 100
            forestProb = 100 - totalProb
            totalProb += forestProb
            For i As Integer = 0 To 24
                For j As Integer = 0 To NewGame.GameSettings.MapWidth
                    Dim GeneratedTile As Single = Rnd()
                    GeneratedTile = GeneratedTile * totalProb
                    If GeneratedTile < waterProb Then
                        Dim water As Water = New Water()
                        NewGame.GameMap.GridCodes(i, j) = 38
                        NewGame.LotObjectMatrix(i, j) = water
                    ElseIf GeneratedTile > waterProb And GeneratedTile <= (grassProb + waterProb) Then
                        Dim grass As Grass = New Grass()
                        NewGame.GameMap.GridCodes(i, j) = -1
                        NewGame.LotObjectMatrix(i, j) = grass
                    ElseIf GeneratedTile > (grassProb + waterProb) Then
                        Dim forest As Forest = New Forest()
                        NewGame.GameMap.GridCodes(i, j) = 39
                        NewGame.LotObjectMatrix(i, j) = forest
                    End If
                    NewGame.LotObjectMatrix(i, j).LandValue = baselandvalue
                Next
            Next
            Dim cityGovernment As Government = New Government()
            cityGovernment.EstablishGovernment()
            NewGame.CityGovernment = cityGovernment
            NewGame.GameMap = map
            NewGame.GameMap.PrintMap(pos, NewGame)
        ElseIf plainMapChoice.Key = ConsoleKey.Y Then
            For i As Integer = 0 To 24
                For j As Integer = 0 To NewGame.GameSettings.MapWidth
                    NewGame.GameMap.GridCodes(i, j) = -1
                    Dim grass As Grass = New Grass()
                    NewGame.LotObjectMatrix(i, j) = grass
                Next
            Next
            Dim cityGovernment As Government = New Government()
            cityGovernment.EstablishGovernment()
            NewGame.CityGovernment = cityGovernment
            NewGame.GameMap.PrintMap(pos, NewGame)
        ElseIf plainMapChoice.Key = ConsoleKey.Escape Then
            StartMenu(GameSettings)
        Else
            If IsStart = False Then
                NewGame.NewGame(False, NewGame, map, GameSettings, Nothing)
            Else
                NewGame.NewGame(True, Nothing, Nothing, GameSettings, Nothing)
            End If
        End If
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
Public Class Map
    Public Textures(42) As Texture
    Public GridCodes(24, 32) As Integer
    Sub LoadTextures()
        Dim grassTexture As Texture
        Dim constructionTexture As Texture
        Dim smallResidentialTexture As Texture
        Dim largeResidentialTexture As Texture
        Dim smallCommercialTexture1 As Texture
        Dim smallCommercialTexture2 As Texture
        Dim largeCommercialTexture As Texture
        Dim smallParkTexture As Texture
        Dim largeParkTexture1 As Texture
        Dim largeParkTexture2 As Texture
        Dim largeParkTexture3 As Texture
        Dim largeParkTexture4 As Texture
        Dim smallRoadHorizontalTexture As Texture
        Dim smallRoadVerticalTexture As Texture
        Dim smallRoad4WayTexture As Texture
        Dim smallRoadUpRightLeftTexture As Texture
        Dim smallRoadRightDownLeftTexture As Texture
        Dim smallRoadUpDownLeftTexture As Texture
        Dim smallRoadUpRightDownTexture As Texture
        Dim smallRoadUpLeftTexture As Texture
        Dim smallRoadUpRightTexture As Texture
        Dim smallRoadDownLeftTexture As Texture
        Dim smallRoadRightDownTexture As Texture
        Dim largeRoadHorizontalTexture As Texture
        Dim largeRoadVerticalTexture As Texture
        Dim largeRoadRightDownLeftTexture As Texture
        Dim largeRoadUpRightLeftTexture As Texture
        Dim largeRoadUpLeftTexture As Texture
        Dim largeRoadUpRightTexture As Texture
        Dim largeRoadDownLeftTexture As Texture
        Dim largeRoadDownRightTexture As Texture
        Dim industryTexture As Texture
        Dim parliamentTexture1 As Texture
        Dim parliamentTexture2 As Texture
        Dim parliamentTexture3 As Texture
        Dim parliamentTexture4 As Texture
        Dim policeTexture As Texture
        Dim waterTexture As Texture
        Dim forestTexture As Texture
        Dim windFarmTexture As Texture
        Dim coalStationTexture As Texture
        Dim largeRoadUpDownLeftTexture As Texture
    End Sub
    Public Sub PrintMap(ByRef Pos, ByRef Game, Optional IsTestMap = False)
        Console.Clear()

        If IsTestMap = False Then
            For y = 0 To 24
                For CurrentLine As Integer = 0 To 3
                    For x = 0 To Game.GameSettings.MapWidth
                        If Pos.y = y And Pos.x = x Then
                            If CurrentLine = 0 Then
                                Console.Write("\   /")
                            ElseIf CurrentLine = 1 Then
                                Console.Write(" \ / ")
                            ElseIf CurrentLine = 2 Then
                                Console.Write(" / \ ")
                            Else
                                Console.Write("/   \")
                            End If
                        Else
                            If CurrentLine = 0 Then
                                Select Case Game.GameMap.GridCodes(y, x)
                                    Case -1
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.DarkGreen
                                        Console.Write(". . .")
                                        Console.ResetColor()
                                    Case 0
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("___|")
                                        Console.ForegroundColor = ConsoleColor.Gray
                                        Console.Write("\")
                                        Console.ResetColor()
                                    Case 1
                                        Console.BackgroundColor = ConsoleColor.Red
                                        Console.ForegroundColor = ConsoleColor.Black
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
                                        Console.Write("|MA|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("_")
                                        Console.ResetColor()
                                    Case 6
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.DarkYellow
                                        Console.Write("_____")
                                        Console.ResetColor()
                                    Case 7
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.DarkYellow
                                        Console.Write(" ____")
                                        Console.ResetColor()
                                    Case 8
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.DarkYellow
                                        Console.Write("____ ")
                                        Console.ResetColor()
                                    Case 9
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.DarkYellow
                                        Console.Write(" ___ ")
                                        Console.ResetColor()
                                    Case 10
                                        Console.BackgroundColor = ConsoleColor.DarkGray
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
                                        Console.Write("/\")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("__")
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
                                        Console.Write("|\")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("__")
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
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("/ : \")
                                        Console.ResetColor()
                                    Case 26
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("/ :|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                    Case 27
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|: \")
                                        Console.ResetColor()
                                    Case 28
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("/ :|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 29
                                        Console.ForegroundColor = ConsoleColor.White
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
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("    _")
                                        Console.ResetColor()
                                    Case 34
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("_    ")
                                        Console.ResetColor()
                                    Case 35
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.DarkCyan
                                        Console.Write("[][")
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 36
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkCyan
                                        Console.Write("][]")
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 37
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("_____")
                                        Console.ResetColor()
                                    Case 38
                                        Console.BackgroundColor = ConsoleColor.Blue
                                        Console.Write("~~~~~")
                                        Console.BackgroundColor = ConsoleColor.Black
                                    Case 39
                                        Console.ForegroundColor = ConsoleColor.Green
                                        Console.BackgroundColor = ConsoleColor.DarkGreen
                                        Console.Write(":::::")
                                        Console.ResetColor()
                                    Case 40
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("x   x")
                                        Console.ResetColor()
                                    Case 41
                                        Console.BackgroundColor = ConsoleColor.Gray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("OOo  ")
                                        Console.ResetColor()
                                    Case 42
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("/ :|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                End Select
                            ElseIf CurrentLine = 1 Then
                                Select Case Game.GameMap.GridCodes(y, x)
                                    Case -1
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.DarkGreen
                                        Console.Write(" . . ")
                                        Console.ResetColor()
                                    Case 0
                                        Console.BackgroundColor = ConsoleColor.Green
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
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.DarkYellow
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.DarkGreen
                                        Console.Write(":::")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.DarkYellow
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 7
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.DarkRed
                                        Console.Write("h  ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 8
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.DarkGreen
                                        Console.Write(".")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(":::")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Gray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 9
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.DarkGreen
                                        Console.Write(":::")
                                        Console.ForegroundColor = ConsoleColor.Gray
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 10
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Yellow
                                        Console.ForegroundColor = ConsoleColor.Red
                                        Console.Write(" _ ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Gray
                                        Console.Write("|")
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
                                        Console.Write("_____")
                                        Console.ResetColor()
                                    Case 15
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("_   _")
                                        Console.ResetColor()
                                    Case 16
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("_ |")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("  ")
                                        Console.ResetColor()
                                    Case 17
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("| __")
                                        Console.ResetColor()
                                    Case 18
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("__/")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("  ")
                                        Console.ResetColor()
                                    Case 19
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("\___")
                                        Console.ResetColor()
                                    Case 20
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("_ \")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("  ")
                                        Console.ResetColor()
                                    Case 21
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.Black
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("/  _")
                                        Console.ResetColor()
                                    Case 22
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("-----")
                                        Console.ResetColor()
                                    Case 23
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
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("-   -")
                                        Console.ResetColor()
                                    Case 25
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("-   -")
                                        Console.ResetColor()
                                    Case 26
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("-   -")
                                        Console.ResetColor()
                                    Case 27
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("-   -")
                                        Console.ResetColor()
                                    Case 28
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("-- |")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 29
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("| --")
                                        Console.ResetColor()
                                    Case 30
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("-- \")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 31
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("/  -")
                                        Console.ResetColor()
                                    Case 32
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("__")
                                        Console.BackgroundColor = ConsoleColor.DarkRed
                                        Console.Write("||")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("_")
                                        Console.ResetColor()
                                    Case 33
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("___")
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write("/ ")
                                        Console.ResetColor()
                                    Case 34
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write(" \")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("___")
                                        Console.ResetColor()
                                    Case 35
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.DarkCyan
                                        Console.Write("[][")
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 36
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkCyan
                                        Console.Write("][]")
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 37
                                        Console.BackgroundColor = ConsoleColor.Gray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write("POL")
                                        Console.BackgroundColor = ConsoleColor.Gray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 38
                                        Console.BackgroundColor = ConsoleColor.Blue
                                        Console.Write("~~~~~")
                                        Console.ResetColor()
                                    Case 39
                                        Console.ForegroundColor = ConsoleColor.DarkRed
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("| | |")
                                        Console.ResetColor()
                                    Case 40
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("| x |")
                                        Console.ResetColor()
                                    Case 41
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.Gray
                                        Console.Write("||")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("  ")
                                        Console.ResetColor()
                                    Case 42
                                        Console.ForegroundColor = ConsoleColor.DarkGray
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write("-  |")
                                        Console.ForegroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                End Select
                            ElseIf CurrentLine = 2 Then
                                Select Case Game.GameMap.GridCodes(y, x)
                                    Case -1
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.DarkGreen
                                        Console.Write(". . .")
                                        Console.ResetColor()
                                    Case 0
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Yellow
                                        Console.Write("   N ")
                                        Console.ResetColor()
                                    Case 1
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("  ")
                                        Console.BackgroundColor = ConsoleColor.Red
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
                                        Console.BackgroundColor = ConsoleColor.Gray
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
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.DarkYellow
                                        Console.Write("|h| |")
                                        Console.ResetColor()
                                    Case 7
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.DarkRed
                                        Console.Write("|hTT")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Gray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 8
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.DarkRed
                                        Console.Write("|h|")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 9
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.DarkRed
                                        Console.Write("|||")
                                        Console.ForegroundColor = ConsoleColor.Gray
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 10
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Yellow
                                        Console.ForegroundColor = ConsoleColor.Red
                                        Console.Write("/U\")
                                        Console.ForegroundColor = ConsoleColor.Gray
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 11
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
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
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("\ /")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 14
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 15
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("\ /")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 16
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("\|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("  ")
                                        Console.ResetColor()
                                    Case 17
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|/")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("  ")
                                        Console.ResetColor()
                                    Case 18
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 19
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 20
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("\|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("  ")
                                        Console.ResetColor()
                                    Case 21
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("| /")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 22
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("_____")
                                        Console.ResetColor()
                                    Case 23
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|:|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 24
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 25
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 26
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("_____")
                                        Console.ResetColor()
                                    Case 27
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 28
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("___/")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 29
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("\___")
                                        Console.ResetColor()
                                    Case 30
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("  :|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 31
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("|:  ")
                                        Console.ResetColor()
                                    Case 32
                                        Console.BackgroundColor = ConsoleColor.DarkRed
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("|   |")
                                        Console.ResetColor()
                                    Case 33
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.DarkCyan
                                        Console.Write("[][")
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 34
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkCyan
                                        Console.Write("][]")
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 35
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("    ")
                                        Console.BackgroundColor = ConsoleColor.Yellow
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 36
                                        Console.BackgroundColor = ConsoleColor.Yellow
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("    ")
                                        Console.ResetColor()
                                    Case 37
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.BackgroundColor = ConsoleColor.Gray
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write("ICE")
                                        Console.BackgroundColor = ConsoleColor.Gray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 38
                                        Console.BackgroundColor = ConsoleColor.Blue
                                        Console.Write("~~~~~")
                                        Console.ResetColor()
                                    Case 39
                                        Console.BackgroundColor = ConsoleColor.DarkGreen
                                        Console.ForegroundColor = ConsoleColor.Green
                                        Console.Write(":::::")
                                        Console.ResetColor()
                                    Case 40
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("X | X")
                                        Console.ResetColor()
                                    Case 41
                                        Console.BackgroundColor = ConsoleColor.Gray
                                        Console.Write("_||__")
                                        Console.ResetColor()
                                    Case 42
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("   |")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                End Select
                            ElseIf CurrentLine = 3 Then
                                Select Case Game.GameMap.GridCodes(y, x)
                                    Case -1
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.DarkGreen
                                        Console.Write(" . . ")
                                        Console.ResetColor()
                                    Case 0
                                        Console.BackgroundColor = ConsoleColor.DarkGray
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
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.Write("  ")
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
                                        Console.ForegroundColor = ConsoleColor.DarkYellow
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("___")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 7
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("_")
                                        Console.ForegroundColor = ConsoleColor.DarkRed
                                        Console.Write("h")
                                        Console.ForegroundColor = ConsoleColor.DarkGray
                                        Console.Write("_")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 8
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("___")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 9
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("___")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 10
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("___")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 11
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
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
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("|| ")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 14
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 15
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("||")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("  ")
                                        Console.ResetColor()
                                    Case 16
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("||")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("  ")
                                        Console.ResetColor()
                                    Case 17
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("|| ")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 18
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 19
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 20
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("|| ")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 21
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("||")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("  ")
                                        Console.ResetColor()
                                    Case 22
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 23
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
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("\ : /")
                                        Console.ResetColor()
                                    Case 25
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("\ : /")
                                        Console.ResetColor()
                                    Case 26
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 27
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("\ : /")
                                        Console.ResetColor()
                                    Case 28
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 29
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 30
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("\ :|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 31
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("|: /")
                                        Console.ResetColor()
                                    Case 32
                                        Console.BackgroundColor = ConsoleColor.DarkRed
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("|___|")
                                        Console.ResetColor()
                                    Case 33
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write("|")
                                        Console.BackgroundColor = ConsoleColor.DarkCyan
                                        Console.Write("[][")
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write(" ")
                                        Console.ResetColor()
                                    Case 34
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkCyan
                                        Console.Write("][]")
                                        Console.BackgroundColor = ConsoleColor.White
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 35
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("   ")
                                        Console.BackgroundColor = ConsoleColor.Yellow
                                        Console.Write("/ ")
                                        Console.ResetColor()
                                    Case 36
                                        Console.BackgroundColor = ConsoleColor.Yellow
                                        Console.Write(" \")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("   ")
                                        Console.ResetColor()
                                    Case 37
                                        Console.BackgroundColor = ConsoleColor.Gray
                                        Console.ForegroundColor = ConsoleColor.Black
                                        Console.Write("|_")
                                        Console.BackgroundColor = ConsoleColor.DarkCyan
                                        Console.Write("[]")
                                        Console.BackgroundColor = ConsoleColor.Gray
                                        Console.Write("|")
                                        Console.ResetColor()
                                    Case 38
                                        Console.BackgroundColor = ConsoleColor.Blue
                                        Console.Write("~~~~~")
                                        Console.ResetColor()
                                    Case 39
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.DarkRed
                                        Console.Write("| | |")
                                        Console.ResetColor()
                                    Case 40
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("|   |")
                                        Console.ResetColor()
                                    Case 41
                                        Console.BackgroundColor = ConsoleColor.Gray
                                        Console.Write("|___|")
                                        Console.ResetColor()
                                    Case 42
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("\ :|")
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.ResetColor()
                                End Select
                            End If
                        End If
                    Next
                    Console.WriteLine()
                Next
            Next
        Else
            For y = 0 To 24
                For CurrentLine As Integer = 0 To 3
                    For x = 0 To 32
                        If CurrentLine = 0 Then
                            Select Case Game.TestMap.GridCodes(y, x)
                                Case -1
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.DarkGreen
                                    Console.Write(". . .")
                                    Console.ResetColor()
                                Case 0
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("___|")
                                    Console.ForegroundColor = ConsoleColor.Gray
                                    Console.Write("\")
                                    Console.ResetColor()
                                Case 1
                                    Console.BackgroundColor = ConsoleColor.Red
                                    Console.ForegroundColor = ConsoleColor.Black
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
                                    Console.Write("|MA|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("_")
                                    Console.ResetColor()
                                Case 6
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.DarkYellow
                                    Console.Write("_____")
                                    Console.ResetColor()
                                Case 7
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.DarkYellow
                                    Console.Write(" ____")
                                    Console.ResetColor()
                                Case 8
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.DarkYellow
                                    Console.Write("____ ")
                                    Console.ResetColor()
                                Case 9
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.DarkYellow
                                    Console.Write(" ___ ")
                                    Console.ResetColor()
                                Case 10
                                    Console.BackgroundColor = ConsoleColor.DarkGray
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
                                    Console.Write("/\")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("__")
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
                                    Console.Write("|\")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("__")
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
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("/ : \")
                                    Console.ResetColor()
                                Case 26
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("/ :|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                Case 27
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|: \")
                                    Console.ResetColor()
                                Case 28
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("/ :|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 29
                                    Console.ForegroundColor = ConsoleColor.White
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
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("    _")
                                    Console.ResetColor()
                                Case 34
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("_    ")
                                    Console.ResetColor()
                                Case 35
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.DarkCyan
                                    Console.Write("[][")
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 36
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkCyan
                                    Console.Write("][]")
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 37
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("_____")
                                    Console.ResetColor()
                                Case 38
                                    Console.BackgroundColor = ConsoleColor.Blue
                                    Console.Write("~~~~~")
                                    Console.BackgroundColor = ConsoleColor.Black
                                Case 39
                                    Console.ForegroundColor = ConsoleColor.Green
                                    Console.BackgroundColor = ConsoleColor.DarkGreen
                                    Console.Write(":::::")
                                    Console.ResetColor()
                                Case 40
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("x   x")
                                    Console.ResetColor()
                                Case 41
                                    Console.BackgroundColor = ConsoleColor.Gray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("OOo  ")
                                    Console.ResetColor()
                                Case 42
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("/ :|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                            End Select
                        ElseIf CurrentLine = 1 Then
                            Select Case Game.TestMap.GridCodes(y, x)
                                Case -1
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.DarkGreen
                                    Console.Write(" . . ")
                                    Console.ResetColor()
                                Case 0
                                    Console.BackgroundColor = ConsoleColor.Green
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
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.DarkYellow
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.DarkGreen
                                    Console.Write(":::")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.DarkYellow
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 7
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.DarkRed
                                    Console.Write("h  ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 8
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.DarkGreen
                                    Console.Write(".")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(":::")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Gray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 9
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.DarkGreen
                                    Console.Write(":::")
                                    Console.ForegroundColor = ConsoleColor.Gray
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 10
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Yellow
                                    Console.ForegroundColor = ConsoleColor.Red
                                    Console.Write(" _ ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Gray
                                    Console.Write("|")
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
                                    Console.Write("_____")
                                    Console.ResetColor()
                                Case 15
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("_   _")
                                    Console.ResetColor()
                                Case 16
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("_ |")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("  ")
                                    Console.ResetColor()
                                Case 17
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("| __")
                                    Console.ResetColor()
                                Case 18
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("__/")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("  ")
                                    Console.ResetColor()
                                Case 19
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("\___")
                                    Console.ResetColor()
                                Case 20
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("_ \")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("  ")
                                    Console.ResetColor()
                                Case 21
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.Black
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("/  _")
                                    Console.ResetColor()
                                Case 22
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("-----")
                                    Console.ResetColor()
                                Case 23
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
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("-   -")
                                    Console.ResetColor()
                                Case 25
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("-   -")
                                    Console.ResetColor()
                                Case 26
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("-   -")
                                    Console.ResetColor()
                                Case 27
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("-   -")
                                    Console.ResetColor()
                                Case 28
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("-- |")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 29
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("| --")
                                    Console.ResetColor()
                                Case 30
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("-- \")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 31
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("/  -")
                                    Console.ResetColor()
                                Case 32
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("__")
                                    Console.BackgroundColor = ConsoleColor.DarkRed
                                    Console.Write("||")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("_")
                                    Console.ResetColor()
                                Case 33
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("___")
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write("/ ")
                                    Console.ResetColor()
                                Case 34
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write(" \")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("___")
                                    Console.ResetColor()
                                Case 35
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.DarkCyan
                                    Console.Write("[][")
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 36
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkCyan
                                    Console.Write("][]")
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 37
                                    Console.BackgroundColor = ConsoleColor.Gray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write("POL")
                                    Console.BackgroundColor = ConsoleColor.Gray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 38
                                    Console.BackgroundColor = ConsoleColor.Blue
                                    Console.Write("~~~~~")
                                    Console.ResetColor()
                                Case 39
                                    Console.ForegroundColor = ConsoleColor.DarkRed
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("| | |")
                                    Console.ResetColor()
                                Case 40
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("| x |")
                                    Console.ResetColor()
                                Case 41
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.Gray
                                    Console.Write("||")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("  ")
                                    Console.ResetColor()
                                Case 42
                                    Console.ForegroundColor = ConsoleColor.DarkGray
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write("-  |")
                                    Console.ForegroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                            End Select
                        ElseIf CurrentLine = 2 Then
                            Select Case Game.TestMap.GridCodes(y, x)
                                Case -1
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.DarkGreen
                                    Console.Write(". . .")
                                    Console.ResetColor()
                                Case 0
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Yellow
                                    Console.Write("   N ")
                                    Console.ResetColor()
                                Case 1
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("  ")
                                    Console.BackgroundColor = ConsoleColor.Red
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
                                    Console.BackgroundColor = ConsoleColor.Gray
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
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.DarkYellow
                                    Console.Write("|h| |")
                                    Console.ResetColor()
                                Case 7
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.DarkRed
                                    Console.Write("|hTT")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Gray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 8
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.DarkRed
                                    Console.Write("|h|")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 9
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.DarkRed
                                    Console.Write("|||")
                                    Console.ForegroundColor = ConsoleColor.Gray
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 10
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Yellow
                                    Console.ForegroundColor = ConsoleColor.Red
                                    Console.Write("/U\")
                                    Console.ForegroundColor = ConsoleColor.Gray
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 11
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
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
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("\ /")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 14
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 15
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("\ /")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 16
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("\|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("  ")
                                    Console.ResetColor()
                                Case 17
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|/")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("  ")
                                    Console.ResetColor()
                                Case 18
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 19
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 20
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("\|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("  ")
                                    Console.ResetColor()
                                Case 21
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("| /")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 22
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("_____")
                                    Console.ResetColor()
                                Case 23
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|:|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 24
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 25
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 26
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("_____")
                                    Console.ResetColor()
                                Case 27
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 28
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("___/")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 29
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("\___")
                                    Console.ResetColor()
                                Case 30
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("  :|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 31
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("|:  ")
                                    Console.ResetColor()
                                Case 32
                                    Console.BackgroundColor = ConsoleColor.DarkRed
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("|   |")
                                    Console.ResetColor()
                                Case 33
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.DarkCyan
                                    Console.Write("[][")
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 34
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkCyan
                                    Console.Write("][]")
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 35
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("    ")
                                    Console.BackgroundColor = ConsoleColor.Yellow
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 36
                                    Console.BackgroundColor = ConsoleColor.Yellow
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("    ")
                                    Console.ResetColor()
                                Case 37
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.BackgroundColor = ConsoleColor.Gray
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write("ICE")
                                    Console.BackgroundColor = ConsoleColor.Gray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 38
                                    Console.BackgroundColor = ConsoleColor.Blue
                                    Console.Write("~~~~~")
                                    Console.ResetColor()
                                Case 39
                                    Console.BackgroundColor = ConsoleColor.DarkGreen
                                    Console.ForegroundColor = ConsoleColor.Green
                                    Console.Write(":::::")
                                    Console.ResetColor()
                                Case 40
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("X | X")
                                    Console.ResetColor()
                                Case 41
                                    Console.BackgroundColor = ConsoleColor.Gray
                                    Console.Write("_||__")
                                    Console.ResetColor()
                                Case 42
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("   |")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                            End Select
                        ElseIf CurrentLine = 3 Then
                            Select Case Game.TestMap.GridCodes(y, x)
                                Case -1
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.DarkGreen
                                    Console.Write(" . . ")
                                    Console.ResetColor()
                                Case 0
                                    Console.BackgroundColor = ConsoleColor.DarkGray
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
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.Write("  ")
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
                                    Console.ForegroundColor = ConsoleColor.DarkYellow
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("___")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 7
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("_")
                                    Console.ForegroundColor = ConsoleColor.DarkRed
                                    Console.Write("h")
                                    Console.ForegroundColor = ConsoleColor.DarkGray
                                    Console.Write("_")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 8
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("___")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 9
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("___")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 10
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("___")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 11
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
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
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("|| ")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 14
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 15
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("||")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("  ")
                                    Console.ResetColor()
                                Case 16
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("||")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("  ")
                                    Console.ResetColor()
                                Case 17
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("|| ")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 18
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 19
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 20
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("|| ")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 21
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("||")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("  ")
                                    Console.ResetColor()
                                Case 22
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 23
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
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("\ : /")
                                    Console.ResetColor()
                                Case 25
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("\ : /")
                                    Console.ResetColor()
                                Case 26
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 27
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("\ : /")
                                    Console.ResetColor()
                                Case 28
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 29
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 30
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("\ :|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 31
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("|: /")
                                    Console.ResetColor()
                                Case 32
                                    Console.BackgroundColor = ConsoleColor.DarkRed
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("|___|")
                                    Console.ResetColor()
                                Case 33
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write("|")
                                    Console.BackgroundColor = ConsoleColor.DarkCyan
                                    Console.Write("[][")
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write(" ")
                                    Console.ResetColor()
                                Case 34
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkCyan
                                    Console.Write("][]")
                                    Console.BackgroundColor = ConsoleColor.White
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 35
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("   ")
                                    Console.BackgroundColor = ConsoleColor.Yellow
                                    Console.Write("/ ")
                                    Console.ResetColor()
                                Case 36
                                    Console.BackgroundColor = ConsoleColor.Yellow
                                    Console.Write(" \")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("   ")
                                    Console.ResetColor()
                                Case 37
                                    Console.BackgroundColor = ConsoleColor.Gray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("|_")
                                    Console.BackgroundColor = ConsoleColor.DarkCyan
                                    Console.Write("[]")
                                    Console.BackgroundColor = ConsoleColor.Gray
                                    Console.Write("|")
                                    Console.ResetColor()
                                Case 38
                                    Console.BackgroundColor = ConsoleColor.Blue
                                    Console.Write("~~~~~")
                                    Console.ResetColor()
                                Case 39
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.DarkRed
                                    Console.Write("| | |")
                                    Console.ResetColor()
                                Case 40
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("|   |")
                                    Console.ResetColor()
                                Case 41
                                    Console.BackgroundColor = ConsoleColor.Gray
                                    Console.Write("|___|")
                                    Console.ResetColor()
                                Case 42
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("\ :|")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.ResetColor()
                            End Select
                        End If
                    Next
                    Console.WriteLine()
                Next
            Next
        End If
        Console.Write("Y" & Int(Pos.y))
        Console.WriteLine(", X" & Int(Pos.x))
        Console.WriteLine("Land Value: " & Game.LotObjectMatrix(Pos.y, Pos.x).LandValue)
        Console.WriteLine("Government Budget: " & Game.cityGovernment.GetTreasury)
        Dim buildingType As Type = Game.LotObjectMatrix(Pos.y, Pos.x).GetType
        Console.WriteLine(buildingType)
        Console.WriteLine("Crime Rate: " & Game.LotObjectMatrix(Pos.y, Pos.x).CrimeRate & "/1000")
        Game.GameMap.MapSelection(Pos, Game)
    End Sub
    Public Sub MapSelection(ByRef Pos, ByRef Game)
        Console.WriteLine("Week " & Game.NoOfWeeksPlayed)
        Console.TreatControlCAsInput = True
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("[WASD/Arrow Keys] Navigate | [RETURN] Select | [N] Finish week | [G] Manage government | [ESC] Main menu")
        Console.ResetColor()
        Dim lot As Lot = New Lot()
        Dim Selected As Boolean = False
        Dim Key1 As ConsoleKey
        Dim Choice As ConsoleKey
        While Selected = False
            Key1 = Console.ReadKey(True).Key
            If Key1 = ConsoleKey.LeftArrow Then
                Pos.x -= 5
                Game.GameMap.PrintMap(Pos, Game)
            ElseIf Key1 = ConsoleKey.RightArrow Then
                Pos.x += 5
                Game.GameMap.PrintMap(Pos, Game)
            ElseIf Key1 = ConsoleKey.DownArrow Then
                Pos.y += 5
                Game.GameMap.PrintMap(Pos, Game)
            ElseIf Key1 = ConsoleKey.UpArrow Then
                Pos.y -= 5
                Game.GameMap.PrintMap(Pos, Game)
            ElseIf Key1 = ConsoleKey.A Then
                Pos.x -= 1
                Game.GameMap.PrintMap(Pos, Game)
            ElseIf Key1 = ConsoleKey.D Then
                Pos.x += 1
                Game.GameMap.PrintMap(Pos, Game)
            ElseIf Key1 = ConsoleKey.S Then
                Pos.y += 1
                Game.GameMap.PrintMap(Pos, Game)
            ElseIf Key1 = ConsoleKey.W Then
                Pos.y -= 1
                Game.GameMap.PrintMap(Pos, Game)
            ElseIf Key1 = ConsoleKey.Enter Then
                Selected = True
            ElseIf Key1 = ConsoleKey.N Then
                Game.FinishWeek(Game)
            ElseIf Key1 = ConsoleKey.Escape Then
                MainMenu(Game, Game.GameMap)
            ElseIf Key1 = ConsoleKey.G Then
                Game.CityGovernment.ShowGovernmentMenu(Game)
            End If
        End While
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("Demolish[D] | Build[B] | Cancel[C]")
        Console.ResetColor()
        Choice = Console.ReadKey(True).Key
        If Choice = ConsoleKey.D Then
            lot.Demolish(Pos, Game)
        ElseIf Choice = ConsoleKey.B Then
            lot.Build(Pos, Game)
        ElseIf Choice = ConsoleKey.C Then
            Game.GameMap.MapSelection(Pos, Game)
        ElseIf Choice = ConsoleKey.Escape Then
            MainMenu(Game, Game.GameMap)
        End If
    End Sub
End Class
Public Class Government
    Const StartingTreasury As Integer = 20000
    Const StartingExecPower As Integer = 100
    Public Treasury As Integer
    Public ExecutivePower As Integer
    Public HasParliament As Boolean
    Public ApprovalRate As Integer
    Public LegislativeControl As Integer
    Public SalesTaxRate As Integer
    Public LowerIncomeTax As Integer
    Public MiddleIncomeTax As Integer
    Public UpperIncomeTax As Integer
    Public EnvironmentStatus As Integer
    Public CivilLibertyIndex As Integer
    Public CityUnemploymentRate As Integer
    Public CityPollutionIndex As Integer
    Public CityLiteracyRate As Integer
    Public CityEconomicInequality As Integer
    Public EconomicMobility As Integer
    Sub EstablishGovernment()
        Treasury = StartingTreasury
        ExecutivePower = StartingExecPower
    End Sub
    Public Function GetTreasury()
        Return Treasury
    End Function
    Public Sub Spend(amount)
        Treasury -= amount
    End Sub
    Public Sub Earn(amount)
        Treasury += amount
    End Sub
    Sub EstablishParliament()
        HasParliament = True
    End Sub
    Sub RemoveParliament()
        HasParliament = False
    End Sub
    Public Sub ShowGovernmentMenu(ByRef Game)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("--THE GOVERNMENT--" & vbCrLf)
        Console.ResetColor()
        Console.WriteLine(" [1] The Treasury | [2] Your Cabinet | [3] The Legislature | [C] Return to navigation")
        Dim key1 As ConsoleKey = Console.ReadKey(True).Key
        Select Case key1
            Case ConsoleKey.D1
                Game.CityGovernment.ShowPolicyMenu(True, Game)
            Case ConsoleKey.D2
                Game.CityGovernment.ShowPolicyMenu(False, Game)
            Case ConsoleKey.D3
                Game.CityGovernment.ShowLegislatureMenu(Game)
            Case ConsoleKey.C
                Dim Pos As Position
                Pos.y = 16
                Pos.x = Game.GameSettings.MapWidth / 2
                Game.GameMap.MapSelection(Pos, Game)
        End Select
    End Sub
    Sub ShowLegislatureMenu(ByRef Game)
        Console.WriteLine(" ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("--THE LEGISLATURE--")
        Console.ResetColor()
        Console.WriteLine(" [1] Propose a bill for the legislature to vote on | [2] Dissolve parliament and trigger a General Election | [C] Return to gov't menu")
        Dim key As ConsoleKey = Console.ReadKey(True).Key
        If key = ConsoleKey.D1 Then
            Game.CityGovernment.ShowPolicyMenu(False, Game)
        End If
    End Sub
    Sub ShowTreasuryMenu(ByRef Game)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("  --THE TREASURY--")
        Console.ResetColor()
        Console.WriteLine("  [1] Adjust tax rates | [2] Adjust interest rate | [3] Issue bonds | [4] View balance sheet | [C] Return to gov't menu")
        Console.ReadLine()
    End Sub
    Sub ShowPolicyMenu(IsExecutive As Boolean, ByRef Game As Game)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("  Change government policy:")
        Console.WriteLine("  Executive power: " & Game.CityGovernment.ExecutivePower)
        Console.WriteLine("  Legislative control:" & Game.CityGovernment.LegislativeControl)
        Console.ResetColor()
    End Sub
    Sub ShowGovtAlert()

    End Sub
End Class