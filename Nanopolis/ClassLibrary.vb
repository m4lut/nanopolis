Public Class Map
    Public Shared GridCodes(30, 33) As Integer
    Public Shared NextTurnGridCodes(25, 32) As Integer
    Public Shared SelectorY As Integer = 14
    Public Shared SelectorX As Integer = 16
    Public Sub PrintMap(ByRef SelectorY, ByRef SelectorX)
        Console.Clear()
        For y As Integer = 0 To 24
            For CurrentLine As Integer = 0 To 3
                For x As Integer = 0 To 32
                    If CurrentLine = 0 Then
                        Select Case GridCodes(y, x)
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
                        End Select
                    ElseIf CurrentLine = 1 Then
                        Select Case GridCodes(y, x)
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
                        End Select
                    ElseIf CurrentLine = 2 Then
                        Select Case GridCodes(y, x)
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
                        End Select
                    ElseIf CurrentLine = 3 Then
                        Select Case GridCodes(y, x)
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
                        End Select
                    End If
                Next
                Console.WriteLine()
            Next
        Next
        Console.Write("Y" & Int(SelectorY))
        Console.Write("X" & Int(SelectorX))
        MapSelection(SelectorY, SelectorX, GridCodes)
    End Sub
    Public Sub MapSelection(ByRef SelectorY, ByRef SelectorX, ByRef GridCodes)
        Console.TreatControlCAsInput = True
        Dim map As Map = New Map()
        Dim Lot As Lot = New Lot()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("Navigate[WASD] | Select[ENTER] | Main Menu[ESC]")
        Console.ResetColor()
        Dim Selected As Boolean = False
        Dim Key As ConsoleKey
        Dim Choice As ConsoleKey
        While Selected = False
            Key = Console.ReadKey(True).Key
            If Key = ConsoleModifiers.Control AndAlso Key = ConsoleKey.A Then
                SelectorX -= 5
                PrintMap(SelectorY, SelectorX)
            ElseIf Key = ConsoleModifiers.Control AndAlso Key = ConsoleKey.D Then
                SelectorX += 5
                PrintMap(SelectorY, SelectorX)
            ElseIf Key = ConsoleModifiers.Control AndAlso Key = ConsoleKey.S Then
                SelectorY += 5
                PrintMap(SelectorY, SelectorX)
            ElseIf Key = ConsoleModifiers.Control AndAlso Key = ConsoleKey.W Then
                SelectorY -= 5
                PrintMap(SelectorY, SelectorX)
            ElseIf Key = ConsoleModifiers.Control AndAlso Key = ConsoleKey.Enter Then
                Selected = True
            End If
            If Key = ConsoleKey.A Then
                SelectorX -= 1
                PrintMap(SelectorY, SelectorX)
            ElseIf Key = ConsoleKey.D Then
                SelectorX += 1
                PrintMap(SelectorY, SelectorX)
            ElseIf Key = ConsoleKey.S Then
                SelectorY += 1
                PrintMap(SelectorY, SelectorX)
            ElseIf Key = ConsoleKey.W Then
                SelectorY -= 1
                PrintMap(SelectorY, SelectorX)
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
                Lot.Demolish(Map.GridCodes, SelectorY, SelectorX, map)
            ElseIf Choice = ConsoleKey.B Then
                Lot.Build(Map.GridCodes, SelectorY, SelectorX, map)
            ElseIf Choice = ConsoleKey.C Then
                map.MapSelection(SelectorY, SelectorX, Map.GridCodes)
            ElseIf Choice = ConsoleKey.Escape Then
                MainMenu(map)
            End If
        End While
    End Sub
End Class

