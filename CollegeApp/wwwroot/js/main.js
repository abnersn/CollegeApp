var app = angular.module("collegeApp", ["ngRoute", "ngResource"]);

app.config(function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix('');
    $locationProvider.html5Mode(true);
    $routeProvider
        .when("/", {
            templateUrl: "pages/home.html",
        })
        .when("/courses", {
            templateUrl: "pages/courses.html",
        })
        .when("/students", {
            templateUrl: "pages/students.html",
        })
        .when("/subjects", {
            templateUrl: "pages/subjects.html",
        })
        .when("/teachers", {
            templateUrl: "pages/teachers.html",
        })
        .otherwise({ redirectTo: '/' });
});

app.controller("NavbarController", function ($scope, $location) {
    $scope.isActive = function (route) {
        return route === $location.path();
    }
});

app.factory('Student', function ($resource) {
    return $resource('/api/Students/:id', null, {
        update: {
            method: 'PUT'
        }
    });
});

app.factory('Teacher', function ($resource) {
    return $resource('/api/Teachers/:id', null, {
        update: {
            method: 'PUT'
        }
    });
});

app.factory('Enrollment', function ($resource) {
    return $resource('/api/Enrollments/:id', null, {
        update: {
            method: 'PUT'
        }
    });
});

app.factory('Course', function ($resource) {
    return $resource('/api/Courses/:id', null, {
        update: {
            method: 'PUT'
        }
    });
});

app.factory('Subject', function ($resource) {
    return $resource('/api/Subjects/:id', null, {
        update: {
            method: 'PUT'
        }
    });
});

app.controller("StudentController", function ($scope, $http, Student, Enrollment) {
    $http.get("/api/Courses").then(function (response) {
        $scope.courses = response.data;
        refresh();
    });

    $http.get("/api/Subjects").then(function (response) {
        $scope.subjects = response.data;
        refresh();
    });
    $scope.getSubject = id => $scope.subjects.find(s => s.subjectID == id);

    $scope.detailsStudent = null;

    function refresh() {
        if ($scope.students)
        $scope.students = $scope.students.map(function (student) {
            if ($scope.courses)
                student.course = $scope.courses.find(c => c.courseID == student.courseID);
            if ($scope.enrollments)
                student.enrollments = $scope.enrollments.filter(e => e.studentID == student.studentID);
            return student;
        });
    }

    var students = Student.query();
    students.$promise.then(function () {
        $scope.students = students;
        refresh();
    });

    var enrollments = Enrollment.query();
    enrollments.$promise.then(function () {
        $scope.enrollments = enrollments;
        refresh();
    });

    $scope.details = function (id) {
        $scope.detailsStudent = $scope.students.find(s => s.studentID === id);
        $scope.detailsStudent.birthday = new Date($scope.detailsStudent.birthday);
        $scope.detailsStudent.courseID = $scope.detailsStudent.courseID.toString();
    }

    $scope.register = function (data) {
        var newStudent = new Student(data);
        newStudent.$save(function (student) {
            $scope.students.push(student);
            refresh();
        });
    }

    $scope.registerEnrollment = function (data) {
        var newEnrollment = new Enrollment(data);
        newEnrollment.SubjectID = $scope.detailsSubject.subjectID;
        newEnrollment.$save(function (enrollment) {
            $scope.enrollments.push(enrollment);
            refresh();
        });
    }

    $scope.update = function (data) {
        var updatedStudent = new Student(data);
        Student.update({ id: data.studentID }, updatedStudent);
    }

    $scope.delete = function (id) {
        console.log("delete", id);
        Student.delete({ id: id }, function () {
            $scope.students = $scope.students.filter(s => s.studentID !== id);
            refresh();
        });
    }

    $scope.deleteEnrollment = function (id) {
        Enrollment.delete({ id: id }, function () {
            $scope.enrollments = $scope.enrollments.filter(s => s.enrollmentID !== id);
            refresh();
        });
    }
});

