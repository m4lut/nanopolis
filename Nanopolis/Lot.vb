Public Class Lot
    Public Path(1) As Position
    Public CrimeRate As Integer = 0
    Public HasRoadConnection As Boolean
    Const PowerDemand As Integer = 50
    Const BaseWeeksUntilAbandoned As Integer = 5
    Public RoadConnectedTo As Position
    Public Const BaseLandValue As Integer = 25
    Public InternalLandValueModifier As Integer
    Public Shadows ExternalLandValueModifier As Integer = 0
    Public Abandoned As Boolean
    Public WeeksUntilAbandoned As Integer = BaseWeeksUntilAbandoned
    Public LandValue As Integer = BaseLandValue
    Public LandValueModifier As Integer
    Sub SetAbandonedWeeks(ByRef Game, Pos)
        If (BaseLandValue - InternalLandValueModifier) >= 0 Then
            WeeksUntilAbandoned = BaseWeeksUntilAbandoned
        Else
            WeeksUntilAbandoned -= 1
        End If
        If WeeksUntilAbandoned <= 0 Then
            AbandonBuilding(Game, Pos)
        End If
    End Sub
    Sub FindBuildingPath(Game, StartPos, EndPos)
        Dim pos As Position
        Dim Fringe(1, 1) As Position
        Dim tempWeight As Integer
        Dim Path(1) As Position
        Dim pathSize As Integer = 1
        pos.y = StartPos.y
        pos.x = StartPos.x
        Dim UpWeight As Integer = -1
        Dim DownWeight As Integer = -1
        Dim LeftWeight As Integer = -1
        Dim RightWeight As Integer = -1
        Dim Found As Boolean = False
        While Not Found
            For i As Integer = -1 To 1
                For j As Integer = -1 To 1
                    If i = -1 And j = -1 Then
                        Continue For
                    End If
                    If i = 1 And j = 1 Then
                        Continue For
                    End If
                    If i = 1 And j = -1 Then
                        Continue For
                    End If
                    If i = -1 And j = 1 Then
                        Continue For
                    End If
                    Game.LotObjectMatrix(pos.y + j, pos.x + i).Visited = True
                    If pos.y = EndPos.y And pos.x = EndPos.x Then
                        Game.LotObjectMatrix(StartPos.y, StartPos.x).Path = Path
                    Else
                        Continue For
                    End If
                    If Game.LotObjectMatrix(pos.y + j, pos.x + i).GetType.ToString = "Nanopolis.SmallRoad" Then
                        tempWeight = 60 - Game.LotObjectMatrix(pos.y + j, pos.x + i).TimesReferenced
                        If i = 0 And j = -1 Then
                            UpWeight = tempWeight
                            Game.LotObjectMatrix(pos.y + j, pos.x + i).Discovered = True
                        ElseIf i = 0 And j = 1 Then
                            DownWeight = tempWeight
                            Game.LotObjectMatrix(pos.y + j, pos.x + i).Discovered = True
                        ElseIf i = -1 And j = 0 Then
                            LeftWeight = tempWeight
                            Game.LotObjectMatrix(pos.y + j, pos.x + i).Discovered = True
                        ElseIf i = 1 And j = 0 Then
                            RightWeight = tempWeight
                            Game.LotObjectMatrix(pos.y + j, pos.x + i).Discovered = True
                        End If
                    End If
                    If Game.LotObjectMatrix(pos.y + j, pos.x + i).GetType.ToString = "Nanopolis.LargeRoad" Then
                        tempWeight = 140 - Game.LotObjectMatrix(pos.y + j, pos.x + i).TimesReferenced
                        If i = 0 And j = -1 Then
                            UpWeight = tempWeight
                            Game.LotObjectMatrix(pos.y + j, pos.x + i).Discovered = True
                        ElseIf i = 0 And j = 1 Then
                            DownWeight = tempWeight
                            Game.LotObjectMatrix(pos.y + j, pos.x + i).Discovered = True
                        ElseIf i = -1 And j = 0 Then
                            LeftWeight = tempWeight
                            Game.LotObjectMatrix(pos.y + j, pos.x + i).Discovered = True
                        ElseIf i = 1 And j = 0 Then
                            RightWeight = tempWeight
                            Game.LotObjectMatrix(pos.y + j, pos.x + i).Discovered = True
                        End If
                    End If
                    Game.LotObjectMatrix(pos.y, pos.x).Complete = True
                    If UpWeight > DownWeight And UpWeight > RightWeight And UpWeight > LeftWeight Then
                        pos.y += 1
                    ElseIf RightWeight > DownWeight And RightWeight > UpWeight And RightWeight > LeftWeight Then
                        pos.y += 1
                    ElseIf LeftWeight > DownWeight And LeftWeight > RightWeight And LeftWeight > UpWeight Then
                        pos.y += 1
                    ElseIf DownWeight > UpWeight And DownWeight > RightWeight And DownWeight > LeftWeight Then
                        pos.y += 1
                    End If
                    Console.Write(pos.y & ", ")
                    Console.WriteLine(pos.x)
                Next
            Next
        End While
    End Sub
    Sub AbandonBuilding(ByRef Game, Pos)
        Me.Demolish(Pos, Game)
    End Sub
    Sub RoadConnectionCheck(Pos, ByRef Game)
        For j As Integer = -1 To 1
            For i As Integer = -1 To 1
                If i = 0 And j = 0 Then
                    Continue For
                End If
                If Pos.y + j < 0 Or Pos.y + j > 24 Then
                    Continue For
                End If
                If Pos.x + i < 0 Or Pos.x + i > (Game.GameSettings.MapWidth - 1) Then
                    Continue For
                End If
                If Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.SmallRoad" Or Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.LargeRoad" Then
                    Game.LotObjectMatrix(Pos.y, Pos.x).HasRoadConnection = True
                    Dim roadConnectionPos As Position
                    roadConnectionPos.y = Pos.y + j
                    roadConnectionPos.x = Pos.x + i
                    Game.LotObjectMatrix(Pos.y, Pos.x).RoadConnectedTo = roadConnectionPos
                End If
            Next
        Next
    End Sub
    Public Sub Build(ByRef Pos, ByRef Game)
        Randomize()
        Dim ShopType As Integer = Math.Round((Rnd()) + 3)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("1")
        Console.ResetColor()
        Console.Write(" Residential ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("2")
        Console.ResetColor()
        Console.Write(" Commercial ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("3")
        Console.ResetColor()
        Console.Write(" Industrial($30) ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("4")
        Console.ResetColor()
        Console.Write(" Road ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("5")
        Console.ResetColor()
        Console.Write(" Power ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("6")
        Console.ResetColor()
        Console.Write(" Park ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("7")
        Console.ResetColor()
        Console.Write(" Police($175) ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("8")
        Console.ResetColor()
        Console.Write(" Parliament($20000) ")
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("9")
        Console.ResetColor()
        Console.WriteLine(" Nature ")
        Console.ResetColor()
        Dim input As ConsoleKeyInfo = Console.ReadKey(True)
        Select Case input.Key
            Case ConsoleKey.D1
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.Write("1")
                Console.ResetColor()
                Console.Write("Low density($20)")
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.Write("2")
                Console.ResetColor()
                Console.WriteLine(" High density($50)")
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 0
                    Dim construction As Construction = New Construction()
                    construction.NextTurnLot = "Nanopolis.SmallResidential"
                    Game.LotObjectMatrix(Pos.y, Pos.x) = construction
                    Game.CityGovernment.Spend(20)
                ElseIf input.Key = ConsoleKey.D2 Then
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 0
                    Dim construction As Construction = New Construction()
                    construction.NextTurnLot = "Nanopolis.LargeResidential"
                    Game.LotObjectMatrix(Pos.y, Pos.x) = construction
                    Game.CityGovernment.Spend(50)
                End If
            Case ConsoleKey.D2
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.Write("1")
                Console.ResetColor()
                Console.Write(" Low density($20) ")
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.Write("2")
                Console.ResetColor()
                Console.WriteLine(" High density($150)")
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = ShopType
                    Dim construction As Construction = New Construction()
                    construction.NextTurnLot = "Nanopolis.SmallCommercial"
                    Game.LotObjectMatrix(Pos.y, Pos.x) = construction
                    Game.HasWorkBuildings = True
                    Game.HasShoppingPlace = True
                    Game.CityGovernment.Spend(20)
                ElseIf input.Key = ConsoleKey.D2 Then
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 5
                    Dim construction As Construction = New Construction()
                    construction.NextTurnLot = "Nanopolis.LargeCommercial"
                    Game.LotObjectMatrix(Pos.y, Pos.x) = construction
                    Game.HasShoppingPlace = True
                    Game.HasWorkBuildings = True
                    Game.CityGovernment.Spend(150)
                End If
            Case ConsoleKey.D3
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 32
                Dim industry As Industry = New Industry()
                Game.LotObjectMatrix(Pos.y, Pos.x) = industry
                Game.HasWorkBuildings = True
                Game.CityGovernment.Spend(30)
            Case ConsoleKey.D4
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.Write("1")
                Console.ResetColor()
                Console.Write(" Low volume($10) ")
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.Write("2")
                Console.ResetColor()
                Console.WriteLine(" High volume($20)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Dim smallRoad As SmallRoad = New SmallRoad()
                    Game.LotObjectMatrix(Pos.y, Pos.x) = smallRoad
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 13
                    'some of the logic for displaying proper road texture
                    For i As Integer = -1 To 1
                        For j As Integer = -1 To 1
                            If Pos.y + j < 0 Then
                                Continue For
                            End If
                            If Pos.y + j > 24 Then
                                Continue For
                            End If
                            If Pos.x + i < 0 Then
                                Continue For
                            End If
                            If Pos.x + i > (Game.GameSettings.MapWidth - 1) Then
                                Continue For
                            End If
                            If i = 0 And j = 0 Then
                                Continue For
                            End If
                            If i = 1 And j = 1 Then
                                Continue For
                            End If
                            If i = 1 And j = -1 Then
                                Continue For
                            End If
                            If i = -1 And j = 1 Then
                                Continue For
                            End If
                            If i = -1 And j = -1 Then
                                Continue For
                            End If
                            If i = -1 And j = 0 And (Game.GameMap.GridCodes(Pos.y + j, Pos.x + i) = 13 Or Game.GameMap.Gridcodes(Pos.y + j, Pos.x + i) = 11) And (Game.GameMap.Gridcodes(Pos.y, Pos.x) = 13 Or Game.GameMap.Gridcodes(Pos.y, Pos.x) = 11) Then
                                Game.GameMap.Gridcodes(Pos.y, Pos.x) = 11
                                Game.GameMap.Gridcodes(Pos.y + j, Pos.x + i) = 11
                            End If
                            If j = -1 And i = 1 And Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.SmallRoad" And Game.LotObjectMatrix(Pos.y, Pos.x + 1).GetType.ToString = "Nanopolis.SmallRoad" And Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString <> "Nanopolis.SmallRoad" Then
                                Game.GameMap.GridCodes(Pos.y, Pos.x) = 19
                            End If
                        Next
                    Next
                    Game.cityGovernment.Spend(10)
                ElseIf input.Key = ConsoleKey.D2 Then
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 24
                    Dim largeRoad As LargeRoad = New LargeRoad()
                    Game.LotObjectMatrix(Pos.y, Pos.x) = largeRoad
                    Game.CityGovernment.Spend(20)
                End If
            Case ConsoleKey.D5
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.Write("1")
                Console.ResetColor()
                Console.Write(" Coal($150) ")
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.Write("2")
                Console.ResetColor()
                Console.Write(" Wind Farm($225)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Dim coalStation As CoalStation = New CoalStation()
                    Game.LotObjectMatrix(Pos.y, Pos.x) = coalStation
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 41
                    Game.CityGovernment.Spend(150)
                    Game.HasWorkBuildings = True
                ElseIf input.Key = ConsoleKey.D2 Then
                    Dim windFarm As WindFarm = New WindFarm()
                    Game.LotObjectMatrix(Pos.y, Pos.x) = windFarm
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 40
                    Game.CityGovernment.Spend(225)
                    Game.HasWorkBuildings = True
                End If
            Case ConsoleKey.D6
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.Write("1")
                Console.ResetColor()
                Console.Write(" Small park($15) ")
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.Write("2")
                Console.ResetColor()
                Console.WriteLine(" Large park($40)")
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Dim smallPark As SmallPark = New SmallPark()
                    Game.LotObjectMatrix(Pos.y, Pos.x) = smallPark
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 6
                    Game.CityGovernment.Spend(15)
                ElseIf input.Key = ConsoleKey.D2 Then
                    Dim largePark As LargePark = New LargePark()
                    Dim largeParkRightPointer As LargeParkPointer = New LargeParkPointer()
                    Dim largeParkDownPointer As LargeParkPointer = New LargeParkPointer()
                    Dim largeParkLowerRightPointer As LargeParkPointer = New LargeParkPointer()
                    Game.LotObjectMatrix(Pos.y, Pos.x) = largePark
                    Game.LotObjectMatrix(Pos.y, Pos.x + 1) = largeParkRightPointer
                    Game.LotObjectMatrix(Pos.y + 1, Pos.x) = largeParkDownPointer
                    Game.LotObjectMatrix(Pos.y + 1, Pos.x + 1) = largeParkLowerRightPointer
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 7
                    Game.GameMap.GridCodes(Pos.y, Pos.x + 1) = 8
                    Game.GameMap.GridCodes(Pos.y + 1, Pos.x) = 9
                    Game.GameMap.GridCodes(Pos.y + 1, Pos.x + 1) = 10
                    Game.CityGovernment.Spend(40)
                End If
            Case ConsoleKey.D7
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 37
                Dim policeStation As PoliceStation = New PoliceStation()
                Game.LotObjectMatrix(Pos.y, Pos.x) = policeStation
                Game.CityGovernment.Spend(175)
                Game.HasWorkBuildings = True
            Case ConsoleKey.D8
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 33
                Game.GameMap.GridCodes(Pos.y, Pos.x + 1) = 34
                Game.GameMap.GridCodes(Pos.y + 1, Pos.x) = 35
                Game.GameMap.GridCodes(Pos.y + 1, Pos.x + 1) = 36
                Game.CityGovernment.EstablishParliament()
                Dim parliament As Parliament = New Parliament()
                Dim parliamentPointerRight As Parliament = New Parliament()
                Dim parliamentPointerDown As ParliamentPointer = New ParliamentPointer()
                Dim parliamentPointerLowerRight As ParliamentPointer = New ParliamentPointer()
                Game.LotObjectMatrix(Pos.y, Pos.x) = parliament
                Game.LotObjectMatrix(Pos.y, Pos.x + 1) = parliamentPointerRight
                Game.LotObjectMatrix(Pos.y + 1, Pos.x) = parliamentPointerDown
                Game.LotObjectMatrix(Pos.y + 1, Pos.x + 1) = parliamentPointerLowerRight
            Case ConsoleKey.D9
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.Write("1")
                Console.ResetColor()
                Console.Write(" Forest($10) ")
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.Write("2")
                Console.ResetColor()
                Console.WriteLine(" Water($30)")
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Dim forest As Forest = New Forest()
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 39
                    Game.LotObjectMatrix(Pos.y, Pos.x) = forest
                    Game.CityGovernment.Spend(10)
                ElseIf input.Key = ConsoleKey.D2 Then
                    Dim water As Water = New Water()
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 38
                    Game.LotObjectMatrix(Pos.y, Pos.x) = water
                    Game.CityGovernment.Spend(30)
                End If
        End Select
        Dim pointerPos As Position
        pointerPos.y = 12
        pointerPos.x = 16
        Return
    End Sub
    Public Sub Demolish(ByRef Pos, ByRef Game)
        Game.GameMap.GridCodes(Pos.y, Pos.x) = -1
        Dim grass As Grass = New Grass()
        Game.LotObjectMatrix(Pos.y, Pos.x) = grass
        Return
    End Sub
    Public Function CalculateCrimeRate(Pos, ByRef Game)
        Dim tempCrimeRate As Integer = 0
        Dim crimeRate As Integer = 0
        For j As Integer = -3 To 3
            For i As Integer = -3 To 3
                If (Pos.x + i) < 0 Then
                    Continue For
                End If
                If (Pos.x + i) > (Game.GameSettings.MapWidth - 1) Then
                    Continue For
                End If
                If (Pos.y + j) < 0 Then
                    Continue For
                End If
                If (Pos.y + j) > 24 Then
                    Continue For
                End If
                If Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.PoliceStation" Then
                    tempCrimeRate -= 50
                End If
            Next
        Next
        crimeRate += tempCrimeRate
        Return crimeRate
    End Function
    Protected Function CalculateLandValueFromInternal(Pos, ByRef Game)
        Dim tempModifier As Integer = 0
        Dim tji As Integer = 0
        If Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.SmallRoad" Or Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.LargeRoad" Then
            Game.LotObjectMatrix(Pos.y, Pos.x).CalculateTJI(Game)
            tempModifier += tji
            Return tempModifier
        ElseIf Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.SmallResidential" Or Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.LargeResidential" Then
            If Game.LotObjecMatrix(Pos.y, Pos.x).DwellerAmount = Game.LotObjectMatrix(Pos.y, Pos.x).MaxNoOfDwellers Then
                tempModifier += 25
            End If
            Return tempModifier
        Else
            Return tempModifier
        End If
    End Function
    Sub CalculateLandValue(Pos, ByRef Game)
        Dim modifierFromExt As Integer = Game.CalculateLandValueFromExternal(Pos, Game)
        Dim modifierFromInt As Integer = CalculateLandValueFromInternal(Pos, Game)
        Game.LotObjectMatrix(Pos.y, Pos.x).LandValueModifier = modifierFromExt + modifierFromInt
        Game.LotObjectMatrix(Pos.y, Pos.x).InternalLandValueModifier = Game.LotObjectMatrix(Pos.y, Pos.x).LandValueModifier
        Game.LotObjectMatrix(Pos.y, Pos.x).LandValue = BaseLandValue + Game.LotObjectMatrix(Pos.y, Pos.x).ExternalLandValueModifier
        Return
    End Sub
End Class
Public Class Road
    Inherits Lot
    Public IsJunction As Boolean
    Public TrafficJamIndex As Integer = 0
    Public TimesReferenced As Integer = 0
    Public Visited As Boolean = False
    Public Complete As Boolean = False
    Public UpWeight As Integer
    Public DownWeight As Integer
    Public LeftWeight As Integer
    Public RightWeight As Integer
    Function CalculateTJI(ByRef Game)
        Dim tempTJI As Integer = 0
        'For i As Integer = 0 To Game.GameSettings.MapWidth
        'For j As Integer = 0 To 24
        'tempTJI += Game.LotObjectMatrix(j, i).TimesReferenced
        'TrafficJamIndex += tempTJI 
        'Next
        'Next
        For i As Integer = -2 To 2
            For j As Integer = -2 To 2
                If Game.LotObjectMatrix(j, i).GetType.ToString = "Nanopolis.SmallResidential" Or Game.LotObjectMatrix(j, i).GetType.ToString = "Nanopolis.LargeResidential" Then
                    tempTJI += (Game.LotObjectMatrix(j, i).DwellerAmount / 2)
                End If
            Next
        Next
        Return TrafficJamIndex
    End Function
    Function CheckIfJunction(ByRef Game, Pos)
        For i As Integer = -1 To 1
            For j As Integer = -1 To 1
                If i = -1 And j = -1 Then
                    Continue For
                End If
                If i = -1 And j = 1 Then
                    Continue For
                End If
                If i = 1 And j = -1 Then
                    Continue For
                End If
                If i = 1 And j = 1 Then
                    Continue For
                End If
                If i = 0 And j = 0 Then
                    Continue For
                End If
                If Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.SmallRoad" Or Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.LargeRoad" Then
                    Return True
                Else
                    Return False
                End If
            Next
        Next
    End Function
End Class
Public Class SmallRoad
    Inherits Road
    Public Shadows Const Capacity As Integer = 60
End Class
Public Class LargeRoad
    Inherits Road
    Public Shadows Const Capacity As Integer = 140
End Class
Public Class Nature
    Inherits Lot
    Public Shadows ExternalLandValueModifier As Integer = 0
End Class
Public Class Grass
    Inherits Nature
End Class
Public Class Water
    Inherits Nature
    Public Shadows ExternalLandValueModifier As Integer = 10
End Class
Public Class Forest
    Inherits Nature
    Public Shadows ExternalLandValueModifier As Integer = 5
End Class
Public Class ResidentialLot
    Inherits Lot
    Public DwellerAmount As Integer
    Public LowerClassCash As Integer = 0
    Public MiddleClassCash As Integer = 0
    Public UpperClassCash As Integer = 0
    Public LowerClassProportion As Integer
    Public MiddleClassProportion As Integer
    Public UpperClassProportion As Integer
    Public UnemployedProportion As Integer
    Public LowerWorkPlaceY As Integer
    Public LowerWorkPlaceX As Integer
    Public MiddleWorkPlaceY As Integer
    Public MiddleWorkPlaceX As Integer
    Public UpperWorkPlaceY As Integer
    Public UpperWorkPlaceX As Integer
    Public LowerShoppingPlaceY As Integer
    Public LowerShoppingPlaceX As Integer
    Public MiddleShoppingPlaceY As Integer
    Public MiddleShoppingPlaceX As Integer
    Public MiddleUpperShoppingPlaceY As Integer
    Public MiddleUpperShoppingPlaceX As Integer
    Sub GenerateWorkOrShoppingPlace(FindingWork, MapWidth, ByRef LotObjectMatrix, pos, SocialClass, HasUpperWorkPlace)
        Randomize()
        Dim Right As Boolean
        Dim Up As Boolean
        Dim RightComponent As Integer
        Dim UpComponent As Integer
        Dim RandomDirectionX As Single
        Dim RandomDirectionY As Single
        Dim RandomY As Single
        Dim RandomX As Single
        Dim Found As Boolean = False
        Dim finalPlaceY As Integer
        Dim finalPlaceX As Integer
        RandomDirectionX = Rnd()
        While Found = False
            Threading.Thread.Sleep(Rnd)
            If FindingWork Then
                If RandomDirectionX >= 0.5 Then
                    Right = True
                Else
                    Right = False
                End If
                RandomDirectionY = Rnd()
                If RandomDirectionY >= 0.5 Then
                    Up = True
                Else
                    Up = False
                End If
                RandomY = Rnd()
                If RandomY > 0.35 And RandomY <= 0.6 Then
                    UpComponent = 1
                ElseIf RandomY > 0.6 And RandomY <= 0.8 Then
                    UpComponent = 2
                ElseIf RandomY > 0.8 And RandomY <= 0.9 Then
                    UpComponent = 3
                ElseIf RandomY > 0.9 And RandomY <= 0.95 Then
                    UpComponent = 4
                ElseIf RandomY > 0.95 And RandomY <= 0.97 Then
                    UpComponent = 5
                ElseIf RandomY > 0.97 Then
                    UpComponent = Int(Rnd() * 100) + 6
                Else
                    UpComponent = 0
                End If
                RandomX = Rnd()
                If RandomX > 0.35 And RandomX <= 0.6 Then
                    RightComponent = 1
                ElseIf RandomX > 0.6 And RandomX <= 0.8 Then
                    RightComponent = 2
                ElseIf RandomX > 0.8 And RandomX <= 0.9 Then
                    RightComponent = 3
                ElseIf RandomX > 0.9 And RandomX <= 0.95 Then
                    RightComponent = 4
                ElseIf RandomX > 0.95 And RandomX <= 0.97 Then
                    RightComponent = 5
                ElseIf RandomX > 0.97 Then
                    RightComponent = Int(Rnd() * 100) + 6
                Else
                    RightComponent = 0
                End If
                If Not Right Then
                    RightComponent *= -1
                End If
                If Not Up Then

                    UpComponent *= -1
                End If
                If RightComponent = 0 And UpComponent = 0 Then
                    Console.WriteLine("#")
                    Continue While
                End If
            Else
                RandomDirectionX = Rnd()
                If RandomDirectionX >= 0.5 Then
                    Right = True
                Else
                    Right = False
                End If
                RandomDirectionY = Rnd()
                If RandomDirectionY >= 0.5 Then
                    Up = True
                Else
                    Up = False
                End If
                RandomY = Rnd()
                If RandomY > 0.35 And RandomY <= 0.6 Then
                    UpComponent = 1
                ElseIf RandomY > 0.6 And RandomY <= 0.8 Then
                    UpComponent = 2
                ElseIf RandomY > 0.8 And RandomY <= 0.9 Then
                    UpComponent = 3
                ElseIf RandomY > 0.9 And RandomY <= 0.95 Then
                    UpComponent = 4
                ElseIf RandomY > 0.95 And RandomY <= 0.97 Then
                    UpComponent = 5
                ElseIf RandomY > 0.97 Then
                    UpComponent = Int(Rnd() * 100) + 5
                Else
                    UpComponent = 0
                End If
                RandomX = Rnd()
                If RandomX > 0.35 And RandomX <= 0.6 Then
                    RightComponent = 1
                ElseIf RandomX > 0.6 And RandomX <= 0.8 Then
                    RightComponent = 2
                ElseIf RandomX > 0.8 And RandomX <= 0.9 Then
                    RightComponent = 3
                ElseIf RandomX > 0.9 And RandomX <= 0.95 Then
                    RightComponent = 4
                ElseIf RandomX > 0.95 And RandomX <= 0.97 Then
                    RightComponent = 5
                ElseIf RandomX > 0.97 Then
                    RightComponent = Int(Rnd() * 100) + 5
                Else
                    RightComponent = 0
                End If
                If RightComponent = 0 And UpComponent = 0 Then
                    Console.WriteLine("#")
                    Continue While
                End If
                If Not Right Then
                    RightComponent *= -1
                End If
                If Not Up Then
                    UpComponent *= -1
                End If
            End If
            finalPlaceY = pos.y + UpComponent
            finalPlaceX = pos.x + RightComponent
            Console.Write(finalPlaceY & ", ")
            Console.WriteLine(finalPlaceX)
            If finalPlaceY < 0 Or finalPlaceY > 24 Then
                Console.WriteLine("Y$")
                Continue While
            End If
            If finalPlaceX < 0 Or finalPlaceX > (MapWidth - 1) Then
                Console.WriteLine("X$")
                Continue While
            End If
            If FindingWork Then
                If SocialClass = "Lower" Then
                    If LotObjectMatrix(finalPlaceY, finalPlaceX).GetType.ToString = "Nanopolis.SmallCommercial" And LotObjectMatrix(finalPlaceY, finalPlaceX).HasRoadConnection Then
                        LotObjectMatrix(pos.y, pos.x).LowerWorkPlaceY = finalPlaceY
                        LotObjectMatrix(pos.y, pos.x).LowerWorkPlaceX = finalPlaceX
                        Found = True
                    ElseIf LotObjectMatrix(finalPlaceY, finalPlaceX).GetType.ToString = "Nanopolis.Industry" And LotObjectMatrix(finalPlaceY, finalPlaceX).HasRoadConnection Then
                        LotObjectMatrix(pos.y, pos.x).LowerWorkPlaceY = finalPlaceY
                        LotObjectMatrix(pos.y, pos.x).LowerWorkPlaceX = finalPlaceX
                        Found = True
                    End If
                ElseIf SocialClass = "Middle" Then
                    If LotObjectMatrix(finalPlaceY, finalPlaceX).GetType.ToString = "Nanopolis.LargeCommercial" And LotObjectMatrix(finalPlaceY, finalPlaceX).HasRoadConnection Then
                        LotObjectMatrix(pos.y, pos.x).MiddleWorkPlaceY = finalPlaceY
                        LotObjectMatrix(pos.y, pos.x).MiddleWorkPlaceX = finalPlaceX
                        Found = True
                    ElseIf LotObjectMatrix(finalPlaceY, finalPlaceX).GetType.ToString = "Nanopolis.SmallCommercial" And LotObjectMatrix(finalPlaceY, finalPlaceX).HasRoadConnection Then
                        LotObjectMatrix(pos.y, pos.x).MiddleWorkPlaceY = finalPlaceY
                        LotObjectMatrix(pos.y, pos.x).MiddleWorkPlaceX = finalPlaceX
                        Found = True
                    End If
                ElseIf SocialClass = "Upper" Then
                    If HasUpperWorkPlace Then
                        If LotObjectMatrix(finalPlaceY, finalPlaceX).GetType.ToString = "Nanopolis.LargeCommercial" And LotObjectMatrix(finalPlaceY, finalPlaceX).HasRoadConnection Then
                            LotObjectMatrix(pos.y, pos.x).UpperWorkPlaceY = finalPlaceY
                            LotObjectMatrix(pos.y, pos.x).UpperWorkPlaceX = finalPlaceX
                            Found = True
                        End If
                    End If
                End If
            Else
                If SocialClass = "Lower" Then
                    If LotObjectMatrix(finalPlaceY, finalPlaceX).GetType.ToString = "Nanopolis.SmallCommercial" And LotObjectMatrix(finalPlaceY, finalPlaceX).HasRoadConnection Then
                        LotObjectMatrix(pos.y, pos.x).LowerShoppingPlaceY = finalPlaceY
                        LotObjectMatrix(pos.y, pos.x).LowerShoppingPlaceX = finalPlaceX
                        Found = True
                    End If
                ElseIf SocialClass = "Middle" Then
                    If (LotObjectMatrix(finalPlaceY, finalPlaceX).GetType.ToString = "Nanopolis.SmallCommercial" Or LotObjectMatrix(finalPlaceY, finalPlaceX).GetType.ToString = "Nanopolis.LargeCommercial") And LotObjectMatrix(finalPlaceY, finalPlaceX).HasRoadConnection Then
                        LotObjectMatrix(pos.y, pos.x).MiddleShoppingPlaceY = finalPlaceY
                        LotObjectMatrix(pos.y, pos.x).MiddleShoppingPlaceX = finalPlaceX
                        Found = True
                    End If
                ElseIf SocialClass = "Upper" Then
                    If LotObjectMatrix(finalPlaceY, finalPlaceX).GetType.ToString = "Nanopolis.LargeCommercial" And LotObjectMatrix(finalPlaceY, finalPlaceX).HasRoadConnection Then
                        LotObjectMatrix(pos.y, pos.x).UpperShoppingPlaceY = finalPlaceY
                        LotObjectMatrix(pos.y, pos.x).UpperShoppingPlaceX = finalPlaceX
                        Found = True
                    End If
                End If
            End If
        End While
    End Sub
    Sub LowerShop(ByRef Game, ShoppingPlace, Pos)
        For i As Integer = 0 To Int(DwellerAmount * LowerClassProportion)
            Game.LotObjectMatrix(ShoppingPlace.y, ShoppingPlace.x).GainRevenue(20)
            Game.LotObjectMatrix(Pos.y, Pos.x).LowerClassCash -= 20
            Game.LotObjectMatrix(Pos.y, Pos.x).PaySalesTax(Game.CityGovernment.LowerIncomeTax)
        Next
    End Sub
    Sub MiddleShop(ByRef Game, ShoppingPlace, Pos)
        For i As Integer = 0 To (DwellerAmount * MiddleClassProportion)
            If Game.LotObjectMatrix(MiddleShoppingPlaceY, MiddleShoppingPlaceX).GetType.ToString = "Nanopolis.LargeCommercial" Then
                Game.LotObjectMatrix(ShoppingPlace.y, ShoppingPlace.x).GainRevenue(250)
                Game.LotObjectMatrix(Pos.y, Pos.x).MiddleClassCash -= 250
                Game.LotObjectMatrix(Pos.y, Pos.x).MiddleClassCash -= 250 * (Game.CityGovernment.SalesTaxRate / 100)
            ElseIf Game.LotObjectMatrix(MiddleShoppingPlaceY, MiddleShoppingPlaceX).GetType.ToString = "Nanopolis.SmallCommercial" Then
                Game.LotObjectMatrix(ShoppingPlace.y, ShoppingPlace.x).GainRevenue(20)
                Game.LotObjectMatrix(Pos.y, Pos.x).MiddleClassCash -= 20
                Game.LotObjectMatrix(Pos.y, Pos.x).MiddleClassCash -= 20 * (Game.CityGovernment.SalesTaxRate / 100)
            End If
        Next
        For i As Integer = 0 To (DwellerAmount * UpperClassProportion)
            Game.LotObjectMatrix(ShoppingPlace.y, ShoppingPlace.x).GainRevenue(250)
            Game.LotObjectMatrix(Pos.y, Pos.x).UpperClassCash -= 250
            Game.LotObjectMatrix(Pos.y, Pos.x).UpperClassCash -= 250 * (Game.CityGovernment.SalesTaxRate / 100)
        Next
    End Sub
    Sub UpperShop(ByRef Game, ShoppingPlace, Pos)

    End Sub
    Sub Work(Lot)
        For i As Integer = 0 To Int(DwellerAmount * LowerClassProportion)
            LowerClassCash += Int(DwellerAmount * LowerClassProportion * 75)
        Next
        For i As Integer = 0 To Int(DwellerAmount * MiddleClassProportion)
            MiddleClassCash += Int(DwellerAmount * MiddleClassProportion * 150)
        Next
        For i As Integer = 0 To (DwellerAmount * UpperClassProportion)
            UpperClassCash += Int(DwellerAmount * UpperClassProportion * 300)
        Next
    End Sub
    Sub PayIncomeTax(LowerRate, MiddleRate, UpperRate, ByRef Game)
        Dim tax As Integer
        For i As Integer = 0 To Int(DwellerAmount * LowerClassProportion)
            tax = LowerClassCash * (LowerRate / 100)
            LowerClassCash -= tax
            Game.CityGovernment.Treasury += tax
        Next
        For i As Integer = 0 To Int(DwellerAmount * MiddleClassProportion)
            tax = MiddleClassCash * (MiddleRate / 100)
            MiddleClassCash -= tax
            Game.CityGovernment.Treasury += tax
        Next
        For i As Integer = 0 To Int(DwellerAmount * UpperClassProportion)
            tax = UpperClassCash * (UpperRate / 100)
            UpperClassCash -= tax
            Game.CityGovernment.Treasury += tax
        Next
    End Sub
    Sub MoveIn()
        DwellerAmount += 5
        If LandValue > 50 Then
            MiddleClassProportion += 0.05
            LowerClassProportion -= 0.05
        End If
        If LandValue > 150 Then
            UpperClassProportion += 0.05
            MiddleClassProportion -= 0.05
        End If
    End Sub
End Class
Public Class SmallResidential
    Inherits ResidentialLot
    Protected MaxNoOfDwellers As Integer = 25
End Class
Public Class LargeResidential
    Inherits ResidentialLot
    Protected MaxNoOfDwellers As Integer = 100
    Overloads Sub MoveIn()
        DwellerAmount += 10
        If LandValue > 40 Then
            MiddleClassProportion += 0.05
            LowerClassProportion -= 0.05
        End If
        If LandValue > 100 Then
            UpperClassProportion += 0.05
            MiddleClassProportion -= 0.05
        End If
    End Sub
End Class
Public Class CommercialLot
    Inherits Lot
    Shadows Const BaseCash As Integer = 2000
    Public Cash As Integer = 0
    Public NoOfWorkers As Integer = 0
    Public NoOfBatchesOrdered As Integer = 0
    Public NoOfBatchesInStock As Integer = 0
    Public NoOfBatchesSold As Integer = 0
    Public FactoryPos As Position
    Sub EstablishStore()
        Cash = BaseCash
    End Sub
    Sub GainRevenue(Amount)
        Cash += Amount
    End Sub
    Sub PaySalesTax(TaxRate)
        If Me.GetType.ToString = "Nanopolis.LargeCommercial" Then
            Cash -= (TaxRate / 2) * NoOfBatchesSold * 250
        ElseIf Me.GetType.ToString = "Nanopolis.SmallCommercial" Then
            Cash -= (TaxRate / 2) * NoOfBatchesSold * 20
        End If
    End Sub
    Sub PaySupplier(FactoryPos, OrderAmount, IsLarge, ByRef Game)
        If Me.GetType.ToString = "Nanopolis.SmallCommercial" Then
            Game.lotobjectmatrix(FactoryPos.y, FactoryPos.x).Cash += OrderAmount * 15
        ElseIf Me.GetType.ToString = "Nanopolis.LargeCommercial" Then
            Game.lotobjectmatrix(FactoryPos.y, FactoryPos.x).Cash += OrderAmount * 200
        End If
    End Sub
    Sub Order(FactoryPos, OrderAmount, ByRef Game)
        Game.LotObjectMatrix(FactoryPos.y, FactoryPos.x).SendPackages(OrderAmount)
    End Sub
End Class
Public Class SmallCommercial
    Inherits CommercialLot
    Shadows Const BaseCash As Integer = 500
End Class
Public Class LargeCommercial
    Inherits CommercialLot
    Public Shadows Const PowerDemand As Integer = 90
End Class
Public Class Park
    Inherits Lot
End Class
Public Class SmallPark
    Inherits Park
    Public Shadows ExternalLandValueModifier As Integer = 15
End Class
Public Class LargePark
    Inherits Park
    Public Shadows ExternalLandValueModifier As Integer = 30
    Shadows Const Height As Integer = 2
    Shadows Const Width As Integer = 2
End Class
Public Class LargeParkPointer
    Inherits Park
    Public PointingToY As Integer
    Public PointingToX As Integer
End Class
Public Class Industry
    Inherits Lot
    Public NoOfWorkers As Integer = 0
    Public Shadows ExternalLandValueModifier As Integer = -15
    Public SmallBatchesMade As Integer = 0
    Public SmallBatchesInStock As Integer = 0
    Public SmallBatchesOrdered As Integer = 0
    Public LargeBatchesMade As Integer = 0
    Public LargeBatchesInStock As Integer = 0
    Public LargeBatchesOrdered As Integer = 0
    Sub SendPackages(CommercialPos, ByRef Game, PackageAmount)
        Game.LotObjectMatrix(CommercialPos.y, CommercialPos.x).NoOfBatchesInStock += PackageAmount
        If Game.LotObjectMatrix(CommercialPos.y, CommercialPos.x).GetType.ToString = "Nanopolis.SmallCommercial" Then
            SmallBatchesInStock -= PackageAmount
        ElseIf Game.LotObjectMatrix(CommercialPos.y, CommercialPos.x).GetType.ToString = "Nanopolis.LargeCommercial" Then
            LargeBatchesInStock -= PackageAmount
        End If
    End Sub
End Class
Public Class Parliament
    Inherits Lot
    Public Shadows ExternalLandValueModifier As Integer = 50
    Public Shadows Const Width As Integer = 2
    Public Shadows Const Height As Integer = 2
End Class
Public Class ParliamentPointer
    Inherits Lot
    Public PointingToY As Integer
    Public PointingToX As Integer
End Class
Public Class Construction
    Inherits Lot
    Shadows Const PowerDemand As Integer = 0
    Public NextTurnLot As String
    Public PointerDirection As String
    Sub FinishConstruction(ByRef Game, Pos)
        Dim tempHasRoadConnection As Boolean = False
        If Game.LotObjectMatrix(Pos.y, Pos.x).HasRoadConnection Then
            tempHasRoadConnection = True
        End If
        Select Case Game.LotObjectMatrix(Pos.y, Pos.x).NextTurnLot.ToString
            Case "Nanopolis.SmallResidential"
                Dim smallResidential As SmallResidential = New SmallResidential()
                smallResidential.HasRoadConnection = tempHasRoadConnection
                Game.LotObjectMatrix(Pos.y, Pos.x) = smallResidential
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 1
                If Game.HasWorkBuildings Then
                    If Game.HasUpperWorkPlace Then
                        Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Lower", True)
                        Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Middle", True)
                        Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Upper", True)
                    Else
                        Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Lower", False)
                        Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Middle", False)
                    End If
                End If
                If Game.HasUpperShoppingPlace Then
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Lower", Nothing)
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Middle", Nothing)
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Upper", Nothing)
                Else
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Lower", Nothing)
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Middle", Nothing)
                End If
            Case "Nanopolis.LargeResidential"
                Dim largeResidential As LargeResidential = New LargeResidential()
                largeResidential.HasRoadConnection = tempHasRoadConnection
                Game.LotObjectMatrix(Pos.y, Pos.x) = largeResidential
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 2
                If Game.HasWorkBuildings Then
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Lower")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Middle")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Upper")
                End If
                If Game.HasShoppingPlace Then
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Lower")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Middle")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.GameSettings.MapWidth, Game.LotObjectMatrix, Pos, "Upper")
                End If
            Case "Nanopolis.SmallCommercial"
                Dim smallCommercial As SmallCommercial = New SmallCommercial()
                smallCommercial.HasRoadConnection = tempHasRoadConnection
                Game.LotObjectMatrix(Pos.y, Pos.x) = smallCommercial
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 3
            Case "Nanopolis.LargeCommercial"
                Dim largeCommercial As LargeCommercial = New LargeCommercial()
                largeCommercial.HasRoadConnection = tempHasRoadConnection
                Game.LotObjectMatrix(Pos.y, Pos.x) = largeCommercial
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 4
            Case "Nanopolis.Parliament"
                Dim parliament As Parliament = New Parliament()
                parliament.HasRoadConnection = tempHasRoadConnection
                Dim parliamentPointerDown As ParliamentPointer = New ParliamentPointer()
                parliamentPointerDown.HasRoadConnection = tempHasRoadConnection
                Dim parliamentPointerRight As ParliamentPointer = New ParliamentPointer()
                parliamentPointerRight.HasRoadConnection = tempHasRoadConnection
                Dim parliamentPointerDownRight As ParliamentPointer = New ParliamentPointer()
                parliamentPointerDownRight.HasRoadConnection = tempHasRoadConnection
                Game.LotObjectMatrix(Pos.y, Pos.x) = parliament
                Game.LotObjectMatrix(Pos.y + 1, Pos.x) = parliamentPointerDown
                Game.LotObjectMatrix(Pos.y, Pos.x) = parliamentPointerRight
                Game.LotObjectMatrix(Pos.y, Pos.x) = parliamentPointerDownRight
                Game.Government.HasParliament = True
            Case "Nanopolis.ParliamentPointer"
                If Game.LotObjectMatrix(Pos.y, Pos.x).PointerDirection = "Up" Then
                    Dim parliamentPointerDown As ParliamentPointer = New ParliamentPointer()
                    parliamentPointerDown.HasRoadConnection = tempHasRoadConnection
                    Game.LotObjectMatrix(Pos.y, Pos.x) = parliamentPointerDown
                ElseIf Game.LotObjectMatrix(Pos.y, Pos.x) Then
                    Dim parliamentPointerRight As ParliamentPointer = New ParliamentPointer()
                    parliamentPointerRight.HasRoadConnection = tempHasRoadConnection
                    Game.LotObjectMatrix(Pos.y, Pos.x) = parliamentPointerRight
                Else
                    Dim parliamentPointerDownRight As ParliamentPointer = New ParliamentPointer()
                    parliamentPointerDownRight.HasRoadConnection = tempHasRoadConnection
                    Game.LotObjectMatrix(Pos.y, Pos.x) = parliamentPointerDownRight
                End If
                Game.Government.HasParliament = True
            Case "Nanopolis.Industry"
                Dim industry As Industry = New Industry()
                industry.HasRoadConnection = tempHasRoadConnection
                Game.LotObjectMatrix(Pos.y, Pos.x) = industry
            Case "Nanopolis.WindFarm"
                Dim windFarm As WindFarm = New WindFarm()
                windFarm.HasRoadConnection = tempHasRoadConnection
                Game.LotObjectMatrix(Pos.y, Pos.x) = windFarm
            Case "Nanopolis.CoalStation"
                Dim coalStation As CoalStation = New CoalStation()
                coalStation.HasRoadConnection = tempHasRoadConnection
                Game.LotObjectMatrix(Pos.y, Pos.x) = coalStation
            Case "Nanopolis.PoliceStation"
                Dim policeStation As PoliceStation = New PoliceStation()
                policeStation.HasRoadConnection = tempHasRoadConnection
                Game.LotObjectMatrix(Pos.y, Pos.x) = policeStation
        End Select
    End Sub
End Class
Public Class PoliceStation
    Inherits Lot
    Shadows Const PowerDemand As Integer = 75
End Class
Public Class PowerPlant
    Inherits Lot
    Shadows Const PowerDemand As Integer = 0
End Class
Public Class CoalStation
    Inherits PowerPlant
    Shadows Const PowerOutput As Integer = 1000
End Class
Public Class WindFarm
    Inherits PowerPlant
    Shadows Const PowerOutput As Integer = 500
End Class