Public Class Lot
    Public xPos As Integer
    Public yPos As Integer
    Public Cost As Integer
    Public RealLandValue As Integer
    Public Shared LotObjectMatrix(30, 33)
    Public Function GetPos(ByVal Gridcodes(,), yPos, xPos)
        Return yPos
        Return xPos
    End Function
    Public Sub Build(ByRef GridCodes, ByRef yPos, ByRef xPos, ByRef map)
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
                    GridCodes(yPos, xPos) = 1
                    Dim smallResidential As SmallResidential = New SmallResidential()
                    smallResidential.yPos = yPos
                    smallResidential.xPos = xPos
                    LotObjectMatrix(yPos, xPos) = smallResidential
                ElseIf input.Key = ConsoleKey.D2 Then
                    GridCodes(yPos, xPos) = 2
                    Dim largeResidential As LargeResidential = New LargeResidential()
                    largeResidential.yPos = yPos
                    largeResidential.xPos = xPos
                    LotObjectMatrix(yPos, xPos) = largeResidential
                End If
            Case ConsoleKey.D2
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Low density[1]($20) | High density[2]($30)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    GridCodes(yPos, xPos) = ShopType
                    Dim smallCommercial As SmallCommercial = New SmallCommercial()
                    smallCommercial.yPos = yPos
                    smallCommercial.xPos = xPos
                    LotObjectMatrix(yPos, xPos) = smallCommercial
                ElseIf input.Key = ConsoleKey.D2 Then
                    GridCodes(yPos, xPos) = 5
                    Dim largeCommercial As LargeCommercial = New LargeCommercial()
                    largeCommercial.yPos = yPos
                    largeCommercial.xPos = xPos
                    LotObjectMatrix(yPos, xPos) = largeCommercial
                End If
            Case ConsoleKey.D3
                GridCodes(yPos, xPos) = 32
                Dim industry As Industry = New Industry()
                industry.yPos = yPos
                industry.xPos = xPos
                LotObjectMatrix(yPos, xPos) = industry
            Case ConsoleKey.D4
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Low volume[1]($10) | High volume[2]($20)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    GridCodes(yPos, xPos) = 13
                    'logic for displaying proper road texture
                    If GridCodes(yPos + 1, xPos) = 13 Then
                        GridCodes(yPos + 1, xPos) = 14
                        GridCodes(yPos, xPos) = 14
                    ElseIf GridCodes(yPos + 1, xPos) = 14 Then
                        GridCodes(yPos, xPos) = 13
                    ElseIf GridCodes(yPos + 1, xPos) = 13 And GridCodes(yPos, xPos + 1) = 14 Then
                        GridCodes(yPos, xPos) = 21
                    ElseIf GridCodes(yPos + 1, xPos) = 13 And GridCodes(yPos, xPos - 1) = 13 Then
                        GridCodes(yPos, xPos) = 20
                    ElseIf GridCodes(yPos - 1, xPos) = 13 And GridCodes(yPos, xPos + 1) = 13 Then
                        GridCodes(yPos, xPos) = 19
                    ElseIf GridCodes(yPos - 1, xPos) = 13 And GridCodes(yPos, xPos - 1) = 13 Then
                        GridCodes(yPos, xPos) = 18
                    End If
                ElseIf input.Key = ConsoleKey.D2 Then
                    GridCodes(yPos, xPos) = 24
                End If
            Case ConsoleKey.D5
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Coal plant[1]($150) | Wind Farm[2]($225)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    GridCodes(yPos, xPos) = 41
                ElseIf input.Key = ConsoleKey.D2 Then
                    GridCodes(yPos, xPos) = 40
                End If
            Case ConsoleKey.D6
                Console.WriteLine("Small park[1]($15) | Large park[2]($35)")
                If input.Key = ConsoleKey.D2 Then
                    GridCodes(yPos, xPos) = 6
                    GridCodes(yPos, xPos + 1) = 7
                    GridCodes(yPos + 1, xPos) = 8
                    GridCodes(yPos + 1, xPos + 1) = 9
                ElseIf input.Key = ConsoleKey.D1 Then
                    GridCodes(yPos, xPos) = 7
                End If
            Case ConsoleKey.D8
                GridCodes(yPos, xPos) = 33
                GridCodes(yPos, xPos + 1) = 34
                GridCodes(yPos + 1, xPos) = 35
                GridCodes(yPos + 1, xPos + 1) = 36
            Case ConsoleKey.D9
                Console.BackgroundColor = ConsoleColor.Gray
                Console.ForegroundColor = ConsoleColor.Black
                Console.WriteLine("Forest[1]($5) | Water[2]($30)")
                Console.ResetColor()
                input = Console.ReadKey(True)
                If input.Key = ConsoleKey.D1 Then
                    GridCodes(yPos, xPos) = 39
                ElseIf input.Key = ConsoleKey.D2 Then
                    GridCodes(yPos, xPos) = 38
                End If
        End Select
        map.PrintMap(14, 16)
    End Sub
    Public Sub Demolish(ByRef GridCodes, ByRef yPos, ByRef xPos, ByRef map)
        GridCodes(yPos, xPos) = -1
        map.Printmap(14, 16)
        Me.Finalize()
    End Sub

    Protected Function ChangeLandValue()

    End Function
    Protected Function CalcLandValue()

    End Function

End Class
Public Class Roads
    Inherits Lot

End Class
Public Class SmallRoad
    Inherits Roads
    Shadows Const Cost As Integer = 10
End Class
Public Class LargeRoad
    Inherits Roads
    Shadows Const Cost As Integer = 20
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
End Class
Public Class SmallResidential
    Inherits ResidentialLot
    Shadows Const BaseLandValue As Integer = 15
    Shadows Const Cost As Integer = 15
End Class
Public Class LargeResidential
    Inherits ResidentialLot
    Shadows Const BaseLandValue As Integer = 30
    Shadows Const Cost As Integer = 25
End Class
Public Class CommercialLot
    Inherits Lot

End Class
Public Class SmallCommercial
    Inherits CommercialLot
    Shadows Const BaseLandValue As Integer = 40
    Shadows Const Cost As Integer = 20
End Class
Public Class LargeCommercial
    Inherits CommercialLot
    Shadows Const BaseLandValue As Integer = 50
    Shadows Const Cost As Integer = 30
End Class
Public Class Park
    Inherits Lot

End Class
Public Class SmallPark
    Inherits Park
    Shadows Const BaseLandValue As Integer = 15
    Shadows Const Cost As Integer = 15
End Class
Public Class LargePark
    Inherits Park
    Shadows Const BaseLandValue As Integer = 35
    Shadows Const Cost As Integer = 35
End Class
Public Class Industry
    Inherits Lot
    Shadows Const BaseLandValue As Integer = 5
End Class
Public Class Parliament
    Inherits Lot
    Shadows Const BaseLandValue As Integer = 25
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
    Shadows Const Cost As Integer = 150
End Class
Public Class WindFarm
    Inherits PowerPlant
    Shadows Const BaseLandValue As Integer = 5
    Shadows Const Cost As Integer = 225
End Class
Public Class Government

End Class