Public Structure Position
    Public x As Integer
    Public y As Integer
End Structure
Public Class Game
    Public Shared GameSettings As GameSettings
    Public HasResidential As Boolean = False
    Const StartingPopulation As Integer = 10
    Public TotalPowerSupply As Integer
    Public TotalPopulation As Integer = 0
    Public TotalPowerDemand As Integer = 0
    Public GameMap As Map
    Public CityGovernment As Government
    Public LotObjectMatrix(24, 32) As Lot
    Public HasWorkBuildings As Boolean = False
    Public HasShoppingPlace As Boolean = False
    Public HasUpperWorkPlace As Boolean = False
    Public HasUpperShoppingPlace As Boolean = False
    Public TestMap As Map
    Public NoOfResidentialLots As Integer = 0
    Public NoOfCommercialLots As Integer = 0
    Public NoOfWeeksPlayed As Integer = 1
    Sub ShowTestMap(Game)
        Dim testMap As Map = New Map()
        For i As Integer = 0 To 32
            For j As Integer = 0 To 24
                testMap.GridCodes(24, 32) = -1
            Next
        Next
        testMap.GridCodes(0, 0) = -1 'grass
        testMap.GridCodes(0, 1) = 0 'construction
        testMap.GridCodes(0, 2) = 1 'small residential
        testMap.GridCodes(0, 3) = 2 'large residential
        testMap.GridCodes(0, 4) = 3 'small commercial 1
        testMap.GridCodes(0, 5) = 4 'small commercial 2
        testMap.GridCodes(0, 6) = 5 'large commercial
        testMap.GridCodes(0, 7) = 6 'small park
        testMap.GridCodes(0, 8) = 7 'large park upper left
        testMap.GridCodes(0, 9) = 8 'large park upper right
        testMap.GridCodes(0, 10) = 9 'large park lower left
        testMap.GridCodes(0, 11) = 10 'large park lower right
        testMap.GridCodes(0, 12) = 11 'small road horizontal
        testMap.GridCodes(0, 13) = 12 'small road vertical
        testMap.GridCodes(0, 14) = 13 'small road 4way
        testMap.GridCodes(0, 15) = 14 'small road up left right
        testMap.GridCodes(0, 16) = 15 'small road down left right
        testMap.GridCodes(0, 17) = 16 'small road down up left
        testMap.GridCodes(0, 18) = 17 'small road down up right
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
    Public Sub NewGame(IsStart As Boolean, CurrentGame As Game, GameSettings As GameSettings, Game As Game)
        Console.Write("Are you sure? ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("ESC")
        Console.ResetColor()
        Console.Write(" No ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("RETURN")
        Console.ResetColor()
        Console.WriteLine(" Yes")
        Dim Input As ConsoleKeyInfo = Console.ReadKey(True)
        If Input.Key = ConsoleKey.Enter Then
            Game.NewMap(Game, IsStart)
            Dim pos As Position
            pos.y = 12
            pos.x = 16
            Play(Game, pos)
        Else
            If IsStart = True Then
                StartMenu(GameSettings)
            Else
                MainMenu(CurrentGame)
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
    Sub FinishWeek(ByRef Game)
        Dim pos As Position
        For pos.y = 0 To 24
            For pos.x = 0 To (Game.GameSettings.Mapwidth - 1)
                If Game.LotObjectMatrix(pos.y, pos.x).GetType.ToString = "Nanopolis.SmallCommercial" Then
                    Game.HasShoppingPlace = True
                    Game.HasWorkBuildings = True
                    Game.LotObjectMatrix(pos.y, pos.x).FindBuildingPath()
                End If
                If Game.LotObjectMatrix(pos.y, pos.x).GetType.ToString = "Nanopolis.LargeCommercial" Then
                    Game.HasUpperWorkPlace = True
                    Game.HasWorkBuildings = True
                    Game.HasShoppingPlace = True
                End If
                Game.LotObjectMatrix(pos.y, pos.x).RoadConnectionCheck(pos, Game)
            Next
        Next
        For pos.y = 0 To 24
            For pos.x = 0 To (Game.GameSettings.MapWidth - 1)
                If Game.LotObjectMatrix(pos.y, pos.x).GetType.ToString = "Nanopolis.Construction" Then
                    Game.LotObjectMatrix(pos.y, pos.x).FinishConstruction(Game, pos)
                End If
                If Game.LotObjectMatrix(pos.y, pos.x).GetType.ToString = "Nanopolis.SmallResidential" Or Game.LotObjectMatrix(pos.y, pos.x).GetType.ToString = "Nanopolis.LargeResidential" Then
                    If Game.LotObjectMatrix(pos.y, pos.x).Path.Length = 1 Then
                        Game.LotObjectMatrix(pos.y, pos.x).FindBuildingPath(Game, pos, Game.LotObjectMatrix(pos.y, pos.x).LowerWorkPlaceY, Game.LotObjectMatrix(pos.y, pos.x).LowerWorkPlaceX)
                        Game.LotObjectMatrix(pos.y, pos.x).FindBuildingPath(Game, pos, Game.LotObjectMatrix(pos.y, pos.x).MiddleWorkPlaceY, Game.LotObjectMatrix(pos.y, pos.x).MiddleWorkPlaceX)
                        Game.LotObjectMatrix(pos.y, pos.x).FindBuildingPath(Game, pos, Game.LotObjectMatrix(pos.y, pos.x).UpperWorkPlaceY, Game.LotObjectMatrix(pos.y, pos.x).UpperWorkPlaceX)
                        Game.LotObjectMatrix(pos.y, pos.x).FindBuildingPath(Game, pos, Game.LotObjectMatrix(pos.y, pos.x).LowerShoppingPlaceY, Game.LotObjectMatrix(pos.y, pos.x).LowerShoppingPlaceX)
                        Game.LotObjectMatrix(pos.y, pos.x).FindBuildingPath(Game, pos, Game.LotObjectMatrix(pos.y, pos.x).MiddleShoppingPlaceY, Game.LotObjectMatrix(pos.y, pos.x).MiddleShoppingPlaceX)
                        Game.LotObjectMatrix(pos.y, pos.x).FindBuildingPath(Game, pos, Game.LotObjectMatrix(pos.y, pos.x).MiddleShoppingPlaceY, Game.LotObjectMatrix(pos.y, pos.x).MiddleShoppingPlaceX)
                    End If
                    If Game.HasWorkBuildings Then
                        Game.LotObjectMatrix(pos.y, pos.x).Work(Game.LotObjectMatrix(pos.y, pos.x))
                    End If
                    Game.NoOfResidentialLots += 1
                    If Game.TotalPopulation < 10 Then
                        Game.LotObjectMatrix(pos.y, pos.x).DwellerAmount += StartingPopulation
                    End If
                    If Game.LotObjectMatrix(pos.y, pos.x).LandValue > 0 Then
                        Game.LotObjectMatrix(pos.y, pos.x).DwellerAmount += 5
                    End If
                    If Game.HasShoppingPlace Then
                        Game.LotObjectMatrix(pos.y, pos.x).LowerShop(Game, Game.LotObjectMatrix(pos.y, pos.x).LowerShoppingPlaceY, Game.LotObjectMatrix(pos.y, pos.x).LowerShoppingPlaceX, pos)
                        Game.LotObjectMatrix(pos.y, pos.x).MiddleShop(Game, Game.LotObjectMatrix(pos.y, pos.x).MiddleShoppingPlaceY, Game.LotObjectMatrix(pos.y, pos.x).MiddleShoppingPlaceX, pos)
                        If Game.HasUpperShoppingPlace Then
                            Game.LotObjectMatrix(pos.y, pos.x).UpperShop(Game, Game.LotObjectMatrix(pos.y, pos.x).UpperShoppingPlaceY, Game.LotObjectMatrix(pos.y, pos.x).UpperShoppingPlaceX, pos)
                        End If
                        If Game.NoOfWeeksPlayed Mod 4 = 0 Then
                            Game.LotObjectMatrix(pos.y, pos.x).PayIncomeTax(Game.CityGovernment.LowerIncomeTax, Game.CityGovernment.MiddleIncomeTax, Game.CityGovernment.UpperIncomeTax, Game)
                        End If
                    End If
                    Game.LotObjectMatrix(pos.y, pos.x).MoveIn()
                    Game.TotalPopulation += Game.LotObjectMatrix(pos.y, pos.x).DwellerAmount
                End If
                If Game.NoOfResidentialLots <> 0 Then
                    Game.HasResidential = True
                Else
                    Game.HasResidential = False
                End If
                Game.LotObjectMatrix(pos.y, pos.x).LandValue = Game.LotObjectMatrix(pos.y, pos.x).CalculateLandValue(pos, Game)
                If Game.LotObjectMatrix(pos.y, pos.x).GetType.ToString = "Nanopolis.SmallResidential" Or Game.LotObjectMatrix(pos.y, pos.x).GetType.ToString = "Nanopolis.LargeResidential" Then
                    If Game.HasWorkBuildings = True Then
                        Game.LotObjectMatrix(pos.y, pos.x).GenerateWorkOrShoppingPlace(True, Game.GameSettings.MapWidth, Game.LotObjectMatrix, pos, "Lower", Game.HasUpperWorkPlace)
                        Game.LotObjectMatrix(pos.y, pos.x).GenerateWorkOrShoppingPlace(True, Game.GameSettings.MapWidth, Game.LotObjectMatrix, pos, "Middle", Game.HasUpperWorkPlace)
                        If Game.HasUpperWorkPlace Then
                            Game.LotObjectMatrix(pos.y, pos.x).GenerateWorkOrShoppingPlace(True, Game.GameSettings.MapWidth, Game.LotObjectMatrix, pos, "Upper", Game.HasUpperWorkPlace)
                        End If
                    End If
                End If
                Game.LotObjectMatrix(pos.y, pos.x).CrimeRate = Game.LotObjectMatrix(pos.y, pos.x).CalculateCrimeRate(pos, Game)
                Game.LotObjectMatrix(pos.y, pos.x).SetAbandonedWeeks(Game, pos)
            Next
        Next
        If Game.CityGovernment.WelfarePolicy = 1 Then
            Game.CityGovernment.Spend(400)
        ElseIf Game.CityGovernment.WelfarePolict = 2 Then
            Game.CityGovernment.Spend(150)
        End If
        pos.y = 12
        pos.x = 16
        Game.NoOfWeeksPlayed += 1
        Return
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
        Console.Write("Create a plain map? ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("Y/N")
        Console.ResetColor()
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
            Return
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
    Public GridCodes(24, 32) As Integer
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
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write("     ")
                                        Console.ResetColor()
                                    Case 26
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("/ : \")
                                        Console.ResetColor()
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
                                        Console.ForegroundColor = ConsoleColor.Black
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
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("|  -")
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
                                        Console.ResetColor()
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.BackgroundColor = ConsoleColor.DarkGray
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
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.Write("|   ")
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
                                        Console.Write("||")
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
                                        Console.BackgroundColor = ConsoleColor.Green
                                        Console.Write(" ")
                                        Console.BackgroundColor = ConsoleColor.DarkGray
                                        Console.ForegroundColor = ConsoleColor.White
                                        Console.Write("|: /")
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
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("     ")
                                    Console.ResetColor()
                                Case 26
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("/ : \")
                                    Console.ResetColor()
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
                                    Console.ForegroundColor = ConsoleColor.Black
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
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("|  -")
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
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("-  |")
                                    Console.BackgroundColor = ConsoleColor.Green
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
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.Write("|   ")
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
                                    Console.Write("|| ")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
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
                                    Console.Write("||")
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
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.Black
                                    Console.Write("||")
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write("  ")
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
                                    Console.BackgroundColor = ConsoleColor.Green
                                    Console.Write(" ")
                                    Console.BackgroundColor = ConsoleColor.DarkGray
                                    Console.ForegroundColor = ConsoleColor.White
                                    Console.Write("|: /")
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
            Console.WriteLine("Move to return to normal map")
            Console.ReadKey(True)
        End If
        Console.Write("Y" & Int(Pos.y))
        Console.Write(", X" & Int(Pos.x))
        Console.Write("   Land Value: " & Game.LotObjectMatrix(Pos.y, Pos.x).LandValue)
        Console.Write("   Government Budget: $" & Game.CityGovernment.GetTreasury & "   ")
        If Game.CityGovernment.GetTreasury < 0 Then
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("IN DEBT!")
            Console.ResetColor()
        End If
        Dim buildingType As Type = Game.LotObjectMatrix(Pos.y, Pos.x).GetType
        Console.WriteLine(buildingType)
        Console.Write("Crime Rate: " & Game.LotObjectMatrix(Pos.y, Pos.x).CrimeRate & "/1000")
        Console.Write("   No. of weeks played: " & Game.NoOfWeeksPLayed)
        If Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.SmallResidential" Or Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.LargeResidential" Then
            Console.Write("   Population: " & Game.TotalPopulation)
            Console.WriteLine("   Finances of this building: " & Game.LotObjectMatrix(Pos.y, Pos.x).LowerClassCash & ", " & Game.LotObjectMatrix(Pos.y, Pos.x).LowerClassCash & ", " & Game.LotObjectMatrix(Pos.y, Pos.x).LowerClassCash & vbCrLf)
        Else
            Console.WriteLine("   Population: " & Game.TotalPopulation)
        End If
        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("WASD/ARROW KEYS")
        Console.ResetColor()
        Console.Write(" Navigate ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("RETURN")
        Console.ResetColor()
        Console.Write(" Select lot ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("N")
        Console.ResetColor()
        Console.Write(" Finish week ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("G")
        Console.ResetColor()
        Console.Write(" Manage government ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("ESC")
        Console.ResetColor()
        Console.WriteLine(" Main menu")
        Return
    End Sub
End Class
Public Class Government
    Const StartingTreasury As Integer = 20000
    Const StartingExecPower As Integer = 100
    Public Treasury As Integer
    Protected ExecutivePower As Integer
    Public HasParliament As Boolean
    Public ApprovalRate As Integer
    Protected LegislativeControl As Integer
    Public SalesTaxRate As Integer
    Public LowerIncomeTax As Integer
    Public MiddleIncomeTax As Integer
    Public UpperIncomeTax As Integer
    Public CivilLibertyIndex As Integer
    Public CityUnemploymentRate As Integer
    Public CityPollutionIndex As Integer
    Public CityLiteracyRate As Integer
    Public CityEconomicInequality As Integer
    Public EconomicMobility As Integer
    Public WelfarePolicy As Integer = 2
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
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("1")
        Console.ResetColor()
        Console.Write(" The Treasury ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("2")
        Console.ResetColor()
        Console.Write(" Your Cabinet ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("3")
        Console.ResetColor()
        Console.Write(" The Legislature ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("C")
        Console.ResetColor()
        Console.WriteLine(" Return to navigation")
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
                Return
        End Select
    End Sub
    Sub ShowTreasuryMenu(ByRef Game)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("  --THE TREASURY--")
        Console.ResetColor()
        Console.WriteLine("  [1] Adjust income tax [2] Adjust sales tax  [C] Return to gov't menu")
        Console.ReadLine()
        Dim input As ConsoleKeyInfo = Console.ReadKey(True)
        If input.Key = ConsoleKey.D1 Then
            Console.WriteLine("[1] Lower tax rate: " & Game.CityGovernment.LowerIncomeTax)
            Console.WriteLine("[2] Middle tax rate: " & Game.CityGovernment.MiddleIncomeTax)
            Console.WriteLine("[3] Upper tax rate: " & Game.CityGovernment.UpperIncomeTax)
            Dim input2 As ConsoleKeyInfo = Console.ReadKey(True)
            Console.Write("Enter tax rate: ")
            If input2.Key = ConsoleKey.D1 Then
                Game.CityGovernment.LowerIncomeTax = Console.ReadLine()
            ElseIf input2.Key = ConsoleKey.D2 Then
                Game.CityGovernment.MiddleIncomeTax = Console.ReadLine()
            ElseIf input2.Key = ConsoleKey.D3 Then
                Game.CityGovernment.UpperIncomeTax = Console.ReadLine()
            End If
        ElseIf input.Key = ConsoleKey.D2 Then
            Console.Write("Enter tax rate: ")
            Game.CityGovernment.SalesTaxRate = Console.ReadLine()
        ElseIf input.Key = ConsoleKey.C Then
            Return
        End If
    End Sub
    Sub ShowPolicyMenu(IsExecutive, ByRef Game)
        Dim CorrectInput As Boolean = False
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("  Change government policy:")
        Console.WriteLine("  Executive power: " & Game.CityGovernment.ExecutivePower)
        Console.WriteLine("  Legislative control:" & Game.CityGovernment.LegislativeControl & vbCrLf)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("  1")
        Console.ResetColor()
        Console.WriteLine("Gun control")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("  2")
        Console.ResetColor()
        Console.WriteLine("Immigration")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("  3")
        Console.ResetColor()
        Console.WriteLine("Surveillance")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("  4")
        Console.ResetColor()
        Console.WriteLine("Drugs")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("  5")
        Console.ResetColor()
        Console.WriteLine("Foreign Policy")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("  6")
        Console.ResetColor()
        Console.WriteLine("Welfare")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("  7")
        Console.ResetColor()
        Console.WriteLine("Environment")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.ResetColor()
        Dim Input As ConsoleKeyInfo = Console.ReadKey(True)
        Dim Input2 As ConsoleKeyInfo
        While Not CorrectInput
            If Input.Key = ConsoleKey.D1 Then
                CorrectInput = True
                Console.WriteLine(vbCrLf & "  Gun Control:")
                Console.Write("  1")
                Console.WriteLine(" Citizens should be able to purchase firearms freely")
                Console.Write("  2")
                Console.WriteLine(" Access to firearms should be restricted to the police and the military")
                Input2 = Console.ReadKey(True)
                If Input2.Key = ConsoleKey.D1 Then
                    Game.CityGovernment.CivilLibertyIndex += 15
                    Game.CityGovernment.CityUnemploymentRate -= 5
                ElseIf Input2.Key = ConsoleKey.D2 Then
                    Game.CityGovernment.CivilLibertyIndex -= 15
                Else
                    CorrectInput = False
                    Continue While
                End If
            ElseIf Input.Key = ConsoleKey.D2 Then
                CorrectInput = True
                Console.WriteLine(vbCrLf & "  Immigration:")
                Console.Write("  1")
                Console.WriteLine(" Open borders with neighbouring nations")
                Console.Write("  2")
                Console.WriteLine(" Immigration restricted to skilled workers who pass a citizenship test")
                Console.Write("  3")
                Console.WriteLine(" Closed borders, only select government officials can leave the country")
                Input2 = Console.ReadKey(True)
                If Input2.Key = ConsoleKey.D1 Then
                    Game.CityGovernment.CivilLibertyIndex += 10
                    Game.CityGovernment.CityUnemploymentRate += 10
                ElseIf Input2.Key = ConsoleKey.D2 Then
                    Game.CityGovernment.CityUnemploymentRate += 5
                ElseIf Input2.Key = ConsoleKey.D3 Then
                    Game.CityGovernment.CityUnemploymentRate -= 5
                    Game.CityGovernment.CivilLibertyIndex -= 15
                Else
                    CorrectInput = False
                    Continue While
                End If
            ElseIf Input.Key = ConsoleKey.D3 Then
                CorrectInput = True
                Console.WriteLine(vbCrLf & " Surveillance:")
                Console.Write("  1")
                Console.WriteLine(" Citizens aren't monitored at all")
                Console.Write("  2")
                Console.WriteLine(" CCTV in public areas")
                Console.Write("  3")
                Console.WriteLine(" Geolocation through cellphones")
                Input2 = Console.ReadKey(True)
                If Input2.Key = ConsoleKey.D1 Then
                    Game.CityGovernment.CivilLibertyIndex += 10
                    For j As Integer = 0 To 24
                        For i As Integer = 0 To (Game.GameSettings.MapWidth - 1)
                            Game.LotObjectMatrix(j, i).CrimeRate += 10
                        Next
                    Next
                ElseIf Input2.Key = ConsoleKey.D2 Then
                    Game.CityGovernment.CivilLibertyIndex -= 15
                    For j As Integer = 0 To 24
                        For i As Integer = 0 To (Game.GameSettings.MapWidth - 1)
                            Game.LotObjectMatrix(j, i).CrimeRate -= 10
                        Next
                    Next
                Else
                    CorrectInput = False
                    Continue While
                End If
            ElseIf Input.Key = ConsoleKey.D4 Then
                CorrectInput = True
                Console.WriteLine(vbCrLf & " Drugs:")
                Console.Write("  1")
                Console.WriteLine(" No addictive substances may be bought")
                Console.Write("  2")
                Console.WriteLine(" Alcohol and tobacco are permitted")
                Console.Write("  3")
                Console.WriteLine(" All drugs are decriminalised, government stores sell drugs that are properly tested")
                Input2 = Console.ReadKey(True)
                If Input2.Key = ConsoleKey.D1 Then
                    Game.CityGovernment.CivilLibertyIndex -= 10
                    For j As Integer = 0 To 24
                        For i As Integer = 0 To (Game.GameSettings.MapWidth - 1)
                            Game.LotObjectMatrix(j, i).CrimeRate += 15
                        Next
                    Next
                ElseIf Input2.Key = ConsoleKey.D3 Then
                    Game.CityGovernment.CivilLibertyIndex += 5
                    For j As Integer = 0 To 24
                        For i As Integer = 0 To (Game.GameSettings.MapWidth - 1)
                            Game.LotObjectMatrix(j, i).CrimeRate -= 10
                        Next
                    Next
                Else
                    CorrectInput = False
                    Continue While
                End If
            ElseIf Input.Key = ConsoleKey.D5 Then
                CorrectInput = True
                Console.WriteLine(vbCrLf & "  Foreign Policy:")
                Console.Write("  1")
                Console.WriteLine(" Expansionism, conquer new territory for the sake of it")
                Console.Write("  2")
                Console.WriteLine(" Realpolitik, intervene in other nation's politics when it benefits you and hurts a rival nation")
                Console.Write("  3")
                Console.WriteLine(" Isolationism, diplomacy, avoid conflict at all costs")
                Input2 = Console.ReadKey(True)
                If Input2.Key = ConsoleKey.D1 Then
                    Game.CityGovernment.CivilLibertyIndex -= 10
                    Game.CityGovernment.CityUnemploymentRate -= 15
                ElseIf Input2.Key = ConsoleKey.D3 Then
                    Game.CityGovernment.CivilLibertyIndex += 5
                Else
                    CorrectInput = False
                    Continue While
                End If
            ElseIf Input.Key = ConsoleKey.D6 Then
                CorrectInput = True
                Console.WriteLine(vbCrLf & " Welfare:")
                Console.Write("  1")
                Console.WriteLine(" Expansive welfare state with unemployment insurance, full aid for the homeless, support single parent households, and free education")
                Console.Write("  2")
                Console.WriteLine(" Some welfare, free 1ary & 2ary education, support food banks and social security")
                Console.Write("  3")
                Console.WriteLine(" No welfare, fully privatised education and pensions")
                Input2 = Console.ReadKey(True)
                If Input2.Key = ConsoleKey.D1 Then
                    Game.CityGovernment.CivilLibertyIndex += 5
                    Game.CityGovernment.CityUnemploymentRate += 10
                    For j As Integer = 0 To 24
                        For i As Integer = 0 To (Game.GameSettings.MapWidth - 1)
                            Game.LotObjectMatrix(j, i).CrimeRate += 5
                        Next
                    Next
                    Game.CityGovernment.WelfarePolicy = 1
                ElseIf Input2.Key = ConsoleKey.D2 Then
                    Game.CityGovernment.WelfarePolicy = 2
                ElseIf Input2.Key = ConsoleKey.D3 Then
                    Game.CityGovernment.CityUnemploymentRate -= 5
                    Game.CityGovernment.CivilLibertyIndex -= 5
                    Game.CityGovernment.WelfarePolicy = 3
                Else
                    CorrectInput = False
                    Continue While
                End If
            ElseIf Input.Key = ConsoleKey.D7 Then
                CorrectInput = True
                Console.WriteLine(vbCrLf & " Environment:")
                Console.Write("  1")
                Console.WriteLine(" High carbon tax, licenses for hunting, restrict foresting, regulations on coal power stations")
                Console.Write("  2")
                Console.WriteLine(" Low carbon tax, mandate that trees are replaced")
                Console.Write("  3")
                Console.WriteLine(" No restrictions on foresting, no carbon tax")
                Input2 = Console.ReadKey(True)
                If Input2.Key = ConsoleKey.D1 Then
                    Game.CityGovernment.CivilLibertyIndex -= 5
                    Game.CityGovernment.CityUnemploymentRate += 15
                ElseIf Input2.Key = ConsoleKey.D3 Then
                    Game.CityGovernment.CivilLibertyIndex += 5
                    Game.CityGovernment.CityUnemploymentRate -= 15
                Else
                    CorrectInput = False
                    Continue While
                End If
            Else
                Continue While
            End If
        End While
    End Sub
End Class