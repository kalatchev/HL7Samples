Imports System.Xml
Imports System.Xml.Serialization

<Serializable>
Public Class Order

    Public Property OrderId As String = "1"

    Public Property MessageId As String = Guid.NewGuid.ToString

    <XmlElement(DataType:="dateTime")>
    Public Property Created As DateTime = Now()

    <XmlIgnore>
    Public Property Action As Constants.Actions = Constants.Actions.New

    Public Property Priority As Constants.Priorities = Constants.Priorities.Normal

    Public Property OrdDoctorUIN As String 'Just storega

    <XmlIgnore>
    Public Property OrderingDoctor As Doctor

    Public Property Location As Location

    <XmlElement(DataType:="dateTime", IsNullable:=True)>
    Public Property FutureVistTime As Date?

    <XmlArray("ExamCodes"), XmlArrayItem(GetType(String), ElementName:="Code")>
    Public Property ExamCodes As List(Of String) 'just storage

    '<XmlArray("Examinations"), XmlArrayItem(GetType(Examination), ElementName:="Examination")>
    <XmlIgnore>
    Public Property Examinations As List(Of Examination)

    Public Property Note As String

    'Public Property PatinetId As Integer
    '<XmlIgnore>
    'Public Property Patient As Patient
    Public Property IsSent As Boolean = False

    Public Property MedicalCaseId As Integer
    <XmlIgnore>
    Public Property MedicalCase As MedicalCase

End Class
