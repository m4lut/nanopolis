Imports System.IO
Imports System.Data
Imports System.Data.DataTable

Module Module1
    Sub StartMenu()
        MsgBox("Welcome to Nanopolis!" & vbCrLf & "Developed by Maksim Al-Utaibi" & vbCrLf & "Make sure to maximise the console window when playing.", vbOKOnly)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("---MAIN MENU---" & vbCrLf)
        Console.ResetColor()
        Console.WriteLine("Please enter one of the bers below:")
        Dim MenuCode As Integer
        Console.WriteLine("1 New game")
        Console.WriteLine("2 Load a previously saved game")
        Console.WriteLine("3 Start a tutorial game")
        Console.WriteLine("4 Display key bindings")
        Console.WriteLine("5 Graphics options")
        Console.WriteLine("6 Quit to desktop")
        Try
            MenuCode = Console.ReadLine()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
        Select Case MenuCode
            Case 1
                NewGame()
            Case 2
                LoadGame()
            Case 3
                Tutorial()
            Case 4
                KeyBindMenu()
            Case 5
                GraphicsMenu()
            Case 6
                End
        End Select
    End Sub

    Sub MainMenu()
        Console.Clear()
        Console.WriteLine("---MAIN MENU---" & vbCrLf)
        Console.WriteLine("Please enter one of the bers below:")
        Dim MenuCode As Integer
        Console.WriteLine("1 New game")
        Console.WriteLine("2 Load a previously saved game")
        Console.WriteLine("3 Start a tutorial game")
        Console.WriteLine("4 Display key bindings")
        Console.WriteLine("5 Pick resolution")
        Console.WriteLine("6 Quit to desktop")
        MenuCode = Console.ReadLine()
        Select Case MenuCode
            Case 1
                NewGame()
            Case 2
                LoadGame()
            Case 3
                Tutorial()
            Case 4
                KeyBindMenu()
            Case 5
                GraphicsMenu()
            Case 6
                End
        End Select
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
    Sub LoadGame()

        Try
            Dim FileReader As New System.IO.StreamReader("name.txt")
            Dim Line As String
            While FileReader.EndOfStream <> True
                Line = FileReader.ReadLine()
                Console.WriteLine(Line)
            End While
            FileReader.Close()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub Tutorial()

    End Sub
    Public Sub NewGame()
        Dim map As Map = New Map()
        Console.WriteLine("Generating map...")
        Dim grid As Map = New Map()
        Map.GridCodes = {{32, 33, 34, 35, 36, 37, 38, 39, 40, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31}, {-1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31}}
        map.PrintMap(1, 16)
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
        Map.PrintMap(1, 16)
    End Sub


    Sub Main()

        StartMenu()
    End Sub

End Module