angular.module("umbraco").controller("Approval.ApprovalHistoryController",
	function ($scope, $routeParams, approvalResource, notificationsService, navigationService, userService, mediaResource) {

	    $scope.loaded = false;
	    var parentId = $scope.$parent.menuNode.parentId;
	    userService.getCurrentUser().then(function (currentUser) { $scope.user = currentUser; });

	    if ($routeParams.id == -1) {
	        $scope.node = {};
	        $scope.loaded = true;
	    }
        else{
	        //get a content id -> service
	        approvalResource.getPropertyById(parentId, $routeParams.id, $scope.user.locale).then(function (response) {
	            $scope.node = response.data;
	            $scope.loaded = true;

	            if ($scope.node.PropertyTypeAlias.indexOf('MultipleMediaPicker') !== -1) {
	                mediaResource.getById($scope.node.PreviousValue).then(function (media) {
	                    $scope.node.oldImage = media;
	                });
	                $scope.node.newImageLink = mediaResource.getById($scope.node.CurrentValue).then(function (media) {
	                    $scope.node.currentImage = media;
	                });
	            }
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

	    $scope.compare = function ($event) {
	        pickADiff($event, $scope);
	    };

	    $scope.callUpdate = function () {
	        pickADiff($scope.node.PreviousValue, $scope.node.CurrentValue);
	    }
	});


function pickADiff(prev, curr, event) {
    
    if (event)
    {
        event.preventDefault();
    }

    $('.picadiff').picadiff({
        //leftContent: $('#previousValue').html(),
        //rightContent: $('#currentValue').html(),
        //leftContent: prev,
        //rightContent: curr,
        //lineLength: 1000,
    });;

    $(".picadiff").picadiff();
}
