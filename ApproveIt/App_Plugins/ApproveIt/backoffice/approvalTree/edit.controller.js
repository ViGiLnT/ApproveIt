angular.module("umbraco").controller("Approval.ApprovalEditController",
	function ($scope, $routeParams, approvalResource, notificationsService, navigationService) {

	    $scope.loaded = false;

	    if ($routeParams.id == -1) {
	        $scope.node = {};
	        $scope.loaded = true;
	    }
        else{
	        //get a content id -> service
	        approvalResource.getById($routeParams.id).then(function (response) {
	            $scope.node = response.data;
	            $scope.loaded = true;
	        });
	    }   

	    $scope.publish = function (node) {
	        approvalResource.publish(node.Id).then(function (response) {
	            $scope.node = response.data;
	            $scope.contentForm.$dirty = false;
	            navigationService.syncTree({ tree: 'approvalTree', path: [-1, -1], forceReload: true });
	            notificationsService.success("Success", node.Name + " has been published");
	        });
	    };
	   
	});