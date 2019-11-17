Imports System.IO
Imports System.Data
Imports System.Data.DataTable

Module Module1
    Sub Main()
        MsgBox("Welcome to Nanopolis!" & vbCrLf & "Developed by Maksim Al-Utaibi" & vbCrLf & "Make sure to maximise the console window when playing.", vbOKOnly)
        StartMenu()
    End Sub
    Sub StartMenu()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("---MAIN MENU---" & vbCrLf)
        Console.ResetColor()
        Console.WriteLine("Please enter one of the keys below:")
        Console.WriteLine("[1] New game")
        Console.WriteLine("[2] Load a previously saved game")
        Console.WriteLine("[3] Start a tutorial game")
        Console.WriteLine("[4] View key bindings")
        Console.WriteLine("[5] Graphics options")
        Console.WriteLine("[6] Quit to desktop")
        Dim MenuCode As ConsoleKeyInfo = Console.ReadKey(True)
        If MenuCode.Key = ConsoleKey.D1 Then
            NewGame()
        ElseIf MenuCode.Key = ConsoleKey.D2 Then
            LoadGame(Nothing, True)
        ElseIf MenuCode.Key = ConsoleKey.D3 Then
            Tutorial()
        ElseIf MenuCode.Key = ConsoleKey.D4 Then
            KeyBindMenu()
        ElseIf MenuCode.Key = ConsoleKey.D5 Then
            GraphicsMenu()
        ElseIf MenuCode.Key = ConsoleKey.D6 Then
            Stop
        Else
            StartMenu()
        End If
    End Sub
    Sub MainMenu(map)
        Console.Clear()
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("---MAIN MENU---" & vbCrLf)
        Console.ResetColor()
        Console.WriteLine("Please enter one of the following keys:")
        Console.WriteLine("[1] New game")
        Console.WriteLine("[2] Load a previously saved game")
        Console.WriteLine("[3] Start a tutorial game")
        Console.WriteLine("[4] View key bindings")
        Console.WriteLine("[5] Graphics options")
        Console.WriteLine("[6] Quit to desktop")
        Console.WriteLine("[ESC] Return to game")
        Dim MenuCode As ConsoleKeyInfo = Console.ReadKey(True)
        If MenuCode.Key = ConsoleKey.D1 Then
            NewGame()
        ElseIf MenuCode.Key = ConsoleKey.D2 Then
            LoadGame(map, False)
        ElseIf MenuCode.Key = ConsoleKey.D3 Then
            Tutorial()
        ElseIf MenuCode.Key = ConsoleKey.D4 Then
            KeyBindMenu()
        ElseIf MenuCode.Key = ConsoleKey.D5 Then
            GraphicsMenu()
        ElseIf MenuCode.Key = ConsoleKey.D6 Then
            Stop
        ElseIf MenuCode.Key = ConsoleKey.Escape Then
            map.printmap(0, 16)
        Else
            MainMenu(map)
        End If
    End Sub
    Function GraphicsMenu()
        Console.WriteLine("--GRAPHICS OPTIONS--")
        Dim MenuCode As Integer = Console.ReadLine()
    End Function
    Sub KeyBindMenu()
        Console.Clear()
        Console.WriteLine("--KEY BINDINGS--")
        Dim MenuCode As Integer = Console.ReadLine()
    End Sub
    Sub LoadGame(map, isStart)
        Dim PathName As String
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write("Input file name:")
        Console.ResetColor()
        PathName = Console.ReadLine()
        Try
            Dim FileReader As New System.IO.StreamReader(PathName)
            Dim Line As String
            While FileReader.EndOfStream <> True
                Line = FileReader.ReadLine()
                Console.WriteLine(Line)
            End While
            FileReader.Close()
        Catch ex As Exception
            Console.BackgroundColor = ConsoleColor.Red
            Console.ForegroundColor = ConsoleColor.Black
            Console.WriteLine(ex.Message)
            Console.ResetColor()
            If isStart = True Then
                StartMenu()
            Else
                MainMenu(map)
            End If
        End Try
    End Sub
    Sub Tutorial()

    End Sub
    Public Sub NewGame()
        Dim map As Map = New Map()
        Console.WriteLine("Generating map...")
        For i As Integer = 0 To 32
            For j As Integer = 0 To 24
                Map.GridCodes(j, i) = -1
            Next
        Next
        map.PrintMap(14, 16)
    End Sub
    Sub SaveGame()
        Dim map As Map = New Map()
        Dim filename As String
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("Enter the name for this save file:")
        Console.ResetColor()
        filename = Console.ReadLine()
        filename = filename & ".txt"
        Try
            Dim FileWriter As New System.IO.StreamWriter(filename)
            FileWriter.WriteLine(Map.GridCodes.ToString)
            FileWriter.Close()
        Catch ex As Exception
        End Try
        Console.Clear()
        map.PrintMap(0, 16)
    End Sub

End Module