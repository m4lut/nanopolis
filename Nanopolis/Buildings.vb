Public Class Map
    Public Grid(,) As Integer = {{-1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31}, {-1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31}}
End Class
Public Class Buildings
    Public Sub GetPos(Map)

    End Sub
    Public Sub Build(Grid, y, x)

    End Sub
    Public Sub Remove(Grid, y, x)

    End Sub
End Class
Public Class Roads
    Inherits Buildings

End Class
Public Class Nature
    Inherits Buildings

End Class
Public Class ResidentialLots
    Inherits Buildings

End Class
Public Class CommericalLots
    Inherits Buildings

End Class
Public Class Parks
    Inherits Buildings

End Class
Public Class Industry
    Inherits Buildings

End Class
Public Class Parliament
    Inherits Buildings

End Class
Public Class Construction
    Inherits Buildings

End Class
Public Class PoliceStations
    Inherits Buildings

End Class
Public Class Energy
    Inherits Buildings

End Class