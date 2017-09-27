Public Class Order
    Public Property OrderId As String = "1"
    Public Property MessageId As String = Guid.NewGuid.ToString
    Public Property Action As Constants.Actions = Constants.Actions.New
    Public Property Priority As Constants.Priorities = Constants.Priorities.Normal
    Public Property Patient As Patient
    Public Property OrderingDoctor As Doctor
    Public Property Location As Location
    Public Property ToVisit As Date?
    Public Property Examinations As List(Of Examination)
    Public Property Note As String
    Public Property Diagnosis As New Dictionary(Of String, String)
End Class
