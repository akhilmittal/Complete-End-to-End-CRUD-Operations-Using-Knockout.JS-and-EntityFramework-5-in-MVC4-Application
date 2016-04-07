var urlPath = window.location.pathname;
$(function () {
    ko.applyBindings(CreateVM);
});

var CreateVM = {
    Domiciles: ko.observableArray(['Delhi', 'Outside Delhi']),
    Genders: ko.observableArray(['Male', 'Female']),
    Students: ko.observableArray([]),
    StudentId: ko.observable(),
    FirstName: ko.observable(),
    LastName: ko.observable(),
    Age: ko.observable(),
    Batch: ko.observable(),
    Address: ko.observable(),
    Class: ko.observable(),
    School: ko.observable(),
    Domicile: ko.observable(),
    Gender: ko.observable(),
    SaveStudent: function () {
        $.ajax({
            url: '/Student/Create',
            type: 'post',
            dataType: 'json',
            data: ko.toJSON(this),
            contentType: 'application/json',
            success: function (result) {
            },
            error: function (err) {
                if (err.responseText == "Creation Failed")
                { window.location.href = '/Student/Index/'; }
                else {
                    alert("Status:"+err.responseText);
                    window.location.href = '/Student/Index/';;
                }
            },
            complete: function () {
                window.location.href = '/Student/Index/';
            }
        });
    }
};
