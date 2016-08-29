(function (angular) {

    angular
        .module("tasksModule")
        .controller("TasksController", ['$scope', "tasksService", TasksController]);

    function TasksController($scope, tasksService) {
        var vm = this;
        vm.newTaskName = "";
        vm.newTaskDueDate = null;
        vm.newTaskPriority = 0;
        vm.newtaskComment = "";
        vm.tasks = [];
        vm.addNewTask = addNewTask;
        vm.updateTask = updateTask;
        vm.completeTask = completeTask;
        vm.checkOverdue = checkOverdue;

        activate();

        function activate() {
            var tasksPromise = tasksService.getTasks();
            tasksPromise
                .then(function (response) {
                    vm.tasks = response.data;
                    var dotNetDate;
                    for (var i = 0; i < vm.tasks.length; i++) {
                        dotNetDate = vm.tasks[i].DueDate;
                        vm.tasks[i].DueDate = new Date(parseInt(dotNetDate.substr(6)));
                    }
                }, function () {
                    console.log("[TasksController] getTasks - fail", arguments);
                    alert("Getting tasks has failed.");
                });
        }

        function addNewTask() {
            var task = {
                Name: vm.newTaskName,
                DueDate: vm.newTaskDueDate,
                Priority: vm.newTaskPriority,
                Comment: vm.newTaskComment
            };

            tasksService.addTask(task)
                .then(function (response) {
                    task.Id = response.data;
                    vm.tasks.push(task);
                    vm.newTaskName = "";
                    vm.newTaskDueDate = null;
                    vm.newTaskPriority = 0;
                    vm.newtaskComment = "";
                }, function () {
                    console.log("[TasksController] addTask - fail", arguments);
                    alert("Adding a new task has failed.");
                });
        }

        function updateTask(task) {
            tasksService.updateTask(task)
                .error(function () {
                    console.log("[TasksController] updateTask - fail", arguments);
                    alert("Updating task has failed.");
                });
        }

        function completeTask(task) {
            var status = confirm("Are you sure? Make this task completed?");
            if (status == true) {
                tasksService.completeTask(task.Id)
                    .then(function () {
                        var index = vm.tasks.indexOf(task);
                        vm.tasks.splice(index, 1);
                    }, function () {
                        console.log("[TasksController] completeTask - fail", arguments);
                        alert("Compliting task has failed.");
                    });
            }
        }

        function checkOverdue(date) {
            return date < Date.now();
        }
    }

})(angular);