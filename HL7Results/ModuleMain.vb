Imports NHapi.Model.V251
Module ModuleMain

    Sub Main()

        Dim BaseFolder As String = IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "SampleData")
        Dim Parser As New NHapi.Base.Parser.PipeParser

        For Each CurFile As String In My.Computer.FileSystem.GetFiles(BaseFolder)
            Dim Data As String = My.Computer.FileSystem.ReadAllText(CurFile)
            Dim Msg As NHapi.Base.Model.IMessage = Parser.Parse(Data)

            Console.WriteLine($"Reading file: {CurFile}")

            If Not TypeOf Msg Is Message.ORU_R01 Then
                Console.WriteLine("Unknown type of message.")
                Continue For
            End If

            Dim ResMsg As NHapi.Model.V251.Message.ORU_R01 = CType(Msg, Message.ORU_R01)

            Console.WriteLine($"Sending application: {ResMsg.MSH.SendingApplication.NamespaceID}")
            Console.WriteLine($"Sending facility: {ResMsg.MSH.SendingFacility.NamespaceID }")
            Console.WriteLine($"Receiving application: {ResMsg.MSH.ReceivingApplication.NamespaceID}")
            Console.WriteLine($"Receiving facility: {ResMsg.MSH.ReceivingFacility.NamespaceID}")

            'ID-та на пациент
            For Each PIDRepetition As Datatype.CX In ResMsg.PATIENT_RESULTs(0).PATIENT.PID.GetPatientIdentifierList
                If PIDRepetition.AssigningAuthority.NamespaceID.Value = "GRAO" AndAlso PIDRepetition.IdentifierTypeCode.Value = "NI" Then
                    Console.WriteLine($"Patient EGN: {PIDRepetition.IDNumber.Value}")
                ElseIf PIDRepetition.AssigningAuthority.NamespaceID.Value = "MVR" AndAlso PIDRepetition.IdentifierTypeCode.Value = "NI" Then
                    Console.WriteLine($"Patient LNCh: {PIDRepetition.IDNumber.Value}")
                ElseIf PIDRepetition.AssigningAuthority.NamespaceID.Value = "Hospital" AndAlso PIDRepetition.IdentifierTypeCode.Value = "MR" Then
                    Console.WriteLine($"Patient's Medical Case (IZ): {PIDRepetition.IDNumber.Value}")
                ElseIf PIDRepetition.AssigningAuthority.NamespaceID.Value = "Ambulatory" AndAlso PIDRepetition.IdentifierTypeCode.Value = "MR" Then
                    Console.WriteLine($"Patient's Ambulatory number: {PIDRepetition.IDNumber.Value}")
                ElseIf PIDRepetition.AssigningAuthority.NamespaceID.Value = "HIS" AndAlso PIDRepetition.IdentifierTypeCode.Value = "MR" Then
                    Console.WriteLine($"Patient's idetifier in HIS: {PIDRepetition.IDNumber.Value}")
                End If
            Next

            'Имена
            Console.WriteLine($"Patient names: {ResMsg.PATIENT_RESULTs(0).PATIENT.PID.GetPatientName(0).GivenName.Value} {ResMsg.PATIENT_RESULTs(0).PATIENT.PID.GetPatientName(0).SecondAndFurtherGivenNamesOrInitialsThereof.Value} {ResMsg.PATIENT_RESULTs(0).PATIENT.PID.GetPatientName(0).FamilyName.OwnSurname.Value}")

            'Важно: Visit ID е водещ идентификатор в ЛИС iLab!
            'Ако даден резултат бъде изпратен повече от веднжъж, той ще има пак същия Visit ID и трябва да "захлупи" предходния!
            Console.WriteLine($"iLab Visit ID: {ResMsg.PATIENT_RESULTs(0).PATIENT.VISIT.PV1.VisitNumber.IDNumber.Value}")

            'Изследвания
            For i As Integer = 0 To ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONRepetitionsUsed - 1
                Console.WriteLine($"{vbTab}Observation number {i + 1} -------------------")
                Console.WriteLine($"{vbTab}Order ID (previously sent bu HIS): {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).ORC.PlacerOrderNumber.EntityIdentifier.Value}")
                'Важно: MC = Completed, A = Preliminary
                Console.WriteLine($"{vbTab}Order status: {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).ORC.OrderStatus.Value}")
                'Type of answer
                If ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).ORC.OrderControl.Value = "NW" Then
                    'Резултат без електронна поръчка
                    'NW=New order/service (кагото е без поръчка)  [HL7 Table 0119: Table-Definition]
                    Console.WriteLine($"{vbTab}Result is wihtout order.")
                ElseIf ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).ORC.OrderControl.Value = "RE" Then
                    'Резултата е по повод поръчка
                    'RE=Observations/Performed Service to follow (когато е от поръчка)  [HL7 Table 0119: Table-Definition]
                    Console.WriteLine($"{vbTab}Result is an answer of a order.")
                Else
                    Console.WriteLine($"{vbTab}It's unclear if result has an order.")
                End If
                'Код на звено/отделение в БИС
                Console.WriteLine($"{vbTab}Ward code: {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).ORC.GetOrderingFacilityName(0).IDNumber.Value}")
                'Име на звено/отделение
                Console.WriteLine($"{vbTab}Ward name: {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).ORC.GetOrderingFacilityName(0).OrganizationName.Value}")

                Console.WriteLine($"{vbTab}Examination code: {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).OBR.UniversalServiceIdentifier.Identifier.Value}")
                Console.WriteLine($"{vbTab}Examination name: {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).OBR.UniversalServiceIdentifier.Text.Value}")

                'Параметри на изследването
                For j = 0 To ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).OBSERVATIONRepetitionsUsed - 1
                    Console.WriteLine($"{vbTab}{vbTab}Parameter number {j + 1} -------------------")
                    Console.WriteLine($"{vbTab}{vbTab}Parameter code: {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).OBSERVATIONs(j).OBX.ObservationIdentifier.Identifier.Value}")
                    Console.WriteLine($"{vbTab}{vbTab}Parameter name: {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).OBSERVATIONs(j).OBX.ObservationIdentifier.Text.Value}")
                    'NM=Numeric, TX=Text
                    Console.WriteLine($"{vbTab}{vbTab}Parameter result type: {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).OBSERVATIONs(j).OBX.ValueType.Value}")
                    Console.WriteLine($"{vbTab}{vbTab}Parameter result: {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).OBSERVATIONs(j).OBX.GetObservationValue(0).Data}")
                    Console.WriteLine($"{vbTab}{vbTab}Parameter units: {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).OBSERVATIONs(j).OBX.Units.Identifier.Value}")
                    Console.WriteLine($"{vbTab}{vbTab}Parameter range: {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).OBSERVATIONs(j).OBX.ReferencesRange.Value}")
                    'i=Pending, F=Final, C=Corrected (изпратен след F)
                    Console.WriteLine($"{vbTab}{vbTab}Parameter status: {ResMsg.PATIENT_RESULTs(0).ORDER_OBSERVATIONs(i).OBSERVATIONs(j).OBX.ObservationResultStatus.Value}")
                Next
                Console.WriteLine()
                Console.WriteLine()
            Next
        Next

        Console.Write("Press any key to continue...")
        Console.ReadKey()


    End Sub

End Module
