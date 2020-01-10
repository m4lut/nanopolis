Public Class Lot
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
    Function LotIs(Type, Game, Y, X) As Boolean
        If Game.LotObjectMatrix(Y, X).GetType.ToString = Game.TypeDict(Type.ToString).ToString Then
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
        Me.Demolish(Pos, Game, Map)
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
    Sub GenerateWorkOrShoppingPlace(FindingWork, ByRef LotObjectMatrix, ByRef workPlace, ByRef shoppingPlace, pos)
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
                    workPlace.y = pos.y + offset.y
                    workPlace.x = pos.x + offset.x
                Else
                    shoppingPlace.y = pos.y + offset.y
                    shoppingPlace.x = pos.x + offset.x
                End If
                If LotObjectMatrix(workPlace.y, workPlace.x).LotIs("Nanopolis.CommercialLot",,,) And FindingWork Then
                    Found = True
                ElseIf LotObjectMatrix(shoppingPlace.y, shoppingPlace.x).LotIs("Nanopolis.CommercialLot",,,) And FindingWork = False Then
                    Found = True
                ElseIf LotObjectMatrix(shoppingPlace.y, shoppingPlace.x).LotIs("Nanopolis.Industry",,,) And FindingWork = False Then
                    Found = True
                End If
            End If
        End While
    End Sub
    Public Sub Build(ByRef Pos, ByRef game, ByRef map)
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
                    map.GridCodes(Pos.y, Pos.x) = 1
                    Dim smallResidential As SmallResidential = New SmallResidential()
                    smallResidential.Pos.y = Pos.y
                    smallResidential.Pos.x = Pos.x
                    game.LotObjectMatrix(Pos.y, Pos.x) = smallResidential
                    game.cityGovernment.Spend(15)
                    Console.WriteLine(map.GridCodes(Pos.y, Pos.x))
                ElseIf input.Key = ConsoleKey.D2 Then
                    map.GridCodes(Pos.y, Pos.x) = 2
                    Dim largeResidential As LargeResidential = New LargeResidential()
                    largeResidential.Pos.y = Pos.y
                    largeResidential.Pos.x = Pos.x
                    game.LotObjectMatrix(Pos.y, Pos.x) = largeResidential
                    game.cityGovernment.Spend(25)
                End If
            Case ConsoleKey.D2
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Low density[1]($20) | High density[2]($30)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    map.GridCodes(Pos.y, Pos.x) = ShopType
                    Dim smallCommercial As SmallCommercial = New SmallCommercial()
                    smallCommercial.Pos.y = Pos.y
                    smallCommercial.Pos.x = Pos.x
                    game.LotObjectMatrix(Pos.y, Pos.x) = smallCommercial
                    game.cityGovernment.Spend(20)
                ElseIf input.Key = ConsoleKey.D2 Then
                    map.GridCodes(Pos.y, Pos.x) = 5
                    Dim largeCommercial As LargeCommercial = New LargeCommercial()
                    largeCommercial.Pos.y = Pos.y
                    largeCommercial.Pos.x = Pos.x
                    game.LotObjectMatrix(Pos.y, Pos.x) = largeCommercial
                    game.cityGovernment.Spend(30)
                End If
            Case ConsoleKey.D3
                map.GridCodes(Pos.y, Pos.x) = 32
                Dim industry As Industry = New Industry()
                industry.Pos.y = Pos.y
                industry.Pos.x = Pos.x
                game.LotObjectMatrix(Pos.y, Pos.x) = industry
                game.cityGovernment.Spend(30)
            Case ConsoleKey.D4
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Low volume[1]($10) | High volume[2]($20)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Dim smallRoad As SmallRoad = New SmallRoad()
                    game.LotObjectMatrix(Pos.y, Pos.x) = smallRoad
                    map.GridCodes(Pos.y, Pos.x) = 13
                    'logic for displaying proper road texture
                    If map.GridCodes(Pos.y + 1, Pos.x) = 13 Then
                        map.GridCodes(Pos.y + 1, Pos.x) = 14
                        map.GridCodes(Pos.y, Pos.x) = 14
                    ElseIf map.GridCodes(Pos.y + 1, Pos.x) = 14 Then
                        map.GridCodes(Pos.y, Pos.x) = 13
                    ElseIf map.GridCodes(Pos.y + 1, Pos.x) = 13 And map.GridCodes(Pos.y, Pos.x + 1) = 14 Then
                        map.GridCodes(Pos.y, Pos.x) = 21
                    ElseIf map.GridCodes(Pos.y + 1, Pos.x) = 13 And map.GridCodes(Pos.y, Pos.x - 1) = 13 Then
                        map.GridCodes(Pos.y, Pos.x) = 20
                    ElseIf map.GridCodes(Pos.y - 1, Pos.x) = 13 And map.GridCodes(Pos.y, Pos.x + 1) = 13 Then
                        map.GridCodes(Pos.y, Pos.x) = 19
                    ElseIf map.GridCodes(Pos.y - 1, Pos.x) = 13 And map.GridCodes(Pos.y, Pos.x - 1) = 13 Then
                        map.GridCodes(Pos.y, Pos.x) = 18
                    End If
                    game.cityGovernment.Spend(10)
                ElseIf input.Key = ConsoleKey.D2 Then
                    map.GridCodes(Pos.y, Pos.x) = 24
                    Dim largeRoad As LargeRoad = New LargeRoad()
                    game.LotObjectMatrix(Pos.y, Pos.x) = largeRoad
                    game.cityGovernment.Spend(20)
                End If
            Case ConsoleKey.D5
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Coal [1]($150) | Wind Farm[2]($225)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Dim coalStation As CoalStation = New CoalStation()
                    game.LotObjectMatrix(Pos.y, Pos.x) = coalStation
                    map.GridCodes(Pos.y, Pos.x) = 41
                    game.cityGovernment.Spend(150)
                ElseIf input.Key = ConsoleKey.D2 Then
                    Dim windFarm As WindFarm = New WindFarm()
                    game.LotObjectMatrix(Pos.y, Pos.x) = windFarm
                    map.GridCodes(Pos.y, Pos.x) = 40
                    game.cityGovernment.Spend(225)
                End If
            Case ConsoleKey.D6
                Console.WriteLine("Small park[1]($15) | Large park[2]($40)")
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Dim smallPark As SmallPark = New SmallPark()
                    game.LotObjectMatrix(Pos.y, Pos.x) = smallPark
                    map.GridCodes(Pos.y, Pos.x) = 6
                    game.cityGovernment.Spend(15)
                ElseIf input.Key = ConsoleKey.D2 Then
                    Dim largePark As LargePark = New LargePark()
                    Dim largeParkRightPointer As LargeParkPointer = New LargeParkPointer()
                    Dim largeParkDownPointer As LargeParkPointer = New LargeParkPointer()
                    Dim largeParkLowerRightPointer As LargeParkPointer = New LargeParkPointer()
                    game.LotObjectMatrix(Pos.y, Pos.x) = largePark
                    game.LotObjectMatrix(Pos.y, Pos.x + 1) = largeParkRightPointer
                    game.LotObjectMatrix(Pos.y + 1, Pos.x) = largeParkDownPointer
                    game.LotObjectMatrix(Pos.y + 1, Pos.x + 1) = largeParkLowerRightPointer
                    map.GridCodes(Pos.y, Pos.x) = 7
                    map.GridCodes(Pos.y, Pos.x + 1) = 8
                    map.GridCodes(Pos.y + 1, Pos.x) = 9
                    map.GridCodes(Pos.y + 1, Pos.x + 1) = 10
                    game.cityGovernment.Spend(40)
                End If
            Case ConsoleKey.D7
                map.GridCodes(Pos.y, Pos.x) = 37
                Dim policeStation As PoliceStation = New PoliceStation()
                game.LotObjectMatrix(Pos.y, Pos.x) = policeStation
                game.cityGovernment.Spend(75)
            Case ConsoleKey.D8
                map.GridCodes(Pos.y, Pos.x) = 33
                map.GridCodes(Pos.y, Pos.x + 1) = 34
                map.GridCodes(Pos.y + 1, Pos.x) = 35
                map.GridCodes(Pos.y + 1, Pos.x + 1) = 36
                game.cityGovernment.EstablishParliament()
                Dim parliament As Parliament = New Parliament()
                Dim parliamentPointerRight As Parliament = New Parliament()
                Dim parliamentPointerDown As ParliamentPointer = New ParliamentPointer()
                Dim parliamentPointerLowerRight As ParliamentPointer = New ParliamentPointer()
                game.LotObjectMatrix(Pos.y, Pos.x) = parliament
                game.LotObjectMatrix(Pos.y, Pos.x + 1) = parliamentPointerRight
                game.LotObjectMatrix(Pos.y + 1, Pos.x) = parliamentPointerDown
                game.LotObjectMatrix(Pos.y + 1, Pos.x + 1) = parliamentPointerLowerRight
            Case ConsoleKey.D9
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Forest[1]($5) | Water[2]($30)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    Dim forest As Forest = New Forest()
                    map.GridCodes(Pos.y, Pos.x) = 39
                    game.LotObjectMatrix(Pos.y, Pos.x) = forest
                    game.cityGovernment.Spend(5)
                ElseIf input.Key = ConsoleKey.D2 Then
                    Dim water As Water = New Water()
                    map.GridCodes(Pos.y, Pos.x) = 38
                    game.LotObjectMatrix(Pos.y, Pos.x) = water
                    game.cityGovernment.Spend(30)
                End If
        End Select
        Dim pointerPos As Position
        pointerPos.y = 12
        pointerPos.x = 16
        game.PrintMap(pointerPos, map, game)
    End Sub
    Public Sub Demolish(ByRef Pos, ByRef Game, ByRef map)
        map.GridCodes(Pos.y, Pos.x) = -1
        Dim grass As Grass = New Grass()
        Game.LotObjectMatrix(Pos.y, Pos.x) = grass
        Game.PrintMap(Pos, map, Game)
    End Sub
    Public Sub CalculateCrimeRate(Position, ByRef Game)
        Dim tempCrimeRate As Integer = 0
        For j As Integer = -2 To 2
            For i As Integer = -2 To 2
                If Game.LotObjectMatrix(Position.y + j, Position.x + i).LotIs("Nanopolis.PoliceStation",,,) Then
                    tempCrimeRate -= 50
                End If
            Next
        Next
    End Sub
    Public Function CalculateLandValueFromInternal(Pos, ByRef Game)
        Dim tempModifier As Integer = 0
        Dim tji As Integer = 0
        If Game.LotObjectMatrix(Pos.y, Pos.x).LotIs("Nanopolis.SmallRoad", Game, Pos.y, Pos.x) = True Or Game.LotObjectMatrix(Pos.y, Pos.x).LotIs("Nanopolis.LargeRoad", Game, Pos.y, Pos.x) Then
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
        Game.LotObjectMatrix(Pos.y, Pos.x).LandValue = BaseLandValue + Game.LotObjectMatrix(Pos.y, Pos.x).LandValueModifier
    End Sub
End Class
Public Class Roads
    Inherits Lot
    Public TrafficJamIndex As Integer
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
    Shadows Const Width As Integer = 2
    Shadows Const Height As Integer = 2
End Class
Public Class ParliamentPointer
    Inherits Lot
    Public PointingToY As Integer
    Public PointingToX As Integer
End Class
Public Class Construction
    Inherits Lot
End Class
Public Class PoliceStation
    Inherits Lot
End Class
Public Class PowerPlant
    Inherits Lot

End Class
Public Class CoalStation
    Inherits PowerPlant
    Shadows Const PowerOutputMW As Integer = 1000
End Class
Public Class WindFarm
    Inherits PowerPlant
    Shadows Const PowerOutputMW As Integer = 500
End Class