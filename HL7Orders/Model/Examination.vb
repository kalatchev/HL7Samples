Imports System.Xml.Serialization

<Serializable>
Public Class Examination
    Public Property LoincCode As String
    Public Property Name As String
    Public Property HisId As String
    <XmlElement(DataType:="boolean")>
    Public Property IsPanel As Boolean
End Class
