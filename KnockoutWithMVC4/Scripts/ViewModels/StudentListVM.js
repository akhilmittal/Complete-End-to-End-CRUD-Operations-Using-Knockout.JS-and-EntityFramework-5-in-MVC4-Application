var urlPath = window.location.pathname;
$(function () {
    ko.applyBindings(StudentListVM);
    StudentListVM.getStudents();
});

//View Model
var StudentListVM = {
    Students: ko.observableArray([]),
    getStudents: function () {
        var self = this;
        $.ajax({
            type: "GET",
            url: '/Student/FetchStudents',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                self.Students(data); //Put the response in ObservableArray
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }
        });
    },
};

self.editStudent = function (student) {
    window.location.href =  '/Student/Edit/' + student.StudentId;
};
self.deleteStudent = function (student) {
    window.location.href = '/Student/Delete/' + student.StudentId;
};

//Model
function Students(data) {
    this.StudentId = ko.observable(data.StudentId);
    this.FirstName = ko.observable(data.FirstName);
    this.LastName = ko.observable(data.LastName);
    this.Age = ko.observable(data.Age);
    this.Gender = ko.observable(data.Gender);
    this.Batch = ko.observable(data.Batch);
    this.Address = ko.observable(data.Address);
    this.Class = ko.observable(data.Class);
    this.School = ko.observable(data.School);
    this.Domicile = ko.observable(data.Domicile);
}