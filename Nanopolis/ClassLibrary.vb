Public Structure Position
    Public x As Integer
    Public y As Integer
End Structure
Structure Texture
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
    Public CityGovernment As Government
    Public LotObjectMatrix(24, 31) As Lot
    Sub NextWeek(ByVal Game, ByRef Map)
        Dim pos As Position
        pos.x = 0
        pos.y = 0
        For pos.y = 0 To 24
            For pos.x = 0 To 31

            Next
        Next
    End Sub
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
        Dim industrialTexture As Texture
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
    Sub NewMap(game, IsStart)
        Randomize()
        Dim thisGame As Game = New Game()
        Dim map As Map = New Map()
        Dim grassProb As Integer
        Dim waterProb As Integer
        Dim forestProb As Integer
        Dim totalProb As Integer
        Console.WriteLine("Create a plain map?[y/n]")
        Dim plainMapChoice As ConsoleKeyInfo = Console.ReadKey(True)
        If plainMapChoice.Key = ConsoleKey.N Then
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
                        thisGame.LotObjectMatrix(i, j) = water
                    ElseIf GeneratedTile > waterProb And GeneratedTile <= (grassProb + waterProb) Then
                        Dim grass As Grass = New Grass()
                        Map.GridCodes(i, j) = -1
                        thisGame.LotObjectMatrix(i, j) = grass
                    ElseIf GeneratedTile > (grassProb + waterProb) Then
                        Dim forest As Forest = New Forest()
                        Map.GridCodes(i, j) = 39
                        thisGame.LotObjectMatrix(i, j) = forest
                    End If
                Next
            Next
            Dim cityGovernment As Government = New Government()
            cityGovernment.EstablishGovernment()
            game.CityGovernment = cityGovernment
            game.PrintMap(14, 16, map, game)
        ElseIf plainMapChoice.Key = ConsoleKey.Y Then
            For i As Integer = 0 To 29
                For j As Integer = 0 To 32
                    Map.GridCodes(i, j) = -1
                Next
            Next
            Dim cityGovernment As Government = New Government()
            cityGovernment.EstablishGovernment()
            thisGame.CityGovernment = cityGovernment
            thisGame.PrintMap(14, 16, map, thisGame)
        ElseIf plainMapChoice.Key = ConsoleKey.Escape Then
            StartMenu()
        Else
            If IsStart = True Then
                NewGame(False, game)
            Else
                NewGame(True, Nothing)
            End If
        End If
    End Sub
    Sub ComputeLandValue(pos, ByRef lotobjectmatrix)
        Dim TempLandValue(2, 2) As Integer
    End Sub
    Public Sub PrintMap(ByRef SelectorY, ByRef SelectorX, map, ByRef game)
        Console.Clear()
        Dim pos As Position
        For pos.y = 0 To 24
            For CurrentLine As Integer = 0 To 3
                For pos.x = 0 To 32
                    If CurrentLine = 0 Then
                        Select Case map.GridCodes(pos.y, pos.x)
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
                        Select Case map.GridCodes(pos.y, pos.x)
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
                                Console.ForegroundColor = ConsoleColor.Gray
                                Console.Write("|")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.ForegroundColor = ConsoleColor.DarkGreen
                                Console.Write(":::")
                                Console.BackgroundColor = ConsoleColor.DarkGray
                                Console.ForegroundColor = ConsoleColor.White
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
                        Select Case map.GridCodes(pos.y, pos.x)
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
                        Select Case map.GridCodes(pos.y, pos.x)
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
                                Console.Write("||")
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.Write("  ")
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
        Console.Write("Y" & Int(SelectorY))
        Console.Write("X" & Int(SelectorX))
        Console.WriteLine()
        Console.WriteLine(game.cityGovernment.GetTreasury)
        map.MapSelection(SelectorY, SelectorX, map, game)
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
Public Class Map
    Public Pos As Position
    Public Shared GridCodes(30, 33) As Integer
    Public Shared NextTurnGridCodes(30, 33) As Integer
    Public Sub MapSelection(ByRef Pos, ByRef map, ByRef game)
        Console.TreatControlCAsInput = True
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("Navigate[WASD] | Select[ENTER] | Main Menu[ESC]")
        Console.ResetColor()
        Dim lot As Lot = New Lot()
        Dim Selected As Boolean = False
        Dim Key As ConsoleKey
        Dim Choice As ConsoleKey
        While Selected = False
            Key = Console.ReadKey(True).Key
            If Key = ConsoleModifiers.Control AndAlso Key = ConsoleKey.A Then
                Pos.x -= 5
                game.PrintMap(Pos, map, game)
            ElseIf Key = ConsoleModifiers.Control AndAlso Key = ConsoleKey.D Then
                Pos.x += 5
                game.PrintMap(Pos, map, game)
            ElseIf Key = ConsoleModifiers.Control AndAlso Key = ConsoleKey.S Then
                Pos.y += 5
                game.PrintMap(Pos, map, game)
            ElseIf Key = ConsoleModifiers.Control AndAlso Key = ConsoleKey.W Then
                Pos.y -= 5
                game.PrintMap(Pos, map, game)
            ElseIf Key = ConsoleModifiers.Control AndAlso Key = ConsoleKey.Enter Then
                Selected = True
            End If
            If Key = ConsoleKey.A Then
                Pos.x -= 1
                game.PrintMap(Pos, map, game)
            ElseIf Key = ConsoleKey.D Then
                Pos.x += 1
                game.PrintMap(Pos, map, game)
            ElseIf Key = ConsoleKey.S Then
                Pos.y += 1
                game.PrintMap(Pos, map, game)
            ElseIf Key = ConsoleKey.W Then
                Pos.y -= 1
                game.PrintMap(Pos, map, game)
            ElseIf Key = ConsoleKey.Enter Then
                Selected = True
            ElseIf Key = ConsoleKey.Escape Then
                MainMenu(map)
            End If
            Console.BackgroundColor = ConsoleColor.Gray
            Console.ForegroundColor = ConsoleColor.Black
            Console.WriteLine("Demolish[D] | Build[B] | Cancel[C]")
            Console.ResetColor()
            Choice = Console.ReadKey(True).Key
            If Choice = ConsoleKey.D Then
                lot.Demolish(Pos, game, map)
            ElseIf Choice = ConsoleKey.B Then
                lot.Build(Pos, game, map)
            ElseIf Choice = ConsoleKey.C Then
                game.MapSelection(Pos, map, game)
            ElseIf Choice = ConsoleKey.Escape Then
                MainMenu(map)
            End If
        End While
    End Sub
