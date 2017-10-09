
Public Class Hospital

    Private mBasePath As String = Application.StartupPath
    Private mWards As List(Of Ward)
    Private mCases As List(Of MedicalCase)
    Private mPatients As List(Of Patient)
    Private mExaminations As List(Of Examination)
    Private mDoctors As List(Of Doctor)
    Private mContracts As List(Of Contract)
    Private mOrders As List(Of Order)
    Private mDiagnoses As List(Of Diagnosis)

    Private Sub LoadData()
        Try
            Me.mWards = XMLUtils.ReadObject(Of List(Of Ward))(IO.Path.Combine(Me.mBasePath, "DemoData", "wards.xml"))
            Me.mCases = XMLUtils.ReadObject(Of List(Of MedicalCase))(IO.Path.Combine(Me.mBasePath, "DemoData", "cases.xml"))
            Me.mPatients = XMLUtils.ReadObject(Of List(Of Patient))(IO.Path.Combine(Me.mBasePath, "DemoData", "patients.xml"))
            Me.mExaminations = XMLUtils.ReadObject(Of List(Of Examination))(IO.Path.Combine(Me.mBasePath, "DemoData", "examinations.xml"))
            Me.mDoctors = XMLUtils.ReadObject(Of List(Of Doctor))(IO.Path.Combine(Me.mBasePath, "DemoData", "doctors.xml"))
            Me.mContracts = XMLUtils.ReadObject(Of List(Of Contract))(IO.Path.Combine(Me.mBasePath, "DemoData", "contracts.xml"))
            Me.mOrders = XMLUtils.ReadObject(Of List(Of Order))(IO.Path.Combine(Me.mBasePath, "DemoData", "orders.xml"))
            Me.mDiagnoses = XMLUtils.ReadObject(Of List(Of Diagnosis))(IO.Path.Combine(Me.mBasePath, "DemoData", "diagnoses.xml"))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            End 'Critical, exit app
        End Try
        'Connect data
        For Each c In Me.mCases
            c.Patient = Me.mPatients.Where(Function(o) o.EntityId = c.PatientId).FirstOrDefault
            c.Ward = Me.mWards.Where(Function(o) o.Code = c.WardCode).FirstOrDefault
            c.Contract = Me.mContracts.Where(Function(o) o.ContractNum = c.ContractNum).FirstOrDefault
            c.Diagnoses = Me.mDiagnoses.Where(Function(o) c.DiagCodes.Contains(o.Code)).ToList
        Next
        For Each o As Order In Me.mOrders
            o.OrderingDoctor = Me.mDoctors.Where(Function(x) x.UIN = o.OrdDoctorUIN).FirstOrDefault
            o.MedicalCase = Me.mCases.Where(Function(x) x.Id = o.MedicalCaseId).FirstOrDefault
            o.Examinations = Me.mExaminations.Where(Function(x) o.ExamCodes.Contains(x.LoincCode)).ToList
        Next

        Me.ListViewWards.Items.Clear()
        For Each w As Ward In Me.mWards
            Me.ListViewWards.Items.Add(New ListViewItem() With {.Text = w.Name, .Tag = w})
        Next
        Me.ListViewWards.Items(0).Focused = True
        Me.ListViewWards.Items(0).Selected = True
    End Sub


    Private Sub Hospital_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub ListViewWards_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewWards.SelectedIndexChanged
        If Me.ListViewWards.SelectedItems.Count < 1 Then
            Me.ToolStripStatusLabelWard.Text = ""
            Exit Sub
        End If
        Dim w As Ward = DirectCast(Me.ListViewWards.SelectedItems.Item(0).Tag, Ward)
        Me.ToolStripStatusLabelWard.Text = w.Name
        Me.ListViewCases.Items.Clear()
        For Each c As MedicalCase In Me.mCases
            If c.WardCode = w.Code Then
                Dim ni As New ListViewItem(c.CaseNumber)
                ni.SubItems.Add(c.RoomCode)
                ni.SubItems.Add(c.BedCode)
                ni.SubItems.Add($"{c.Patient.GivenName} {c.Patient.MiddleName} {c.Patient.FamilyName}")
                ni.SubItems.Add(c.Patient.Gender)
                If c.Patient.DateOfBirth.HasValue Then
                    ni.SubItems.Add(c.Patient.DateOfBirth.Value.ToString("dd.MM.yyyy"))
                Else
                    ni.SubItems.Add("")
                End If
                ni.SubItems.Add($"{c.Patient.PID}")
                ni.SubItems.Add($"{c.Contract.Name}")
                ni.Tag = c
                Me.ListViewCases.Items.Add(ni)
            End If
        Next
        'If Me.ListViewCases.Items.Count > 0 Then
        '    Me.ListViewCases.Items(0).Focused = True
        '    Me.ListViewCases.Items(0).Selected = True
        'End If
        If Me.ListViewCases.SelectedItems.Count < 1 Then
            Me.ToolStripStatusLabelPatient.Text = ""
            Me.ListViewOrders.Items.Clear()
        End If
    End Sub

    Private Sub ToolStripButtonExit_Click(sender As Object, e As EventArgs) Handles ToolStripButtonExit.Click
        If MessageBox.Show("Are you sure you want to exit?",
                           My.Application.Info.ProductName,
                           MessageBoxButtons.YesNoCancel,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Close()
        End If

    End Sub


    Private Sub ToolStripButtonOrder_Click(sender As Object, e As EventArgs)
        'Dim f As New FormAddExam
        'f.LoadData(Me.mExaminations)
        'f.ShowDialog()
    End Sub

    Private Sub ListViewWards_ItemActivate(sender As Object, e As EventArgs) Handles ListViewWards.ItemActivate

    End Sub

    Private Sub OpenOrders()
        If Me.ListViewCases.SelectedItems.Count < 1 Then Exit Sub
        Dim c As MedicalCase = DirectCast(Me.ListViewCases.SelectedItems.Item(0).Tag, MedicalCase)



    End Sub

    Private Sub Hospital_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        XMLUtils.WriteObject(Me.mOrders, IO.Path.Combine(Me.mBasePath, "DemoData", "orders.xml"))
    End Sub

    Private Sub ListViewCases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewCases.SelectedIndexChanged
        If Me.ListViewCases.SelectedItems.Count < 1 Then
            Me.ToolStripStatusLabelPatient.Text = ""
            Exit Sub
        End If
        Dim c As MedicalCase = DirectCast(Me.ListViewCases.SelectedItems.Item(0).Tag, MedicalCase)
        Me.ToolStripStatusLabelPatient.Text = $"{c.Patient.GivenName} {c.Patient.FamilyName}"
        Me.ListViewOrders.Items.Clear()
        For Each o As Order In Me.mOrders
            If o.MedicalCaseId = c.Id Then
                Dim ni As New ListViewItem(o.OrderId)
                ni.SubItems.Add(o.Created.ToString("dd.MM.yyyy"))
                'ni.SubItems.Add(c.RoomCode)
                'ni.SubItems.Add(c.BedCode)
                ni.SubItems.Add($"{o.OrderingDoctor.Title} {o.OrderingDoctor.GivenName} {o.OrderingDoctor.FamilyName}")
                If o.Priority = Constants.Priorities.Emergency Then
                    ni.SubItems.Add("yes")
                Else
                    ni.SubItems.Add("no")
                End If
                If o.IsSent Then
                    ni.SubItems.Add("yes")
                Else
                    ni.SubItems.Add("no")
                End If
                'ni.SubItems.Add(c.Patient.Gender)
                'If c.Patient.DateOfBirth.HasValue Then
                '    ni.SubItems.Add(c.Patient.DateOfBirth.Value.ToString("dd.MM.yyyy"))
                'Else
                '    ni.SubItems.Add("")
                'End If
                'ni.SubItems.Add($"{c.Patient.PID}")
                'ni.SubItems.Add($"{c.Contract.Name}")
                ni.Tag = o
                Me.ListViewOrders.Items.Add(ni)
            End If
        Next
    End Sub

    Private Sub ToolStripButtonView_Click(sender As Object, e As EventArgs) Handles ToolStripButtonView.Click
        If Me.ListViewOrders.SelectedItems.Count <> 1 Then
            MessageBox.Show("Select order to view.")
            Exit Sub
        End If
        If Me.ListViewOrders.SelectedItems.Count < 1 Then Exit Sub
        Dim o As Order = DirectCast(Me.ListViewOrders.SelectedItems.Item(0).Tag, Order)
        Dim cont As String = HL7Utils.GetWinStringMsg(HL7Utils.Order2Message(ConfigData.Instance, o))
        Dim fn As String = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "hl7orders.txt")
        My.Computer.FileSystem.WriteAllText(fn, cont, False)
        Process.Start(fn)
    End Sub

    Private Function GetSelectedOrder() As Order
        If Me.ListViewOrders.SelectedItems.Count <> 1 Then
            MessageBox.Show("Please, select on order.")
            Return Nothing
        End If
        Return DirectCast(Me.ListViewOrders.SelectedItems.Item(0).Tag, Order)
    End Function

    Private Sub DeleteSelectedOrder()
        If Me.ListViewOrders.SelectedItems.Count <> 1 Then
            MessageBox.Show("Please, select on order.")
            Exit Sub
        End If
        Dim o As Order = DirectCast(Me.ListViewOrders.SelectedItems.Item(0).Tag, Order)
        Me.mOrders.Remove(o)
        Me.ListViewOrders.Items.Remove(Me.ListViewOrders.SelectedItems.Item(0))
    End Sub

    Private Function SendOrder(o As Order) As String
        Dim a As NHapi.Base.Model.IMessage
        Dim s As New NHapiTools.Base.Net.SimpleMLLPClient(ConfigData.Instance.RemoteHost, ConfigData.Instance.RemoetPort, System.Text.Encoding.UTF8)
        Try
            a = s.SendHL7Message(HL7Utils.Order2Message(ConfigData.Instance, o))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
        Dim Res As String = ""
        Select Case CType(a, NHapi.Model.V251.Message.ACK).MSA.AcknowledgmentCode.Value
            Case "AA"
                Res = "OK"
            Case "AR"
                Res = "Rejected: " & CType(a, NHapi.Model.V251.Message.ACK).MSA.TextMessage.Value
            Case "AE"
                Res = "Error: " & CType(a, NHapi.Model.V251.Message.ACK).MSA.TextMessage.Value
            Case Else
                Res = "Unknown answer"
        End Select
        MessageBox.Show("Server result:" & vbCrLf & Res, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return CType(a, NHapi.Model.V251.Message.ACK).MSA.AcknowledgmentCode.Value
    End Function

    Private Sub ToolStripButtonSend_Click(sender As Object, e As EventArgs) Handles ToolStripButtonSend.Click
        Dim o As Order = GetSelectedOrder()
        If o IsNot Nothing Then
            o.Action = Constants.Actions.[New]
            SendOrder(o)
        End If
    End Sub

    Private Sub ToolStripButtonDelete_Click(sender As Object, e As EventArgs) Handles ToolStripButtonDelete.Click
        Dim o As Order = GetSelectedOrder()
        If o Is Nothing Then Exit Sub
        o.Action = Constants.Actions.Cancel
        If Me.SendOrder(o) = "AA" Then
            'Me.DeleteSelectedOrder()
        End If
    End Sub

    Private Sub ToolStripButtonEdit_Click(sender As Object, e As EventArgs) Handles ToolStripButtonEdit.Click
        Dim o As Order = GetSelectedOrder()
        If o Is Nothing Then Exit Sub
        o.Action = Constants.Actions.Update
        Me.SendOrder(o)
    End Sub
End Class