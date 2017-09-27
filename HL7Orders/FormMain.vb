Public Class FormMain


    Private mMessage As NHapi.Base.Model.IMessage
    Private mAnswer As NHapi.Base.Model.IMessage
    Private mBizHelper As New BizHL7
    Private mSender As New NHapiTools.Base.Net.SimpleMLLPClient(ConfigData.Instance.RemoteHost, ConfigData.Instance.RemoetPort, System.Text.Encoding.UTF8)
    Private mParser As New NHapi.Base.Parser.PipeParser


    Private Sub ToolStripButtonGenerate_Click(sender As Object, e As EventArgs) Handles ToolStripButtonGenerate.Click
        Dim Ord As Order = DemoGenerator.GetDemoData

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
