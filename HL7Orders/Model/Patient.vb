Public Class Patient

    Public Property EntityId As String

    Public Property PIDType As Constants.PIDTypes
    Public Property PID As String

    Public Property MedicalCase As String
    Public Property AmbulatoryNumber As String

    Public Property GivenName As String
    Public Property MiddleName As String
    Public Property FamilyName As String
    Public Property DateOfBirth As Date?
    Public Property Gender As String = "U"

End Class
