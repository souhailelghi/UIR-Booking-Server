using Application.IServices;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ReservationFeature.Commands.AddReservation
{
    public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, string>
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfService _unitOfService;


        public AddReservationCommandHandler(IReservationService reservationService, IMapper mapper, IEmailSender emailSender , IUnitOfService unitOfService)
        {
            _reservationService = reservationService;
            _mapper = mapper;
            _emailSender = emailSender;
            _unitOfService = unitOfService;
        }

        public async Task<string> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            // Check if the user or team can book
            string canBookMessage = await _reservationService.CanTeamOrUserBookAsync(request.CodeUIR, request.CodeUIRList, request.SportId);

            if (!canBookMessage.Contains("No conflicting reservations found"))
            {
                return canBookMessage; // Return the error message directly
            }

            // Attempt to create the reservation
            string bookingMessage = await _reservationService.BookAsync(
                request.CodeUIR,
                request.SportCategoryId,
                request.ReservationDate,
                request.DayBooking,
                request.HourStart,
                request.HourEnd,
                request.CodeUIRList,
                request.SportId
            );

            Sport result = await _unitOfService.SportService.GetSportByIdAsync(request.SportId);
            var nameSport = result.Name;


            //    if (bookingMessage == "Reservation successfully created.")
            //    {
            //        // Fetch names of all students in CodeUIRList
            //        string studentNamesFormatted = "No students found";
            //        var students = await _unitOfService.StudentService.GetStudentByCodeUIRAsync(request.CodeUIR);
            //        string codeUIRListFormatted = request.CodeUIRList != null ? string.Join(", ", request.CodeUIRList) : "No codes provided";

            //        if (request.CodeUIRList != null && request.CodeUIRList.Count > 0)
            //        {
            //            var studentNames = new List<string>();

            //            foreach (var codeUIR in request.CodeUIRList)
            //            {
            //                var student = await _unitOfService.StudentService.GetStudentByCodeUIRAsync(codeUIR);
            //                if (student != null)
            //                {
            //                    studentNames.Add($"{student.FirstName} {student.LastName}");
            //                }
            //            }

            //            studentNamesFormatted = string.Join(", ", studentNames);
            //        }

            //        // Prepare the email notification
            //        string emailSubject = "New Reservation Created";
            //        string emailBody = $@"
            //A new reservation has been successfully created for:
            //- CodeUIR: {request.CodeUIR} - name studnet : - name student: {students.FirstName} {students.LastName}
            //- List of codes: {codeUIRListFormatted} name student: {studentNamesFormatted}
            //- Hour start: {request.HourStart}
            //- Hour end: {request.HourEnd}
            //- Sport reserved: {nameSport}";

            //        await _emailSender.SendEmailAsync("contact@souhail.me", emailSubject, emailBody);
            //    }


            if (bookingMessage == "Reservation successfully created.")
            {
                // Prepare a mapping of CodeUIR to student names
                var codeUIRWithStudents = new List<string>();
                var students = await _unitOfService.StudentService.GetStudentByCodeUIRAsync(request.CodeUIR);

                if (request.CodeUIRList != null && request.CodeUIRList.Count > 0)
                {
                    foreach (var codeUIR in request.CodeUIRList)
                    {
                        var student = await _unitOfService.StudentService.GetStudentByCodeUIRAsync(codeUIR);
                        if (student != null)
                        {
                            codeUIRWithStudents.Add($"{codeUIR}: {student.FirstName} {student.LastName}");
                        }
                        else
                        {
                            codeUIRWithStudents.Add($"{codeUIR}: Student not found");
                        }
                    }
                }
                else
                {
                    codeUIRWithStudents.Add("No codes provided");
                }

                // Format the CodeUIR-Student pairs as a string
                string codeUIRListFormatted = string.Join("\n", codeUIRWithStudents);

                // Prepare the email notification
                string emailSubject = "New Reservation Created";
                string emailBody = $@"
        A new reservation has been successfully created for:
            - Students and codes:
        {request.CodeUIR}: {students.FirstName} {students.LastName}
          {codeUIRListFormatted}
        - Hour start: {request.HourStart}
        - Hour end: {request.HourEnd}
        - Sport reserved: {nameSport}";

                await _emailSender.SendEmailAsync("contact@souhail.me", emailSubject, emailBody);
            }


            return bookingMessage; // Return the booking message
        }
    }
}
