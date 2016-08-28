(function (angular) {

    angular
        .module("tasksModule")
        .factory("tasksService", ['$http', tasksService]);

    function tasksService($http) {

        var service = {
            getTasks: getTasksAjax,
            addTask: addTask,
            completeTask: completeTask,
            updateTask: updateTask
        };

        return service;

        function getTasksAjax() {
            var promise = $http.get("/Task/GetTasks");
            return promise;
        }

        function addTask(task) {
            var promise = $http.post("/Task/AddTask", { name: task.Name, dueDate: task.DueDate.toISOString(), priority: task.Priority, comment: task.Comment });
            return promise;
        }

        function updateTask(task) {
            var promise = $http.post("/Task/UpdateTask", { id: task.Id, name: task.Name, dueDate: task.DueDate.toISOString(), priority: task.Priority, comment: task.Comment });
            return promise;
        }

        function completeTask(id) {
            var promise = $http.post("/Task/CompleteTask", { taskId: id });
            return promise;
        }

    }

})(angular);