End Class
Public Class Lot
    Const Width As Integer = 1
    Const Height As Integer = 1
    Public Pos As Position
    Public LandValueModifier As Integer
    Public ActualLandValue As Integer
    Public WorkPlace As Position
    Public ShoppingPlace As Position
    Public ConnectedToRoad As Boolean
    Public Abandoned As Boolean
    Public WeeksUntilAbandoned As Boolean = 3
    Function RoadConnectionCheck(Position, ByRef LotObjectMatrix)
        For j As Integer = -1 To 1
            For i As Integer = -1 To 1
                If LotObjectMatrix(Position.y, Position.x).IsSmallRoad Or LotObjectMatrix(Position.y, Position.x).IsLargeRoad Then
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
                If LotObjectMatrix(workPlace.y, workPlace.x).IsCommercialLot And FindingWork Then
                    Found = True
                ElseIf LotObjectMatrix(shoppingPlace.y, shoppingPlace.x).IsCommercialLot And FindingWork = False Then
                    Found = True
                ElseIf LotObjectMatrix(shoppingPlace.y, shoppingPlace.x).IsIndustry And FindingWork = False Then
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
                    game.LotObjectMatrix(Pos.y, Pos.x + 1) = Nothing
                    game.LotObjectMatrix(Pos.y + 1, Pos.x) = Nothing
                    game.LotObjectMatrix(Pos.y + 1, Pos.x + 1) = Nothing
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
            Case ConsoleKey.D9
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Forest[1]($5) | Water[2]($30)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    map.GridCodes(Pos.y, Pos.x) = 39
                    game.cityGovernment.Spend(5)
                ElseIf input.Key = ConsoleKey.D2 Then
                    map.GridCodes(Pos.y, Pos.x) = 38
                    game.cityGovernment.Spend(30)
                End If
        End Select
        game.PrintMap(14, 16, map, game)
    End Sub
    Public Sub Demolish(ByRef Pos, ByRef game, ByRef map)
        map.GridCodes(Pos.y, Pos.x) = -1
        game.PrintMap(14, 16, map, game)
        If map.GridCodes(Pos.y, Pos.x) = 33 Or map.GridCodes(Pos.y, Pos.x) = 34 Or map.GridCodes(Pos.y, Pos.x) = 35 Or map.GridCodes(Pos.y, Pos.x) = 36 Then
            game.cityGovernment.RemoveParliament()
        End If
        Dim grass As Grass = New Grass()
        game.LotObjectMatrix(Pos.y, Pos.x) = grass
    End Sub
    Protected Function CalculateLandValue(Position, LotObjectMatrix)
        Dim tempLandValue As Integer
        Dim NoOfParliamentPointers As Integer = 0
        Dim NoOfLargeParkPointers As Integer = 0
        For j As Integer = -1 To 1
            For i As Integer = -1 To 1
                If NoOfLargeParkPointers <> 0 And LotObjectMatrix(Position.y + j, Position.x + i).IsLargeParkPointer Then
                    Continue For
                End If
                If NoOfParliamentPointers <> 0 And LotObjectMatrix(Position.y + j, Position.x + i).IsParliamentPointer Then
                    Continue For
                End If
                tempLandValue += LotObjectMatrix(Position.y + j, Position.x + i).LandValueModifier
            Next
        Next
        LotObjectMatrix(Position.y, Position.x).ActualLandValue = tempLandValue
        Return LotObjectMatrix
    End Function
    Protected Function GetLandValue()
        Return ActualLandValue
    End Function