app.controller("SubjectController", function ($scope, $http, Subject, Enrollment) {
    $http.get("/api/Teachers").then(function (response) {
        $scope.teachers = response.data;
        refresh();
    });

    $http.get("/api/Students").then(function (response) {
        $scope.students = response.data;
        refresh();
    });
    $scope.getStudent = id => $scope.students.find(s => s.studentID == id);

    function refresh() {
        if ($scope.subjects)
            $scope.subjects = $scope.subjects.map(function (subject) {
                if ($scope.teachers)
                    subject.teacher = $scope.teachers.find(c => c.teacherID == subject.teacherID);
                if ($scope.enrollments) {
                    subject.enrollments = $scope.enrollments.filter(e => e.subjectID == subject.subjectID);
                    if (subject.enrollments.length > 0) {
                        subject.average = subject.enrollments.reduce((acc, it) => it.grade ? acc + Number(it.grade) : acc, 0)/subject.enrollments.length;
                    }
                }
                return subject;
            });
    }

    var subjects = Subject.query();
    subjects.$promise.then(function () {
        $scope.subjects = subjects;
        refresh();
    });
    var enrollments = Enrollment.query();
    enrollments.$promise.then(function () {
        $scope.enrollments = enrollments;
        refresh();
    });
    
    $scope.detailsSubject = null;
    $scope.details = function (id) {
        $scope.detailsSubject = $scope.subjects.find(s => s.subjectID === id);
        $scope.detailsSubject.teacherID = $scope.detailsSubject.teacherID.toString();
    }

    $scope.register = function (data) {
        var newSubject = new Subject(data);
        newSubject.$save(function (subject) {
            $scope.subjects.push(subject);
            refresh();
        });
    }

    $scope.registerEnrollment = function (data) {
        var newEnrollment = new Enrollment(data);
        newEnrollment.SubjectID = $scope.detailsSubject.subjectID;
        newEnrollment.$save(function (enrollment) {
            $scope.enrollments.push(enrollment);
            refresh();
        });
    }

    $scope.update = function (data) {
        var updatedSubject = new Subject(data);
        Subject.update({ id: data.subjectID }, updatedSubject, refresh);
    }

    $scope.delete = function (id) {
        Subject.delete({ id: id }, function () {
            $scope.subjects = $scope.subjects.filter(s => s.subjectID !== id);
            refresh();
        });
    }

    $scope.deleteEnrollment = function (id) {
        Enrollment.delete({ id: id }, function () {
            $scope.enrollments = $scope.enrollments.filter(s => s.enrollmentID !== id);
            refresh();
        });
    }
});

app.controller("CoursesController", function ($scope, $http, Course) {
    $http.get("/api/Subjects").then(function (response) {
        $scope.subjects = response.data;
        refresh();
    });

    $http.get("/api/Enrollments").then(function (response) {
        $scope.enrollments = response.data;
        refresh();
    });

    function refresh() {
        if ($scope.courses)
            $scope.courses = $scope.courses.map(function (course) {
                if ($scope.subjects) {
                    course.subjects = $scope.subjects.map(s => {
                        var subject = $scope.subjects.find(a => a.subjectID == s.subjectID);
                        if ($scope.enrollments) {
                            subject.enrollments = $scope.enrollments.filter(e => e.subjectID == s.subjectID);
                        }
                        return subject;
                    });
                }
                return course;
            });
    }

    var courses = Course.query();
    courses.$promise.then(function () {
        $scope.courses = courses;
        refresh();
    });

    $scope.detailsCourse = null;
    $scope.details = function (course) {
        $scope.detailsCourse = course;
    }

    $scope.register = function (data) {
        var newCourse = new Course(data);
        newCourse.$save(function (course) {
            $scope.courses.push(course);
            refresh();
        });
    }
    

    $scope.update = function (data) {
        var updatedCourse = new Course(data);
        Course.update({ id: data.courseID }, updatedCourse, refresh);
    }

    $scope.delete = function (id) {
        console.log(id);
        Course.delete({ id: id }, function () {
            $scope.courses = $scope.courses.filter(s => s.courseID !== id);
            refresh();
        });
    }
});

app.controller("TeacherController", function ($scope, $http, Teacher) {
    $http.get("/api/Subjects").then(function (response) {
        $scope.subjects = response.data;
        refresh();
    });
    $scope.getSubject = id => $scope.subjects.find(s => s.subjectID == id);

    $scope.detailsTeacher = null;

    function refresh() {
        if ($scope.teachers)
            $scope.teachers = $scope.teachers.map(function (teacher) {
                if ($scope.subjects)
                    teacher.subjects = $scope.subjects.filter(e => e.teacherID == teacher.teacherID);
                return teacher;
            });
    }

    var teachers = Teacher.query();
    teachers.$promise.then(function () {
        $scope.teachers = teachers;
        refresh();
    });

    $scope.details = function (teacher) {
        $scope.detailsTeacher = teacher;
        $scope.detailsTeacher.birthday = new Date(teacher.birthday);
    }

    $scope.register = function (data) {
        var newTeacher = new Teacher(data);
        newTeacher.$save(function (teacher) {
            $scope.teachers.push(teacher);
            refresh();
        });
    }

    $scope.update = function (data) {
        var updatedTeacher = new Teacher(data);
        Teacher.update({ id: data.teacherID }, updatedTeacher);
    }

    $scope.delete = function (id) {
        Teacher.delete({ id: id }, function () {
            $scope.teachers = $scope.teachers.filter(s => s.teacherID !== id);
            refresh();
        });
    }
});

app.controller("HomeController", function ($scope, $http) {
    $http.get("/api/Courses").then(function (response) {
        $scope.courses = response.data;
    });
    $http.get("/api/Teachers").then(function (response) {
        $scope.teachers = response.data;
    });
    $http.get("/api/Students").then(function (response) {
        $scope.students = response.data;
    });
});