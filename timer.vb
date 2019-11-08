Imports System.Timers
Module Module1

    Sub Main()
        aTimer.AutoReset = True
        aTimer.Interval = 2000 '2 seconds
        AddHandler aTimer.Elapsed, AddressOf tick
        aTimer.Start()
        Console.ReadKey()
    End Sub

    Dim aTimer As New System.Timers.Timer

    Private Sub tick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Console.WriteLine("tick")
    End Sub

End Module