End Class
Public Class Roads
    Inherits Lot
    Public TrafficJamIndex As Integer
    Public Function CalcTJI(Size, RoadGraph)
        Return TrafficJamIndex
    End Function
End Class
Public Class SmallRoad
    Inherits Roads
    Shadows Const BaseLandValue As Integer = 0
End Class
Public Class LargeRoad
    Inherits Roads
    Shadows Const BaseLandValue As Integer = 0
End Class
Public Class Nature
    Inherits Lot
End Class
Public Class Grass
    Inherits Nature
    Shadows Const BaseLandValue As Integer = 0

End Class
Public Class Water
    Inherits Nature
    Shadows Const BaseLandValue As Integer = 0

End Class
Public Class Forest
    Inherits Nature
    Shadows Const BaseLandValue As Integer = 0
End Class
Public Class ResidentialLot
    Inherits Lot
    Private DwellerAmount As Integer
    Private WorkingClassProportion As Integer
    Private MiddleClassProportion As Integer
    Private UpperClassProportion As Integer
End Class
Public Class SmallResidential
    Inherits ResidentialLot
    Shadows Const BaseLandValue As Integer = 15
End Class
Public Class LargeResidential
    Inherits ResidentialLot
    Shadows Const BaseLandValue As Integer = 30
End Class
Public Class CommercialLot
    Inherits Lot
    Shadows Const BaseLandValue As Integer = 15
    Const BaseRevenue As Integer = 0
    Public Revenue As Integer
    Public NumberOfWorkers As Integer
    Sub EstablishStore()
        Revenue = BaseRevenue
        ActualLandValue = BaseLandValue
    End Sub
    Sub GainRevenue()

    End Sub
    Sub PaySalesTax()

    End Sub
End Class
Public Class SmallCommercial
    Inherits CommercialLot
    Shadows Const BaseLandValue As Integer = 40
End Class
Public Class LargeCommercial
    Inherits CommercialLot
    Shadows Const BaseLandValue As Integer = 50
End Class
Public Class Park
    Inherits Lot

End Class
Public Class SmallPark
    Inherits Park
    Shadows Const BaseLandValue As Integer = 15
End Class
Public Class LargePark
    Inherits Park
    Shadows Const BaseLandValue As Integer = 35
    Shadows Const Height As Integer = 2
    Shadows Const Width As Integer = 2
End Class
Public Class LargeParkPointer
    Inherits Park
    Public PointingTo As String
End Class
Public Class Industry
    Inherits Lot
    Shadows Const BaseLandValue As Integer = 5
End Class
Public Class Parliament
    Inherits Lot
    Shadows Const BaseLandValue As Integer = 25
    Shadows Const Width As Integer = 2
    Shadows Const Height As Integer = 2
End Class
Public Class ParliamentPointer
    Inherits Lot
    Public PointingTo As String
End Class
Public Class Construction
    Inherits Lot
    Shadows Const BaseLandValue As Integer = 0
End Class
Public Class PoliceStation
    Inherits Lot
    Shadows Const BaseLandValue As Integer = 5
End Class
Public Class PowerPlant
    Inherits Lot

End Class
Public Class CoalStation
    Inherits PowerPlant
    Shadows Const BaseLandValue As Integer = 5
End Class
Public Class WindFarm
    Inherits PowerPlant
    Shadows Const BaseLandValue As Integer = 5
End Class
Public Class Government
    Const StartingTreasury As Integer = 20000
    Const StartingExecPower As Integer = 100
    Public Treasury As Integer
    Public ExecutivePower As Integer
    Public HasParliament As Boolean
    Public ApprovalRate As Integer
    Public SalesTaxRate As Integer
    Public EnvironmentStatus As Integer
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
    Public Sub ShowPolicyMenu()

    End Sub
    Sub ShowPolicyAlert()

    End Sub
End Class