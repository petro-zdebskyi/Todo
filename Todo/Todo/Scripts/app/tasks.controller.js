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
            tasksPromise.then(function (response) {
                vm.tasks = response.data;
                var dotNetDate;
                for (var i = 0; i < vm.tasks.length; i++) {
                    dotNetDate = vm.tasks[i].DueDate;
                    vm.tasks[i].DueDate = new Date(parseInt(dotNetDate.substr(6)));
                }
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
                    console.log("[TasksController] addTask - success ( " + response.data + " )", arguments);
                    task.Id = response.data;
                    vm.tasks.push(task);
                    vm.newTaskName = "";
                    vm.newTaskDueDate = null;
                    vm.newTaskPriority = 0;
                    vm.newtaskComment = "";
                }, function (error) {
                    console.log("[TasksController] addTask - fail ( " + error.data + " )", arguments);
                    alert("Adding a new task has failed.");
                });
        }

        function updateTask(task) {
            tasksService.updateTask(task)
                .then(function (response) {
                    console.log("[TasksController] updateTask - success ( " + response.data + " )", arguments);
                }, function (error) {
                    console.log("[TasksController] updateTask - fail ( " + error.data + " )", arguments);
                    alert("Updating task has failed.");
                });
        }

        function completeTask(task) {
            var status = confirm("Are you sure? Make this task completed?");
            if (status == true) {
                var index = vm.tasks.indexOf(task);
                vm.tasks.splice(index, 1);
                tasksService.completeTask(task.Id)
                    .then(function (response) {
                        console.log("[TasksController] completeTask - success ( " + response.data + " )", arguments);
                    }, function (error) {
                        console.log("[TasksController] completeTask - fail ( " + error.data + " )", arguments);
                        alert("Compliting task has failed.");
                    });
            }
        }

        function checkOverdue(date) {
            return date < Date.now();
        }
    }

})(angular);