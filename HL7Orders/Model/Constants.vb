Public Class Constants

    Public Enum PIDTypes As Integer
        Anonymous = 0
        EGN = 1
        ENCh = 2
    End Enum

    Public Enum Actions As Integer
        [New]
        Update
        Cancel
    End Enum

    Public Enum Priorities
        Normal
        Emergency
    End Enum

End Class
