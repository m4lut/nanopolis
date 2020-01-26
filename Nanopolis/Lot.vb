Public Class Lot
    Public CrimeRate As Integer = 0
    Const PowerDemand As Integer = 50
    Const BaseWeeksUntilAbandoned As Integer = 5
    Public Const BaseLandValue As Integer = 25
    Const Width As Integer = 1
    Const Height As Integer = 1
    Public Pos As Position
    Public InternalLandValueModifier As Integer
    Public Shadows ExternalLandValueModifier As Integer = 0
    Public WorkPlace As Position
    Public ShoppingPlace As Position
    Public ConnectedToRoad As Boolean
    Public Abandoned As Boolean
    Public WeeksUntilAbandoned As Boolean
    Public LandValue As Integer = BaseLandValue
    Public LandValueModifier As Integer
    Public Function LotIs(Type, Game, Y, X) As Boolean
        Type = Type.ToString
        Console.WriteLine(Game.LotObjectMatrix(Y, X).GetType.ToString)
        Console.WriteLine(Game.TypeDict(Type).ToString)
        Console.ReadLine()
        If Game.LotObjectMatrix(Y, X).GetType.ToString = Game.TypeDict(Type).ToString Then
            Console.WriteLine(Game.LotObjectMatrix(Y, X).GetType.ToString)
            Console.ReadLine()
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub SetAbandonedWeeks(ByRef Game, Pos, ByRef Map)
        If (BaseLandValue - InternalLandValueModifier) >= 0 Then
            WeeksUntilAbandoned = BaseWeeksUntilAbandoned
        Else
            WeeksUntilAbandoned -= 1
        End If
        If WeeksUntilAbandoned <= 0 Then
            AbandonBuilding(Game, Pos, Map)
        End If
    End Sub
    Public Overridable Sub AbandonBuilding(ByRef Game, Pos, ByRef Map)
        Me.Demolish(Pos, Game)
    End Sub
    Function RoadConnectionCheck(Position, ByRef Game)
        For j As Integer = -1 To 1
            For i As Integer = -1 To 1
                If Game.LotObjectMatrix(Position.y, Position.x).LotIs("Nanopolis.SmallRoad",,,) Or Game.LotObjectMatrix(Position.y, Position.x).LotIs("Nanopolis.LargeRoad",,,) Then
                    Return True
                ElseIf i = 0 And j = 0 Then
                    Continue For
                End If
            Next
        Next
        Return False
    End Function

    Public Sub Build(ByRef Pos, ByRef Game)
        Randomize()
        Dim ShopType As Integer = Math.Round((Rnd()) + 3)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("Residential[1] | Commercial[2] | Industrial[3]($30) | Road[4] | Power[5] | Park[6] | Police[7]($75) | Parliament[8]($20000) | Nature[9]")
        Console.ResetColor()
        Dim input As ConsoleKeyInfo = Console.ReadKey(True)
        Select Case input.Key
            Case ConsoleKey.D1
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Low density[1]($15) | High density[2]($25)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 0
                    Dim construction As Construction = New Construction()
                    construction.Pos.y = Pos.y
                    construction.Pos.x = Pos.x
                    construction.NextTurnLot = "Nanopolis.SmallResidential"
                    Game.LotObjectMatrix(Pos.y, Pos.x) = construction
                    Game.CityGovernment.Spend(15)
                ElseIf input.Key = ConsoleKey.D2 Then
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 0
                    Dim construction As Construction = New Construction()
                    construction.Pos.y = Pos.y
                    construction.Pos.x = Pos.x
                    construction.NextTurnLot = "Nanopolis.LargeResidential"
                    Game.LotObjectMatrix(Pos.y, Pos.x) = construction
                    If Game.HasWorkBuildings Then
                        Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game.LotObjectMatrix, Pos)
                    End If
                    If Game.HasWorkBuildings Then
                        Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.LotObjectMatrix, Pos)
                    End If
                    Game.CityGovernment.Spend(25)
                End If
            Case ConsoleKey.D2
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Low density[1]($20) | High density[2]($30)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = ShopType
                    Dim construction As Construction = New Construction()
                    construction.Pos.y = Pos.y
                    construction.Pos.x = Pos.x
                    construction.NextTurnLot = "Nanopolis.SmallCommercial"
                    Game.LotObjectMatrix(Pos.y, Pos.x) = construction
                    Game.HasWorkBuildings = True
                    Game.HasShoppingPlace = True
                    Game.CityGovernment.Spend(20)
                ElseIf input.Key = ConsoleKey.D2 Then
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 5
                    Dim construction As Construction = New Construction()
                    construction.Pos.y = Pos.y
                    construction.Pos.x = Pos.x
                    construction.NextTurnLot = "Nanopolis.LargeCommercial"
                    Game.LotObjectMatrix(Pos.y, Pos.x) = construction
                    Game.HasShoppingPlace = True
                    Game.HasWorkBuildings = True
                    Game.CityGovernment.Spend(30)
                End If
            Case ConsoleKey.D3
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 32
                Dim industry As Industry = New Industry()
                industry.Pos.y = Pos.y
                industry.Pos.x = Pos.x
                Game.LotObjectMatrix(Pos.y, Pos.x) = industry
                Game.HasWorkBuildings = True
                Game.CityGovernment.Spend(30)
            Case ConsoleKey.D4
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Low volume[1]($10) | High volume[2]($20)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Dim smallRoad As SmallRoad = New SmallRoad()
                    Game.LotObjectMatrix(Pos.y, Pos.x) = smallRoad
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 13
                    'some of the logic for displaying proper road texture
                    If Game.GameMap.GridCodes(Pos.y + 1, Pos.x) = 13 Then
                        Game.GameMap.GridCodes(Pos.y + 1, Pos.x) = 14
                        Game.GameMap.GridCodes(Pos.y, Pos.x) = 14
                    ElseIf Game.GameMap.GridCodes(Pos.y + 1, Pos.x) = 14 Then
                        Game.GameMap.GridCodes(Pos.y, Pos.x) = 13
                    ElseIf Game.GameMap.GridCodes(Pos.y + 1, Pos.x) = 13 And Game.GameMap.GridCodes(Pos.y, Pos.x + 1) = 14 Then
                        Game.GameMap.GridCodes(Pos.y, Pos.x) = 21
                    ElseIf Game.GameMap.GridCodes(Pos.y + 1, Pos.x) = 13 And Game.GameMap.GridCodes(Pos.y, Pos.x - 1) = 13 Then
                        Game.GameMap.GridCodes(Pos.y, Pos.x) = 20
                    ElseIf Game.GameMap.GridCodes(Pos.y - 1, Pos.x) = 13 And Game.GameMap.GridCodes(Pos.y, Pos.x + 1) = 13 Then
                        Game.GameMap.GridCodes(Pos.y, Pos.x) = 19
                    ElseIf Game.GameMap.GridCodes(Pos.y - 1, Pos.x) = 13 And Game.GameMap.GridCodes(Pos.y, Pos.x - 1) = 13 Then
                        Game.GameMap.GridCodes(Pos.y, Pos.x) = 18
                    End If
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
                Console.WriteLine("Coal [1]($150) | Wind Farm[2]($225)")
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
                Console.WriteLine("Small park[1]($15) | Large park[2]($40)")
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
                Game.CityGovernment.Spend(75)
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
                Console.WriteLine("Forest[1]($5) | Water[2]($30)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Dim forest As Forest = New Forest()
                    Game.GameMap.GridCodes(Pos.y, Pos.x) = 39
                    Game.LotObjectMatrix(Pos.y, Pos.x) = forest
                    Game.CityGovernment.Spend(5)
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
        Game.GameMap.PrintMap(pointerPos, Game)
    End Sub
    Public Sub Demolish(ByRef Pos, ByRef Game)
        Game.GameMap.GridCodes(Pos.y, Pos.x) = -1
        Dim grass As Grass = New Grass()
        Game.LotObjectMatrix(Pos.y, Pos.x) = grass
        Game.GameMap.PrintMap(Pos, Game.GameMap)
    End Sub
    Public Function CalculateCrimeRate(Pos, ByRef Game)
        Dim tempCrimeRate As Integer = 0
        Dim crimeRate As Integer = 0
        For j As Integer = -2 To 2
            For i As Integer = -2 To 2
                If Game.LotObjectMatrix(Pos.y + j, Pos.x + i).GetType.ToString = "Nanopolis.PoliceStation" Then
                    tempCrimeRate -= 50
                End If
            Next
        Next
        crimeRate += tempCrimeRate
        Return crimeRate
    End Function
    Public Function CalculateLandValueFromInternal(Pos, ByRef Game)
        Dim tempModifier As Integer = 0
        Dim tji As Integer = 0
        If Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.SmallRoad" Or Game.LotObjectMatrix(Pos.y, Pos.x).GetType.ToString = "Nanopolis.LargeRoad" Then
            Game.LotObjectMatrix(Pos.y, Pos.x).CalculateTJI
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
    End Sub
End Class
Public Class Roads
    Inherits Lot
    Public TrafficJamIndex As Integer
    Public TimesReferenced As Integer = 0
    Public Function CalculateTJI(ByRef Game, RoadGraph)
        Return TrafficJamIndex
    End Function
End Class
Public Class SmallRoad
    Inherits Roads
    Shadows Const Capacity As Integer = 60
End Class
Public Class LargeRoad
    Inherits Roads
    Shadows Const Capacity As Integer = 140
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
    Public WorkingClassProportion As Integer
    Public MiddleClassProportion As Integer
    Public UpperClassProportion As Integer
    Public UnemployedProportion As Integer
    Sub GenerateWorkOrShoppingPlace(FindingWork, ByRef LotObjectMatrix, pos)
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
                    UpComponent = 2
                ElseIf RandomY > 0.6 And RandomY <= 0.8 Then
                    UpComponent = 3
                ElseIf RandomY > 0.8 And RandomY <= 0.9 Then
                    UpComponent = 4
                ElseIf RandomY > 0.9 And RandomY <= 0.95 Then
                    UpComponent = 5
                ElseIf RandomY > 0.95 And RandomY <= 0.97 Then
                    UpComponent = 6
                ElseIf RandomY > 0.97 Then
                    UpComponent = Int(Rnd() * 100) + 6
                Else
                    UpComponent = 1
                End If
                RandomX = Rnd()
                If RandomX > 0.35 And RandomX <= 0.6 Then
                    RightComponent = 2
                ElseIf RandomX > 0.6 And RandomX <= 0.8 Then
                    RightComponent = 3
                ElseIf RandomX > 0.8 And RandomX <= 0.9 Then
                    RightComponent = 4
                ElseIf RandomX > 0.9 And RandomX <= 0.95 Then
                    RightComponent = 5
                ElseIf RandomX > 0.95 And RandomX <= 0.97 Then
                    RightComponent = 6
                ElseIf RandomX > 0.97 Then
                    RightComponent = Int(Rnd() * 100) + 6
                Else
                    RightComponent = 1
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
                    UpComponent = 2
                ElseIf RandomY > 0.6 And RandomY <= 0.8 Then
                    UpComponent = 3
                ElseIf RandomY > 0.8 And RandomY <= 0.9 Then
                    UpComponent = 4
                ElseIf RandomY > 0.9 And RandomY <= 0.95 Then
                    UpComponent = 5
                ElseIf RandomY > 0.95 And RandomY <= 0.97 Then
                    UpComponent = 6
                ElseIf RandomY > 0.97 Then
                    UpComponent = Int(Rnd() * 100) + 6
                Else
                    UpComponent = 1
                End If
                RandomX = Rnd()
                If RandomX > 0.35 And RandomX <= 0.6 Then
                    RightComponent = 2
                ElseIf RandomX > 0.6 And RandomX <= 0.8 Then
                    RightComponent = 3
                ElseIf RandomX > 0.8 And RandomX <= 0.9 Then
                    RightComponent = 4
                ElseIf RandomX > 0.9 And RandomX <= 0.95 Then
                    RightComponent = 5
                ElseIf RandomX > 0.95 And RandomX <= 0.97 Then
                    RightComponent = 6
                ElseIf RandomX > 0.97 Then
                    RightComponent = Int(Rnd() * 100) + 6
                Else
                    RightComponent = 1
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
                If FindingWork Then
                    WorkPlace.y = pos.y + offset.y
                    WorkPlace.x = pos.x + offset.x
                Else
                    ShoppingPlace.y = pos.y + offset.y
                    ShoppingPlace.x = pos.x + offset.x
                End If
                If LotObjectMatrix(WorkPlace.y, WorkPlace.x).LotIs("Nanopolis.CommercialLot",,,) And FindingWork Then
                    Found = True
                ElseIf LotObjectMatrix(ShoppingPlace.y, ShoppingPlace.x).LotIs("Nanopolis.CommercialLot",,,) And FindingWork = False Then
                    Found = True
                ElseIf LotObjectMatrix(ShoppingPlace.y, ShoppingPlace.x).LotIs("Nanopolis.Industry",,,) And FindingWork = False Then
                    Found = True
                End If
            End If
        End While
    End Sub
End Class
Public Class SmallResidential
    Inherits ResidentialLot
End Class
Public Class LargeResidential
    Inherits ResidentialLot
End Class
Public Class CommercialLot
    Inherits Lot
    Const BaseRevenue As Integer = 0
    Public Revenue As Integer
    Public NumberOfWorkers As Integer
    Sub EstablishStore()
        Revenue = BaseRevenue
    End Sub
    Sub GainRevenue()

    End Sub
    Sub PaySalesTax()

    End Sub
End Class
Public Class SmallCommercial
    Inherits CommercialLot
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
    Public Shadows ExternalLandValueModifier As Integer = -15
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
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game.LotObjectMatrix, Pos)
                End If
                If Game.HasShoppingPlace Then
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.LotObjectMatrix, Pos)
                End If
            Case "Nanopolis.LargeResidential"
                Dim largeResidential As LargeResidential = New LargeResidential()
                Game.LotObjectMatrix(Pos.y, Pos.x) = largeResidential
                Game.GameMap.GridCodes(Pos.y, Pos.x) = 2
                If Game.HasWorkBuildings Then
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(True, Game.LotObjectMatrix, Pos)
                End If
                If Game.HasShoppingPlace Then
                    Game.LotObjectMatrix(Pos.y, Pos.x).GenerateWorkOrShoppingPlace(False, Game.LotObjectMatrix, Pos)
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