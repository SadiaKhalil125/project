using HospitalDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
namespace HospitalManagementSystem
{
    public class Menu
    {
        public void startHospitalManagementSystem()
        {
            DoctorDataAccess doctorDA = new DoctorDataAccess();
            PatientDataAccess patientDA = new PatientDataAccess();
            AppointmentDataAccess appointmentDA = new AppointmentDataAccess();
            HistoryLogger historyLogger = new HistoryLogger();
            InputValidation validation = new InputValidation();
            Console.WriteLine("WELCOME TO THE HOSPITAL MANAGEMENT SYSTEM!\n");
            int choice = -1;
            do
            {
                Console.WriteLine("\nSELECT NUMBER FOR THE RESPECTIVE SERVICE!\n");
                Console.WriteLine("1.Add a new patient");
                Console.WriteLine("2.Update a patient");
                Console.WriteLine("3.Delete a patient(and save deleted record to history)");
                Console.WriteLine("4.Search for patients by name");
                Console.WriteLine("5.View all patients");
                Console.WriteLine("6.Add a new doctor");
                Console.WriteLine("7.Update a doctor");
                Console.WriteLine("8.Delete a doctor(and save deleted record to history)");
                Console.WriteLine("9.Search for doctors by specialization");
                Console.WriteLine("10.View all doctors");
                Console.WriteLine("11.Book an appointment");
                Console.WriteLine("12.View all appointments");
                Console.WriteLine("13.Search appointments by doctor or patient");
                Console.WriteLine("14.Cancel an appointment(and save deleted appointment to history)");
                Console.WriteLine("15.View history of deleted records(patients, doctors, or appointments)");
                Console.WriteLine("16.Update an appointment");
                Console.WriteLine("17.Exit the application\n");
                Console.WriteLine("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine("\n");
                if (choice == 1)
                {
                    Patient patient = new Patient();
                    Console.WriteLine("Enter Patient Name: ");
                    patient.Name = Console.ReadLine();
                    Console.WriteLine("Enter Patient Email: ");
                    patient.Email = Console.ReadLine();
                    Console.WriteLine("Enter Patient Disease: ");
                    patient.Disease = Console.ReadLine();
                    validation.ValidatePatientData(patient);
                    patientDA.InsertPatient(patient);
                }
                else if (choice == 2)
                {
                    Patient patient = new Patient();
                    Console.WriteLine("Enter Patient Id to update: ");
                    patient.PatientId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter New Patient Name: ");
                    patient.Name = Console.ReadLine();
                    Console.WriteLine("Enter Patient Email: ");
                    patient.Email = Console.ReadLine();
                    Console.WriteLine("Enter Patient Disease: ");
                    patient.Disease = Console.ReadLine();
                    validation.ValidatePatientData(patient);
                    patientDA.UpdatePatientInDatabase(patient);
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Enter Patient Id: ");
                    int Id = int.Parse(Console.ReadLine());
                    DeletedPatient patient = new DeletedPatient();
                    patient.Patient = patientDA.SearchPatientById(Id);
                    patient.DeletionTime = DateTime.Now;
                    patientDA.DeletePatientFromDatabase(Id);
                    historyLogger.AddDeletedPatientRecordInHistory(patient);
                }
                else if (choice == 4)
                {
                    Console.WriteLine("Enter Patient Name: ");
                    string Name = Console.ReadLine();
                    List<Patient> patients = patientDA.SearchPatientsInDatabase(Name);
                    foreach (Patient patient in patients)
                    {
                        Console.WriteLine(patient);
                    }
                }
                else if (choice == 5)
                {
                    List<Patient> patients = patientDA.GetAllPatientsFromDataBase();
                    foreach (Patient patient in patients)
                    {
                        Console.WriteLine(patient);
                    }
                }
                else if (choice == 6)
                {
                    Doctor doctor = new Doctor();
                    Console.WriteLine("Enter Doctor Name: ");
                    doctor.Name = Console.ReadLine();
                    Console.WriteLine("Enter Doctor Sepcialization: ");
                    doctor.Specialization = Console.ReadLine();
                    validation.ValidateDoctorData(doctor);
                    doctorDA.InsertDoctor(doctor);
                }
                else if (choice == 7)
                {
                    Doctor doctor = new Doctor();
                    Console.WriteLine("Enter Doctor ID To Update: ");
                    doctor.DoctorId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter New Doctor Name: ");
                    doctor.Name = Console.ReadLine();
                    Console.WriteLine("Enter Doctor Sepcialization: ");
                    doctor.Specialization = Console.ReadLine();
                    validation.ValidateDoctorData(doctor);
                    doctorDA.UpdateDoctorInDatabase(doctor);
                }
                else if (choice == 8)
                {
                    Console.WriteLine("Enter Doctor ID: ");
                    int DoctorId = int.Parse(Console.ReadLine());
                    DeletedDoctor doctor = new DeletedDoctor();
                    doctor.Doctor = doctorDA.searchDoctorById(DoctorId);
                    doctor.DeletionTime = DateTime.Now;
                    doctorDA.DeleteDoctorFromDatabase(DoctorId);
                    historyLogger.AddDeletedDoctorRecordInHistory(doctor);
                }
                else if (choice == 9)
                {
                    Console.WriteLine("Enter Doctor Sepcialization: ");
                    string Specialization = Console.ReadLine();
                    List<Doctor> doctors = doctorDA.SearchDoctorsInDatabase(Specialization);
                    foreach (Doctor doctor in doctors)
                    {
                        Console.WriteLine(doctor);
                    }
                }
                else if (choice == 10)
                {
                    List<Doctor> doctors = doctorDA.GetAllDoctorsFromDataBase();
                    foreach (Doctor doctor in doctors)
                    {
                        Console.WriteLine(doctor);
                    }
                }
                else if (choice == 11)
                {
                    Appointment appointment = new Appointment();
                    Console.WriteLine("Enter Patient Id: ");
                    appointment.PatientId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Doctor Id: ");
                    appointment.DoctorId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Appointment DATE: ");
                    appointment.AppointmentDate = DateTime.Parse(Console.ReadLine());
                    validation.validateAppointmentDate(appointment);
                    appointmentDA.InsertAppointment(appointment);
                }
                else if (choice == 12)
                {
                    List<Appointment> appointments = appointmentDA.GetAllAppointmentsFromDataBase();
                    foreach (Appointment appointment in appointments)
                    {
                        Console.WriteLine(appointment);
                    }
                }
                else if (choice == 13)
                {
                    Console.WriteLine("Enter Patient Id: ");
                    int PatientId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Doctor Id: ");
                    int DoctorId = int.Parse(Console.ReadLine());
                    List<Appointment> appointmentList = appointmentDA.SearchAppointmentsInDatabase(DoctorId, PatientId);
                    foreach (Appointment appointment in appointmentList)
                    {
                        Console.WriteLine(appointment);
                    }
                }
                else if (choice == 14)
                {
                    Console.WriteLine("Enter Appointment Id: ");
                    int AppointmentId = int.Parse(Console.ReadLine());
                    DeletedAppointment deletedAppointment = new DeletedAppointment();
                    deletedAppointment.Appointment = appointmentDA.SearchAppointmentById(AppointmentId);
                    deletedAppointment.DeletionTime = DateTime.Now;
                    appointmentDA.DeleteAppointmentFromDatabase(AppointmentId);
                    historyLogger.AddDeletedApointmentRecordInHistory(deletedAppointment);
                }
                else if (choice == 15)
                {
                    int option = -1;
                    do
                    {
                        Console.WriteLine("Enter 1 for the history of removed patients: ");
                        Console.WriteLine("Enter 2 for the history of removed doctors: ");
                        Console.WriteLine("Enter 3 for the history of removed appointments: ");
                        Console.WriteLine("Enter 0 for exit! ");
                        option = int.Parse(Console.ReadLine());
                        if (option == 1)
                        {
                            historyLogger.ShowDeletedPatients();
                        }
                        else if (option == 2)
                        {
                            historyLogger.ShowDeletedDoctors();
                        }
                        else if (option == 3)
                        {
                            historyLogger.ShowDeletedAppointments();
                        }
                        else
                        {
                            return;
                        }
                    }
                    while (option != 0);
                }
                else if (choice == 16)
                {
                    Appointment appointment = new Appointment();
                    Console.WriteLine("Enter Appointment Id To Update: ");
                    appointment.AppointmentId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter New Patient Id: ");
                    appointment.PatientId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter New Doctor Id: ");
                    appointment.DoctorId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter New Appointment DATE: ");
                    appointment.AppointmentDate = DateTime.Parse(Console.ReadLine());
                    validation.validateAppointmentDateForUpdation(appointment);
                    appointmentDA.UpdateAppointmentInDatabase(appointment);
                }
                else
                {
                    Console.WriteLine("Exiting! GOODBYE!:)");
                    return;
                }

            }
            while (choice != 17);
        }
    }
}
