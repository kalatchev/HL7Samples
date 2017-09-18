Public Class FormMain

    Private Const REMOTE_HOST As String = "demo.skyware-group.com"
    Private Const REMOTE_PORT As Integer = 2575

    Private mMessage As NHapi.Base.Model.IMessage
    Private mAnswer As NHapi.Base.Model.IMessage
    Private mBizHelper As New BizHL7
    Private mSender As New NHapiTools.Base.Net.SimpleMLLPClient(REMOTE_HOST, REMOTE_PORT, System.Text.Encoding.UTF8)
    Private mParser As New NHapi.Base.Parser.PipeParser


    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.mBizHelper.HospitalName = "МБАЛ Щерев АД"
        Me.mBizHelper.LabName = "МДЛ Цибалаб ЕООД"
    End Sub

    Private Sub ToolStripButtonGenerate_Click(sender As Object, e As EventArgs) Handles ToolStripButtonGenerate.Click
        Dim Ord As New BizHL7.Order With {
            .OrderId = "158A",
            .Action = BizHL7.Action.[New],
            .Priority = BizHL7.Priority.Emergency,
            .ToVisit = DateAdd(DateInterval.Day, 1, Now()),
            .Note = "Това е тестов запис."
        }
        Ord.Patient = New BizHL7.Patient With {
            .PIDType = BizHL7.PIDType.Anonymous,
            .GivenName = "Първан",
            .MiddleName = "Младенов",
            .FamilyName = "Чорбаджийски",
            .MedicalCase = "1745/2016",
            .EntityId = "556898",
            .DateOfBirth = New Date(1971, 2, 3),
            .Gender = "M"
        }
        Ord.Doctor = New BizHL7.Doctor With {
            .UIN = "0300999977",
            .Title = "Д-р",
            .GivenName = "Мария",
            .MiddleName = "Димитрова",
            .FamilyName = "Стаменова"
        }
        Ord.Location = New BizHL7.Location With {
            .WardCode = "225631",
            .WardName = "Отделение по вътрешни болести",
            .Room = "2R",
            .Bed = "3"
        }
        Ord.Examinations = New List(Of BizHL7.Examination)
        Ord.Examinations.Add(New BizHL7.Examination With {.LoincCode = "4537-7", .Name = "СУЕ"})
        Ord.Examinations.Add(New BizHL7.Examination With {.LoincCode = "57021-8", .Name = "ПКК"})
        Ord.Examinations.Add(New BizHL7.Examination With {.LoincCode = "14682-9", .Name = "Креатинин"})
        Ord.Examinations.Add(New BizHL7.Examination With {.LoincCode = "14749-6", .Name = "Глюкоза"})
        Ord.Examinations.Add(New BizHL7.Examination With {.LoincCode = "24357-6", .Name = "Урина - общо химично"})

        Me.mMessage = Me.mBizHelper.GenerateOrder(Ord)
        Me.TextBoxMessage.Text = Me.mParser.Encode(Me.mMessage).Replace(vbCr, vbCrLf)
    End Sub

    Private Sub ToolStripButtonSend_Click(sender As Object, e As EventArgs) Handles ToolStripButtonSend.Click
        If Me.mMessage Is Nothing Then
            MessageBox.Show("No message to send.")
            Exit Sub
        End If
        Dim s As String
        Try
            Me.mAnswer = mSender.SendHL7Message(Me.mMessage)
            's = mSender.SendHL7Message(Me.mParser.Encode(Me.mMessage))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Me.mAnswer = Nothing
            Exit Sub
        End Try
        Me.TextBoxAnswer.Text = Me.mParser.Encode(Me.mAnswer).Replace(vbCr, vbCrLf)
        Dim Res As String = ""
        Select Case CType(Me.mAnswer, NHapi.Model.V251.Message.ACK).MSA.AcknowledgmentCode.Value
            Case "AA"
                Res = "Answer: OK."
            Case "AR"
                Res = "Answer: Rejected." & vbCrLf & CType(Me.mAnswer, NHapi.Model.V251.Message.ACK).MSA.TextMessage.Value
            Case "AE"
                Res = "Answer: Error." & vbCrLf & CType(Me.mAnswer, NHapi.Model.V251.Message.ACK).MSA.TextMessage.Value
            Case Else
                Res = "Unknown answer."
        End Select
        MessageBox.Show(Res)
    End Sub
End Class
