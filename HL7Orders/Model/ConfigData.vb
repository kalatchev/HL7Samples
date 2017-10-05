Imports System.Xml.Serialization

<Serializable>
Public Class ConfigData

    'Default data
    Public Property OriginApp As String = "BestHIS"
    Public Property OriginFacility As String = "МБАЛ АД"
    Public Property TargetApp As String = "iLab"
    Public Property TargetFacility As String = "Клинична лаборатория"
    Public Property RemoteHost As String = "demo.skyware-group.com"
    Public Property RemoetPort As Integer = 2575
    Public Property OrderNumberNamespace As String = "R"

    'Singletone pattern
    'Singletone instance
    Private Shared mInstance As ConfigData

    'Orovate constructor to prevent instantination
    Private Sub New()

    End Sub

    'Shared (static) function to get one and only one shared (static) instance
    Public Shared Function Instance() As ConfigData
        If ConfigData.mInstance Is Nothing Then
            'Read data from conf file
            mInstance = XMLUtils.ReadObject(Of ConfigData)(IO.Path.Combine(Application.StartupPath, "DemoData", "conf.xml"))
        End If
        Return ConfigData.mInstance
    End Function

End Class
