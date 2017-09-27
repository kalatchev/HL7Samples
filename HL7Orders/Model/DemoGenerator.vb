Public Class DemoGenerator

    Public Shared Function GetDemoData() As Order
        Dim Ord As New Order With {
            .OrderId = "158A",
            .Action = Constants.Actions.[New],
            .Priority = Constants.Priorities.Emergency,
            .ToVisit = DateAdd(DateInterval.Day, 1, Now()),
            .Note =
                "Това е тестова забележка, която дори включва специални символи кто |, ^, & и ~." & vbCrLf &
                "И дори Windows-style нов ред, както и Linux-style." & vbCr & " Демонстрира escape техниките в HL7."
        }
        Ord.Diagnosis.Add("D56.0", "Алфа таласемия")
        Ord.Diagnosis.Add("O26.0", "Наднормено наддаване на тегло по време на бременността")
        Ord.Patient = New Patient With {
            .PIDType = Constants.PIDTypes.EGN,
            .PID = "9706166900",
            .GivenName = "Християн",
            .MiddleName = "Петров",
            .FamilyName = "Димитров",
            .MedicalCase = "12032/2017",
            .EntityId = "61922",
            .DateOfBirth = New Date(1997, 6, 16),
            .Gender = "M"
        }
        Ord.OrderingDoctor = New Doctor With {
            .UIN = "0300999977",
            .Title = "Д-р",
            .GivenName = "Мария",
            .MiddleName = "Димитрова",
            .FamilyName = "Стаменова"
        }
        Ord.Location = New Location With {
            .WardCode = "3310",
            .WardName = "Отделение хирургия",
            .Room = "3",
            .Bed = "2"
        }
        Ord.Examinations = New List(Of Examination)
        Ord.Examinations.Add(New Examination With {.LoincCode = "4537-7", .Name = "СУЕ", .HisId = "22"})
        Ord.Examinations.Add(New Examination With {.LoincCode = "57021-8", .Name = "ПКК", .HisId = "55"})
        Ord.Examinations.Add(New Examination With {.LoincCode = "14682-9", .Name = "Креатинин", .HisId = "84"})
        Ord.Examinations.Add(New Examination With {.LoincCode = "14749-6", .Name = "Глюкоза", .HisId = "85"})
        Ord.Examinations.Add(New Examination With {.LoincCode = "24357-6", .Name = "Урина - общо химично", .HisId = "92"})

        Return Ord
    End Function


End Class
