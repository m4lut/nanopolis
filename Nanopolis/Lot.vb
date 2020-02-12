Public Class Lot
    Public CrimeRate As Integer = 0
    Public HasRoadConnection As Boolean
    Const PowerDemand As Integer = 50
    Const BaseWeeksUntilAbandoned As Integer = 5
    Public Const BaseLandValue As Integer = 25
    Public InternalLandValueModifier As Integer
    Public Shadows ExternalLandValueModifier As Integer = 0
    Public WorkPlace As Position
    Public ShoppingPlace As Position
    Public ConnectedToRoad As Boolean
    Public Abandoned As Boolean
    Public WeeksUntilAbandoned As Integer = BaseWeeksUntilAbandoned
    Public LandValue As Integer = BaseLandValue
    Public LandValueModifier As Integer
    Sub FindPath(ByRef Game, WhereFrom, WhereTo, ByRef Path)
        Dim Queue As New List(Of Position)
        Dim Discovered As New List(Of Position)
        Dim Parent As New List(Of Position)
        Dim temp As Integer = 0
        Dim tempPos As Position
        Queue.Add(WhereFrom)
        Discovered.Add(WhereFrom)
        Dim Found As Boolean = False
    End Sub
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
    Sub AbandonBuilding(ByRef Game, Pos)
        Me.Demolish(Pos, Game)
    End Sub
    Sub RoadConnectionCheck(Pos, ByRef Game)
        For j As Integer = -1 To 1
            For i As Integer = -1 To 1
                If Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.SmallRoad" Or Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.LargeRoad" Then
                    Game.LotObjectMatrix(Pos.y, Pos.x).HasRoadConnection = True
                    Return
                ElseIf i = 0 And j = 0 Then
                    Continue For
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
    Public TrafficJamIndex As Integer
    Public TimesReferenced As Integer = 0
    Public Shared RoadGraph(Game.GameSettings.MapWidth, 24) As Integer
    'Function CalculateTJI(ByRef Game)
    'Dim tempTJI As Integer = 0
    'For i As Integer = 0 To Game.GameSettings.MapWidth
    'For j As Integer = 0 To 25
    '            tempTJI += Game.LotObjectMatrix(j, i).TimesReferenced
    'Next
    'Next
    'Return TrafficJamIndex
    'End Function
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
    Sub CreateRoadGraph(Game)
        Dim pos As Position
        For pos.x = 0 To 32
            For pos.y = 0 To 24
                Game.LotObjectMatrix(pos.y, pos.x).CheckIfJunction(Game, pos)
            Next
        Next
    End Sub
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
    Public LowerWorkPlace As Position
    Public MiddleWorkPlace As Position
    Public UpperWorkPlace As Position
    Public LowerMiddleShoppingPlace As Position
    Public MiddleUpperShoppingPlace As Position
    Sub GenerateWorkOrShoppingPlace(FindingWork, ByRef Game, pos, SocialClass)
        Randomize()
        Dim offset As Position
        Dim Right As Boolean
        Dim Up As Boolean
        Dim RightComponent As Integer
        Dim UpComponent As Integer
        Dim RandomDirectionX As Single
        Dim RandomDirectionY As Single
        Dim RandomY As Single
        Dim RandomX As Single
        Dim Found As Boolean = False
        RandomDirectionX = Rnd()
        While Found = False
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
                If Right Then
                    RightComponent *= 1
                Else
                    RightComponent *= -1
                End If
                If RightComponent = 0 And UpComponent = 0 Then
                    Continue While
                End If
                If Up Then
                    UpComponent *= 1
                Else
                    UpComponent *= -1
                End If
                offset.x = RightComponent
                offset.y = UpComponent
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
                    Continue While
                End If
                If Right Then
                    RightComponent *= 1
                Else
                    RightComponent *= -1
                End If
                If Up Then
                    UpComponent *= 1
                Else
                    UpComponent *= -1
                End If
                offset.x = RightComponent
                offset.y = UpComponent
            End If
            If FindingWork Then
                WorkPlace.y = pos.y + offset.y
                Console.Write(WorkPlace.y & ",")
                WorkPlace.x = pos.x + offset.x
                Console.Write(WorkPlace.x & ",")
                If WorkPlace.y < 0 Or WorkPlace.y > 24 Then
                    Console.WriteLine("y is out of range ")
                    Continue While
                End If
                If WorkPlace.x < 0 Or WorkPlace.x > (Game.GameSettings.MapWidth - 1) Then
                    Console.WriteLine("x is out of range ")
                    Continue While
                End If
                If SocialClass = "Lower" Then
                    If Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).GetType.ToString = "Nanopolis.SmallCommercial" And Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).HasRoadConnection Then
                        Console.WriteLine("small commercial found!")
                        Game.LotObjectMatrix(pos.y, pos.x).LowerWorkPlace.y = WorkPlace.y
                        Game.LotObjectMatrix(pos.y, pos.x).LowerWorkPlace.x = WorkPlace.x
                        Found = True
                    ElseIf Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).GetType.ToString = "Nanopolis.Industry" And Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).HasRoadConnection Then
                        Console.WriteLine("Industry found!")
                        Game.LotObjectMatrix(pos.y, pos.x).LowerWorkPlace.y = WorkPlace.y
                        Game.LotObjectMatrix(pos.y, pos.x).LowerWorkPlace.x = WorkPlace.x
                        Found = True
                    End If
                ElseIf SocialClass = "Middle" Then
                    If (Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).GetType.ToString = "Nanopolis.SmallCommercial" Or Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).GetType.ToString = "Nanopolis.LargeCommercial") And Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).HasRoadConnection Then
                        Console.WriteLine("middle class workplace found!")
                        Game.LotObjectMatrix(pos.y, pos.x).MiddleWorkPlace.y = WorkPlace.y
                        Game.LotObjectMatrix(pos.y, pos.x).MiddleWorkPlace.x = WorkPlace.x
                        Found = True
                    End If
                ElseIf SocialClass = "Upper" Then
                    If Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).GetType.ToString = "Nanopolis.LargeCommercial" And Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).HasRoadConnection Then
                        Console.WriteLine("upper class workplace found!")
                        Game.LotObjectMatrix(pos.y, pos.x).UpperWorkPlace.y = WorkPlace.y
                        Game.LotObjectMatrix(pos.y, pos.x).UpperWorkPlace.x = WorkPlace.x
                        Found = True
                    End If
                End If
            Else
                ShoppingPlace.y = pos.y + offset.y
                ShoppingPlace.x = pos.x + offset.x
                If SocialClass = "Lower" Then
                    If Game.LotObjectMatrix(ShoppingPlace.y, ShoppingPlace.x).GetType.ToString = "Nanopolis.SmallCommercial" And Game.LotObjectMatrix(ShoppingPlace.y, ShoppingPlace.x).HasRoadConnection Then
                        Found = True
                    End If
                ElseIf SocialClass = "Middle" Then
                    If (Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).GetType.ToString = "Nanopolis.SmallCommercial" Or Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).GetType.ToString = "Nanopolis.LargeCommercial") And Game.LotObjectMatrix(ShoppingPlace.y, ShoppingPlace.x).HasRoadConnection Then
                        Found = True
                    End If
                ElseIf SocialClass = "Upper" Then
                    If Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).GetType.ToString = "Nanopolis.LargeCommercial" And Game.LotObjectMatrix(WorkPlace.y, WorkPlace.x).HasRoadConnection Then
                        Found = True
                    End If
                End If
            End If
            Console.WriteLine("")
        End While
    End Sub
    Sub LowerShop(SalesTaxRate, ByRef Game, ShoppingPlace, Pos)
        For i As Integer = 0 To Int(DwellerAmount * LowerClassProportion)
            Game.LotObjectMatrix(ShoppingPlace.y, ShoppingPlace.x).GainRevenue(20)
            Game.LotObjectMatrix(Pos.y, Pos.x).LowerClassCash -= 20
            Game.LotObjectMatrix(Pos.y, Pos.x).PaySalesTax(SalesTaxRate)
        Next
    End Sub
    Sub MiddleUpperShop(SalesTaxRate, ByRef Game, MiddleUpperShoppingPlace, Pos)
        For i As Integer = 0 To (DwellerAmount * MiddleClassProportion)
            If Game.LotObjectMatrix(LowerMiddleShoppingPlace).GetType.ToString = "Nanopolis.LargeCommercial" Then
                Game.LotObjectMatrix(MiddleUpperShoppingPlace.y, MiddleUpperShoppingPlace.x).GainRevenue(250)
                Game.LotObjectMatrix(Pos.y, Pos.x).MiddleClassCash -= 250
                Game.LotObjectMatrix(Pos.y, Pos.x).MiddleClassCash -= 250 * (Game.CityGovernment.SalesTaxRate / 100)
            ElseIf Game.LotObjectMatrix(LowerMiddleShoppingPlace).GetType.ToString = "Nanopolis.SmallCommercial" Then
                Game.LotObjectMatrix(MiddleUpperShoppingPlace.y, MiddleUpperShoppingPlace.x).GainRevenue(20)
                Game.LotObjectMatrix(Pos.y, Pos.x).MiddleClassCash -= 20
                Game.LotObjectMatrix(Pos.y, Pos.x).MiddleClassCash -= 20 * (Game.CityGovernment.SalesTaxRate / 100)
            End If
        Next
        For i As Integer = 0 To (DwellerAmount * UpperClassProportion)
            Game.LotObjectMatrix(MiddleUpperShoppingPlace.y, MiddleUpperShoppingPlace.x).GainRevenue(250)
            Game.LotObjectMatrix(Pos.y, Pos.x).UpperClassCash -= 250
            Game.LotObjectMatrix(Pos.y, Pos.x).UpperClassCash -= 250 * (Game.CityGovernment.SalesTaxRate / 100)
        Next
    End Sub
    Sub Work(LowerMiddleWorkPlace, MiddleUpperWorkPlace)
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
        Select Case Game.LotObjectMatrix(Pos.y, Pos.x).NextTurnLot.ToString
            Case "Nanopolis.SmallResidential"
                Dim smallResidential As SmallResidential = New SmallResidential()
                Game.LotObjectMatrix(Pos.y, Pos.x) = smallResidential
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 1
                If Game.HasWorkBuildings Then
                    Console.WriteLine("starting lower")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game, Pos, "Lower")
                    Console.WriteLine("lower finished")
                    Console.WriteLine("starting middle")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game, Pos, "Middle")
                    Console.WriteLine("middle finished")
                    Console.WriteLine("starting upper")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game, Pos, "Upper")
                    Console.WriteLine("upper finished")
                End If
                If Game.HasShoppingPlace Then
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game, Pos, "Lower")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game, Pos, "Middle")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game, Pos, "Upper")
                End If
            Case "Nanopolis.LargeResidential"
                Dim largeResidential As LargeResidential = New LargeResidential()
                Game.LotObjectMatrix(Pos.y, Pos.x) = largeResidential
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 2
                If Game.HasWorkBuildings Then
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game, Pos, "Lower")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game, Pos, "Middle")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game, Pos, "Upper")
                End If
                If Game.HasShoppingPlace Then
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.LotObjectMatrix, Pos, "Lower")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.LotObjectMatrix, Pos, "Middle")
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.LotObjectMatrix, Pos, "Upper")
                End If
            Case "Nanopolis.SmallCommercial"
                Dim smallCommercial As SmallCommercial = New SmallCommercial()
                Game.LotObjectMatrix(Pos.y, Pos.x) = smallCommercial
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 3
            Case "Nanopolis.LargeCommercial"
                Dim largeCommercial As LargeCommercial = New LargeCommercial()
                Game.LotObjectMatrix(Pos.y, Pos.x) = largeCommercial
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 4
            Case "Nanopolis.Parliament"
                Dim parliament As Parliament = New Parliament()
                Dim parliamentPointerDown As ParliamentPointer = New ParliamentPointer()
                Dim parliamentPointerRight As ParliamentPointer = New ParliamentPointer()
                Dim parliamentPointerDownRight As ParliamentPointer = New ParliamentPointer()
                Game.LotObjectMatrix(Pos.y, Pos.x) = parliament
                Game.LotObjectMatrix(Pos.y + 1, Pos.x) = parliamentPointerDown
                Game.LotObjectMatrix(Pos.y, Pos.x) = parliamentPointerRight
                Game.LotObjectMatrix(Pos.y, Pos.x) = parliamentPointerDownRight
                Game.Government.HasParliament = True
            Case "Nanopolis.ParliamentPointer"
                If Game.LotObjectMatrix(Pos.y, Pos.x).PointerDirection = "Up" Then
                    Dim parliamentPointerDown As ParliamentPointer = New ParliamentPointer()
                    Game.LotObjectMatrix(Pos.y, Pos.x) = parliamentPointerDown
                ElseIf Game.LotObjectMatrix(Pos.y, Pos.x) Then
                    Dim parliamentPointerRight As ParliamentPointer = New ParliamentPointer()
                    Game.LotObjectMatrix(Pos.y, Pos.x) = parliamentPointerRight
                Else
                    Dim parliamentPointerDownRight As ParliamentPointer = New ParliamentPointer()
                    Game.LotObjectMatrix(Pos.y, Pos.x) = parliamentPointerDownRight
                End If
                Game.Government.HasParliament = True
            Case "Nanopolis.Industry"
                Dim industry As Industry = New Industry()
                Game.LotObjectMatrix(Pos.y, Pos.x) = industry
            Case "Nanopolis.WindFarm"
                Dim windFarm As WindFarm = New WindFarm()
                Game.LotObjectMatrix(Pos.y, Pos.x) = windFarm
            Case "Nanopolis.CoalStation"
                Dim coalStation As CoalStation = New CoalStation()
                Game.LotObjectMatrix(Pos.y, Pos.x) = coalStation
            Case "Nanopolis.PoliceStation"
                Dim policeStation As PoliceStation = New PoliceStation()
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