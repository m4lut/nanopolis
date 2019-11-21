Imports System.Diagnostics
Module Module1
    Sub Main()

        Dim stopwatch As StopWatch = New Stopwatch()
        stopwatch.Start()
        Thread.Sleep(10000)
        stopwatch.Stop()
        'Get the elapsed time as a TimeSpan value.
        Dim ts As TimeSpan = stopwatch.Elapsed

        'Format And display the TimeSpan value.
        Dim elapsedTime As String = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10)
        Console.WriteLine("RunTime " + elapsedTime)
    End Sub
End Module
