
$(function () {
    $('#datetimepicker1').datetimepicker();
    $('#datetimepicker2').datetimepicker();
});

$('#addExerciseDate').blur(function () {
    $('#addExerciseDate').trigger('change');
})

$('#editExerciseDate').blur(function () {
    $('#editExerciseDate').trigger('change');
})

var appModule = angular.module('indexModule', []);

appModule.controller('DataController', function ($scope, $http) {
    $scope.currentPage = 1;
    $scope.pageTotalSize = 1;
    $scope.exerciseList = [];

    $scope.init = function () {
        $scope.fetchData();
    };

    $scope.doSearch = function () {
        $scope.fetchData();
    };

    $scope.previous = function () {
        if ($scope.currentPage > 1) {
            $scope.currentPage -= 1;
            $scope.fetchData();
        }
    };

    $scope.next = function () {
        if ($scope.currentPage < $scope.totalPageSize) {
            $scope.currentPage += 1;
            $scope.fetchData();
        }
    };

    $scope.fetchData = function () {
        $http({
            method: 'POST', url: '../Home/FetchData',
            data: { page: $scope.currentPage, query: $scope.query }
        }
            ).then(function (response) {
                $scope.currentPage = response.data.CurrentPage;
                $scope.totalPageSize = response.data.TotalPageSize;
                $scope.totalSize = response.data.TotalSize;
                $scope.exerciseList = response.data.ExerciseList;
            }, function () {
                alert('something wrong :( ');
            });
    };

    $scope.addExeriseForm = function () {
        $http({
            method: 'POST', url: '../Home/AddExercise',
            data: { name: $scope.newName, duration: $scope.newDuration, date: $scope.newDate }
        }
            ).then(function (response) {
                if (response.data == 0)
                    $scope.fetchData();
            }, function () {
                alert('something wrong :( ');
            });
    };

    $scope.delete = function ($id) {
        $http({
            method: 'POST', url: '../Home/DeleteExercise',
            data: { id: $id }
        }
            ).then(function (response) {
                if (response.data == 0)
                    $scope.fetchData();
            }, function () {
                alert('something wrong :( ');
            });
    };

    $scope.edit = function ($id) {
        $http({
            method: 'POST', url: '../Home/GetExercise',
            data: { id: $id }
        }).then(function (response) {
            if (response.data.Result == 0)
            {
                $scope.editId = response.data.Exercise.Id;
                $scope.editName = response.data.Exercise.Name;
                $scope.editDate = response.data.Exercise.Date;
                $scope.editDuration = response.data.Exercise.Duration;
                $("#editModal").modal();
            } else {
                alert('can\'t find the exercise object.');
            }
        }, function () {
            alert('something wrong :( ');
        });
    };

    $scope.editExeriseForm = function () {
        $http({
            method: 'POST', url: '../Home/UpdateExercise',
            data: {id:$scope.editId, name: $scope.editName, duration: $scope.editDuration, date: $scope.editDate }
        }
            ).then(function (response) {
                if (response.data == 0)
                    $scope.fetchData();
            }, function () {
                alert('something wrong :( ');
            });
    };

    $scope.init();
});

