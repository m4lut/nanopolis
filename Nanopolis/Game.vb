Imports System.IO
Module Module1
    Sub StartMenu()
        MsgBox("Welcome to Nanopolis!" & vbCrLf & "Developed by Maksim Al-Utaibi" & vbCrLf & "Make sure to maximise the console window when playing.", vbOKOnly)
        Console.BackgroundColor = ConsoleColor.Gray
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("---MAIN MENU---" & vbCrLf)
        Console.ResetColor()
        Console.WriteLine("Please enter one of the numbers below:")
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
        Console.WriteLine("Please enter one of the numbers below:")
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
        Console.WriteLine("--LOAD GAME--")
        Dim MenuCode As Integer = Console.ReadLine()
    End Sub
    Sub Tutorial()

    End Sub
    Public Sub NewGame()
        Console.WriteLine("Generating map...")
        Dim grid As Grid = New Grid()
        Grid.GridCodes = {{32, 33, 34, 35, 36, 37, 38, 39, 40, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31}, {-1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31}}
        grid.PrintMap(1, 16)
    End Sub
    Sub SaveGame()
        Dim MenuCode As Integer = Console.ReadLine()
    End Sub

    Sub Main()

        StartMenu()
    End Sub

End Module