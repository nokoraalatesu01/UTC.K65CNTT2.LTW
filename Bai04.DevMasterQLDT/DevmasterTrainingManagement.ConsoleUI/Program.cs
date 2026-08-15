using DevmasterTrainingManagement.Application.Interfaces;
using DevmasterTrainingManagement.Application.Reports;
using DevmasterTrainingManagement.Application.Services;
using DevmasterTrainingManagement.ConsoleUI.Menus;
using DevmasterTrainingManagement.Domain.Entities;
using DevmasterTrainingManagement.Infrastructure.Csv;
using DevmasterTrainingManagement.Infrastructure.Logging;
using DevmasterTrainingManagement.Infrastructure.Persistence;
using DevmasterTrainingManagement.Infrastructure.Repositories;

// Setup paths
var dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
var logsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

// Ensure directories exist
Directory.CreateDirectory(dataDirectory);
Directory.CreateDirectory(logsDirectory);

// Create logger
var logger = new FileLogger(logsDirectory);
logger.LogInfo("Application startup");

// Setup repositories (JSON-based)
var courseRepository = new JsonRepository<Course>(dataDirectory);
courseRepository.LoadAsync().Wait();

var classRepository = new JsonRepository<Class>(dataDirectory);
classRepository.LoadAsync().Wait();

var studentRepository = new JsonRepository<Student>(dataDirectory);
studentRepository.LoadAsync().Wait();

var enrollmentRepository = new JsonRepository<Enrollment>(dataDirectory);
enrollmentRepository.LoadAsync().Wait();

var careRepository = new JsonRepository<CareRecord>(dataDirectory);
careRepository.LoadAsync().Wait();

// Create services
var courseService = new CourseService(courseRepository);
var studentService = new StudentService(studentRepository);
var enrollmentService = new EnrollmentService(enrollmentRepository, studentRepository, classRepository);
var classService = new ClassService(classRepository, enrollmentService);
var careService = new CareService(careRepository);

// Create report service
var reportService = new ReportService(courseService, classService, studentService, enrollmentService, careService);

// Create CSV service
var csvService = new StudentCsvService(logger);

// Create backup service
var backupService = new BackupService(dataDirectory, logger);

// Create main menu
var mainMenu = new MainMenu(
    courseService,
    classService,
    studentService,
    enrollmentService,
    careService,
    reportService,
    backupService,
    csvService,
    logger);

// Run application
mainMenu.Run();
